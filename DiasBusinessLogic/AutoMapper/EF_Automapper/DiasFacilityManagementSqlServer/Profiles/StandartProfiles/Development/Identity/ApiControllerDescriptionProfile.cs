using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class ApiControllerDescriptionProfile : Profile
    {
        public ApiControllerDescriptionProfile() { }

        public ApiControllerDescriptionProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.ApiControllerDescription, Dto.ApiControllerDescriptionDto>();
                        CreateMap<Dto.ApiControllerDescriptionDto, Model.ApiControllerDescription>();
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
