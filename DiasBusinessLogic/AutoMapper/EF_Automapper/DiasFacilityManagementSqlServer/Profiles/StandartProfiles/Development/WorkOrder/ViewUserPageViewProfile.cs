using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class ViewUserPageViewProfile : Profile
    {
        public ViewUserPageViewProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.UserPageView, Dto.ViewUserPageViewDto>();

                        CreateMap<Dto.ViewUserPageViewDto, Model.UserPageView>();

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
