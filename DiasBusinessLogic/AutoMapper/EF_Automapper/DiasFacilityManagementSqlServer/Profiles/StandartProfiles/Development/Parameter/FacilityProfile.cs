using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class FacilityProfile : Profile
    {
        public FacilityProfile() { }

        public FacilityProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.Facility, Dto.FacilityDto>();

                        CreateMap<Dto.FacilityDto, Model.Facility>();

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
