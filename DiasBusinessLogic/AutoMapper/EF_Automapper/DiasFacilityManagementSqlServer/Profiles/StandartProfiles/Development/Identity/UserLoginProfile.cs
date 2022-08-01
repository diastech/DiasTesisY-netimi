using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class UserLoginProfile : Profile
    {
        public UserLoginProfile() { }

        public UserLoginProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.UserLogin, Dto.UserLoginDto>();
                        CreateMap<Dto.UserLoginDto, Model.UserLogin>();
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
