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
    public class AssignmentGroupEmployeeProfile : Profile
    {
        public AssignmentGroupEmployeeProfile() { }
        public AssignmentGroupEmployeeProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.AssignmentGroupEmployee, Dto.AssignmentGroupEmployeeDto>();

                        CreateMap<Dto.AssignmentGroupEmployeeDto, Model.AssignmentGroupEmployee>();

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
