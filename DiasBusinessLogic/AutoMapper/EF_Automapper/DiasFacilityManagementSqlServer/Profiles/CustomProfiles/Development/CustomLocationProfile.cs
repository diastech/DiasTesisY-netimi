using AutoMapper;
using System;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto2 = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development
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
                        CreateMap<Dto.CustomLocationDto, Model.LocationV2>()
                             .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.LocationOriginalName));
                        CreateMap<Model.LocationV2, Dto.CustomLocationDto>()
                            .ForMember(dest => dest.LocationOriginalName, opt => opt.MapFrom(src => src.LocationName));

                        CreateMap<Dto.CustomLocationDto, Dto2.LocationV2Dto>()
                             .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.LocationOriginalName));
                        CreateMap<Dto2.LocationV2Dto, Dto.CustomLocationDto>()
                             .ForMember(dest => dest.LocationOriginalName, opt => opt.MapFrom(src => src.LocationName));
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
