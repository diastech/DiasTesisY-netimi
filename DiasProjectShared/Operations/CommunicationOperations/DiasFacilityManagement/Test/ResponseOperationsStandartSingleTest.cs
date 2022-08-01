using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using DiasShared.Services.Communication.BusinessLogicMessage;
using System;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using static DiasShared.Enums.Standart.HttpCodesEnum;

//TODO : Namespace  DiasShared.Operations.CommunicationOperations.DiasFacilityManagement olacak
namespace DiasShared.Operations.CommunicationOperations
{
    /// <summary>
    /// Standart, tek Dto içeren request response kalıbı
    /// </summary>
    /// <typeparam name="TDto">Dto sınıfı, standart olmak zorunda</typeparam>
    public static class ResponseOperationsStandartSingleTest<TDto> where TDto : BaseDevelopmentStandartDto
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
    }   

}
