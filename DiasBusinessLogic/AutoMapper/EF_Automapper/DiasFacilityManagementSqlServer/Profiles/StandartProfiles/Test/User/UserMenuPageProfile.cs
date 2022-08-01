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
