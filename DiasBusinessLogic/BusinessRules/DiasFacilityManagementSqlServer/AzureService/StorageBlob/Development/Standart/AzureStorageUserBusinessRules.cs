using DiasBusinessLogic.AutoMapper.Configuration;
using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles;
using diasFmDevStn = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.AzureService.Standart;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.AzureService.Development.Standart;
using DiasBusinessLogic.Shared.Configuration;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.AzureService.StorageBlob.Development.Standart
{
    public class AzureStorageUserBusinessRules : BusinessRuleAbstract, IAzureStorageUserBusinessRules, IBaseAzureStorageUserBusinessRules
    {

        //İş kuralına özel profil generic AutoMapper mapper nesnemiz, statikdir
        private static AutoMapperProfileMapper<diasFmDevStn.UserProfile> DtoMapperDiasFacilityManagementSqlServer
            => new (new (DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));
    }
}
