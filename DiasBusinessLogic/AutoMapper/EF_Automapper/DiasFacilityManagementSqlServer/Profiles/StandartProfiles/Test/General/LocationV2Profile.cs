using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test
{
    public class LocationV2Profile : Profile
    {
        public LocationV2Profile() { }
        public LocationV2Profile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.LocationV2, Dto.LocationV2Dto>();

                        CreateMap<Dto.LocationV2Dto, Model.LocationV2>();

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
