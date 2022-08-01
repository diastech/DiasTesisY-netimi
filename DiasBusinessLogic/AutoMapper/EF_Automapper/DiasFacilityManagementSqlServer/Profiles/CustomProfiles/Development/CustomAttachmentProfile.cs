using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using Dto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development
{
    public class CustomAttachmentProfile : Profile
    {
        public CustomAttachmentProfile() { }

        public CustomAttachmentProfile(Type dataContextType)
        {
            switch (dataContextType.Name)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        CreateMap<Model.CustomAttachmentDto, Dto.AttachmentDto>();
                        CreateMap<Dto.AttachmentDto, Model.CustomAttachmentDto>();
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
