using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage;
using System;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using static DiasShared.Enums.Standart.HttpCodesEnum;

//TODO : Namespace  DiasShared.Operations.CommunicationOperations.DiasFacilityManagement olacak
namespace DiasShared.Operations.CommunicationOperations.DiasFacilityManagement
{
    /// <summary>
    /// Standart, tek Dto içeren request response kalıbı
    /// </summary>
    /// <typeparam name="TDto">Dto sınıfı, standart olmak zorunda</typeparam>
    public static class ResponseOperationsStandartSingle<TDto> where TDto : BaseDevelopmentStandartDto
    {
        [ObsoleteAttribute]
        public static BusinessLogicResponse ConvertServiceResultToBusinessLogicResponse(
                            Tuple<ErrorCodes, object> serviceResult, HttpResponseCodes httpCode = HttpResponseCodes.OK,
                                                bool operationResult = false, string optionalJsonResult = null, string additionalInfo = null, string jwtToken = null)
        {
            switch (serviceResult.Item1)
            {
                case ErrorCodes.None:
                    {                            
                        //standart Dtolar
                        return new BusinessLogicResponse((TDto)serviceResult.Item2,
                                        HttpResponseCodes.OK, serviceResult.Item1, true, optionalJsonResult, additionalInfo, jwtToken);                                    
                    }

                case ErrorCodes.NonCrudError:
                    {
                        return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo, jwtToken);
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
                                                        serviceResult.Item1, false, optionalJsonResult, additionalInfo, jwtToken);
                    }
            }
        }

        //TODO: İç içe if olmadan çözelim-Tarık
        //TODO : HttpResponseCodes tüm projede ayarlandıktan sonra default değer ok olmayacak
        public static BusinessLogicResponse ConvertServiceResultToBusinessLogicResponse(
           Tuple<Error, object> serviceResult, HttpResponseCodes httpCode = HttpResponseCodes.OK,
                               bool operationResult = false, string optionalJsonResult = null, string additionalInfo = null, string jwtToken = null,
                                 DataSourceLoadOptions devExtremeloadOptions = null, LoadResult devExtremeDataMetadata = null)
        {
            if (serviceResult.Item1 == null)
            {
                return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo, jwtToken, devExtremeloadOptions, devExtremeDataMetadata);

            }

            if (serviceResult.Item1.Code == "success")
            {
                return new BusinessLogicResponse((TDto)serviceResult.Item2,
                                       HttpResponseCodes.OK, serviceResult.Item1, true, optionalJsonResult, additionalInfo, jwtToken, devExtremeloadOptions, devExtremeDataMetadata);
            }
            if (serviceResult.Item1.Code == "error")
            {
                return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo, jwtToken, devExtremeloadOptions, devExtremeDataMetadata);
            }
            if (serviceResult.Item1.Code == "database.not.found")
            {
                return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo, jwtToken, devExtremeloadOptions, devExtremeDataMetadata);
            }
            if (serviceResult.Item1.Code == "request.null")
            {
                return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo, jwtToken, devExtremeloadOptions, devExtremeDataMetadata);
            }
            if (serviceResult.Item1.Code == "mapping.error")
            {
                return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo, jwtToken, devExtremeloadOptions, devExtremeDataMetadata);
            }
            if (serviceResult.Item1.Code == "connection.timeout")
            {
                return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo, jwtToken, devExtremeloadOptions, devExtremeDataMetadata);
            }
            if (serviceResult.Item1.Code == "argumentnullexception")
            {
                return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo, jwtToken, devExtremeloadOptions, devExtremeDataMetadata);
            }
            else
            {
                return new BusinessLogicResponse(null, httpCode, serviceResult.Item1, false, optionalJsonResult, additionalInfo, jwtToken, devExtremeloadOptions, devExtremeDataMetadata);
            }
        }

    }



}
