﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using ModelIdentity = DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development
{
    public class PeriodicTicketProfile : Profile
    {
        public PeriodicTicketProfile() { }
        public PeriodicTicketProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.PeriodicTicket, Dto.PeriodicTicketDto>();
                        
                        CreateMap<Dto.PeriodicTicketDto, Model.PeriodicTicket>();

                        CreateMap<ModelIdentity.User, Dto.UserDto>();

                        CreateMap<Dto.UserDto, ModelIdentity.User>();

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
