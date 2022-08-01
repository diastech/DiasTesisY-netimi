using DevBL_Sql = DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart;
using TestBL_Sql = DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Test.Standart;
using DiasBusinessLogic.Data.EF_Data.UnitOfWork;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.Configuration;
using DevBL_InterfaceSql = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using TestBL_InterfaceSql = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using DevBL_InterfaceAzure =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.AzureService.Development.Standart;
using DevBL_Azure =  DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.AzureService.StorageBlob.Development.Standart;
using DevBL_InterfaceSqlCustom =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using TestBL_InterfaceSqlCustom = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom;
using DevBL_SqlCustom =  DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using TestBL_SqlCustom = DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom;
using DevFmBlInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DevFmStanDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevFmMapProf = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using GenFMDev =  DiasBusinessLogic.Shared.GenericMethods.DiasFacilityManagementSqlServer.Standart.Development;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration
{
    public class DI_ServiceLocator
    {
        private static readonly IDI_ServiceLocator di_serviceLocator;

        static DI_ServiceLocator()
        {

            di_serviceLocator = new DefaultServiceLocator_Live();
        }


        public static IDI_ServiceLocator Current
        {
            get
            {
                return di_serviceLocator;
            }
        }

        private sealed class DefaultServiceLocator_Live : IDI_ServiceLocator
        {
            private readonly Container container;

            public DefaultServiceLocator_Live()
            {
                container = new();
                container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
                container.Options.AllowOverridingRegistrations = true;

                LoadBindings();
            }

            public T Get<T>() where T : class
            {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    if (typeof(T).IsInterface)
                    {
                        return container.GetInstance<T>();
                    }
                    else
                    {
                        return null;
                    }
                }

            }

            /// <summary>
            ///İş kuralı constructorları di_serviceLocator istediğinden
            ///Container içinde yer alan iş kurallarına UoW inject edildikten sonra kullanılmalıdır
            ///Öncesi bir kullanım null reference exception çıkartır
            /// </summary>            
            public void VerifyContainer()
            {
                container.Verify();
            }

            private void LoadInstanceBindings()
            {
                //Global
                container.Register<IUnitOfWork_EF, UnitOfWork_EF>(Lifestyle.Scoped);

                //İş kurallarına özel

                //generic iş kuralı interfaceleri
                //DiasFacilityManagement
                //Sql Server
                //development
                container.Register<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.CompanyRoleClaimDto, DevFmMapProf.CompanyRoleClaimProfile>,
                                    GenFMDev.GenericStandartBusinessRules<DevFmStanDto.CompanyRoleClaimDto, DevFmMapProf.CompanyRoleClaimProfile>>(Lifestyle.Scoped);

                container.Register<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.CompanyRoleUserDto, DevFmMapProf.CompanyRoleUserProfile>,
                    GenFMDev.GenericStandartBusinessRules<DevFmStanDto.CompanyRoleUserDto, DevFmMapProf.CompanyRoleUserProfile>>(Lifestyle.Scoped);

                container.Register<DevFmBlInterface.IGenericStandartBusinessRules<DevFmStanDto.LocationCodeDto, DevFmMapProf.LocationCodeProfile>,
                   GenFMDev.GenericStandartBusinessRules<DevFmStanDto.LocationCodeDto, DevFmMapProf.LocationCodeProfile>>(Lifestyle.Scoped);


                //standart iş kuralı interfaceleri

                //DiasFacilityManagement
                //Sql Server
                //development
                container.Register<DevBL_InterfaceSql.IUserBusinessRules, DevBL_Sql.UserBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IAssignmentGroupBusinessRules, DevBL_Sql.AssignmentGroupBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IAssignmentGroupAuthorizedLocationBusinessRules, DevBL_Sql.AssignmentGroupAuthorizedLocationBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IAssignmentGroupEmployeeBusinessRules, DevBL_Sql.AssignmentGroupEmployeeBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IAttachmentBusinessRules, DevBL_Sql.AttachmentBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ITicketStateFlowBusinessRules, DevBL_Sql.TicketStateFlowBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IBasicTicketBusinessRules, DevBL_Sql.BasicTicketBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ILocationBusinessRules, DevBL_Sql.LocationBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ILocationV2BusinessRules, DevBL_Sql.LocationV2BusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IMenuPageBusinessRules, DevBL_Sql.MenuPageBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IPeriodicTicketBusinessRules, DevBL_Sql.PeriodicTicketBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IResolutionFormBusinessRules, DevBL_Sql.ResolutionFormBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IResolutionFormAnswerBusinessRules, DevBL_Sql.ResolutionFormAnswerBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IResolutionFormChoiceOptionBusinessRules, DevBL_Sql.ResolutionFormChoiceOptionBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IResolutionFormMultipleChoiceBusinessRules, DevBL_Sql.ResolutionFormMultipleChoiceBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IResolutionFormQuestionAnswerBusinessRules, DevBL_Sql.ResolutionFormQuestionAnswerBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IResolutionFormQuestionBusinessRules, DevBL_Sql.ResolutionFormQuestionBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IResolutionFormQuestionTypeBusinessRules, DevBL_Sql.ResolutionFormQuestionTypeBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IResolutionFormSingleQuestionBusinessRules, DevBL_Sql.ResolutionFormSingleQuestionBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IResolutionFormYesNoBusinessRules, DevBL_Sql.ResolutionFormYesNoBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ITicketAuditHistoryBusinessRules, DevBL_Sql.TicketAuditHistoryBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ITicketBusinessRules, DevBL_Sql.TicketBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ITicketNoteBusinessRules, DevBL_Sql.TicketNoteBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ITicketReasonBusinessRules, DevBL_Sql.TicketReasonBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ITicketReasonCategoryBusinessRules, DevBL_Sql.TicketReasonCategoryBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ITicketReasonCategoryV2BusinessRules, DevBL_Sql.TicketReasonCategoryV2BusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ITicketRelatedLocationBusinessRules, DevBL_Sql.TicketRelatedLocationBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ITicketStateBusinessRules, DevBL_Sql.TicketStateBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ITicketPriorityBusinessRules, DevBL_Sql.TicketPriorityBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IBasicTicketStateBusinessRules, DevBL_Sql.BasicTicketStateBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ITicketStateTransitionBusinessRules, DevBL_Sql.TicketStateTransitionBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IUserMenuPageBusinessRules, DevBL_Sql.UserMenuPageBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IViewAssigmentGroupEmployeeBusinessRules, DevBL_Sql.ViewAssigmentGroupEmployeeBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IViewReasonCategoryBusinessRules, DevBL_Sql.ViewReasonCategoryBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IViewTicketBusinessRules, DevBL_Sql.ViewTicketBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IViewTicketFormQBusinessRules, DevBL_Sql.ViewTicketFormQBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IViewTicketLocationBusinessRules, DevBL_Sql.ViewTicketLocationBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IViewTicketNoteBusinessRules, DevBL_Sql.ViewTicketNoteBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IViewTicketStateLevelBusinessRules, DevBL_Sql.ViewTicketStateLevelBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IViewUserPageBusinessRules, DevBL_Sql.ViewUserPageBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IWorkShiftBusinessRules, DevBL_Sql.WorkShiftBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ICompanyRoleClaimBusinessRules, DevBL_Sql.CompanyRoleClaimBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ICompanyRoleBusinessRules, DevBL_Sql.CompanyRoleBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IUserClaimBusinessRules, DevBL_Sql.UserClaimBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IUserLoginBusinessRules, DevBL_Sql.UserLoginBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.ICompanyRoleUserBusinessRules, DevBL_Sql.CompanyRoleUserBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSql.IUserTokenBusinessRules, DevBL_Sql.UserTokenBusinessRules>(Lifestyle.Scoped);

                //

                //test
                container.Register<TestBL_InterfaceSql.IUserBusinessRules, TestBL_Sql.UserBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IAssignmentGroupBusinessRules, TestBL_Sql.AssignmentGroupBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IAssignmentGroupAuthorizedLocationBusinessRules, TestBL_Sql.AssignmentGroupAuthorizedLocationBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IAssignmentGroupEmployeeBusinessRules, TestBL_Sql.AssignmentGroupEmployeeBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IAttachmentBusinessRules, TestBL_Sql.AttachmentBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IBasicTicketBusinessRules, TestBL_Sql.BasicTicketBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.ILocationBusinessRules, TestBL_Sql.LocationBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.ILocationV2BusinessRules, TestBL_Sql.LocationV2BusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IMenuPageBusinessRules, TestBL_Sql.MenuPageBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IPeriodicTicketBusinessRules, TestBL_Sql.PeriodicTicketBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IResolutionFormBusinessRules, TestBL_Sql.ResolutionFormBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IResolutionFormAnswerBusinessRules, TestBL_Sql.ResolutionFormAnswerBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IResolutionFormChoiceOptionBusinessRules, TestBL_Sql.ResolutionFormChoiceOptionBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IResolutionFormMultipleChoiceBusinessRules, TestBL_Sql.ResolutionFormMultipleChoiceBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IResolutionFormQuestionAnswerBusinessRules, TestBL_Sql.ResolutionFormQuestionAnswerBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IResolutionFormQuestionBusinessRules, TestBL_Sql.ResolutionFormQuestionBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IResolutionFormQuestionTypeBusinessRules, TestBL_Sql.ResolutionFormQuestionTypeBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IResolutionFormSingleQuestionBusinessRules, TestBL_Sql.ResolutionFormSingleQuestionBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IResolutionFormYesNoBusinessRules, TestBL_Sql.ResolutionFormYesNoBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.ITicketAuditHistoryBusinessRules, TestBL_Sql.TicketAuditHistoryBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.ITicketBusinessRules, TestBL_Sql.TicketBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.ITicketNoteBusinessRules, TestBL_Sql.TicketNoteBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.ITicketReasonBusinessRules, TestBL_Sql.TicketReasonBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.ITicketReasonCategoryBusinessRules, TestBL_Sql.TicketReasonCategoryBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.ITicketReasonCategoryV2BusinessRules, TestBL_Sql.TicketReasonCategoryV2BusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.ITicketRelatedLocationBusinessRules, TestBL_Sql.TicketRelatedLocationBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.ITicketStateBusinessRules, TestBL_Sql.TicketStateBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.ITicketPriorityBusinessRules, TestBL_Sql.TicketPriorityBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IBasicTicketStateBusinessRules, TestBL_Sql.BasicTicketStateBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.ITicketStateTransitionBusinessRules, TestBL_Sql.TicketStateTransitionBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IUserMenuPageBusinessRules, TestBL_Sql.UserMenuPageBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IViewAssigmentGroupEmployeeBusinessRules, TestBL_Sql.ViewAssigmentGroupEmployeeBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IViewReasonCategoryBusinessRules, TestBL_Sql.ViewReasonCategoryBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IViewTicketBusinessRules, TestBL_Sql.ViewTicketBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IViewTicketFormQBusinessRules, TestBL_Sql.ViewTicketFormQBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IViewTicketLocationBusinessRules, TestBL_Sql.ViewTicketLocationBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IViewTicketNoteBusinessRules, TestBL_Sql.ViewTicketNoteBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IViewTicketStateLevelBusinessRules, TestBL_Sql.ViewTicketStateLevelBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IViewUserPageBusinessRules, TestBL_Sql.ViewUserPageBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSql.IWorkShiftBusinessRules, TestBL_Sql.WorkShiftBusinessRules>(Lifestyle.Scoped);



                //
                //
                //

                //custom iş kuralı interfaceleri

                //DiasFacilityManagement
                //Sql Server
                //development
                container.Register<DevBL_InterfaceSqlCustom.IUserTransactionalBusinessRules, DevBL_SqlCustom.UserTransactionalBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSqlCustom.ITicketWrapperBusinessRules, DevBL_SqlCustom.TicketWrapperBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSqlCustom.ITicketNoteWrapperBusinessRules, DevBL_SqlCustom.TicketNoteWrapperBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSqlCustom.IAttachmentWrapperBusinessRules, DevBL_SqlCustom.AttachmentWrapperBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSqlCustom.IUserAssignmentGroupWrapperBusinessRules, DevBL_SqlCustom.UserAssignmentGroupWrapperBusinessRules>(Lifestyle.Scoped);


                container.Register<DevBL_InterfaceSqlCustom.ITicketStateFlowWrapperBusinessRules, DevBL_SqlCustom.TicketStateFlowWrapperBusinessRules>(Lifestyle.Scoped);

                container.Register<DevBL_InterfaceSqlCustom.IBasicTicketWrapperBusinessRules, DevBL_SqlCustom.BasicTicketWrapperBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSqlCustom.IPeriodicTicketWrapperBusinessRules, DevBL_SqlCustom.PeriodicTicketWrapperBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSqlCustom.ILocationWrapperBusinessRules, DevBL_SqlCustom.LocationWrapperBusinessRules>(Lifestyle.Scoped);
                container.Register<DevBL_InterfaceSqlCustom.ITicketReasonCategoryWrapperBusinessRules, DevBL_SqlCustom.TicketReasonCategoryWrapperBusinessRules>(Lifestyle.Scoped);
                //test
                container.Register<TestBL_InterfaceSqlCustom.IUserTransactionalBusinessRules, TestBL_SqlCustom.UserTransactionalBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSqlCustom.ITicketWrapperBusinessRules, TestBL_SqlCustom.TicketWrapperBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSqlCustom.IBasicTicketWrapperBusinessRules, TestBL_SqlCustom.BasicTicketWrapperBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSqlCustom.IPeriodicTicketWrapperBusinessRules, TestBL_SqlCustom.PeriodicTicketWrapperBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSqlCustom.ILocationWrapperBusinessRules, TestBL_SqlCustom.LocationWrapperBusinessRules>(Lifestyle.Scoped);
                container.Register<TestBL_InterfaceSqlCustom.ITicketReasonCategoryWrapperBusinessRules, TestBL_SqlCustom.TicketReasonCategoryWrapperBusinessRules>(Lifestyle.Scoped);
                //
                //                

                //cloud iş kuralları
                //Azure Storage
                container.Register<DevBL_InterfaceAzure.IAzureStorageUserBusinessRules, DevBL_Azure.AzureStorageUserBusinessRules>(Lifestyle.Scoped);

                //
                //
            }

            private void LoadSingletonBindings()
            {
                
            }

            private void LoadBindings()
            {
                LoadInstanceBindings();
                LoadSingletonBindings();

            }
        }
    }
}
