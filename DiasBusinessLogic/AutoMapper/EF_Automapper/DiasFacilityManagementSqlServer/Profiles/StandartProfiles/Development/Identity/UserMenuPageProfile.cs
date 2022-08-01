using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using AutoMapper;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class UserMenuPageProfile : Profile
    {
        public UserMenuPageProfile() { }

        public UserMenuPageProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.UserMenuPage, Dto.UserMenuPageDto>();

                        CreateMap<Dto.UserMenuPageDto, Model.UserMenuPage>();

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
