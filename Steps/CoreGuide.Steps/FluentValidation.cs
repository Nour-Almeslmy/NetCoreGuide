using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class FluentValidation
    {
        #region Steps
        /// 1) add package "FluentValidation.AspNetCore"
        /// 2) create new validator by inheriting from AbstractValidator<TInput>
        /// 3) to register all validators with internal access modifiers as well use this, which registers them as Scoped
        ///     this will run automatic validation at the beginning of the request directly during model binding
        const string registeration =  @"
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterUserInputValidator>(includeInternalTypes:true));
            ";
        /// When disabled, ASP.NET won’t attempt to use FluentValidation to validate objects during model binding.
        const string registerationWithManualValidation = @"
            services.AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<RegisterUserInputValidator>(includeInternalTypes: true);
                config.AutomaticValidationEnabled = false;
            });            ";
        #endregion

        #region To create new custom validator for specific type
        /// 1) Create new generic class by implementing ": PropertyValidator<T, IType>",ex "FileContentTypeValidator"
        /// 2) if it takes any parameters, add it to the CTOR
        /// 3) Add new extenion method to "IRuleBuilder<T, IType>", for example
        const string example = @"        
            public static IRuleBuilderOptions<T, IFormFile> AllowedTypes<T>(this IRuleBuilder<T, IFormFile> ruleBuilder, string[] allowedFileExtensions)
                {
                    return ruleBuilder.SetValidator(new FileContentTypeValidator<T>(allowedFileExtensions)));
                }
                ";
        #endregion

        #region References
        /// https://docs.fluentvalidation.net/en/latest/aspnet.html
        /// https://docs.fluentvalidation.net/en/latest/aspnet.html#disabling-automatic-validation
        /// https://docs.fluentvalidation.net/en/latest/async.html
        #endregion
    }
}
