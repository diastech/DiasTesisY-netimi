using AutoMapper;
using System;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile() { }
        public RefreshTokenProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        //TODO : Veritabanı karşılığı yok, olmayacaksa dto ile birlikte toptan silinsin
                        //CreateMap<Model.RefreshToken, Dto.RefreshTokenDto>();
                        //CreateMap<Dto.RefreshTokenDto, Model.RefreshToken>();
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
