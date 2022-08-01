
using DevExtreme.AspNet.Mvc;
using DiasShared.Errors;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using static DiasShared.Enums.Standart.HttpCodesEnum;

namespace DiasShared.Services.Communication.BusinessLogicMessage
{
    public class BusinessLogicResponse
    {
        /// <summary>
        /// Crud'un yapıldığı Dto
        /// BL veya client tarafında kendi Dto tipine dönüştürülecektir
        /// Get dışındaki isteklerde veya hata dönüşlerinde null olabilir
        /// </summary>
        public object RelevantDto { get; set; }

        /// <summary>
        /// Dönen http kodu
        /// </summary>
        public HttpResponseCodes HttpResponseCode { get; set; }

        //TODO : Obsolete
        public ErrorCodes ErrorCode { get; set; }

        public Error ErrorObj { get; set; }

        /// <summary>
        /// başarılı --> true, hata --> false
        /// </summary>        
        public bool OperationResult { get; set; }

        /// <summary>
        /// Ekstra Json response verisi
        /// Farklı projelere GSMS Dtolarını taşıyamayacağımız için, haberleşmede bu property kullanılacak
        /// </summary>
        public string OptionalJsonResult { get; set; }

        /// <summary>
        /// hata veya başarılı dönüşle alakalı ekstra açıklamalar
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Jwt token'ın tutulduğu yer, Anonymouslar için boştur(null da olabilir)
        /// </summary>
        public string JwtTokenStr { get; set; }

        //DevExpress filter ve diğer load optionslar
        DataSourceLoadOptions DevExpressLoadOptions { get; set; }

        public BusinessLogicResponse() { }


        //TODO : Obsolete
        public BusinessLogicResponse(object relevantDto, HttpResponseCodes httpResponseCode, ErrorCodes crudErrorCode, 
            bool operationResult, string optionalJsonResult = null, string additionalInfo = null, string jwtToken = null)
        {
            RelevantDto = relevantDto;
            HttpResponseCode = httpResponseCode;
            ErrorCode = crudErrorCode;
            OperationResult = operationResult;
            OptionalJsonResult = optionalJsonResult;
            AdditionalInfo = additionalInfo;
            JwtTokenStr = jwtToken;
        }

        public BusinessLogicResponse(object relevantDto, HttpResponseCodes httpResponseCode, Error crudErrorCode,
           bool operationResult, string optionalJsonResult = null, string additionalInfo = null, string jwtToken = null)
        {
            RelevantDto = relevantDto;
            HttpResponseCode = httpResponseCode;
            ErrorObj = crudErrorCode;
            OperationResult = operationResult;
            OptionalJsonResult = optionalJsonResult;
            AdditionalInfo = additionalInfo;
            JwtTokenStr = jwtToken;
        }
    }
}
