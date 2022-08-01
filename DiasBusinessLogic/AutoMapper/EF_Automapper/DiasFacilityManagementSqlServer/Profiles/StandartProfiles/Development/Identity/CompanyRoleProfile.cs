using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class CompanyRoleProfile : Profile
    {
        public CompanyRoleProfile() { }

        public CompanyRoleProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.CompanyRole, Dto.CompanyRoleDto>();
                        CreateMap<Dto.CompanyRoleDto, Model.CompanyRole>();
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
