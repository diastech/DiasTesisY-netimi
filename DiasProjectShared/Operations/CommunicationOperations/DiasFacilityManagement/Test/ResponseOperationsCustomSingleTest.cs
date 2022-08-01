using DiasShared.Errors;
using DiasShared.InterfacesAbstracts.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using DiasShared.Services.Communication.BusinessLogicMessage;
using System;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using static DiasShared.Enums.Standart.HttpCodesEnum;

//TODO : Namespace  DiasShared.Operations.CommunicationOperations.DiasFacilityManagement olacak
namespace DiasShared.Operations.CommunicationOperations
{
    /// <summary>
    /// Custom, tek Dto içeren request response kalıbı
    /// </summary>
    /// <typeparam name="TDto">Dto sınıfı, custom olmak zorunda</typeparam>

    public static class ResponseOperationsCustomSingleTest<TDto> where TDto : IBaseDevelopmentCustomDto
    {
        public static BusinessLogicResponse ConvertServiceResultToBusinessLogicResponse(
                    Tuple<ErrorCodes, object> serviceResult, HttpResponseCodes httpCode = HttpResponseCodes.OK,
                                        bool operationResult = false, string optionalJsonResult = null, string additionalInfo = null)
        {
            switch (serviceResult.Item1)
            {
                case ErrorCodes.None:
                    {
                        //standart Dtolar
                        return new BusinessLogicResponse((TDto)serviceResult.Item2,
                                        HttpResponseCodes.OK, serviceResult.Item1, true, optionalJsonResult, additionalInfo);
                    }

                case ErrorCodes.NonCrudError:
                    {
                        return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo);
                    }

                //TODO : implemente edilecek(Hata yönetimi kısmı bitince)
                //case ErrorCodes.UndefinedWebApiEnvironmentConfiguration:

                //case ErrorCodes.NonInitializedWebApiEnvironmentConfiguration:
                //    {
                //        return new BusinessLogicRequestResponse(null, HttpResponseCodes.NotImplemented,
                //                                                         serviceResult.Item1, false, optionalJsonResult, additionalInfo);
                //    }


                default:
                    {
                        return new BusinessLogicResponse(null, HttpResponseCodes.InternalServerError,
                                                        serviceResult.Item1, false, optionalJsonResult, additionalInfo);
                    }
            }
        }

        public static BusinessLogicResponse ConvertServiceResultToBusinessLogicResponse(
           Tuple<Error, object> serviceResult, HttpResponseCodes httpCode = HttpResponseCodes.OK,
                               bool operationResult = false, string optionalJsonResult = null, string additionalInfo = null)
        {


            if (serviceResult.Item1.Code == "success")
            {
                return new BusinessLogicResponse((TDto)serviceResult.Item2,
                                       HttpResponseCodes.OK, serviceResult.Item1, true, optionalJsonResult, additionalInfo);
            }
            if (serviceResult.Item1.Code == "error")
            {
                return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo);
            }
            if (serviceResult.Item1.Code == "database.not.found")
            {
                return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo);
            }
            if (serviceResult.Item1.Code == "request.null")
            {
                return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo);
            }
            if (serviceResult.Item1.Code == "mapping.error")
            {
                return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo);
            }
            if (serviceResult.Item1.Code == "connection.timeout")
            {
                return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo);
            }
            if (serviceResult.Item1.Code == "argumentnullexception")
            {
                return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo);
            }
            else
            {
                return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo);
            }


        }


    }
}
