using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class LocationCodeProfile : Profile
    {
        public LocationCodeProfile() { }

        public LocationCodeProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.LocationCode, Dto.LocationCodeDto>();

                        CreateMap<Dto.LocationCodeDto, Model.LocationCode>();

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
