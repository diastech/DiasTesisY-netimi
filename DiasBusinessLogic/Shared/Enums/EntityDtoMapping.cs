using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiasBusinessLogic.Shared.Enums
{
    public class EntityDtoMapping
    {
        /// <summary>
        /// Display -> Entity, Description -> Dto
        /// </summary>
        public enum EntityDtoMap
        {
            //Son değer 57
            #region DiasFacilityManagement            

                #region General

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.AttachmentDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.Attachment")]
                DiasFacilityManagementAttachment = 1,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.LocationDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.Location")]
                DiasFacilityManagementLocation = 2,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.LocationV2Dto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.LocationV2")]
                DiasFacilityManagementLocationV2 = 3,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.ResolutionFormAnswerDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.ResolutionFormAnswer")]
                DiasFacilityManagementResolutionFormAnswer = 4,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.ResolutionFormChoiceOptionDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.ResolutionFormChoiceOption")]
                DiasFacilityManagementResolutionFormChoiceOption = 5,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.ResolutionFormDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.ResolutionForm")]
                DiasFacilityManagementResolutionForm = 6,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.ResolutionFormMultipleChoiceDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.ResolutionFormMultipleChoice")]
                DiasFacilityManagementResolutionFormMultipleChoice = 7,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.ResolutionFormQuestionAnswerDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.ResolutionFormQuestionAnswer")]
                DiasFacilityManagementResolutionFormQuestionAnswer = 8,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.ResolutionFormQuestionDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.ResolutionFormQuestion")]
                DiasFacilityManagementResolutionFormQuestion = 9,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.ResolutionFormQuestionTypeDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.ResolutionFormQuestionType")]
                DiasFacilityManagementResolutionFormQuestionType = 10,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.ResolutionFormSingleQuestionDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.ResolutionFormSingleQuestion")]
                DiasFacilityManagementResolutionFormSingleQuestion = 11,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.ResolutionFormV2Dto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.ResolutionFormV2")]
                DiasFacilityManagementResolutionFormV2 = 12,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.ResolutionFormYesNoDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.ResolutionFormYesNo")]
                DiasFacilityManagementResolutionFormYesNo = 13,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.WorkShiftDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.WorkShift")]
                DiasFacilityManagementWorkShift = 14,

            #endregion General

                //bu kesimde namespaceler DiasFacilityManagement den farklı olabilir
                #region Identity

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.ApiActionDescriptionDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.ApiActionDescription")]
                DiasFacilityManagementApiActionDescription = 15,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.ApiControllerDescriptionDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.ApiControllerDescription")]
                DiasFacilityManagementApiControllerDescription = 16,

            
                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.CompanyRoleClaimDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models.CompanyRoleClaim")]
                DiasFacilityManagementCompanyRoleClaim = 17,


                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.CompanyRoleDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models.CompanyRole")]
                DiasFacilityManagementCompanyRole = 18,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.CompanyRoleUserDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models.CompanyRoleUser")]
                DiasFacilityManagementCompanyRoleUser = 19,

                //TODO : RefreshToken veritabanında tanımlı olunca buraya eklenecek

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.TicketStateFlowRoleDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.TicketStateFlowRole")]
                DiasFacilityManagementTicketStateFlowRole = 20,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.UserClaimDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models.UserClaim")]
                DiasFacilityManagementUserClaim = 22,


                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.UserDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models.User")]
                DiasFacilityManagementUser = 23,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.UserLoginDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models.UserLogin")]
                DiasFacilityManagementUserLogin = 24,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.UserMenuPageDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.UserMenuPage")]
                DiasFacilityManagementTicketStateRoleUserMenuPage = 25,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.UserTokenDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models.UserToken")]
                DiasFacilityManagementUserToken = 26,

                #endregion Identity

                #region Parameter

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.BasicTicketStateDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.BasicTicketState")]
                DiasFacilityManagementBasicTicketState = 27,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.MenuPageDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.MenuPage")]
                DiasFacilityManagementMenuPage = 28,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.MenuPageV2Dto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.MenuPageV2")]
                DiasFacilityManagementMenuPageV2 = 29,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.TicketPriorityDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.TicketPriority")]
                DiasFacilityManagementTicketPriority = 30,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.TicketStateDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.TicketState")]
                DiasFacilityManagementTicketState = 31,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.TicketStateFlowDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.TicketStateFlow")]
                DiasFacilityManagementTicketStateFlow = 32,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.TicketStateRoleDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.TicketStateRole")]
                DiasFacilityManagementTicketStateRole = 33,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.FacilityDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.Facility")]
                DiasFacilityManagementFacility = 57,



                #endregion Parameter

                #region WorkOrder

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.AssignmentGroupAuthorizedLocationDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.AssignmentGroupAuthorizedLocation")]
                DiasFacilityManagementAssignmentGroupAuthorizedLocation = 34,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.AssignmentGroupDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.AssignmentGroup")]
                DiasFacilityManagementAssignmentGroup = 35,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.AssignmentGroupEmployeeDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.AssignmentGroupEmployee")]
                DiasFacilityManagementAssignmentGroupEmployee = 36,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.BasicTicketDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.BasicTicket")]
                DiasFacilityManagementBasicTicket = 37,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.PeriodicTicketDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.PeriodicTicket")]
                DiasFacilityManagementPeriodicTicket = 38,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.TicketAuditHistoryDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.TicketAuditHistory")]
                DiasFacilityManagementTicketAuditHistory = 39,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.TicketDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.Ticket")]
                DiasFacilityManagementTicket = 40,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.TicketNoteDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.TicketNote")]
                DiasFacilityManagementTicketNote = 41,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.TicketReasonCategoryDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.TicketReasonCategory")]
                DiasFacilityManagementTicketReasonCategory = 42,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.TicketReasonCategoryV2Dto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.TicketReasonCategoryV2")]
                DiasFacilityManagementTicketReasonCategoryV2 = 43,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.TicketReasonDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.TicketReason")]
                DiasFacilityManagementTicketReason = 44,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.TicketRelatedLocationDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.TicketRelatedLocation")]
                DiasFacilityManagementTicketRelatedLocation = 45,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.TicketStateTransitionDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.TicketStateTransition")]
                DiasFacilityManagementTicketStateTransition = 46,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard. TicketStateTransitionFlowDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.TicketStateTransitionFlow")]
                DiasFacilityManagementTicketStateTransitionFlow = 47,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard. ViewAssigmentGroupEmployeeDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.VwAssigmentGroupEmployee")]
                DiasFacilityManagementViewAssigmentGroupEmployee = 48,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard. ViewReasonCategoryDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.VwReasonCategory")]
                DiasFacilityManagementViewReasonCategory = 49,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard. ViewTicketDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.VwTicket")]
                DiasFacilityManagementViewTicket = 50,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard. ViewTicketFormQDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.VwTicketFormQ")]
                DiasFacilityManagementViewTicketFormQ = 51,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard. ViewTicketLocation1Dto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.VwTicketLocation1")]
                DiasFacilityManagementViewTicketLocation1 = 52,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard. ViewTicketLocationDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.VwTicketLocation")]
                DiasFacilityManagementViewTicketLocation = 53,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard. ViewTicketNoteDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.VwTicketNote")]
                DiasFacilityManagementViewTicketNote = 54,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard. ViewTicketStateLevelDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.VwTicketStateLevel")]
                DiasFacilityManagementViewTicketStateLevel = 55,

                [Description("DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard. ViewUserPageViewDto")]
                [Display(Name = "DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.UserPageView")]
                DiasFacilityManagementViewUserPageView = 56


                #endregion WorkOrder

            #endregion  DiasFacilityManagement
        }
    }
}
