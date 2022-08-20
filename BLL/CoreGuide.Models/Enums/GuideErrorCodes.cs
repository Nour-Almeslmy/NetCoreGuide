using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Models.Enums
{
    public enum GuideErrorCodes
    {
        Success = 0,
        ServerError = 1,
        NullReference = 2,
        MissingValue = 3,
        UserNameNotUnique = 4,
        UserNotFound = 5,
        WrongPassword = 6,
        UserLockedOut = 7,
        InvalidDial = 8,
        InvalidPassowrd = 9,
        NewPassowrdEqualsCurrent = 10,
        InvalidOTP = 11,
        ExpiredOTP = 12,
        SMSNotSent = 13,
        InvalidId = 14,
        InvalidEmail = 15,
        EmailNotSent = 16,
        OTPNotFound = 17,
        InvalidName = 18,
        InvalidFile = 19,
        InvalidDepartment = 20,
        InvalidManager = 21,
        InvalidToken = 22,
        ExpiredToken = 23,
    }
}
