using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
{
    /// <summary>
    /// Bu context base bir contextdir yani kendine ait bir veritabanı yoktur
    /// Başka bir veritabanı bundan türetiletilebilir
    /// Eğer Microsoft contextlere abstract olabilme izni verse idi o şekilde yapılacaktı
    /// </summary>
    public partial class DiasFacilityManagementTemplateSqlServer : IdentityManagement_DFMTemplateSqlServer
    {
        public DiasFacilityManagementTemplateSqlServer()
        {
        }

        public DiasFacilityManagementTemplateSqlServer(DbContextOptions<IdentityManagement_DFMTemplateSqlServer> baseoptions,
            DbContextOptions<DiasFacilityManagementTemplateSqlServer> options)
            : base(baseoptions)
        {
        } 

        public virtual DbSet<AssignmentGroup> AssignmentGroups { get; set; }
        public virtual DbSet<AssignmentGroupAuthorizedLocation> AssignmentGroupAuthorizedLocations { get; set; }
        public virtual DbSet<AssignmentGroupEmployee> AssignmentGroupEmployees { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<BasicTicket> BasicTickets { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationV2> LocationV2s { get; set; }
        public virtual DbSet<MenuPage> MenuPages { get; set; }
        public virtual DbSet<MenuPageV2> MenuPageV2s { get; set; }
        public virtual DbSet<PeriodicTicket> PeriodicTickets { get; set; }
        public virtual DbSet<ResolutionForm> ResolutionForms { get; set; }
        public virtual DbSet<ResolutionFormAnswer> ResolutionFormAnswers { get; set; }
        public virtual DbSet<ResolutionFormChoiceOption> ResolutionFormChoiceOptions { get; set; }
        public virtual DbSet<ResolutionFormMultipleChoice> ResolutionFormMultipleChoices { get; set; }
        public virtual DbSet<ResolutionFormQuestion> ResolutionFormQuestions { get; set; }
        public virtual DbSet<ResolutionFormQuestionAnswer> ResolutionFormQuestionAnswers { get; set; }
        public virtual DbSet<ResolutionFormQuestionType> ResolutionFormQuestionTypes { get; set; }
        public virtual DbSet<ResolutionFormSingleQuestion> ResolutionFormSingleQuestions { get; set; }
        public virtual DbSet<ResolutionFormV2> ResolutionFormV2s { get; set; }
        public virtual DbSet<ResolutionFormYesNo> ResolutionFormYesNos { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketAuditHistory> TicketAuditHistories { get; set; }
        public virtual DbSet<TicketNote> TicketNotes { get; set; }
        public virtual DbSet<TicketReason> TicketReasons { get; set; }
        public virtual DbSet<TicketReasonCategory> TicketReasonCategories { get; set; }
        public virtual DbSet<TicketReasonCategoryV2> TicketReasonCategoryV2s { get; set; }
        public virtual DbSet<TicketRelatedLocation> TicketRelatedLocations { get; set; }
        public virtual DbSet<TicketState> TicketStates { get; set; }
        public virtual DbSet<TicketStateTransition> TicketStateTransitions { get; set; }
        public virtual DbSet<UserMenuPage> UserMenuPages { get; set; }
        public virtual DbSet<UserPageView> UserPageViews { get; set; }
        public virtual DbSet<VwAssigmentGroupEmployee> VwAssigmentGroupEmployees { get; set; }
        public virtual DbSet<VwReasonCategory> VwReasonCategories { get; set; }
        public virtual DbSet<VwTicket> VwTickets { get; set; }
        public virtual DbSet<VwTicketFormQ> VwTicketFormQs { get; set; }
        public virtual DbSet<VwTicketLocation> VwTicketLocations { get; set; }
        public virtual DbSet<VwTicketLocation1> VwTicketLocations1 { get; set; }
        public virtual DbSet<VwTicketNote> VwTicketNotes { get; set; }
        public virtual DbSet<VwTicketStateLevel> VwTicketStateLevels { get; set; }
        public virtual DbSet<WorkShift> WorkShifts { get; set; }
        public virtual DbSet<TicketPriority> TicketProperties { get; set; }
        public virtual DbSet<BasicTicketState> BasicTicketStates { get; set; }


    }
}


