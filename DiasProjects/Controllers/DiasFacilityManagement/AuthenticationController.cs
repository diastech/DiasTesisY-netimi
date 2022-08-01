using DIManagementStdDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using static DiasShared.Enums.Standart.HttpCodesEnum;
using Newtonsoft.Json;
using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Operations.SecurityOperations;
using static DiasShared.Enums.Standart.AuthorizationEnums;
using DiasShared.Operations.EnumOperations;
using static DiasShared.Enums.Standart.TicketEnums;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IUserDtoRepository _usersRepository;
        public AuthenticationController(IUserDtoRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/Login")]
        [HttpPost]
        public async Task<BusinessLogicResponse> LoginAsync([FromQuery] string username, [FromQuery] string password)
        {
            //SaltOperations saltOperationsObj = new SaltOperations();
            //string UserPassword = saltOperationsObj.HashPassword(password);


            long ilkSayiSeviye = 1;
            long ikinciSayiYetkiKod = 64;

           

            //web
            WebMenuHierarchicalNode nodeEnum = ikinciSayiYetkiKod.GetEnumValue<WebMenuHierarchicalNode>();
            MenuHierarchicalLevel menuHierarchy = nodeEnum.GetDisplayOrValueFromEnum<WebMenuHierarchicalNode>().
                                                            GetEnumValue<MenuHierarchicalLevel>();



            if(menuHierarchy  == MenuHierarchicalLevel.MenuHierarchicalLevel1 && nodeEnum == WebMenuHierarchicalNode.SimpleWorkOrderAssignment)
            {

            }

            //mobil
            MobilDashboardHierarchicalNode nodeEnumMobil = ikinciSayiYetkiKod.GetEnumValue<MobilDashboardHierarchicalNode>();
            MenuHierarchicalLevel menuHierarchyMobil = nodeEnum.GetDisplayOrValueFromEnum<WebMenuHierarchicalNode>().
                                                            GetEnumValue<MenuHierarchicalLevel>();

            if (menuHierarchyMobil == MenuHierarchicalLevel.MenuHierarchicalLevel1 && nodeEnumMobil == MobilDashboardHierarchicalNode.MyLeaves)
            {

            }

            //burada ticketdto daki LocationCodeId yi alacaksın, null kontrolü yaparak  
            int locationCode = 1;

            string codeStr = locationCode.GetEnumValue<LocationCodeEnum>().GetDisplayOrValueFromEnum<LocationCodeEnum>();

            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingle<DIManagementStdDto.UserDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }
            Tuple<Error, DIManagementStdDto.UserDto> serviceResult = await _usersRepository.Login(username, password);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingle<DIManagementStdDto.UserDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));
            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            //TODO : sırf login için ConvertServiceResultToBusinessLogicResponse değiştirmek istemedim, aşağıda kodu nasıl sadeleştiririz araştır
            returnResponse.JwtTokenStr = serviceResult.Item2.JwtToken;
            serviceResult.Item2.JwtToken = null;
            returnResponse.RelevantDto = serviceResult.Item2;

            return returnResponse;
        }
    }
}
