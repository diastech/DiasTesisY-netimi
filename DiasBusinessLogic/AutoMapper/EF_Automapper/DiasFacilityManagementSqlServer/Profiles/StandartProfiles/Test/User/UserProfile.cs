using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;



namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test
{
    public class UserProfile : Profile
    {
        public UserProfile() { }
        public UserProfile(Type dataContextType)
        {
            //Contextin tipini öğrenip, uygun entity ile mapleyelim Dto yu
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.User, Dto.UserDto>();

                        //Ters
                        CreateMap<Dto.UserDto, Model.User>();

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
