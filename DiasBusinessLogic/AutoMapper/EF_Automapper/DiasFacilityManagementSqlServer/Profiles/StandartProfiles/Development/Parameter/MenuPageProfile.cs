using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using AutoMapper;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class MenuPageProfile : Profile
    {
        public MenuPageProfile() { }

        public MenuPageProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.MenuPage, Dto.MenuPageDto>();

                        CreateMap<Dto.MenuPageDto, Model.MenuPage>();

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
