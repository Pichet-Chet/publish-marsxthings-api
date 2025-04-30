using System;
using System.Net;

namespace MarsXApps.Models.Constants
{
	public class Constants
	{
        #region Genetal Message


        #region English

        public const string UserNameInvalid = "The user account was not found within the system.";

        public const string UnitOfTime = "ms.";

        public const string Nullable = "N/A";

        public const string Required = "Parameter is required";

        public const string Identify = "Identify";

        public const string DataHasBeenSaved = "Data Saved Successfully.";

        public const string InvalidAccessToken = "Invalid access token or refresh token.";

        public const string InvalidEmailDuplicate = "Duplicate email within the system";

        public const string InvalidUsernameDuplicate = "Duplicate username within the system";

        public const string InvalidDataDuplicate = "Duplicate information within the system";

        public const string InvalidDataFormat = "The data format is incorrect.";

        public const string UpdateDataNotFound = "Information key was not found.";

        public const string RecordDataNotFound = "The searched information was not found.";

        public const string CustomerNotFound = "Customer ID not found.";

        public const string AuthenticationUsernameNotFound = "No user account found.";

        public const string AuthenticationTokenNotFound = "The token account not found.";

        public const string AuthenticationTokenExpire = "The token account is expired.";

        public const string AuthenticationInvalidPassword = "The password is incorrect.";

        public const string AuthenticationAccountBanned = "User account has been blocked.";

        public const string RegisterMobilePhoneHasAlready = "The registered phone number has already been found within the system.";

        public const string RegisterMobilePhoneOTPFaild = "The system cannot send the OTP message to the customer. Please try again..";

        public const string RegisterMobilePhoneOTPVerifyFaild = "OTP is invalid.";

        public const string PasswordNotMatch = "Password not match.";

        public const string InvalidFileImage = "Invalid file format. Only .jpg and .png are allowed.";

        public const string LastConsiderationIsReject = "Your last consideration is reject.";


        public const string ValidationNotFound = "Data not found";

        public const string ValidationRetryAgain = "Please check and re-try again.";

        public const string DeleteModelMachineFailed = "Failed to delete data.";

        public const string DeleteModelMachineSuccess = "Success to delete mahcine.";

        public const string PostingLimitTooMany = "Unable to create a post because the number of posts exceeds the limit.";



        #endregion


        #region Thai

        #endregion




        #endregion


        #region HTTP response status codes

        public const int HttpCode200 = 200;
        public const string HttpCode200Message = "OK";

        public const int HttpCode204 = 204;
        public const string HttpCode204Message = "No Content";


        public const int HttpCode300 = 300;
        public const string HttpCode300Message = "Multiple Choices";


        public const int HttpCode400 = 400;
        public const string HttpCode400Message = "Bad Request";

        public const int HttpCode401 = 401;
        public const string HttpCode401Message = "Unauthorized";

        public const int HttpCode402 = 402;
        public const string HttpCode402Message = "Payment Required Experimental";

        public const int HttpCode429 = 429;
        public const string HttpCode429Message = "Too many request";



        public const int HttpCode500 = 500;
        public const string HttpCode500Message = "Internal Server Error : The server has encountered a situation it does not know how to handle.";

        public const int HttpCode511 = 511;
        public const string HttpCode511Message = "Network Authentication Required.";


        

        #endregion

        #region Type

        public const string MessageSuccess = "Success";
        public const string MessageError = "Error";
        public const string MessageWarning = "Warning";

        #endregion

        #region Status

        public const bool StatusSuccess = true;
        public const bool StatusError = false;

        #endregion

        #region StatusCode

        public const int StatusCodeOK = 100001;
        public const int StatusCodeDataNotFound = 20001;
        public const int StatusCodeDataDuplicate = 20002;
        public const int StatusCodeParamRequired = 30011;
        public const int StatusCodeParamInvalid = 30021;
        public const int StatusCodeException = 90000;

        #endregion

    }
}

