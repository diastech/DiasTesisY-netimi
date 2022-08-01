using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using DiasDataAccessLayer.InterfacesAbstracts.Interfaces.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;


namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models
{
    /// <summary>
    /// Custom User entitymiz
    /// </summary>
    public class User : IdentityUser<int>, IDevelopmentBaseEntity //C# iki classtan türetmeye izin vermediği için IDevelopmentBaseEntity kullanıyoruz
    {
        public User()
        {
            AssignmentGroupAddedByUsers = new HashSet<AssignmentGroup>();
            AssignmentGroupAuthorizedLocationAddedByUsers = new HashSet<AssignmentGroupAuthorizedLocation>();
            AssignmentGroupAuthorizedLocationLastModifiedByUsers = new HashSet<AssignmentGroupAuthorizedLocation>();
            AssignmentGroupEmployeeAddedByUsers = new HashSet<AssignmentGroupEmployee>();
            AssignmentGroupEmployeeEmployeeUsers = new HashSet<AssignmentGroupEmployee>();
            AssignmentGroupEmployeeLastModifiedByUsers = new HashSet<AssignmentGroupEmployee>();
            AssignmentGroupGroupManagerUsers = new HashSet<AssignmentGroup>();
            AssignmentGroupLastModifiedByUsers = new HashSet<AssignmentGroup>();
            AttachmentAddedByUsers = new HashSet<Attachment>();
            AttachmentLastModifiedByUsers = new HashSet<Attachment>();
            BasicTicketAddedByUsers = new HashSet<BasicTicket>();
            BasicTicketLastModifiedByUsers = new HashSet<BasicTicket>();
            InverseAddedByUser = new HashSet<User>();
            InverseLastModifiedByUser = new HashSet<User>();
            LocationAddedByUsers = new HashSet<Location>();
            LocationLastModifiedByUsers = new HashSet<Location>();
            LocationV2AddedByUsers = new HashSet<LocationV2>();
            LocationV2LastModifiedByUsers = new HashSet<LocationV2>();
            MenuPageAddedByUsers = new HashSet<MenuPage>();
            MenuPageLastModifiedByUsers = new HashSet<MenuPage>();
            MenuPageV2AddedByUsers = new HashSet<MenuPageV2>();
            MenuPageV2LastModifiedByUsers = new HashSet<MenuPageV2>();
            PeriodicTicketAddedByUsers = new HashSet<PeriodicTicket>();
            PeriodicTicketLastModifiedByUsers = new HashSet<PeriodicTicket>();
            ResolutionFormAddedByUsers = new HashSet<ResolutionForm>();
            ResolutionFormAnswerAddedByUsers = new HashSet<ResolutionFormAnswer>();
            ResolutionFormAnswerLastModifiedByUsers = new HashSet<ResolutionFormAnswer>();
            ResolutionFormChoiceOptionAddedByUsers = new HashSet<ResolutionFormChoiceOption>();
            ResolutionFormChoiceOptionLastModifiedByUsers = new HashSet<ResolutionFormChoiceOption>();
            ResolutionFormLastModifiedByUsers = new HashSet<ResolutionForm>();
            ResolutionFormMultipleChoiceAddedByUsers = new HashSet<ResolutionFormMultipleChoice>();
            ResolutionFormMultipleChoiceLastModifiedByUsers = new HashSet<ResolutionFormMultipleChoice>();
            ResolutionFormQuestionAddedByUsers = new HashSet<ResolutionFormQuestion>();
            ResolutionFormQuestionAnswerAddedByUsers = new HashSet<ResolutionFormQuestionAnswer>();
            ResolutionFormQuestionAnswerLastModifiedByUsers = new HashSet<ResolutionFormQuestionAnswer>();
            ResolutionFormQuestionLastModifiedByUsers = new HashSet<ResolutionFormQuestion>();
            ResolutionFormQuestionTypeAddedByUsers = new HashSet<ResolutionFormQuestionType>();
            ResolutionFormQuestionTypeLastModifiedByUsers = new HashSet<ResolutionFormQuestionType>();
            ResolutionFormSingleQuestionAddedByUsers = new HashSet<ResolutionFormSingleQuestion>();
            ResolutionFormSingleQuestionLastModifiedByUsers = new HashSet<ResolutionFormSingleQuestion>();
            ResolutionFormV2AddedByUsers = new HashSet<ResolutionFormV2>();
            ResolutionFormV2LastModifiedByUsers = new HashSet<ResolutionFormV2>();
            ResolutionFormYesNoAddedByUsers = new HashSet<ResolutionFormYesNo>();
            ResolutionFormYesNoLastModifiedByUsers = new HashSet<ResolutionFormYesNo>();
            TicketAddedByUsers = new HashSet<Ticket>();
            TicketAuditHistoryAddedByUsers = new HashSet<TicketAuditHistory>();
            TicketAuditHistoryLastModifiedByUsers = new HashSet<TicketAuditHistory>();
            TicketAuditHistoryNextTicketAssignedUsers = new HashSet<TicketAuditHistory>();
            TicketAuditHistoryPreviousTicketAssignedUsers = new HashSet<TicketAuditHistory>();
            TicketLastModifiedByUsers = new HashSet<Ticket>();
            TicketNoteAddedByUsers = new HashSet<TicketNote>();
            TicketNoteLastModifiedByUsers = new HashSet<TicketNote>();
            TicketReasonAddedByUsers = new HashSet<TicketReason>();
            TicketReasonCategoryAddedByUsers = new HashSet<TicketReasonCategory>();
            TicketReasonCategoryLastModifiedByUsers = new HashSet<TicketReasonCategory>();
            TicketReasonCategoryV2AddedByUsers = new HashSet<TicketReasonCategoryV2>();
            TicketReasonCategoryV2LastModifiedByUsers = new HashSet<TicketReasonCategoryV2>();
            TicketReasonLastModifiedByUsers = new HashSet<TicketReason>();
            TicketRelatedLocationAddedByUsers = new HashSet<TicketRelatedLocation>();
            TicketRelatedLocationLastModifiedByUsers = new HashSet<TicketRelatedLocation>();
            TicketStateAddedByUsers = new HashSet<TicketState>();
            TicketStateLastModifiedByUsers = new HashSet<TicketState>();
            TicketStateTransitionAddedByUsers = new HashSet<TicketStateTransition>();
            TicketStateTransitionLastModifiedByUsers = new HashSet<TicketStateTransition>();
            TicketTicketAssignedUsers = new HashSet<Ticket>();
            UserMenuPageAddedByUsers = new HashSet<UserMenuPage>();
            UserMenuPageLastModifiedByUsers = new HashSet<UserMenuPage>();
            WorkShiftAddedByUsers = new HashSet<WorkShift>();
            WorkShiftLastModifiedByUsers = new HashSet<WorkShift>();            
            CompanyRolesAddedByUsers = new HashSet<CompanyRole>();
            CompanyRolesLastModifiedByUsers = new HashSet<CompanyRole>();
            ApiControllerDescriptionAddedByUsers = new HashSet<ApiControllerDescription>();
            ApiControllerDescriptionLastModifiedByUsers = new HashSet<ApiControllerDescription>();
            ApiActionDescriptionAddedByUsers = new HashSet<ApiActionDescription>();
            ApiActionDescriptionLastModifiedByUsers = new HashSet<ApiActionDescription>();
            TicketStateRoleAddedByUsers = new HashSet<TicketStateRole>();
            TicketStateRoleLastModifiedByUsers = new HashSet<TicketStateRole>();
            TicketPriorityAddedByUsers = new HashSet<TicketPriority>();
            TicketPriorityLastModifiedByUsers = new HashSet<TicketPriority>();
            TicketStateFlowLastModifiedByUsers = new HashSet<TicketStateFlow>();
            TicketStateFlowAddedByUsers = new HashSet<TicketStateFlow>();
            TicketStateFlowRoleAddedByUsers = new HashSet<TicketStateFlowRole>();
            TicketStateFlowRoleLastModifiedByUsers = new HashSet<TicketStateFlowRole>();
            TicketStateTransitionFlowAddedByUsers = new HashSet<TicketStateTransitionFlow>();
            TicketStateTransitionFlowLastModifiedByUsers = new HashSet<TicketStateTransitionFlow>();
            UserClaimAddedByUsers = new HashSet<UserClaim>();
            UserClaimLastModifiedByUsers = new HashSet<UserClaim>();
            CompanyRoleUserAddedByUsers = new HashSet<CompanyRoleUser>();
            CompanyRoleUserLastModifiedByUsers = new HashSet<CompanyRoleUser>();
            UserLoginAddedByUsers = new HashSet<UserLogin>();
            UserLoginLastModifiedByUsers = new HashSet<UserLogin>();
            CompanyRoleClaimAddedByUsers = new HashSet<CompanyRoleClaim>();
            CompanyRoleClaimLastModifiedByUsers = new HashSet<CompanyRoleClaim>();
            UserTokenAddedByUsers = new HashSet<UserToken>();
            UserTokenLastModifiedByUsers = new HashSet<UserToken>();
            TicketReportedUsers = new HashSet<Ticket>();
            TicketOwnerUsers = new HashSet<Ticket>();
            RestClientAddedByUsers = new HashSet<RestClientType>();
            RestClientLastModifiedByUsers = new HashSet<RestClientType>();
            MobileMenuPageAddedByUsers = new HashSet<MobileMenuPage>();
            MobileMenuPageLastModifiedByUsers = new HashSet<MobileMenuPage>();
            LocationCodeAddedByUsers = new HashSet<LocationCode>();
            LocationCodeLastModifiedByUsers = new HashSet<LocationCode>();
        }

        public string FirstName { get; set; } //NonIdentityProperty

        public string MiddleName { get; set; } //NonIdentityProperty

        public string LastName { get; set; } //NonIdentityProperty

        public string MobilePhoneNumber { get; set; } //NonIdentityProperty

        public int WorkShiftId { get; set; } //NonIdentityProperty

        public DateTime AccountLockTime { get; set; } //NonIdentityProperty

        public byte AccountLockout { get; set; } //NonIdentityProperty


        #region IDevelopmentBaseEntityProperties
        public int AddedByUserId { get; set; } = 1; //NonIdentityProperty
        public DateTime AddedTime { get; set; } = DateTime.Now; //NonIdentityProperty
        public int? LastModifiedByUserId { get; set; } //NonIdentityProperty
        public DateTime? LastModifiedTime { get; set; } //NonIdentityProperty
        public bool? IsActive { get; set; } = true; //NonIdentityProperty
        public bool IsDeleted { get; set; } = false; //NonIdentityProperty
        #endregion IDevelopmentBaseEntityProperties


        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual WorkShift WorkShift { get; set; }

        #region AuditPaths
        public virtual ICollection<CompanyRole> CompanyRolesAddedByUsers { get; set; }
        public virtual ICollection<CompanyRole> CompanyRolesLastModifiedByUsers { get; set; }
        public virtual ICollection<AssignmentGroup> AssignmentGroupAddedByUsers { get; set; }
        public virtual ICollection<AssignmentGroupAuthorizedLocation> AssignmentGroupAuthorizedLocationAddedByUsers { get; set; }
        public virtual ICollection<AssignmentGroupAuthorizedLocation> AssignmentGroupAuthorizedLocationLastModifiedByUsers { get; set; }
        public virtual ICollection<AssignmentGroupEmployee> AssignmentGroupEmployeeAddedByUsers { get; set; }
        public virtual ICollection<AssignmentGroupEmployee> AssignmentGroupEmployeeEmployeeUsers { get; set; }
        public virtual ICollection<AssignmentGroupEmployee> AssignmentGroupEmployeeLastModifiedByUsers { get; set; }
        public virtual ICollection<AssignmentGroup> AssignmentGroupGroupManagerUsers { get; set; }
        public virtual ICollection<AssignmentGroup> AssignmentGroupLastModifiedByUsers { get; set; }
        public virtual ICollection<Attachment> AttachmentAddedByUsers { get; set; }
        public virtual ICollection<Attachment> AttachmentLastModifiedByUsers { get; set; }
        public virtual ICollection<BasicTicket> BasicTicketAddedByUsers { get; set; }
        public virtual ICollection<BasicTicket> BasicTicketLastModifiedByUsers { get; set; }
        public virtual ICollection<User> InverseAddedByUser { get; set; }
        public virtual ICollection<User> InverseLastModifiedByUser { get; set; }
        public virtual ICollection<Location> LocationAddedByUsers { get; set; }
        public virtual ICollection<Location> LocationLastModifiedByUsers { get; set; }
        public virtual ICollection<LocationV2> LocationV2AddedByUsers { get; set; }
        public virtual ICollection<LocationV2> LocationV2LastModifiedByUsers { get; set; }
        public virtual ICollection<MenuPage> MenuPageAddedByUsers { get; set; }
        public virtual ICollection<MenuPage> MenuPageLastModifiedByUsers { get; set; }
        public virtual ICollection<MenuPageV2> MenuPageV2AddedByUsers { get; set; }
        public virtual ICollection<MenuPageV2> MenuPageV2LastModifiedByUsers { get; set; }
        public virtual ICollection<PeriodicTicket> PeriodicTicketAddedByUsers { get; set; }
        public virtual ICollection<PeriodicTicket> PeriodicTicketLastModifiedByUsers { get; set; }
        public virtual ICollection<ResolutionForm> ResolutionFormAddedByUsers { get; set; }
        public virtual ICollection<ResolutionFormAnswer> ResolutionFormAnswerAddedByUsers { get; set; }
        public virtual ICollection<ResolutionFormAnswer> ResolutionFormAnswerLastModifiedByUsers { get; set; }
        public virtual ICollection<ResolutionFormChoiceOption> ResolutionFormChoiceOptionAddedByUsers { get; set; }
        public virtual ICollection<ResolutionFormChoiceOption> ResolutionFormChoiceOptionLastModifiedByUsers { get; set; }
        public virtual ICollection<ResolutionForm> ResolutionFormLastModifiedByUsers { get; set; }
        public virtual ICollection<ResolutionFormMultipleChoice> ResolutionFormMultipleChoiceAddedByUsers { get; set; }
        public virtual ICollection<ResolutionFormMultipleChoice> ResolutionFormMultipleChoiceLastModifiedByUsers { get; set; }
        public virtual ICollection<ResolutionFormQuestion> ResolutionFormQuestionAddedByUsers { get; set; }
        public virtual ICollection<ResolutionFormQuestionAnswer> ResolutionFormQuestionAnswerAddedByUsers { get; set; }
        public virtual ICollection<ResolutionFormQuestionAnswer> ResolutionFormQuestionAnswerLastModifiedByUsers { get; set; }
        public virtual ICollection<ResolutionFormQuestion> ResolutionFormQuestionLastModifiedByUsers { get; set; }
        public virtual ICollection<ResolutionFormQuestionType> ResolutionFormQuestionTypeAddedByUsers { get; set; }
        public virtual ICollection<ResolutionFormQuestionType> ResolutionFormQuestionTypeLastModifiedByUsers { get; set; }
        public virtual ICollection<ResolutionFormSingleQuestion> ResolutionFormSingleQuestionAddedByUsers { get; set; }
        public virtual ICollection<ResolutionFormSingleQuestion> ResolutionFormSingleQuestionLastModifiedByUsers { get; set; }
        public virtual ICollection<ResolutionFormV2> ResolutionFormV2AddedByUsers { get; set; }
        public virtual ICollection<ResolutionFormV2> ResolutionFormV2LastModifiedByUsers { get; set; }
        public virtual ICollection<ResolutionFormYesNo> ResolutionFormYesNoAddedByUsers { get; set; }
        public virtual ICollection<ResolutionFormYesNo> ResolutionFormYesNoLastModifiedByUsers { get; set; }
        public virtual ICollection<Ticket> TicketAddedByUsers { get; set; }
        public virtual ICollection<TicketAuditHistory> TicketAuditHistoryAddedByUsers { get; set; }
        public virtual ICollection<TicketAuditHistory> TicketAuditHistoryLastModifiedByUsers { get; set; }
        public virtual ICollection<TicketAuditHistory> TicketAuditHistoryNextTicketAssignedUsers { get; set; }
        public virtual ICollection<TicketAuditHistory> TicketAuditHistoryPreviousTicketAssignedUsers { get; set; }
        public virtual ICollection<Ticket> TicketLastModifiedByUsers { get; set; }
        public virtual ICollection<TicketNote> TicketNoteAddedByUsers { get; set; }
        public virtual ICollection<TicketNote> TicketNoteLastModifiedByUsers { get; set; }
        public virtual ICollection<TicketReason> TicketReasonAddedByUsers { get; set; }
        public virtual ICollection<TicketReasonCategory> TicketReasonCategoryAddedByUsers { get; set; }
        public virtual ICollection<TicketReasonCategory> TicketReasonCategoryLastModifiedByUsers { get; set; }
        public virtual ICollection<TicketReasonCategoryV2> TicketReasonCategoryV2AddedByUsers { get; set; }
        public virtual ICollection<TicketReasonCategoryV2> TicketReasonCategoryV2LastModifiedByUsers { get; set; }
        public virtual ICollection<TicketReason> TicketReasonLastModifiedByUsers { get; set; }
        public virtual ICollection<TicketRelatedLocation> TicketRelatedLocationAddedByUsers { get; set; }
        public virtual ICollection<TicketRelatedLocation> TicketRelatedLocationLastModifiedByUsers { get; set; }
        public virtual ICollection<TicketState> TicketStateAddedByUsers { get; set; }
        public virtual ICollection<TicketState> TicketStateLastModifiedByUsers { get; set; }
        public virtual ICollection<TicketStateTransition> TicketStateTransitionAddedByUsers { get; set; }
        public virtual ICollection<TicketStateTransition> TicketStateTransitionLastModifiedByUsers { get; set; }
        public virtual ICollection<Ticket> TicketTicketAssignedUsers { get; set; }
        public virtual ICollection<UserMenuPage> UserMenuPageAddedByUsers { get; set; }
        public virtual ICollection<UserMenuPage> UserMenuPageLastModifiedByUsers { get; set; }
        public virtual ICollection<WorkShift> WorkShiftAddedByUsers { get; set; }
        public virtual ICollection<WorkShift> WorkShiftLastModifiedByUsers { get; set; }
        public virtual ICollection<ApiControllerDescription> ApiControllerDescriptionAddedByUsers { get; set; }
        public virtual ICollection<ApiControllerDescription> ApiControllerDescriptionLastModifiedByUsers { get; set; }
        public virtual ICollection<ApiActionDescription> ApiActionDescriptionAddedByUsers { get; set; }
        public virtual ICollection<ApiActionDescription> ApiActionDescriptionLastModifiedByUsers { get; set; }
        public virtual ICollection<TicketStateRole> TicketStateRoleAddedByUsers { get; set; }
        public virtual ICollection<TicketStateRole> TicketStateRoleLastModifiedByUsers { get; set; }
        public virtual ICollection<TicketPriority> TicketPriorityAddedByUsers { get; set; }
        public virtual ICollection<TicketPriority> TicketPriorityLastModifiedByUsers { get; set; }
        public virtual ICollection<TicketStateFlow> TicketStateFlowLastModifiedByUsers { get; set; }
        public virtual ICollection<TicketStateFlow> TicketStateFlowAddedByUsers { get; set; }
        public virtual ICollection<TicketStateFlowRole> TicketStateFlowRoleAddedByUsers { get; set; }
        public virtual ICollection<TicketStateFlowRole> TicketStateFlowRoleLastModifiedByUsers { get; set; }
        public virtual ICollection<TicketStateTransitionFlow> TicketStateTransitionFlowAddedByUsers { get; set; }
        public virtual ICollection<TicketStateTransitionFlow> TicketStateTransitionFlowLastModifiedByUsers { get; set; }
        public virtual ICollection<UserClaim> UserClaimAddedByUsers { get; set; }
        public virtual ICollection<UserClaim> UserClaimLastModifiedByUsers { get; set; }
        public virtual ICollection<CompanyRoleUser> CompanyRoleUserAddedByUsers { get; set; }
        public virtual ICollection<CompanyRoleUser> CompanyRoleUserLastModifiedByUsers { get; set; }
        public virtual ICollection<UserLogin> UserLoginAddedByUsers { get; set; }
        public virtual ICollection<UserLogin> UserLoginLastModifiedByUsers { get; set; }
        public virtual ICollection<CompanyRoleClaim> CompanyRoleClaimAddedByUsers { get; set; }
        public virtual ICollection<CompanyRoleClaim> CompanyRoleClaimLastModifiedByUsers { get; set; }
        public virtual ICollection<UserToken> UserTokenAddedByUsers { get; set; }
        public virtual ICollection<UserToken> UserTokenLastModifiedByUsers { get; set; }
        public virtual ICollection<Ticket> TicketReportedUsers{ get; set; }
        public virtual ICollection<Ticket> TicketOwnerUsers { get; set; }
        public virtual ICollection<RestClientType> RestClientAddedByUsers { get; set; }
        public virtual ICollection<RestClientType> RestClientLastModifiedByUsers { get; set; }
        public virtual ICollection<MobileMenuPage> MobileMenuPageAddedByUsers { get; set; }
        public virtual ICollection<MobileMenuPage> MobileMenuPageLastModifiedByUsers { get; set; }
        public virtual ICollection<LocationCode> LocationCodeAddedByUsers { get; set; }
        public virtual ICollection<LocationCode> LocationCodeLastModifiedByUsers { get; set; }

        #endregion AuditPaths

    }
}
