using AutoMapper;
using System;
using Model = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using Dto = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
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
                        CreateMap<Model.CustomLocationDto, Dto.LocationV2>()
                             .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.LocationOriginalName));
                        CreateMap<Dto.LocationV2, Model.CustomLocationDto>()
                            .ForMember(dest => dest.LocationOriginalName, opt => opt.MapFrom(src => src.LocationName));

                        CreateMap<Model.CustomLocationDto, Dto2.LocationV2Dto>()
                             .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.LocationOriginalName));
                        CreateMap<Dto2.LocationV2Dto, Model.CustomLocationDto>()
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
