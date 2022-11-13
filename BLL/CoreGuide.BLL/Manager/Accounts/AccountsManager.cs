using CoreGuide.BLL.Business.Utilities;
using CoreGuide.BLL.Business.Utilities.FileUploaderService;
using CoreGuide.BLL.Business.Utilities.GuideFillerService;
using CoreGuide.BLL.Business.Utilities.MapperService;
using CoreGuide.BLL.Business.Utilities.TokenService;
using CoreGuide.BLL.Business.Utilities.ValidationService;
using CoreGuide.BLL.Models.Accounts.RegisterUser.Input;
using CoreGuide.BLL.Models.Accounts.SignIn.Input;
using CoreGuide.BLL.Models.Accounts.SignIn.Output;
using CoreGuide.BLL.Models.Enums;
using CoreGuide.Common.Entities.Output;
using CoreGuide.Common.Utilities;
using CoreGuide.DAL.Repository.Respositories.Contracts;
using CoreGuide.DAL.Repository.UnitOfWork.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Manager.Accounts
{
    internal class AccountsManager : IAccountsManager
    {
        #region Props
        private readonly IValidationService _validationService;
        private readonly IGuideFillerService _guideFillerService;
        private readonly ITokenService _tokenService;
        private readonly IMapperService _mapperService;
        private readonly IFileUploaderService _fileUploaderService;
        private readonly IEmployeeRepository _employeeRepository;
        #endregion

        #region CTOR
        public AccountsManager(
            IValidationService validationService,
            IGuideFillerService guideFillerService,
            ITokenService tokenService,
            IMapperService mapperService,
            IFileUploaderService fileUploaderService,
            IEmployeeRepository employeeRepository
            )
        {
            _validationService = validationService;
            _guideFillerService = guideFillerService;
            _tokenService = tokenService;
            _mapperService = mapperService;
            _fileUploaderService = fileUploaderService;
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Methods

        #region Register User
        public async Task<BaseOutput> RegisterEmployee(RegisterUserInput input)
        {
            var validationResult = await _validationService.ValidateInputIfNullOrInvalidAsync(input);
            if (!validationResult.IsValid)
                return _guideFillerService.FillOutput<BaseOutput>(validationResult);

            var employee = _mapperService.Map(input);
            var uploadResult = await _fileUploaderService.SaveFile(input.ProfilePicture, BusinessStrings.DirectoryNames.ProfilePictures);
            if (!uploadResult.IsSuccess)
                return _guideFillerService.FillOutput<BaseOutput>(uploadResult);

            employee.ProfilePictureURL = uploadResult.FilePath;
            var userRole = input.IsAdmin ? Strings.Roles.Admin : Strings.Roles.User;
            var identityResult = await _employeeRepository.CreateUserWithRoleAsync(employee, userRole, input.Password);
            if (!identityResult.Succeeded)
                return _guideFillerService.FillOutput<BaseOutput>(GuideErrorCodes.ServerError, BusinessStrings.Resources.ErrorMessagesKeys.RegistrationFailed);

            return _guideFillerService.FillSuccessOutput<BaseOutput>(BusinessStrings.Resources.ErrorMessagesKeys.RegistrationSuccess);
        }
        #endregion

        #region Sign in
        public async Task<SignInOutput> SignIn(SignInInput input, CancellationToken cancellationToken)
        {
            var validationResult = await _validationService.ValidateInputIfNullOrInvalidAsync(input);
            if (!validationResult.IsValid)
                return _guideFillerService.FillOutput<SignInOutput>(validationResult);

            var employee = await _employeeRepository.GetUserByUserNameWithRoleAsync(input.UserName, cancellationToken);

            var isLockedOut = await _employeeRepository.IsUserLockedoutAsync(employee);
            if (isLockedOut)
                return _guideFillerService.FillOutput<SignInOutput>(GuideErrorCodes.UserLockedOut, BusinessStrings.Resources.ErrorMessagesKeys.UserLockedOut);

            var isValidPassword = await _employeeRepository.CheckPasswordAsync(employee, input.Password);
            if (!isValidPassword)
            {
                await _employeeRepository.AddFailedAccessAsync(employee);
                return _guideFillerService.FillOutput<SignInOutput>(GuideErrorCodes.WrongPassword, BusinessStrings.Resources.ErrorMessagesKeys.PasswordWrong);
            }

            await _employeeRepository.ResetFailedAccessAsync(employee);

            var accessToken = _tokenService.GenerateAccessToken(employee);
            var result = await _tokenService.GenerateRefreshTokenAsync(null, cancellationToken, employee.Id);
            if (result.refreshToken == null)
                return _guideFillerService.FillOutput<SignInOutput>(GuideErrorCodes.InvalidToken, BusinessStrings.Resources.ErrorMessagesKeys.RefreshTokenFailed);

            var output = _guideFillerService.FillSuccessOutput<SignInOutput>(BusinessStrings.Resources.ErrorMessagesKeys.SignInSuccess);
            output = output with { AccessToken = accessToken , RefreshToken = result.refreshToken.Token};
            return output;
        }
        #endregion

        #region Refresh token
        public async Task<SignInOutput> RefreshToken(string refreshToken, CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(refreshToken))
                return _guideFillerService.FillOutput<SignInOutput>(GuideErrorCodes.NullReference, BusinessStrings.Resources.ErrorMessagesKeys.RefreshTokenNotFound);

            var result = await _tokenService.GenerateRefreshTokenAsync(refreshToken, cancellationToken);
            if (result.refreshToken == null)
                return _guideFillerService.FillOutput<SignInOutput>(GuideErrorCodes.InvalidToken, BusinessStrings.Resources.ErrorMessagesKeys.RefreshTokenNotFound);

            switch (result.validationOutput)
            {
                case TokenValidationOutput.Expired:
                    return _guideFillerService.FillOutput<SignInOutput>(GuideErrorCodes.ExpiredToken, BusinessStrings.Resources.ErrorMessagesKeys.RefreshTokenExpired);

                case TokenValidationOutput.Invalid:
                    return _guideFillerService.FillOutput<SignInOutput>(GuideErrorCodes.InvalidToken, BusinessStrings.Resources.ErrorMessagesKeys.RefreshTokenInvalid);

                default:
                    break;
            }
            var employee = await _employeeRepository.GetUserByIdWithRoleAsync(result.refreshToken.EmployeeId, cancellationToken);
            var accessToken = _tokenService.GenerateAccessToken(employee);
            var output = _guideFillerService.FillSuccessOutput<SignInOutput>(BusinessStrings.Resources.ErrorMessagesKeys.SignInSuccess); 
            output = output with { AccessToken = accessToken, RefreshToken = result.refreshToken.Token };
            return output;
        }
        #endregion
       
        #endregion

    }
}
