using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using AutoMapper;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development.Parameter
{
    public class MobileMenuPageProfile : Profile
    {
        public MobileMenuPageProfile() { }

        public MobileMenuPageProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.MobileMenuPage, Dto.MobileMenuPageDto>();

                        CreateMap<Dto.MobileMenuPageDto, Model.MobileMenuPage>();

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
