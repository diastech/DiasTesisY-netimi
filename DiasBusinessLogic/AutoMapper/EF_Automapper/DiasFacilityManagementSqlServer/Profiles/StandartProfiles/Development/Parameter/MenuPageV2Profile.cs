using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using AutoMapper;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class MenuPageV2Profile : Profile
    {
        public MenuPageV2Profile() { }

        public MenuPageV2Profile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.MenuPageV2, Dto.MenuPageV2Dto>();

                        CreateMap<Dto.MenuPageV2Dto, Model.MenuPageV2>();

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
