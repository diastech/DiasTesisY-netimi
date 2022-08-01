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
    public class ResolutionFormV2Profile : Profile
    {
        public ResolutionFormV2Profile() { }
        public ResolutionFormV2Profile(Type dataContextType)
        {            
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.ResolutionFormV2, Dto.ResolutionFormV2Dto>();
                        
                        CreateMap<Dto.ResolutionFormV2Dto, Model.ResolutionFormV2>();

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
