using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using AutoMapper;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test
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
