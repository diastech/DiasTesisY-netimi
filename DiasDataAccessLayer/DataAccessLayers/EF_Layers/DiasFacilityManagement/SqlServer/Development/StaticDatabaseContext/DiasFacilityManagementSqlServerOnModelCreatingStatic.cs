using DiasDataAccessLayer.Shared.EfExtensions.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public partial class DiasFacilityManagementSqlServer
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<AssignmentGroup>(entity =>
            {
                entity.ToTable("AssignmentGroup", "adm");

                entity.Property(e => e.Id).HasComment("Atama Grubu tanım tablosunun primary keyi");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");

                entity.Property(e => e.GroupManagerUserId).HasComment("Atama grubunun sorumlusu HR tablosunun ID si ile tutulur FK");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("Atama grubu adını tutar");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.TicketReasonId).HasComment("arama nedeni tablosunun ID si");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.AssignmentGroupAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssignmentGroup_AddedByUserId_User_Id");

                entity.HasOne(d => d.GroupManagerUser)
                    .WithMany(p => p.AssignmentGroupGroupManagerUsers)
                    .HasForeignKey(d => d.GroupManagerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assignmentgroup_hr");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.AssignmentGroupLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssignmentGroup_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.TicketReason)
                    .WithMany(p => p.AssignmentGroups)
                    .HasForeignKey(d => d.TicketReasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assignmentgroup_reason");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<AssignmentGroupAuthorizedLocation>(entity =>
            {
                entity.ToTable("AssignmentGroupAuthorizedLocation", "adm");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.AssignmentGroupAuthorizedLocationAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssignmentGroupAuthorizedLocation_AddedByUserId_User_Id");

                entity.HasOne(d => d.AssignmentGroup)
                    .WithMany(p => p.AssignmentGroupAuthorizedLocations)
                    .HasForeignKey(d => d.AssignmentGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assignmentgroupauthorizedplaces_assignmentgroup");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.AssignmentGroupAuthorizedLocationLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssignmentGroupAuthorizedLocation_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.AssignmentGroupAuthorizedLocations)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssignmentGroupAuthorizedLocation_LocationId_LocationV2_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<AssignmentGroupEmployee>(entity =>
            {
                entity.ToTable("AssignmentGroupEmployee", "adm");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.AssignmentGroupEmployeeAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssignmentGroupEmployee_AddedByUserId_User_Id");

                entity.HasOne(d => d.AssignmentGroup)
                    .WithMany(p => p.AssignmentGroupEmployees)
                    .HasForeignKey(d => d.AssignmentGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assignmentgroupemployee_assignmentgroup");

                entity.HasOne(d => d.EmployeeUser)
                    .WithMany(p => p.AssignmentGroupEmployeeEmployeeUsers)
                    .HasForeignKey(d => d.EmployeeUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assignmentgroupemployee_hr");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.AssignmentGroupEmployeeLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssignmentGroupEmployee_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("Attachment", "usr");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AttachmentDescription)
                    .HasMaxLength(500);

                entity.Property(e => e.FolderName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.AttachmentAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attachment_AddedByUserId_User_Id");

                entity.HasOne(d => d.BasicTicket)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.BasicTicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attachments_Basic_Ticket");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.AttachmentLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attachment_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticketattachments_ticketattachments");

                entity.HasOne(d => d.TicketNote)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.TicketNoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attachments_Ticket_Notes");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<BasicTicket>(entity =>
            {
                entity.ToTable("BasicTicket", "usr");

                entity.Property(e => e.StateId).HasDefaultValueSql("((9))");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");


                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.TicketDescription)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.BasicTicketAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BasicTicket_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.BasicTicketLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BasicTicket_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.ReportedUser)
                   .WithMany(p => p.BasicTicketReportedByUsers)
                   .HasForeignKey(d => d.ReportedUserId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_BasicTicket_ReportedUserId_User_Id");

                entity.HasOne(d => d.StateOfBasicTicket)
                  .WithMany(p => p.BasicTicketStateFK)
                  .HasForeignKey(d => d.StateId)
                  .OnDelete(DeleteBehavior.ClientCascade)
                  .HasConstraintName("FK_BasicTicket_BasicTicketState_StateId_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<BasicTicketState>(entity =>
                {
                    entity.ToTable("BasicTicketState", "lst");

                    entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                    entity.Property(e => e.AddedTime)
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                    entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(200);
                        

                    entity.Property(e => e.NormalizedName)
                       .IsRequired()
                       .HasMaxLength(200)
                       .IsUnicode(false);

                    entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location", "lst");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HierarchicalParentId)
                    .HasMaxLength(500);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.LocationDescription)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LocationHierarchy)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LocationNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.LocationAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.LocationLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<TicketPriority>(entity=>
                { 
                     entity.ToTable("TicketPriority", "lst");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Id)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(500);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                     entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                    entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                    entity.HasOne(d => d.AddedByUser)
                   .WithMany(p => p.TicketPriorityAddedByUsers)
                   .HasForeignKey(d => d.AddedByUserId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_TicketPriority_AddedByUserId_User_Id");

                    entity.HasOne(d => d.LastModifiedByUser)
                        .WithMany(p => p.TicketPriorityLastModifiedByUsers)
                         .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasForeignKey(d => d.LastModifiedByUserId)
                        .HasConstraintName("FK_TicketPriority_LastModifiedByUserId_User_Id");
                });

            modelBuilder.Entity<LocationV2>(entity =>
            {
                entity.ToTable("LocationV2", "lst");

                entity.HasIndex(e => e.HierarchyId)
                .IsUnique()
                .IsClustered(false);

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HierarchyId).IsRequired();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.LocationDescription)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LocationNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.LocationV2AddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationV2_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.LocationV2LastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationV2_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            //modelBuilder.Entity<LocationCode>(entity =>
            //{
            //    entity.ToTable("LocationCode", "lst");

            //    entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.AddedTime)
            //        .HasColumnType("datetime")
            //        .HasDefaultValueSql("(getdate())");

            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.Name)
            //        .IsRequired()
            //        .HasMaxLength(200);

            //    entity.HasOne(d => d.AddedByUser)
            //        .WithMany(p => p.LocationCodeAddedByUsers)
            //        .HasForeignKey(d => d.AddedByUserId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_LocationCode_AddedByUserId_User_Id");

            //    entity.HasOne(d => d.LastModifiedByUser)
            //        .WithMany(p => p.LocationCodeLastModifiedByUsers)
            //        .HasForeignKey(d => d.LastModifiedByUserId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_LocationCode_LastModifiedByUserId_User_Id");

            //    entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            //});

            modelBuilder.Entity<MenuPage>(entity =>
            {
               
                entity.ToTable("MenuPage", "lst");

                entity.Property(e => e.Id).IsRequired().HasColumnOrder(0);

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.MenuIcon)
                    .HasMaxLength(255);

                entity.Property(e => e.MenuImagePath).IsUnicode(false);

                entity.Property(e => e.MenuText)
                    .HasMaxLength(255);

                entity.Property(e => e.ParentId)
                    .HasMaxLength(255);

                entity.Property(e => e.UrlPath).IsUnicode(false);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.MenuPageAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuPage_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.MenuPageLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuPage_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.ParentAddedByUser)
                   .WithMany(p => p.ParentMenuPageByUsers)
                   .HasForeignKey(d => d.ParentId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_MenuPage_ParentId_MenuPage_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<MenuPageV2>(entity =>
            {
                entity.ToTable("MenuPageV2", "lst");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HierarchyId).IsRequired();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.MenuIcon)
                    .HasMaxLength(255);

                entity.Property(e => e.MenuImagePath).IsUnicode(false);

                entity.Property(e => e.MenuText)
                    .HasMaxLength(255);

                entity.Property(e => e.UrlPath).IsUnicode(false);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.MenuPageV2AddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuPageV2_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.MenuPageV2LastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuPageV2_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<MobileMenuPage>(entity =>
            {

                entity.ToTable("MobileMenuPage", "lst");

                entity.Property(e => e.Id).IsRequired().HasColumnOrder(0);

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.MenuIcon)
                    .HasMaxLength(255);

                entity.Property(e => e.MenuImagePath).IsUnicode(false);

                entity.Property(e => e.MenuText)
                    .HasMaxLength(255);

                entity.Property(e => e.ParentId)
                    .HasMaxLength(255);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.MobileMenuPageAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MobileMenuPage_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.MobileMenuPageLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MobileMenuPage_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.ParentAddedByUser)
                   .WithMany(p => p.ParentMobileMenuPageByUsers)
                   .HasForeignKey(d => d.ParentId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_MobileMenuPage_ParentId_MobileMenuPage_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<PeriodicTicket>(entity =>
            {
                entity.ToTable("PeriodicTicket", "usr");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PeriodFrequency)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PeriodicName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.PeriodicTicketAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeriodicTicket_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.PeriodicTicketLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeriodicTicket_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.PeriodicTickets)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_periodicticketdefinitions_place");

                entity.HasOne(d => d.TicketReason)
                    .WithMany(p => p.PeriodicTickets)
                    .HasForeignKey(d => d.TicketReasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_periodicticketdefinitions_reason");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<ResolutionForm>(entity =>
            {
                entity.ToTable("ResolutionForm", "adm");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FormDescription)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.ResolutionFormAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionForm_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.ResolutionFormLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionForm_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.TicketReasonCategory)
                    .WithMany(p => p.ResolutionForms)
                    .HasForeignKey(d => d.TicketReasonCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionForm_TicketReasonCategoryId_TicketReasonCategoryV2_Id");

                entity.HasOne(d => d.TicketReason)
                    .WithMany(p => p.ResolutionForms)
                    .HasForeignKey(d => d.TicketReasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticketforms_reason");

                entity.HasOne(d => d.TicketState)
                    .WithMany(p => p.ResolutionForms)
                    .HasForeignKey(d => d.TicketStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticketforms_ticketstate");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<ResolutionFormAnswer>(entity =>
            {
                entity.ToTable("ResolutionFormAnswer", "adm");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Answer)
                    .HasMaxLength(2000)
                    .HasComment("If yes or no question it is null");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.YesOrNo).HasComment("If not yes or no question it is null");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.ResolutionFormAnswerAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormAnswer_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.ResolutionFormAnswerLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormAnswer_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.ResolutionForm)
                    .WithMany(p => p.ResolutionFormAnswers)
                    .HasForeignKey(d => d.ResolutionFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormAnswer_ResolutionFormId_ResolutionForm_Id");

                entity.HasOne(d => d.ResolutionFormQuestion)
                    .WithMany(p => p.ResolutionFormAnswers)
                    .HasForeignKey(d => d.ResolutionFormQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormAnswer_ResolutionFormQuestionId_ResolutionFormQuestion_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<ResolutionFormChoiceOption>(entity =>
            {
                entity.ToTable("ResolutionFormChoiceOption", "adm");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ChoiceOptionText)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.ResolutionFormChoiceOptionAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormChoiceOption_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.ResolutionFormChoiceOptionLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormChoiceOption_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<ResolutionFormMultipleChoice>(entity =>
            {
                entity.ToTable("ResolutionFormMultipleChoice", "adm");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Option1Text)
                    .HasMaxLength(2000);

                entity.Property(e => e.Option2Text)
                    .HasMaxLength(2000);

                entity.Property(e => e.Option3Text)
                    .HasMaxLength(2000);

                entity.Property(e => e.Option4Text)
                    .HasMaxLength(2000);

                entity.Property(e => e.Option5Text)
                    .HasMaxLength(2000);

                entity.Property(e => e.QuestionText)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.ResolutionFormMultipleChoiceAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormMultipleChoice_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.ResolutionFormMultipleChoiceLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormMultipleChoice_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.TicketForm)
                    .WithMany(p => p.ResolutionFormMultipleChoices)
                    .HasForeignKey(d => d.TicketFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticketformQwithMultiple_ticketformQwithMultiple");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<ResolutionFormQuestion>(entity =>
            {
                entity.ToTable("ResolutionFormQuestion", "adm");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.QuestionText)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.ResolutionFormQuestionAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormQuestion_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.ResolutionFormQuestionLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormQuestion_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.ResolutionFormQuestionType)
                    .WithMany(p => p.ResolutionFormQuestions)
                    .HasForeignKey(d => d.ResolutionFormQuestionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormQuestion_ResolutionFormQuestionTypeId_ResolutionFormQuestionType_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<ResolutionFormQuestionAnswer>(entity =>
            {
                entity.ToTable("ResolutionFormQuestionAnswer", "adm");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AnswerText)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.QuestionText)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.ResolutionFormQuestionAnswerAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormQuestionAnswer_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.ResolutionFormQuestionAnswerLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormQuestionAnswer_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.ResolutionForm)
                    .WithMany(p => p.ResolutionFormQuestionAnswers)
                    .HasForeignKey(d => d.ResolutionFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticketformQAnswers_ticketforms");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.ResolutionFormQuestionAnswers)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticketformQAnswers_ticket");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<ResolutionFormQuestionType>(entity =>
            {
                entity.ToTable("ResolutionFormQuestionType", "lst");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.QuestionType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.ResolutionFormQuestionTypeAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormQuestionType_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.ResolutionFormQuestionTypeLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormQuestionType_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<ResolutionFormSingleQuestion>(entity =>
            {
                entity.ToTable("ResolutionFormSingleQuestion", "adm");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.QuestionText)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.ResolutionFormSingleQuestionAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormSingleQuestion_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.ResolutionFormSingleQuestionLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormSingleQuestion_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<ResolutionFormV2>(entity =>
            {
                entity.ToTable("ResolutionFormV2", "adm");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FormXml)
                    .IsRequired()
                    .HasMaxLength(5000);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.ResolutionFormV2AddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormV2_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.ResolutionFormV2LastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormV2_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.TicketReasonCategory)
                    .WithMany(p => p.ResolutionFormV2s)
                    .HasForeignKey(d => d.TicketReasonCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormV2_TicketReasonCategoryId_TicketReasonCategoryV2_Id");

                entity.HasOne(d => d.TicketReason)
                    .WithMany(p => p.ResolutionFormV2s)
                    .HasForeignKey(d => d.TicketReasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormV2_TicketReasonId_TicketReason_Id");

                entity.HasOne(d => d.TicketState)
                    .WithMany(p => p.ResolutionFormV2s)
                    .HasForeignKey(d => d.TicketStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormV2_TicketStateId_TicketState_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<ResolutionFormYesNo>(entity =>
            {
                entity.ToTable("ResolutionFormYesNo", "adm");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.QuestionText)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.ResolutionFormYesNoAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormYesNo_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.ResolutionFormYesNoLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResolutionFormYesNo_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.TicketForm)
                    .WithMany(p => p.ResolutionFormYesNos)
                    .HasForeignKey(d => d.TicketFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticketformQwithYesNo_ticketforms");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket", "usr");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FacilityId)                   
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.TicketDescription)
                    .HasMaxLength(2000);

                entity.Property(e => e.TicketOpenedTime).HasColumnType("datetime");

                entity.Property(e => e.TicketOwnerUserId)
                    .IsRequired();
                    

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.TicketAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_AddedByUserId_User_Id");
              
                //entity.HasOne(d => d.TicketReportedUsers)
                //    .WithMany(p => p.TicketReportedUsers)
                //    .HasForeignKey(d => d.TicketReportedUserId)
                //    .OnDelete(DeleteBehavior.ClientCascade)
                //    .HasConstraintName("FK_Ticket_ReportedUserId_User_Id");

                entity.HasOne(d => d.TicketOwnerUsers)
                   .WithMany(p => p.TicketOwnerUsers)
                   .HasForeignKey(d => d.TicketOwnerUserId)
                   .OnDelete(DeleteBehavior.ClientCascade)
                   .HasConstraintName("FK_Ticket_OwnerUserId_User_Id");

                entity.HasOne(d => d.PeriodicTickets)
                   .WithMany(p => p.PeriodicTickets)
                   .HasForeignKey(d => d.PeriodicTicketId)
                   .OnDelete(DeleteBehavior.ClientCascade)
                   .HasConstraintName("FK_Ticket_PeriodicTicketId_PeriodicTicket_Id");


                entity.HasOne(d => d.BasicTicket)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.BasicTicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Basic_Tickets");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TicketLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.TickedAssignedAssignmentGroup)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TickedAssignedAssignmentGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticket_assignmentgroup");

                entity.HasOne(d => d.TicketAssignedUser)
                    .WithMany(p => p.TicketTicketAssignedUsers)
                    .HasForeignKey(d => d.TicketAssignedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticket_hr");

                entity.HasOne(d => d.TicketReason)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicketReasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticket_reason");

                entity.HasOne(d => d.TicketStatus)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicketStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticket_ticketstate");

                entity.HasOne(d => d.TicketPriority)
                    .WithMany(p => p.TicketAddedByUsers)
                    .HasForeignKey(d => d.TicketPriorityId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_ticket_ticketpriority");

                entity.HasOne(d => d.FacilityByUser)
                   .WithMany(p => p.TicketAddedByUsers)
                   .HasForeignKey(d => d.FacilityId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Ticket_FacilityId_Facility_Id");

                entity.Property(e => e.ExpectedResponseTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExpectedResolutionTime)
               .HasColumnType("datetime")
               .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<TicketAuditHistory>(entity =>
            {
                entity.ToTable("TicketAuditHistory", "usr");

                entity.Property(e => e.ActivityEndTime).HasColumnType("datetime");

                entity.Property(e => e.ActivityStartTime).HasColumnType("datetime");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HistoryAddTime).HasColumnType("datetime");

                entity.Property(e => e.HistoryType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.TicketAuditHistoryAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketAuditHistory_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TicketAuditHistoryLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketAuditHistory_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TicketAuditHistories)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketAuditHistory_LocationId_LocationV2_Id");

                entity.HasOne(d => d.NextAssignedAssignmentGroup)
                    .WithMany(p => p.TicketAuditHistoryNextAssignedAssignmentGroups)
                    .HasForeignKey(d => d.NextAssignedAssignmentGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticketHistory_assignmentgroup1");

                entity.HasOne(d => d.NextTicketAssignedUser)
                    .WithMany(p => p.TicketAuditHistoryNextTicketAssignedUsers)
                    .HasForeignKey(d => d.NextTicketAssignedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticketHistory_hr2");

                entity.HasOne(d => d.NextTicketState)
                    .WithMany(p => p.TicketAuditHistoryNextTicketStates)
                    .HasForeignKey(d => d.NextTicketStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticketHistory_ticketstate1");

                entity.HasOne(d => d.PreviousAssignedAssignmentGroup)
                    .WithMany(p => p.TicketAuditHistoryPreviousAssignedAssignmentGroups)
                    .HasForeignKey(d => d.PreviousAssignedAssignmentGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticketHistory_assignmentgroup");

                entity.HasOne(d => d.PreviousTicketAssignedUser)
                    .WithMany(p => p.TicketAuditHistoryPreviousTicketAssignedUsers)
                    .HasForeignKey(d => d.PreviousTicketAssignedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticketHistory_hr1");

                entity.HasOne(d => d.PreviousTicketState)
                    .WithMany(p => p.TicketAuditHistoryPreviousTicketStates)
                    .HasForeignKey(d => d.PreviousTicketStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticketHistory_ticketstate");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketAuditHistories)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_ticketHistory_ticket");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<TicketNote>(entity =>
            {
                entity.ToTable("TicketNote", "usr");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.NoteText)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.TicketNoteAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketNote_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TicketNoteLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketNote_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketNotes)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_Ticket_Notes_Ticket");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<TicketReason>(entity =>
            {
                entity.ToTable("TicketReason", "adm");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ReasonDescription)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ReasonName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.TicketReasonAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketReason_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TicketReasonLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketReason_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.TicketReasonCategory)
                    .WithMany(p => p.TicketReasons)
                    .HasForeignKey(d => d.TicketReasonCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketReason_TicketReasonCategoryId_TicketReasonCategoryV2_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<TicketReasonCategory>(entity =>
            {
                entity.ToTable("TicketReasonCategory", "adm");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CategoryDescription)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.CategoryHierarchy)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.HierarchicalParentId)
                    .HasMaxLength(250);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.TicketReasonCategoryAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketReasonCategory_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TicketReasonCategoryLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketReasonCategory_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<TicketReasonCategoryV2>(entity =>
            {
                entity.ToTable("TicketReasonCategoryV2", "adm");

                entity.HasIndex(e => e.HierarchyId)
                .IsUnique()
                .IsClustered(false);

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CategoryDescription)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.HierarchyId).IsRequired();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.TicketReasonCategoryV2AddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketReasonCategoryV2_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TicketReasonCategoryV2LastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketReasonCategoryV2_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<TicketRelatedLocation>(entity =>
            {
                entity.ToTable("TicketRelatedLocation", "usr");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.TicketRelatedLocationAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketRelatedLocation_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TicketRelatedLocationLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketRelatedLocation_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketRelatedLocations)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ticketRelations_ticket");

                entity.HasOne(d => d.TicketLocation)
                    .WithMany(p => p.TicketRelatedLocations)
                    .HasForeignKey(d => d.TicketLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketRelatedLocation_TicketLocationId_LocationV2_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<TicketState>(entity =>
            {
                entity.ToTable("TicketState", "lst");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.TicketStateAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketState_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TicketStateLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketState_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<TicketStateTransition>(entity =>
            {
                entity.ToTable("TicketStateTransition", "lst");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.TicketStateTransitionAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateTransition_AddedByUserId_User_Id");

                entity.HasOne(d => d.DestinationTicketState)
                    .WithMany(p => p.TicketStateTransitionDestinationTicketStates)
                    .HasForeignKey(d => d.DestinationTicketStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateTransition_DestinationTicketStateId_ticketstate_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TicketStateTransitionLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateTransition_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.SourceTicketState)
                    .WithMany(p => p.TicketStateTransitionSourceTicketStates)
                    .HasForeignKey(d => d.SourceTicketStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateTransition_SourceTicketStateId_ticketstate_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<UserMenuPage>(entity =>
            {
                entity.ToTable("UserMenuPage", "adm");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ApplicationPageId)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ApplicationUserId)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.UserMenuPageAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserMenuPage_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.UserMenuPageLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserMenuPage_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<UserPageView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("UserPageView");

                entity.Property(e => e.Icon)
                    .HasMaxLength(255);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.PageId)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ParentId)
                    .HasMaxLength(255);

                entity.Property(e => e.Path).IsUnicode(false);

                entity.Property(e => e.Text)
                    .HasMaxLength(255);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(255);

            });

            modelBuilder.Entity<VwAssigmentGroupEmployee>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_AssigmentGroupEmployee");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<VwReasonCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_ReasonCategory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsDisabled).HasColumnName("isDisabled");

                entity.Property(e => e.ReasonCategory)
                    .IsRequired()
                    .HasMaxLength(281)
                    .HasColumnName("reasonCategory");

                entity.Property(e => e.ReasonCategoryDescription)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("reasonCategoryDescription");

                entity.Property(e => e.ReasonCategoryHierarchy)
                    .HasMaxLength(250)
                    .HasColumnName("reasonCategoryHierarchy");

                entity.Property(e => e.ReasonCategoryName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("reasonCategoryName");

                entity.Property(e => e.ReasonCategoryParentId)
                    .HasMaxLength(250)
                    .HasColumnName("reasonCategoryParentId");
            });

            modelBuilder.Entity<VwTicket>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Ticket");

                entity.Property(e => e.ExpectedResolutionTime)
                    .HasColumnType("datetime")
                    .HasColumnName("expectedResolutionTime");

                entity.Property(e => e.ExpectedResponseTime)
                    .HasColumnType("datetime")
                    .HasColumnName("expectedResponseTime");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(100);

                entity.Property(e => e.LocationHierarchy)
                    .HasMaxLength(8000)
                    .HasColumnName("locationHierarchy");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(8000)
                    .HasColumnName("locationName");

                entity.Property(e => e.ReasonCategory)
                    .IsRequired()
                    .HasMaxLength(281)
                    .HasColumnName("reasonCategory");

                entity.Property(e => e.ReasonName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ResponsibleUser)
                    .IsRequired()
                    .HasColumnName("responsibleUser");

                entity.Property(e => e.ResponsibleUserId)
                    .HasMaxLength(30)
                    .HasColumnName("responsibleUserId");

                entity.Property(e => e.StateDescription)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.TicketDescription)
                    .HasMaxLength(2000);

                entity.Property(e => e.TicketLocations)
                    .HasMaxLength(8000)
                    .HasColumnName("ticketLocations");

                entity.Property(e => e.TicketOpenedTime).HasColumnType("datetime");

                entity.Property(e => e.TicketOwnerUser)
                    .IsRequired()
                    .HasColumnName("ticketOwnerUser");

                entity.Property(e => e.TicketOwnerUserId)
                    .IsRequired();
                    
            });

            modelBuilder.Entity<VwTicketFormQ>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_TicketFormQ");

                entity.Property(e => e.Q1)
                    .HasMaxLength(2000);

                entity.Property(e => e.Q2)
                    .HasMaxLength(2000);

                entity.Property(e => e.Q3)
                    .HasMaxLength(2000);

                entity.Property(e => e.Q4)
                    .HasMaxLength(2000);

                entity.Property(e => e.Q5)
                    .HasMaxLength(2000);

                entity.Property(e => e.QuestionText)
                    .IsRequired()
                    .HasMaxLength(2000);
            });

            modelBuilder.Entity<VwTicketLocation>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_TicketLocation");

                entity.Property(e => e.HierarchicalParentId)
                    .HasMaxLength(500);

                entity.Property(e => e.LocationDescription)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LocationHierarchy)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LocationNumber)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VwTicketLocation1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_TicketLocations");

                entity.Property(e => e.Location)
                    .HasMaxLength(8000)
                    .HasColumnName("location");

                entity.Property(e => e.LocationHierarchy)
                    .HasMaxLength(8000)
                    .HasColumnName("locationHierarchy");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(8000)
                    .HasColumnName("locationName");
            });

            modelBuilder.Entity<VwTicketNote>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_TicketNotes");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasColumnName("addedTime");

                entity.Property(e => e.NoteText)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.NotesAddedUser)
                    .IsRequired()
                    .HasColumnName("notesAddedUser");

                entity.Property(e => e.TicketNoteAttachments)
                    .HasMaxLength(8000)
                    .HasColumnName("ticketNoteAttachments");
            });

            modelBuilder.Entity<VwTicketStateLevel>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_TicketStateLevel");

                entity.Property(e => e.TicketStateDestination)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("ticketStateDestination");

                entity.Property(e => e.TicketStateDestinationId).HasColumnName("ticketStateDestinationId");

                entity.Property(e => e.TicketStateSource)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("ticketStateSource");

                entity.Property(e => e.TicketStateSourceId).HasColumnName("ticketStateSourceId");
            });

            modelBuilder.Entity<WorkShift>(entity =>
            {
                entity.ToTable("WorkShift", "lst");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ShiftEndTime).HasColumnType("datetime");

                entity.Property(e => e.ShiftName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ShiftStartTime).HasColumnType("datetime");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.WorkShiftAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkShift_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.WorkShiftLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkShift_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<ApiControllerDescription>(entity =>
            {
                entity.ToTable("ApiControllerDescription", "idn");

                entity.Property(e => e.Id).HasComment("ApiControllerDescription tanım tablosunun primary keyi");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.ApiControllerDescriptionAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApiControllerDescription_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.ApiControllerDescriptionLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApiControllerDescription_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ApiActionDescription>(entity =>
            {
                entity.ToTable("ApiActionDescription", "idn");

                entity.Property(e => e.Id).HasComment("ApiActionDescription tanım tablosunun primary keyi");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ParentApiController)
                   .WithMany(p => p.ApiActionsAddedByAdmins)
                   .HasForeignKey(d => d.ApiControllerDescriptionId)
                   .OnDelete(DeleteBehavior.ClientCascade)
                   .HasConstraintName("FK_ApiControllerDescription_ApiControllerDescriptionId_ApiControllerDescription_Id");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.ApiActionDescriptionAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApiActionDescription_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.ApiActionDescriptionLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApiActionDescription_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TicketStateRole>(entity =>
            {
                entity.ToTable("TicketStateRole", "lst");

                entity.Property(e => e.Id).HasComment("TicketStateRole tanım tablosunun primary keyi");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.TicketStateRoleAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateRole_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TicketStateRoleLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateRole_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TicketStateFlow>(entity =>
            {
                entity.ToTable("TicketStateFlow", "lst");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.TicketStateFlowAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateFlow_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TicketStateFlowLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateFlow_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<TicketStateFlowRole>(entity =>
            {
                entity.ToTable("TicketStateFlowRole", "idn");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.PermissionGranted).HasDefaultValueSql("((0))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.TicketStateFlowRoleAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateFlowRole_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TicketStateFlowRoleLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateFlowRole_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.TicketStateTransitionFlowByTicketStateRole)
                    .WithMany(p => p.TicketStateTransitionFlowByTicketStateFlowRoles)
                    .HasForeignKey(d => d.TicketStateTransitionFlowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateFlowRole_TicketStateTransitionFlowId_TicketStateTransitionFlow_Id");

                entity.HasOne(d => d.TicketStateRoleByUser)
                    .WithMany(p => p.TicketStateFlowRoleByTicketStateRoles)
                    .HasForeignKey(d => d.TicketStateRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateFlowRole_TicketStateRoleId_TicketStateRole_TicketStateRoleId");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TicketStateTransitionFlow>(entity =>
            {
                entity.ToTable("TicketStateTransitionFlow", "lst");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.TicketStateTransitionFlowAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateTransitionFlow_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TicketStateTransitionFlowLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateTransitionFlow_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.TicketStateFlowByTicketStateTransitionFlow)
                    .WithMany(p => p.TicketStateTransitionFlowByTicketStateFlows)
                    .HasForeignKey(d => d.TicketStateFlowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateTransitionFlow_TicketStateFlowId_TicketStateFlow_TicketStateFlowId");

                entity.HasOne(d => d.TicketStateTransitionByTicketStateTransitionFlow)
                    .WithMany(p => p.TicketStateTransitionFlowByTicketStateTransitions)
                    .HasForeignKey(d => d.TicketStateTransitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketStateTransitionFlow_TicketStateTransitionId_TicketStateRole_TicketStateRoleId");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<RestClientType>(entity =>
            {
                entity.ToTable("RestClientType", "lst");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.RestClientAddedByUsers)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RestClientType_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.RestClientLastModifiedByUsers)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RestClientType_LastModifiedByUserId_User_Id");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.ToTable("Facility", "lst");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Id)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(500);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime)
               .HasColumnType("datetime")
               .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.AddedByUser)
               .WithMany(p => p.FacilityAddedByUsers)
               .HasForeignKey(d => d.AddedByUserId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Facility_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.FacilityLastModifiedByUsers)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .HasConstraintName("FK_Facility_LastModifiedByUserId_User_Id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
