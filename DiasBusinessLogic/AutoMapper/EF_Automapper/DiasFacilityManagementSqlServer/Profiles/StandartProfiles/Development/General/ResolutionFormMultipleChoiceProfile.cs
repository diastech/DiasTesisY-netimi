using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class ResolutionFormMultipleChoiceProfile : Profile
    {
        public ResolutionFormMultipleChoiceProfile() {}
        public ResolutionFormMultipleChoiceProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.ResolutionFormMultipleChoice, Dto.ResolutionFormMultipleChoiceDto>();
                        
                        CreateMap<Dto.ResolutionFormMultipleChoiceDto, Model.ResolutionFormMultipleChoice>();

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
