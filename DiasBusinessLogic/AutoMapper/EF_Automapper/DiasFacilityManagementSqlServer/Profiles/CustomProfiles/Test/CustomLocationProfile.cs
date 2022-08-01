using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using Dto = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using Dto2 = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Test
{
    public class CustomLocationProfile : Profile
    {
        public CustomLocationProfile() { }
        public CustomLocationProfile(Type dataContextType) 
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.CustomLocationDto, Dto.LocationV2>();
                        CreateMap<Dto.LocationV2, Model.CustomLocationDto>();

                        CreateMap<Model.CustomLocationDto, Dto2.LocationV2Dto>();
                        CreateMap<Dto2.LocationV2Dto, Model.CustomLocationDto>();
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
