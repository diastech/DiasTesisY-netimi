using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentViewReasonCategoryInterface =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentViewReasonCategoryProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentModel =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DevelopmentContext =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using static DiasShared.Enums.Standart.UserEnums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class ViewReasonCategoryBusinessRules : BusinessRuleAbstract, DevelopmentViewReasonCategoryInterface.IViewReasonCategoryBusinessRules, IBaseViewReasonCategoryBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentViewReasonCategoryProfile.ViewReasonCategoryProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new  (new (DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public ViewReasonCategoryBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private ViewReasonCategoryBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

    }
}
