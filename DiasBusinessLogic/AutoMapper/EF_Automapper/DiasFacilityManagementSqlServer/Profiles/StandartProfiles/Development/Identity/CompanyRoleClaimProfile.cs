using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class CompanyRoleClaimProfile : Profile
    {
        public CompanyRoleClaimProfile() { }

        public CompanyRoleClaimProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.CompanyRoleClaim, Dto.CompanyRoleClaimDto>();
                        CreateMap<Dto.CompanyRoleClaimDto, Model.CompanyRoleClaim>();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
