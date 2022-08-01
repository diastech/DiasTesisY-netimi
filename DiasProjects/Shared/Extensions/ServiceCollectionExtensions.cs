using DiasBusinessLogic.BusinessRules.CustomMappingRules;
using DevelopmentBusinessRule = DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Custom;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using WebApi = DiasWebApi.Shared.Configuration;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DiasFacilityRepository = DiasWebApi.Repositories.DiasFacilityManagement;
using DiasFacilityRepositoryInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DiasWebApi.Repositories.DiasFacilityManagement;
using DevFmBlInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DevFmStanDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevFmMapProf = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DevFmBlClass = DiasBusinessLogic.Shared.GenericMethods.DiasFacilityManagementSqlServer.Standart.Development;
using TestFmBlInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Test;
using TestFmStanDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using TestFmMapProf = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;
using TestFmBlClass = DiasBusinessLogic.Shared.GenericMethods.DiasFacilityManagementSqlServer.Standart.Test;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using Microsoft.Extensions.Caching.Distributed;

namespace DiasWebApi.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinessLogicBusinessRulesServiceScopes(this IServiceCollection services)
        {
            //DI scopeları
            //Uygulama tipine göre base iş kuralı interface'ini repository sınıfına inject eder
            //Ambiguity olmaması için environmental business rules sınıfları full namespacelerle yazıldı

            IConfiguration configurationSettings = WebApi.ConfigurationHelper.GetConfig();

            //global DI lar
            services.AddScoped<IBaseMappingRulesBusinessRules, DtoToDtoMappingRules>();

            //environmental DI lar
            switch (WebApi.ConfigurationHelper.GetBusinessLogicEnvironment(configurationSettings))
            {
                case ApplicationBusinessLogicEnvironment.Live:
                    {
                        break;
                    }

                case ApplicationBusinessLogicEnvironment.Test:
                    {
                        //TODO : region düzenlemeleri
                        //TODO : try catch eklenmesi
                        #region Test

                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.AssignmentGroupAuthorizedLocationDto, TestFmMapProf.AssignmentGroupAuthorizedLocationProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.AssignmentGroupAuthorizedLocationDto, TestFmMapProf.AssignmentGroupAuthorizedLocationProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.AssignmentGroupDto, TestFmMapProf.AssignmentGroupProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.AssignmentGroupDto, TestFmMapProf.AssignmentGroupProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.AssignmentGroupEmployeeDto, TestFmMapProf.AssignmentGroupEmployeeProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.AssignmentGroupEmployeeDto, TestFmMapProf.AssignmentGroupEmployeeProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.AttachmentDto, TestFmMapProf.AttachmentProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.AttachmentDto, TestFmMapProf.AttachmentProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.BasicTicketDto, TestFmMapProf.BasicTicketProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.BasicTicketDto, TestFmMapProf.BasicTicketProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.LocationDto, TestFmMapProf.LocationProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.LocationDto, TestFmMapProf.LocationProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.LocationV2Dto, TestFmMapProf.LocationV2Profile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.LocationV2Dto, TestFmMapProf.LocationV2Profile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.MenuPageDto, TestFmMapProf.MenuPageProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.MenuPageDto, TestFmMapProf.MenuPageProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.MenuPageV2Dto, TestFmMapProf.MenuPageV2Profile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.MenuPageV2Dto, TestFmMapProf.MenuPageV2Profile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.PeriodicTicketDto, TestFmMapProf.PeriodicTicketProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.PeriodicTicketDto, TestFmMapProf.PeriodicTicketProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.ResolutionFormAnswerDto, TestFmMapProf.ResolutionFormAnswerProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.ResolutionFormAnswerDto, TestFmMapProf.ResolutionFormAnswerProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.ResolutionFormChoiceOptionDto, TestFmMapProf.ResolutionFormChoiceOptionProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.ResolutionFormChoiceOptionDto, TestFmMapProf.ResolutionFormChoiceOptionProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.ResolutionFormDto, TestFmMapProf.ResolutionFormProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.ResolutionFormDto, TestFmMapProf.ResolutionFormProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.ResolutionFormMultipleChoiceDto, TestFmMapProf.ResolutionFormMultipleChoiceProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.ResolutionFormMultipleChoiceDto, TestFmMapProf.ResolutionFormMultipleChoiceProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.ResolutionFormQuestionAnswerDto, TestFmMapProf.ResolutionFormQuestionAnswerProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.ResolutionFormQuestionAnswerDto, TestFmMapProf.ResolutionFormQuestionAnswerProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.ResolutionFormQuestionDto, TestFmMapProf.ResolutionFormQuestionProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.ResolutionFormQuestionDto, TestFmMapProf.ResolutionFormQuestionProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.ResolutionFormQuestionTypeDto, TestFmMapProf.ResolutionFormQuestionTypeProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.ResolutionFormQuestionTypeDto, TestFmMapProf.ResolutionFormQuestionTypeProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.ResolutionFormSingleQuestionDto, TestFmMapProf.ResolutionFormSingleQuestionProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.ResolutionFormSingleQuestionDto, TestFmMapProf.ResolutionFormSingleQuestionProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.ResolutionFormV2Dto, TestFmMapProf.ResolutionFormV2Profile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.ResolutionFormV2Dto, TestFmMapProf.ResolutionFormV2Profile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.ResolutionFormYesNoDto, TestFmMapProf.ResolutionFormYesNoProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.ResolutionFormYesNoDto, TestFmMapProf.ResolutionFormYesNoProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketAuditHistoryDto, TestFmMapProf.TicketAuditHistoryProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.TicketAuditHistoryDto, TestFmMapProf.TicketAuditHistoryProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketDto, TestFmMapProf.TicketProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.TicketDto, TestFmMapProf.TicketProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketNoteDto, TestFmMapProf.TicketNoteProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.TicketNoteDto, TestFmMapProf.TicketNoteProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketReasonDto, TestFmMapProf.TicketReasonProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.TicketReasonDto, TestFmMapProf.TicketReasonProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketReasonCategoryDto, TestFmMapProf.TicketReasonCategoryProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.TicketReasonCategoryDto, TestFmMapProf.TicketReasonCategoryProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketReasonCategoryV2Dto, TestFmMapProf.TicketReasonCategoryV2Profile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.TicketReasonCategoryV2Dto, TestFmMapProf.TicketReasonCategoryV2Profile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketRelatedLocationDto, TestFmMapProf.TicketRelatedLocationProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.TicketRelatedLocationDto, TestFmMapProf.TicketRelatedLocationProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketStateDto, TestFmMapProf.TicketStateProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.TicketStateDto, TestFmMapProf.TicketStateProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketPriorityDto, TestFmMapProf.TicketPriorityProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.TicketPriorityDto, TestFmMapProf.TicketPriorityProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.BasicTicketStateDto, TestFmMapProf.BasicTicketStateProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.BasicTicketStateDto, TestFmMapProf.BasicTicketStateProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketStateTransitionDto, TestFmMapProf.TicketStateTransitionProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.TicketStateTransitionDto, TestFmMapProf.TicketStateTransitionProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.UserMenuPageDto, TestFmMapProf.UserMenuPageProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.UserMenuPageDto, TestFmMapProf.UserMenuPageProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.WorkShiftDto, TestFmMapProf.WorkShiftProfile>,
                            TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.WorkShiftDto, TestFmMapProf.WorkShiftProfile>>();
                        services.AddScoped<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.UserDto, TestFmMapProf.UserProfile>,
                                TestFmBlClass.GenericStandartBusinessRules<TestFmStanDto.UserDto, TestFmMapProf.UserProfile>>();

                        #endregion Test

                        //TODO :  #region Repository ->   #region FacilityManager eklenecek

                        break;

                    }

                case ApplicationBusinessLogicEnvironment.Development:
                    {
                        try
                        {


                            #region BaseBusinessRules

                            #region Standart

                            #region FacilityManager

                            services.AddScoped<IBaseCompanyRoleBusinessRules, DevelopmentBusinessRule.CompanyRoleBusinessRules>();
                            services.AddScoped<IBaseCompanyRoleClaimBusinessRules, DevelopmentBusinessRule.CompanyRoleClaimBusinessRules>();
                            services.AddScoped<IBaseUserBusinessRules, DevelopmentBusinessRule.UserBusinessRules>();
                            services.AddScoped<IBaseUserClaimBusinessRules, DevelopmentBusinessRule.UserClaimBusinessRules>();
                            services.AddScoped<IBaseCompanyRoleUserBusinessRules, DevelopmentBusinessRule.CompanyRoleUserBusinessRules>();
                            services.AddScoped<IBaseUserLoginBusinessRules, DevelopmentBusinessRule.UserLoginBusinessRules>();
                            services.AddScoped<IBaseUserTokenBusinessRules, DevelopmentBusinessRule.UserTokenBusinessRules>();
                            services.AddScoped<IBaseAssignmentGroupAuthorizedLocationBusinessRules, DevelopmentBusinessRule.AssignmentGroupAuthorizedLocationBusinessRules>();
                            services.AddScoped<IBaseAssignmentGroupBusinessRules, DevelopmentBusinessRule.AssignmentGroupBusinessRules>();
                            services.AddScoped<IBaseAssignmentGroupEmployeeBusinessRules, DevelopmentBusinessRule.AssignmentGroupEmployeeBusinessRules>();
                            services.AddScoped<IBaseAttachmentBusinessRules, DevelopmentBusinessRule.AttachmentBusinessRules>();
                            services.AddScoped<IBaseBasicTicketBusinessRules, DevelopmentBusinessRule.BasicTicketBusinessRules>();
                            services.AddScoped<IBaseLocationBusinessRules, DevelopmentBusinessRule.LocationBusinessRules>();
                            services.AddScoped<IBaseLocationV2BusinessRules, DevelopmentBusinessRule.LocationV2BusinessRules>();
                            services.AddScoped<IBaseMenuPageBusinessRules, DevelopmentBusinessRule.MenuPageBusinessRules>();
                            services.AddScoped<IBaseMenuPageV2BusinessRules, DevelopmentBusinessRule.MenuPageV2BusinessRules>();
                            services.AddScoped<IBasePeriodicTicketBusinessRules, DevelopmentBusinessRule.PeriodicTicketBusinessRules>();
                            services.AddScoped<IBaseResolutionFormAnswerBusinessRules, DevelopmentBusinessRule.ResolutionFormAnswerBusinessRules>();
                            services.AddScoped<IBaseResolutionFormBusinessRules, DevelopmentBusinessRule.ResolutionFormBusinessRules>();
                            services.AddScoped<IBaseResolutionFormChoiceOptionBusinessRules, DevelopmentBusinessRule.ResolutionFormChoiceOptionBusinessRules>();
                            services.AddScoped<IBaseResolutionFormMultipleChoiceBusinessRules, DevelopmentBusinessRule.ResolutionFormMultipleChoiceBusinessRules>();
                            services.AddScoped<IBaseResolutionFormQuestionAnswerBusinessRules, DevelopmentBusinessRule.ResolutionFormQuestionAnswerBusinessRules>();
                            services.AddScoped<IBaseResolutionFormQuestionBusinessRules, DevelopmentBusinessRule.ResolutionFormQuestionBusinessRules>();
                            services.AddScoped<IBaseResolutionFormQuestionTypeBusinessRules, DevelopmentBusinessRule.ResolutionFormQuestionTypeBusinessRules>();
                            services.AddScoped<IBaseResolutionFormSingleQuestionBusinessRules, DevelopmentBusinessRule.ResolutionFormSingleQuestionBusinessRules>();
                            services.AddScoped<IBaseResolutionFormYesNoBusinessRules, DevelopmentBusinessRule.ResolutionFormYesNoBusinessRules>();
                            services.AddScoped<IBaseTicketAuditHistoryBusinessRules, DevelopmentBusinessRule.TicketAuditHistoryBusinessRules>();
                            services.AddScoped<IBaseTicketBusinessRules, DevelopmentBusinessRule.TicketBusinessRules>();
                            services.AddScoped<IBaseTicketNoteBusinessRules, DevelopmentBusinessRule.TicketNoteBusinessRules>();
                            services.AddScoped<IBaseTicketReasonBusinessRules, DevelopmentBusinessRule.TicketReasonBusinessRules>();
                            services.AddScoped<IBaseTicketReasonCategoryBusinessRules, DevelopmentBusinessRule.TicketReasonCategoryBusinessRules>();
                            services.AddScoped<IBaseTicketReasonCategoryV2BusinessRules, DevelopmentBusinessRule.TicketReasonCategoryV2BusinessRules>();
                            services.AddScoped<IBaseTicketRelatedLocationBusinessRules, DevelopmentBusinessRule.TicketRelatedLocationBusinessRules>();
                            services.AddScoped<IBaseTicketStateBusinessRules, DevelopmentBusinessRule.TicketStateBusinessRules>();
                            services.AddScoped<IBaseTicketPriorityBusinessRules, DevelopmentBusinessRule.TicketPriorityBusinessRules>();
                            services.AddScoped<IBaseBasicTicketStateBusinessRules, DevelopmentBusinessRule.BasicTicketStateBusinessRules>();
                            services.AddScoped<IBaseTicketStateTransitionBusinessRules, DevelopmentBusinessRule.TicketStateTransitionBusinessRules>();
                            services.AddScoped<IBaseUserMenuPageBusinessRules, DevelopmentBusinessRule.UserMenuPageBusinessRules>();
                            services.AddScoped<IBaseViewAssigmentGroupEmployeeBusinessRules, DevelopmentBusinessRule.ViewAssigmentGroupEmployeeBusinessRules>();
                            services.AddScoped<IBaseViewReasonCategoryBusinessRules, DevelopmentBusinessRule.ViewReasonCategoryBusinessRules>();
                            services.AddScoped<IBaseViewTicketBusinessRules, DevelopmentBusinessRule.ViewTicketBusinessRules>();
                            services.AddScoped<IBaseViewTicketFormQBusinessRules, DevelopmentBusinessRule.ViewTicketFormQBusinessRules>();
                            services.AddScoped<IBaseViewTicketLocationBusinessRules, DevelopmentBusinessRule.ViewTicketLocationBusinessRules>();
                            services.AddScoped<IBaseViewTicketNoteBusinessRules, DevelopmentBusinessRule.ViewTicketNoteBusinessRules>();
                            services.AddScoped<IBaseViewTicketStateLevelBusinessRules, DevelopmentBusinessRule.ViewTicketStateLevelBusinessRules>();
                            services.AddScoped<IBaseViewUserPageBusinessRules, DevelopmentBusinessRule.ViewUserPageBusinessRules>();
                            services.AddScoped<IBaseWorkShiftBusinessRules, DevelopmentBusinessRule.WorkShiftBusinessRules>();

                            #endregion FacilityManager

                            #endregion Standart

                            #region Custom

                            #region FacilityManager

                            //custom transactional iş kurallar
                            services.AddScoped<IBaseUserTransactionalBusinessRules, UserTransactionalBusinessRules>();
                            services.AddScoped<IBasePeriodicTicketWrapperTransactionalBusinessRules, PeriodicTicketWrapperTransactionalBusinessRules>();
                            services.AddScoped<IBaseBasicTicketWrapperTransactionalBusinessRules, BasicTicketWrapperTransactionalBusinessRules>();
                            services.AddScoped<IBaseTicketWrapperTransactionalBusinessRules, TicketWrapperTransactionalBusinessRules>();
                            services.AddScoped<IBaseTicketNoteWrapperTransactionalBusinessRules, TicketNoteWrapperTransactionalBusinessRules>();
                            services.AddScoped<IBaseAttachmentWrapperTransactionalBusinessRules, AttachmentWrapperTransactionalBusinessRules>();


                            //Custom nontransactional iş kuralları
                            services.AddScoped<IBaseTicketWrapperBusinessRules, TicketWrapperBusinessRules>();
                            services.AddScoped<IBaseTicketNoteWrapperBusinessRules, TicketNoteWrapperBusinessRules>();
                            services.AddScoped<IBaseBasicTicketWrapperBusinessRules, BasicTicketWrapperBusinessRules>();
                            services.AddScoped<IBasePeriodicTicketWrapperBusinessRules, PeriodicTicketWrapperBusinessRules>();
                            services.AddScoped<IBaseLocationWrapperBusinessRules, LocationWrapperBusinessRules>();
                            services.AddScoped<IBaseTicketReasonCategoryWrapperBusinessRules, TicketReasonCategoryWrapperBusinessRules>();
                            services.AddScoped<IBaseTicketNoteWrapperBusinessRules, TicketNoteWrapperBusinessRules>();
                            services.AddScoped<IBaseAttachmentWrapperBusinessRules, AttachmentWrapperBusinessRules>();
                            services.AddScoped<IBaseTicketStateFlowWrapperBusinessRules, TicketStateFlowWrapperBusinessRules>();
                            services.AddScoped<IBaseUserAssignmentGroupWrapperBusinessRules, UserAssignmentGroupWrapperBusinessRules>();


                            #endregion FacilityManager

                            #endregion Custom

                            #endregion BaseBusinessRules

                            #region Generic

                            #region FacilityManager

                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.RefreshTokenDto, DevFmMapProf.RefreshTokenProfile>,
                                    DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.RefreshTokenDto, DevFmMapProf.RefreshTokenProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.CompanyRoleClaimDto, DevFmMapProf.CompanyRoleClaimProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.CompanyRoleClaimDto, DevFmMapProf.CompanyRoleClaimProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.CompanyRoleDto, DevFmMapProf.CompanyRoleProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.CompanyRoleDto, DevFmMapProf.CompanyRoleProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserClaimDto, DevFmMapProf.UserClaimProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.UserClaimDto, DevFmMapProf.UserClaimProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserDto, DevFmMapProf.UserProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.UserDto, DevFmMapProf.UserProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserLoginDto, DevFmMapProf.UserLoginProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.UserLoginDto, DevFmMapProf.UserLoginProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.CompanyRoleUserDto, DevFmMapProf.CompanyRoleProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.CompanyRoleUserDto, DevFmMapProf.CompanyRoleProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserTokenDto, DevFmMapProf.UserTokenProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.UserTokenDto, DevFmMapProf.UserTokenProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.AssignmentGroupAuthorizedLocationDto, DevFmMapProf.AssignmentGroupAuthorizedLocationProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.AssignmentGroupAuthorizedLocationDto, DevFmMapProf.AssignmentGroupAuthorizedLocationProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.AssignmentGroupDto, DevFmMapProf.AssignmentGroupProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.AssignmentGroupDto, DevFmMapProf.AssignmentGroupProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.AssignmentGroupEmployeeDto, DevFmMapProf.AssignmentGroupEmployeeProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.AssignmentGroupEmployeeDto, DevFmMapProf.AssignmentGroupEmployeeProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.AttachmentDto, DevFmMapProf.AttachmentProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.AttachmentDto, DevFmMapProf.AttachmentProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.BasicTicketDto, DevFmMapProf.BasicTicketProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.BasicTicketDto, DevFmMapProf.BasicTicketProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.LocationDto, DevFmMapProf.LocationProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.LocationDto, DevFmMapProf.LocationProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.LocationV2Dto, DevFmMapProf.LocationV2Profile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.LocationV2Dto, DevFmMapProf.LocationV2Profile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.MenuPageDto, DevFmMapProf.MenuPageProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.MenuPageDto, DevFmMapProf.MenuPageProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.MenuPageV2Dto, DevFmMapProf.MenuPageV2Profile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.MenuPageV2Dto, DevFmMapProf.MenuPageV2Profile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.PeriodicTicketDto, DevFmMapProf.PeriodicTicketProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.PeriodicTicketDto, DevFmMapProf.PeriodicTicketProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormAnswerDto, DevFmMapProf.ResolutionFormAnswerProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.ResolutionFormAnswerDto, DevFmMapProf.ResolutionFormAnswerProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormChoiceOptionDto, DevFmMapProf.ResolutionFormChoiceOptionProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.ResolutionFormChoiceOptionDto, DevFmMapProf.ResolutionFormChoiceOptionProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormDto, DevFmMapProf.ResolutionFormProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.ResolutionFormDto, DevFmMapProf.ResolutionFormProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormMultipleChoiceDto, DevFmMapProf.ResolutionFormMultipleChoiceProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.ResolutionFormMultipleChoiceDto, DevFmMapProf.ResolutionFormMultipleChoiceProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormQuestionAnswerDto, DevFmMapProf.ResolutionFormQuestionAnswerProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.ResolutionFormQuestionAnswerDto, DevFmMapProf.ResolutionFormQuestionAnswerProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormQuestionDto, DevFmMapProf.ResolutionFormQuestionProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.ResolutionFormQuestionDto, DevFmMapProf.ResolutionFormQuestionProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormQuestionTypeDto, DevFmMapProf.ResolutionFormQuestionTypeProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.ResolutionFormQuestionTypeDto, DevFmMapProf.ResolutionFormQuestionTypeProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormSingleQuestionDto, DevFmMapProf.ResolutionFormSingleQuestionProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.ResolutionFormSingleQuestionDto, DevFmMapProf.ResolutionFormSingleQuestionProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormV2Dto, DevFmMapProf.ResolutionFormV2Profile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.ResolutionFormV2Dto, DevFmMapProf.ResolutionFormV2Profile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormYesNoDto, DevFmMapProf.ResolutionFormYesNoProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.ResolutionFormYesNoDto, DevFmMapProf.ResolutionFormYesNoProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketAuditHistoryDto, DevFmMapProf.TicketAuditHistoryProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.TicketAuditHistoryDto, DevFmMapProf.TicketAuditHistoryProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketDto, DevFmMapProf.TicketProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.TicketDto, DevFmMapProf.TicketProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketNoteDto, DevFmMapProf.TicketNoteProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.TicketNoteDto, DevFmMapProf.TicketNoteProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketReasonDto, DevFmMapProf.TicketReasonProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.TicketReasonDto, DevFmMapProf.TicketReasonProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketReasonCategoryDto, DevFmMapProf.TicketReasonCategoryProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.TicketReasonCategoryDto, DevFmMapProf.TicketReasonCategoryProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketReasonCategoryV2Dto, DevFmMapProf.TicketReasonCategoryV2Profile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.TicketReasonCategoryV2Dto, DevFmMapProf.TicketReasonCategoryV2Profile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketRelatedLocationDto, DevFmMapProf.TicketRelatedLocationProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.TicketRelatedLocationDto, DevFmMapProf.TicketRelatedLocationProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketStateDto, DevFmMapProf.TicketStateProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.TicketStateDto, DevFmMapProf.TicketStateProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketPriorityDto, DevFmMapProf.TicketPriorityProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.TicketPriorityDto, DevFmMapProf.TicketPriorityProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.BasicTicketStateDto, DevFmMapProf.BasicTicketStateProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.BasicTicketStateDto, DevFmMapProf.BasicTicketStateProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketStateTransitionDto, DevFmMapProf.TicketStateTransitionProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.TicketStateTransitionDto, DevFmMapProf.TicketStateTransitionProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserMenuPageDto, DevFmMapProf.UserMenuPageProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.UserMenuPageDto, DevFmMapProf.UserMenuPageProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.WorkShiftDto, DevFmMapProf.WorkShiftProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.WorkShiftDto, DevFmMapProf.WorkShiftProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserDto, DevFmMapProf.UserProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.UserDto, DevFmMapProf.UserProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ApiActionDescriptionDto, DevFmMapProf.ApiActionDescriptionProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.ApiActionDescriptionDto, DevFmMapProf.ApiActionDescriptionProfile>>();
                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ApiControllerDescriptionDto, DevFmMapProf.ApiControllerDescriptionProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.ApiControllerDescriptionDto, DevFmMapProf.ApiControllerDescriptionProfile>>();

                            services.AddScoped<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.FacilityDto, DevFmMapProf.FacilityProfile>,
                                DevFmBlClass.GenericStandartBusinessRules<DevFmStanDto.FacilityDto, DevFmMapProf.FacilityProfile>>();

                            #endregion FacilityManager

                            #endregion Generic

                            #region Repository

                            #region FacilityManager

                            //TODO : TestFmBlInterface ile başlayan injectler test switchine taşınacak
                            #region Standart

                            services.AddScoped<ITicketStateRoleDtoRepository, TicketStateRoleDtoRepository>(sp =>
                            {
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketStateRoleDto, DevFmMapProf.TicketStateRoleProfile> genericUserBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketStateRoleDto, DevFmMapProf.TicketStateRoleProfile>>();
                                return new DiasFacilityRepository.TicketStateRoleDtoRepository(ApplicationBusinessLogicEnvironment.Development, genericUserBusinessRules);
                            });
                            services.AddScoped<ITicketStateFlowRoleDtoRepository, TicketStateFlowRoleRepository>(sp =>
                            {
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketStateFlowRoleDto, DevFmMapProf.TicketStateFlowRoleProfile> genericUserBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketStateFlowRoleDto, DevFmMapProf.TicketStateFlowRoleProfile>>();
                                return new DiasFacilityRepository.TicketStateFlowRoleRepository(ApplicationBusinessLogicEnvironment.Development, genericUserBusinessRules);
                            });
                            services.AddScoped<ITicketStateFlowDtoRepository, TicketStateFlowDtoRepository>(sp =>
                            {
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketStateFlowDto, DevFmMapProf.TicketStateFlowProfile> genericUserBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketStateFlowDto, DevFmMapProf.TicketStateFlowProfile>>();
                                return new DiasFacilityRepository.TicketStateFlowDtoRepository(ApplicationBusinessLogicEnvironment.Development, genericUserBusinessRules);
                            });

                            services.AddScoped<IFacilityDtoRepository, FacilityDtoRepository>(sp =>
                            {
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.FacilityDto, DevFmMapProf.FacilityProfile> genericUserBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.FacilityDto, DevFmMapProf.FacilityProfile>>();
                                return new DiasFacilityRepository.FacilityDtoRepository(ApplicationBusinessLogicEnvironment.Development, genericUserBusinessRules);
                            });


                            services.AddScoped<ICompanyRoleClaimDtoRepository, CompanyRoleClaimDtoRepository>(sp =>
                            {
                                IBaseCompanyRoleClaimBusinessRules baseCompanyRoleClaimBusinessRules = sp.GetRequiredService<IBaseCompanyRoleClaimBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.CompanyRoleClaimDto, DevFmMapProf.CompanyRoleClaimProfile> genericUserBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.CompanyRoleClaimDto, DevFmMapProf.CompanyRoleClaimProfile>>();
                                return new DiasFacilityRepository.CompanyRoleClaimDtoRepository(baseCompanyRoleClaimBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericUserBusinessRules);
                            });
                            services.AddScoped<ICompanyRoleUserDtoRepository, CompanyRoleUserDtoRepository>(sp =>
                            {
                                IBaseCompanyRoleUserBusinessRules baseCompanyRoleUserBusinessRules = sp.GetRequiredService<IBaseCompanyRoleUserBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.CompanyRoleUserDto, DevFmMapProf.CompanyRoleUserProfile> genericUserBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.CompanyRoleUserDto, DevFmMapProf.CompanyRoleUserProfile>>();
                                return new DiasFacilityRepository.CompanyRoleUserDtoRepository(baseCompanyRoleUserBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericUserBusinessRules);
                            });
                            services.AddScoped<IUserClaimDtoRepository, UserClaimDtoRepository>(sp =>
                            {
                                IBaseUserClaimBusinessRules baseUserClaimBusinessRules = sp.GetRequiredService<IBaseUserClaimBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserClaimDto, DevFmMapProf.UserClaimProfile> genericUserBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserClaimDto, DevFmMapProf.UserClaimProfile>>();
                                return new DiasFacilityRepository.UserClaimDtoRepository(baseUserClaimBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericUserBusinessRules);
                            });
                            services.AddScoped<IUserLoginDtoRepository, UserLoginDtoRepository>(sp =>
                            {
                                IBaseUserLoginBusinessRules baseUserLoginBusinessRules = sp.GetRequiredService<IBaseUserLoginBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserLoginDto, DevFmMapProf.UserLoginProfile> genericUserBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserLoginDto, DevFmMapProf.UserLoginProfile>>();
                                return new DiasFacilityRepository.UserLoginDtoRepository(baseUserLoginBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericUserBusinessRules);
                            });
                            services.AddScoped<ICompanyRoleDtoRepository, CompanyRoleDtoRepository>(sp =>
                            {
                                IBaseCompanyRoleBusinessRules baseCompanyRoleBusinessRules = sp.GetRequiredService<IBaseCompanyRoleBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.CompanyRoleDto, DevFmMapProf.CompanyRoleProfile> genericUserBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.CompanyRoleDto, DevFmMapProf.CompanyRoleProfile>>();
                                return new DiasFacilityRepository.CompanyRoleDtoRepository(baseCompanyRoleBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericUserBusinessRules);
                            });
                            services.AddScoped<IUserTokenDtoRepository, UserTokenDtoRepository>(sp =>
                            {
                                IBaseUserTokenBusinessRules baseUserTokenBusinessRules = sp.GetRequiredService<IBaseUserTokenBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserTokenDto, DevFmMapProf.UserTokenProfile> genericUserBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserTokenDto, DevFmMapProf.UserTokenProfile>>();
                                return new DiasFacilityRepository.UserTokenDtoRepository(baseUserTokenBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericUserBusinessRules);
                            });
                            services.AddScoped<DiasFacilityRepositoryInterface.IUserDtoRepository, DiasFacilityRepository.UserDtoRepository>(sp =>
                            {
                                IBaseUserBusinessRules baseUserBusinessRules = sp.GetRequiredService<IBaseUserBusinessRules>();
                                IBaseUserAssignmentGroupWrapperBusinessRules baseUserAssignmentGroupWrapperBusinessRules = sp.GetRequiredService<IBaseUserAssignmentGroupWrapperBusinessRules>();

                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserDto, DevFmMapProf.UserProfile> genericUserBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserDto, DevFmMapProf.UserProfile>>();

                                IDistributedCache redisCache = sp.GetRequiredService<IDistributedCache>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.UserDto, TestFmMapProf.UserProfile> genericUserBusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.UserDto, TestFmMapProf.UserProfile>>();

                                return new DiasFacilityRepository.UserDtoRepository(baseUserBusinessRules, baseUserAssignmentGroupWrapperBusinessRules,
                                                                                        ApplicationBusinessLogicEnvironment.Development, genericUserBusinessRules, null, redisCache);
                            });
                            services.AddScoped<IAssignmentGroupAuthorizedLocationDtoRepository, AssignmentGroupAuthorizedLocationDtoRepository>(sp =>
                            {
                                IBaseAssignmentGroupAuthorizedLocationBusinessRules baseAssignmentGroupAuthorizedLocationBusinessRules = sp.GetRequiredService<IBaseAssignmentGroupAuthorizedLocationBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.AssignmentGroupAuthorizedLocationDto, DevFmMapProf.AssignmentGroupAuthorizedLocationProfile> genericAssignmentGroupAuthorizedLocationBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.AssignmentGroupAuthorizedLocationDto, DevFmMapProf.AssignmentGroupAuthorizedLocationProfile>>();
                                return new AssignmentGroupAuthorizedLocationDtoRepository(baseAssignmentGroupAuthorizedLocationBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericAssignmentGroupAuthorizedLocationBusinessRules);
                            });
                            services.AddScoped<IAssignmentGroupDtoRepository, AssignmentGroupDtoRepository>(sp =>
                            {
                                IBaseAssignmentGroupBusinessRules baseAssignmentGroupBusinessRules = sp.GetRequiredService<IBaseAssignmentGroupBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.AssignmentGroupDto, DevFmMapProf.AssignmentGroupProfile> genericAssignmentGroupBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.AssignmentGroupDto, DevFmMapProf.AssignmentGroupProfile>>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.AssignmentGroupDto, TestFmMapProf.AssignmentGroupProfile> genericAssignmentGroupBusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.AssignmentGroupDto, TestFmMapProf.AssignmentGroupProfile>>();

                                return new AssignmentGroupDtoRepository(baseAssignmentGroupBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericAssignmentGroupBusinessRules, null);
                            });
                            services.AddScoped<IAssignmentGroupEmployeeDtoRepository, AssignmentGroupEmployeeDtoRepository>(sp =>
                            {
                                IBaseAssignmentGroupEmployeeBusinessRules baseAssignmentGroupEmployeeBusinessRules = sp.GetRequiredService<IBaseAssignmentGroupEmployeeBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.AssignmentGroupEmployeeDto, DevFmMapProf.AssignmentGroupEmployeeProfile> genericAssignmentGroupEmployeeBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.AssignmentGroupEmployeeDto, DevFmMapProf.AssignmentGroupEmployeeProfile>>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.AssignmentGroupEmployeeDto, TestFmMapProf.AssignmentGroupEmployeeProfile> genericAssignmentGroupEmployeeBusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.AssignmentGroupEmployeeDto, TestFmMapProf.AssignmentGroupEmployeeProfile>>();

                                return new AssignmentGroupEmployeeDtoRepository(baseAssignmentGroupEmployeeBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericAssignmentGroupEmployeeBusinessRules, null);
                            });
                            services.AddScoped<IAttachmentDtoRepository, AttachmentDtoRepository>(sp =>
                            {
                                IBaseAttachmentBusinessRules baseAttachmentBusinessRules = sp.GetRequiredService<IBaseAttachmentBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.AttachmentDto, DevFmMapProf.AttachmentProfile> genericAttachmentBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.AttachmentDto, DevFmMapProf.AttachmentProfile>>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.AttachmentDto, TestFmMapProf.AttachmentProfile> genericAttachmentBusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.AttachmentDto, TestFmMapProf.AttachmentProfile>>();

                                return new AttachmentDtoRepository(baseAttachmentBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericAttachmentBusinessRules, null);
                            });
                            services.AddScoped<IBasicTicketDtoRepository, BasicTicketDtoRepository>(sp =>
                            {
                                IBaseBasicTicketBusinessRules baseBasicTicketBusinessRules = sp.GetRequiredService<IBaseBasicTicketBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.BasicTicketDto, DevFmMapProf.BasicTicketProfile> genericBasicTicketBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.BasicTicketDto, DevFmMapProf.BasicTicketProfile>>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.BasicTicketDto, TestFmMapProf.BasicTicketProfile> genericBasicTicketBusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.BasicTicketDto, TestFmMapProf.BasicTicketProfile>>();

                                return new BasicTicketDtoRepository(baseBasicTicketBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericBasicTicketBusinessRules, null);
                            });
                            services.AddScoped<ILocationDtoRepository, LocationDtoRepository>(sp =>
                            {
                                IBaseLocationBusinessRules baseLocationBusinessRules = sp.GetRequiredService<IBaseLocationBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.LocationDto, DevFmMapProf.LocationProfile> genericLocationBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.LocationDto, DevFmMapProf.LocationProfile>>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.LocationDto, TestFmMapProf.LocationProfile> genericLocationBusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.LocationDto, TestFmMapProf.LocationProfile>>();

                                return new LocationDtoRepository(baseLocationBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericLocationBusinessRules, null);
                            });
                            services.AddScoped<ILocationV2DtoRepository, LocationV2DtoRepository>(sp =>
                            {
                                IBaseLocationV2BusinessRules baseLocationV2BusinessRules = sp.GetRequiredService<IBaseLocationV2BusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.LocationV2Dto, DevFmMapProf.LocationV2Profile> genericLocationV2BusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.LocationV2Dto, DevFmMapProf.LocationV2Profile>>();

                                IDistributedCache redisCache = sp.GetRequiredService<IDistributedCache>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.LocationV2Dto, TestFmMapProf.LocationV2Profile> genericLocationV2BusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.LocationV2Dto, TestFmMapProf.LocationV2Profile>>();

                                return new LocationV2DtoRepository(baseLocationV2BusinessRules, ApplicationBusinessLogicEnvironment.Development, genericLocationV2BusinessRules, null, redisCache);
                            });
                            services.AddScoped<IMenuPageV2DtoRepository, MenuPageV2DtoRepository>(sp =>
                            {
                                IBaseMenuPageV2BusinessRules baseMenuPageV2BusinessRules = sp.GetRequiredService<IBaseMenuPageV2BusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.MenuPageV2Dto, DevFmMapProf.MenuPageV2Profile> genericMenuPageV2BusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.MenuPageV2Dto, DevFmMapProf.MenuPageV2Profile>>();
                                return new MenuPageV2DtoRepository(baseMenuPageV2BusinessRules, ApplicationBusinessLogicEnvironment.Development, genericMenuPageV2BusinessRules);
                            });
                            services.AddScoped<IMenuPageDtoRepository, MenuPageDtoRepository>(sp =>
                            {
                                IBaseMenuPageBusinessRules baseMenuPageBusinessRules = sp.GetRequiredService<IBaseMenuPageBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.MenuPageDto, DevFmMapProf.MenuPageProfile> genericMenuPageBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.MenuPageDto, DevFmMapProf.MenuPageProfile>>();
                                return new MenuPageDtoRepository(baseMenuPageBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericMenuPageBusinessRules);
                            });
                            services.AddScoped<IPeriodicTicketDtoRepository, PeriodicTicketDtoRepository>(sp =>
                            {
                                IBasePeriodicTicketBusinessRules basePeriodicTicketBusinessRules = sp.GetRequiredService<IBasePeriodicTicketBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.PeriodicTicketDto, DevFmMapProf.PeriodicTicketProfile> genericPeriodicTicketBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.PeriodicTicketDto, DevFmMapProf.PeriodicTicketProfile>>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.PeriodicTicketDto, TestFmMapProf.PeriodicTicketProfile> genericPeriodicTicketBusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.PeriodicTicketDto, TestFmMapProf.PeriodicTicketProfile>>();

                                return new PeriodicTicketDtoRepository(basePeriodicTicketBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericPeriodicTicketBusinessRules, null);
                            });
                            services.AddScoped<IResolutionFormAnswerDtoRepository, ResolutionFormAnswerDtoRepository>(sp =>
                            {
                                IBaseResolutionFormAnswerBusinessRules baseResolutionFormAnswerBusinessRules = sp.GetRequiredService<IBaseResolutionFormAnswerBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormAnswerDto, DevFmMapProf.ResolutionFormAnswerProfile> genericResolutionFormAnswerBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormAnswerDto, DevFmMapProf.ResolutionFormAnswerProfile>>();
                                return new ResolutionFormAnswerDtoRepository(baseResolutionFormAnswerBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericResolutionFormAnswerBusinessRules);
                            });
                            services.AddScoped<IResolutionFormChoiceOptionDtoRepository, ResolutionFormChoiceOptionDtoRepository>(sp =>
                            {
                                IBaseResolutionFormChoiceOptionBusinessRules baseResolutionFormChoiceOptionBusinessRules = sp.GetRequiredService<IBaseResolutionFormChoiceOptionBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormChoiceOptionDto, DevFmMapProf.ResolutionFormChoiceOptionProfile> genericResolutionFormChoiceOptionBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormChoiceOptionDto, DevFmMapProf.ResolutionFormChoiceOptionProfile>>();
                                return new ResolutionFormChoiceOptionDtoRepository(baseResolutionFormChoiceOptionBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericResolutionFormChoiceOptionBusinessRules);
                            });
                            services.AddScoped<IResolutionFormDtoRepository, ResolutionFormDtoRepository>(sp =>
                            {
                                IBaseResolutionFormBusinessRules baseResolutionFormBusinessRules = sp.GetRequiredService<IBaseResolutionFormBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormDto, DevFmMapProf.ResolutionFormProfile> genericResolutionFormBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormDto, DevFmMapProf.ResolutionFormProfile>>();
                                return new ResolutionFormDtoRepository(baseResolutionFormBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericResolutionFormBusinessRules);
                            });
                            services.AddScoped<IResolutionFormMultipleChoiceDtoRepository, ResolutionFormMultipleChoiceDtoRepository>(sp =>
                            {
                                IBaseResolutionFormMultipleChoiceBusinessRules baseResolutionFormMultipleChoiceBusinessRules = sp.GetRequiredService<IBaseResolutionFormMultipleChoiceBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormMultipleChoiceDto, DevFmMapProf.ResolutionFormMultipleChoiceProfile> genericResolutionFormMultipleChoiceBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormMultipleChoiceDto, DevFmMapProf.ResolutionFormMultipleChoiceProfile>>();
                                return new ResolutionFormMultipleChoiceDtoRepository(baseResolutionFormMultipleChoiceBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericResolutionFormMultipleChoiceBusinessRules);
                            });
                            services.AddScoped<IResolutionFormQuestionAnswerDtoRepository, ResolutionFormQuestionAnswerDtoRepository>(sp =>
                            {
                                IBaseResolutionFormQuestionAnswerBusinessRules baseResolutionFormQuestionAnswerBusinessRules = sp.GetRequiredService<IBaseResolutionFormQuestionAnswerBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormQuestionAnswerDto, DevFmMapProf.ResolutionFormQuestionAnswerProfile> genericResolutionFormQuestionAnswerBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormQuestionAnswerDto, DevFmMapProf.ResolutionFormQuestionAnswerProfile>>();
                                return new ResolutionFormQuestionAnswerDtoRepository(baseResolutionFormQuestionAnswerBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericResolutionFormQuestionAnswerBusinessRules);
                            });
                            services.AddScoped<IResolutionFormQuestionDtoRepository, ResolutionFormQuestionDtoRepository>(sp =>
                            {
                                IBaseResolutionFormQuestionBusinessRules baseResolutionFormQuestionBusinessRules = sp.GetRequiredService<IBaseResolutionFormQuestionBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormQuestionDto, DevFmMapProf.ResolutionFormQuestionProfile> genericResolutionFormQuestionBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormQuestionDto, DevFmMapProf.ResolutionFormQuestionProfile>>();
                                return new ResolutionFormQuestionDtoRepository(baseResolutionFormQuestionBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericResolutionFormQuestionBusinessRules);
                            });
                            services.AddScoped<IResolutionFormQuestionTypeDtoRepository, ResolutionFormQuestionTypeDtoRepository>(sp =>
                            {
                                IBaseResolutionFormQuestionTypeBusinessRules baseResolutionFormQuestionTypeBusinessRules = sp.GetRequiredService<IBaseResolutionFormQuestionTypeBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormQuestionTypeDto, DevFmMapProf.ResolutionFormQuestionTypeProfile> genericResolutionFormQuestionTypeBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormQuestionTypeDto, DevFmMapProf.ResolutionFormQuestionTypeProfile>>();
                                return new ResolutionFormQuestionTypeDtoRepository(baseResolutionFormQuestionTypeBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericResolutionFormQuestionTypeBusinessRules);
                            });
                            services.AddScoped<IResolutionFormSingleQuestionDtoRepository, ResolutionFormSingleQuestionDtoRepository>(sp =>
                            {
                                IBaseResolutionFormSingleQuestionBusinessRules baseResolutionFormSingleQuestionBusinessRules = sp.GetRequiredService<IBaseResolutionFormSingleQuestionBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormSingleQuestionDto, DevFmMapProf.ResolutionFormSingleQuestionProfile> genericResolutionFormSingleQuestionBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormSingleQuestionDto, DevFmMapProf.ResolutionFormSingleQuestionProfile>>();
                                return new ResolutionFormSingleQuestionDtoRepository(baseResolutionFormSingleQuestionBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericResolutionFormSingleQuestionBusinessRules);
                            });
                            services.AddScoped<IResolutionFormYesNoDtoRepository, ResolutionFormYesNoDtoRepository>(sp =>
                            {
                                IBaseResolutionFormYesNoBusinessRules baseResolutionFormYesNoBusinessRules = sp.GetRequiredService<IBaseResolutionFormYesNoBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormYesNoDto, DevFmMapProf.ResolutionFormYesNoProfile> genericResolutionFormYesNoBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ResolutionFormYesNoDto, DevFmMapProf.ResolutionFormYesNoProfile>>();
                                return new ResolutionFormYesNoDtoRepository(baseResolutionFormYesNoBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericResolutionFormYesNoBusinessRules);
                            });
                            services.AddScoped<ITicketAuditHistoryDtoRepository, TicketAuditHistoryDtoRepository>(sp =>
                            {
                                IBaseTicketAuditHistoryBusinessRules baseTicketAuditHistoryBusinessRules = sp.GetRequiredService<IBaseTicketAuditHistoryBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketAuditHistoryDto, DevFmMapProf.TicketAuditHistoryProfile> genericTicketAuditHistoryBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketAuditHistoryDto, DevFmMapProf.TicketAuditHistoryProfile>>();
                                return new TicketAuditHistoryDtoRepository(baseTicketAuditHistoryBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericTicketAuditHistoryBusinessRules);
                            });
                            services.AddScoped<ITicketDtoRepository, TicketDtoRepository>(sp =>
                            {
                                IBaseTicketBusinessRules baseTicketBusinessRules = sp.GetRequiredService<IBaseTicketBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketDto, DevFmMapProf.TicketProfile> genericTicketBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketDto, DevFmMapProf.TicketProfile>>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketDto, TestFmMapProf.TicketProfile> genericTicketBusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketDto, TestFmMapProf.TicketProfile>>();

                                return new TicketDtoRepository(baseTicketBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericTicketBusinessRules, null);
                            });
                            services.AddScoped<ITicketNoteDtoRepository, TicketNoteDtoRepository>(sp =>
                            {
                                IBaseTicketNoteBusinessRules baseTicketNoteBusinessRules = sp.GetRequiredService<IBaseTicketNoteBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketNoteDto, DevFmMapProf.TicketNoteProfile> genericTicketNoteBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketNoteDto, DevFmMapProf.TicketNoteProfile>>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketNoteDto, TestFmMapProf.TicketNoteProfile> genericTicketNoteBusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketNoteDto, TestFmMapProf.TicketNoteProfile>>();

                                return new TicketNoteDtoRepository(baseTicketNoteBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericTicketNoteBusinessRules, null);
                            });
                            services.AddScoped<ITicketReasonCategoryDtoRepository, TicketReasonCategoryDtoRepository>(sp =>
                            {
                                IBaseTicketReasonCategoryBusinessRules baseTicketReasonCategoryBusinessRules = sp.GetRequiredService<IBaseTicketReasonCategoryBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketReasonCategoryDto, DevFmMapProf.TicketReasonCategoryProfile> genericTicketReasonCategoryBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketReasonCategoryDto, DevFmMapProf.TicketReasonCategoryProfile>>();

                                IDistributedCache redisCache = sp.GetRequiredService<IDistributedCache>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketReasonCategoryDto, TestFmMapProf.TicketReasonCategoryProfile> genericTicketReasonCategoryBusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketReasonCategoryDto, TestFmMapProf.TicketReasonCategoryProfile>>();

                                return new TicketReasonCategoryDtoRepository(baseTicketReasonCategoryBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericTicketReasonCategoryBusinessRules, null, redisCache);
                            });
                            services.AddScoped<ITicketReasonDtoRepository, TicketReasonDtoRepository>(sp =>
                            {
                                IBaseTicketReasonBusinessRules baseTicketReasonBusinessRules = sp.GetRequiredService<IBaseTicketReasonBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketReasonDto, DevFmMapProf.TicketReasonProfile> genericTicketReasonBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketReasonDto, DevFmMapProf.TicketReasonProfile>>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketReasonDto, TestFmMapProf.TicketReasonProfile> genericTicketReasonBusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketReasonDto, TestFmMapProf.TicketReasonProfile>>();

                                return new TicketReasonDtoRepository(baseTicketReasonBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericTicketReasonBusinessRules, null);
                            });
                            services.AddScoped<ITicketRelatedLocationDtoRepository, TicketRelatedLocationDtoRepository>(sp =>
                            {
                                IBaseTicketRelatedLocationBusinessRules baseTicketRelatedLocationBusinessRules = sp.GetRequiredService<IBaseTicketRelatedLocationBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketRelatedLocationDto, DevFmMapProf.TicketRelatedLocationProfile> genericTicketRelatedLocationBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketRelatedLocationDto, DevFmMapProf.TicketRelatedLocationProfile>>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketRelatedLocationDto, TestFmMapProf.TicketRelatedLocationProfile> genericTicketRelatedLocationBusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketRelatedLocationDto, TestFmMapProf.TicketRelatedLocationProfile>>();

                                return new TicketRelatedLocationDtoRepository(baseTicketRelatedLocationBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericTicketRelatedLocationBusinessRules, null);
                            });
                            services.AddScoped<ITicketStateDtoRepository, TicketStateDtoRepository>(sp =>
                            {
                                IBaseTicketStateBusinessRules baseTicketStateBusinessRules = sp.GetRequiredService<IBaseTicketStateBusinessRules>();

                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketStateDto, DevFmMapProf.TicketStateProfile> genericTicketStateBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketStateDto, DevFmMapProf.TicketStateProfile>>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketStateDto, TestFmMapProf.TicketStateProfile> genericTicketBusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketStateDto, TestFmMapProf.TicketStateProfile>>();

                                return new TicketStateDtoRepository(baseTicketStateBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericTicketStateBusinessRules, null);
                            });
                            services.AddScoped<ITicketPriorityDtoRepository, TicketPriorityDtoRepository>(sp =>
                            {
                                IBaseTicketPriorityBusinessRules baseTicketPriorityBusinessRules = sp.GetRequiredService<IBaseTicketPriorityBusinessRules>();

                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketPriorityDto, DevFmMapProf.TicketPriorityProfile> genericTicketPriorityBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketPriorityDto, DevFmMapProf.TicketPriorityProfile>>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketPriorityDto, TestFmMapProf.TicketPriorityProfile> genericTicketBusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.TicketPriorityDto, TestFmMapProf.TicketPriorityProfile>>();

                                return new TicketPriorityDtoRepository(baseTicketPriorityBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericTicketPriorityBusinessRules, null);
                            });
                            services.AddScoped<IBasicTicketStateDtoRepository, BasicTicketStateDtoRepository>(sp =>
                            {
                                IBaseBasicTicketStateBusinessRules baseBasicTicketStateBusinessRules = sp.GetRequiredService<IBaseBasicTicketStateBusinessRules>();

                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.BasicTicketStateDto, DevFmMapProf.BasicTicketStateProfile> genericBasicTicketStateBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.BasicTicketStateDto, DevFmMapProf.BasicTicketStateProfile>>();

                                //TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.BasicTicketStateDto, TestFmMapProf.BasicTicketStateProfile> genericBasicTicketStateBusinessRulesTest =
                                //    sp.GetRequiredService<TestFmBlInterface.IGenericStandartBusinessRules<TestFmStanDto.BasicTicketStateDto, TestFmMapProf.BasicTicketStateProfile>>();

                                return new BasicTicketStateDtoRepository(baseBasicTicketStateBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericBasicTicketStateBusinessRules, null);
                            });
                            services.AddScoped<ITicketStateTransitionDtoRepository, TicketStateTransitionDtoRepository>(sp =>
                            {
                                IBaseTicketStateTransitionBusinessRules baseTicketStateTransitionBusinessRules = sp.GetRequiredService<IBaseTicketStateTransitionBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketStateTransitionDto, DevFmMapProf.TicketStateTransitionProfile> genericTicketStateTransitionBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.TicketStateTransitionDto, DevFmMapProf.TicketStateTransitionProfile>>();
                                return new TicketStateTransitionDtoRepository(baseTicketStateTransitionBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericTicketStateTransitionBusinessRules);
                            });
                            services.AddScoped<IUserMenuPageDtoRepository, UserMenuPageDtoRepository>(sp =>
                            {
                                IBaseUserMenuPageBusinessRules baseUserMenuPageBusinessRules = sp.GetRequiredService<IBaseUserMenuPageBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserMenuPageDto, DevFmMapProf.UserMenuPageProfile> genericUserMenuPageBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.UserMenuPageDto, DevFmMapProf.UserMenuPageProfile>>();
                                return new UserMenuPageDtoRepository(baseUserMenuPageBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericUserMenuPageBusinessRules);
                            });
                            services.AddScoped<IViewAssigmentGroupEmployeeDtoRepository, ViewAssigmentGroupEmployeeDtoRepository>(sp =>
                            {
                                IBaseViewAssigmentGroupEmployeeBusinessRules baseViewAssigmentGroupEmployeeBusinessRules = sp.GetRequiredService<IBaseViewAssigmentGroupEmployeeBusinessRules>();
                                return new ViewAssigmentGroupEmployeeDtoRepository(baseViewAssigmentGroupEmployeeBusinessRules, ApplicationBusinessLogicEnvironment.Development);
                            });
                            services.AddScoped<IViewReasonCategoryDtoRepository, ViewReasonCategoryDtoRepository>(sp =>
                            {
                                IBaseViewReasonCategoryBusinessRules baseViewReasonCategoryBusinessRules = sp.GetRequiredService<IBaseViewReasonCategoryBusinessRules>();
                                return new ViewReasonCategoryDtoRepository(baseViewReasonCategoryBusinessRules, ApplicationBusinessLogicEnvironment.Development);
                            });
                            services.AddScoped<IViewTicketDtoRepository, ViewTicketDtoRepository>(sp =>
                            {
                                IBaseViewTicketBusinessRules baseViewTicketBusinessRules = sp.GetRequiredService<IBaseViewTicketBusinessRules>();
                                return new ViewTicketDtoRepository(baseViewTicketBusinessRules, ApplicationBusinessLogicEnvironment.Development);
                            });
                            services.AddScoped<IViewTicketFormQDtoRepository, ViewTicketFormQDtoRepository>(sp =>
                            {
                                IBaseViewTicketFormQBusinessRules baseViewTicketFormQBusinessRules = sp.GetRequiredService<IBaseViewTicketFormQBusinessRules>();
                                return new ViewTicketFormQDtoRepository(baseViewTicketFormQBusinessRules, ApplicationBusinessLogicEnvironment.Development);
                            });
                            services.AddScoped<IViewTicketLocationDtoRepository, ViewTicketLocationDtoRepository>(sp =>
                            {
                                IBaseViewTicketLocationBusinessRules baseViewTicketLocationBusinessRules = sp.GetRequiredService<IBaseViewTicketLocationBusinessRules>();
                                return new ViewTicketLocationDtoRepository(baseViewTicketLocationBusinessRules, ApplicationBusinessLogicEnvironment.Development);
                            });
                            services.AddScoped<IViewTicketNoteDtoRepository, ViewTicketNoteDtoRepository>(sp =>
                            {
                                IBaseViewTicketNoteBusinessRules baseViewTicketNoteBusinessRules = sp.GetRequiredService<IBaseViewTicketNoteBusinessRules>();
                                return new ViewTicketNoteDtoRepository(baseViewTicketNoteBusinessRules, ApplicationBusinessLogicEnvironment.Development);
                            });
                            services.AddScoped<IViewTicketStateLevelDtoRepository, ViewTicketStateLevelDtoRepository>(sp =>
                            {
                                IBaseViewTicketStateLevelBusinessRules baseViewTicketStateLevelBusinessRules = sp.GetRequiredService<IBaseViewTicketStateLevelBusinessRules>();
                                return new ViewTicketStateLevelDtoRepository(baseViewTicketStateLevelBusinessRules, ApplicationBusinessLogicEnvironment.Development);
                            });
                            services.AddScoped<IViewUserPageDtoRepository, ViewUserPageDtoRepository>(sp =>
                            {
                                IBaseViewUserPageBusinessRules baseViewUserPageBusinessRules = sp.GetRequiredService<IBaseViewUserPageBusinessRules>();
                                return new ViewUserPageDtoRepository(baseViewUserPageBusinessRules, ApplicationBusinessLogicEnvironment.Development);
                            });
                            services.AddScoped<IWorkShiftDtoRepository, WorkShiftDtoRepository>(sp =>
                            {
                                IBaseWorkShiftBusinessRules baseWorkShiftBusinessRules = sp.GetRequiredService<IBaseWorkShiftBusinessRules>();
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.WorkShiftDto, DevFmMapProf.WorkShiftProfile> genericWorkShiftBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.WorkShiftDto, DevFmMapProf.WorkShiftProfile>>();
                                return new WorkShiftDtoRepository(baseWorkShiftBusinessRules, ApplicationBusinessLogicEnvironment.Development, genericWorkShiftBusinessRules);
                            });
                            services.AddScoped<IApiActionDescriptionDtoRepository, ApiActionDescriptionDtoRepository>(sp =>
                            {
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ApiActionDescriptionDto, DevFmMapProf.ApiActionDescriptionProfile> genericApiActionDescriptionBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ApiActionDescriptionDto, DevFmMapProf.ApiActionDescriptionProfile>>();
                                return new ApiActionDescriptionDtoRepository(ApplicationBusinessLogicEnvironment.Development, genericApiActionDescriptionBusinessRules);
                            });
                            services.AddScoped<IApiControllerDescriptionDtoRepository, ApiControllerDescriptionDtoRepository>(sp =>
                            {
                                DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ApiControllerDescriptionDto, DevFmMapProf.ApiControllerDescriptionProfile> genericApiActionDescriptionBusinessRules =
                                    sp.GetRequiredService<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.ApiControllerDescriptionDto, DevFmMapProf.ApiControllerDescriptionProfile>>();
                                return new ApiControllerDescriptionDtoRepository(ApplicationBusinessLogicEnvironment.Development, genericApiActionDescriptionBusinessRules);
                            });
                            services.AddScoped<IAuthenticationTestRepository, AuthenticationTestRepository>(sp =>
                            {
                                IBaseCompanyRoleClaimBusinessRules baseCompanyRoleClaimBusinessRules = sp.GetRequiredService<IBaseCompanyRoleClaimBusinessRules>();
                                return new DiasFacilityRepository.AuthenticationTestRepository(baseCompanyRoleClaimBusinessRules, ApplicationBusinessLogicEnvironment.Development);
                            });


                            #endregion Standart

                            #region Custom

                            services.AddScoped<ITicketWrapperDtoRepository, TicketWrapperDtoRepository>(sp =>
                            {
                                IBaseTicketWrapperBusinessRules baseTicketWrapperBusinessRules = sp.GetRequiredService<IBaseTicketWrapperBusinessRules>();
                                IBaseTicketWrapperTransactionalBusinessRules baseTicketWrapperTransactionalBusinessRules = sp.GetRequiredService<IBaseTicketWrapperTransactionalBusinessRules>();
                                return new TicketWrapperDtoRepository(baseTicketWrapperBusinessRules, baseTicketWrapperTransactionalBusinessRules, ApplicationBusinessLogicEnvironment.Development);
                            });
                            services.AddScoped<ITicketNoteWrapperDtoRepository, TicketNoteWrapperDtoRepository>(sp =>
                            {
                                IBaseTicketNoteWrapperBusinessRules baseTicketNoteWrapperBusinessRules = sp.GetRequiredService<IBaseTicketNoteWrapperBusinessRules>();
                                IBaseTicketNoteWrapperTransactionalBusinessRules baseTicketNoteWrapperTransactionalBusinessRules = sp.GetRequiredService<IBaseTicketNoteWrapperTransactionalBusinessRules>();
                                return new TicketNoteWrapperDtoRepository(baseTicketNoteWrapperBusinessRules, baseTicketNoteWrapperTransactionalBusinessRules, ApplicationBusinessLogicEnvironment.Development);
                            });
                            services.AddScoped<IAttachmentWrapperDtoRepository, AttachmentWrapperDtoRepository>(sp =>
                            {
                                IBaseAttachmentWrapperBusinessRules baseAttachmentWrapperBusinessRules = sp.GetRequiredService<IBaseAttachmentWrapperBusinessRules>();
                                IBaseAttachmentWrapperTransactionalBusinessRules baseAttachmentWrapperTransactionalBusinessRules = sp.GetRequiredService<IBaseAttachmentWrapperTransactionalBusinessRules>();
                                return new AttachmentWrapperDtoRepository(baseAttachmentWrapperBusinessRules, baseAttachmentWrapperTransactionalBusinessRules, ApplicationBusinessLogicEnvironment.Development);
                            });
                            services.AddScoped<IBasicTicketWrapperDtoRepository, BasicTicketWrapperDtoRepository>(sp =>
                            {
                                IBaseBasicTicketWrapperBusinessRules baseBasicTicketWrapperBusinessRules = sp.GetRequiredService<IBaseBasicTicketWrapperBusinessRules>();
                                IBaseBasicTicketWrapperTransactionalBusinessRules baseBasicTicketWrapperTransactionalBusinessRules = sp.GetRequiredService<IBaseBasicTicketWrapperTransactionalBusinessRules>();
                                return new BasicTicketWrapperDtoRepository(baseBasicTicketWrapperBusinessRules, baseBasicTicketWrapperTransactionalBusinessRules, ApplicationBusinessLogicEnvironment.Development);
                            });
                            services.AddScoped<IPeriodicTicketWrapperDtoRepository, PeriodicTicketWrapperDtoRepository>(sp =>
                            {
                                IBasePeriodicTicketWrapperBusinessRules basePeriodicTicketWrapperBusinessRules = sp.GetRequiredService<IBasePeriodicTicketWrapperBusinessRules>();
                                IBasePeriodicTicketWrapperTransactionalBusinessRules basePeriodicTicketWrapperTransactionalBusinessRules = sp.GetRequiredService<IBasePeriodicTicketWrapperTransactionalBusinessRules>();
                                return new PeriodicTicketWrapperDtoRepository(basePeriodicTicketWrapperBusinessRules, basePeriodicTicketWrapperTransactionalBusinessRules, ApplicationBusinessLogicEnvironment.Development);
                            });
                            services.AddScoped<ILocationWrapperDtoRepository, LocationWrapperDtoRepository>(sp =>
                            {
                                IBaseLocationWrapperBusinessRules baseLocationWrapperBusinessRules = sp.GetRequiredService<IBaseLocationWrapperBusinessRules>();
                                IDistributedCache redisCache = sp.GetRequiredService<IDistributedCache>();
                                return new LocationWrapperDtoRepository(baseLocationWrapperBusinessRules, ApplicationBusinessLogicEnvironment.Development, redisCache);
                            });
                            services.AddScoped<ITicketReasonCategoryWrapperDtoRepository, TicketReasonCategoryWrapperDtoRepository>(sp =>
                            {
                                IBaseTicketReasonCategoryWrapperBusinessRules baseTicketReasonCategoryWrapperBusinessRules = sp.GetRequiredService<IBaseTicketReasonCategoryWrapperBusinessRules>();
                                IDistributedCache redisCache = sp.GetRequiredService<IDistributedCache>();

                                return new TicketReasonCategoryWrapperDtoRepository(baseTicketReasonCategoryWrapperBusinessRules, ApplicationBusinessLogicEnvironment.Development, redisCache);
                            });
                            services.AddScoped<ITicketStateFlowWrapperDtoRepository, TicketStateFlowWrapperDtoRepository>(sp =>
                            {
                                IBaseTicketStateFlowWrapperBusinessRules baseTicketStateFlowWrapperBusinessRules = sp.GetRequiredService<IBaseTicketStateFlowWrapperBusinessRules>();
                                return new TicketStateFlowWrapperDtoRepository(baseTicketStateFlowWrapperBusinessRules, ApplicationBusinessLogicEnvironment.Development);
                            });

                            #endregion Custom

                            #endregion FacilityManager

                            #endregion Repository
                        }
                        catch (InvalidOperationException)
                        {
                            throw;
                        }

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
