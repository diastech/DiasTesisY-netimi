using Microsoft.EntityFrameworkCore;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models
{
    public partial class IdentityManagement_DFMSqlServer
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //bunu yapmazsak AspNetUser yapar tablonun adını
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User");

                entity.HasIndex(e => e.Email)
                .IsUnique()
                .IsClustered(false);

                entity.Property(e => e.AccountLockTime).HasColumnType("datetime");
                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");
                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                     .HasColumnType("nvarchar(50)");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.MobilePhoneNumber)
                    .IsRequired()
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.InverseAddedByUser)
                    .HasForeignKey(d => d.AddedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.InverseLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .HasConstraintName("FK_User_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.WorkShift)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.WorkShiftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_hr_shift");
            });

            //bunu yapmazsak AspNetRole yapar tablonun adını
            modelBuilder.Entity<CompanyRole>(entity =>
            {
                entity.ToTable(name: "CompanyRole");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");

                entity.Property(e => e.LastModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.AddedByUser)
                   .WithMany(p => p.CompanyRolesAddedByUsers)
                   .HasForeignKey(d => d.AddedByUserId)
                   .OnDelete(DeleteBehavior.ClientSetNull)                   
                   .HasConstraintName("FK_CompanyRole_AddedByUserId_User_Id");

                entity.HasOne(d => d.AddedByUser)
                  .WithMany(p => p.CompanyRolesAddedByUsers)
                  .HasForeignKey(d => d.AddedByUserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_CompanyRole_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                  .WithMany(p => p.CompanyRolesLastModifiedByUsers)
                  .HasForeignKey(d => d.LastModifiedByUserId)
                  .HasConstraintName("FK_CompanyRole_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.ParentCompanyRole)
                  .WithMany(p => p.ParentCompanyRolesAddedByRoles)
                  .HasForeignKey(d => d.ParentCompanyRoleId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .IsRequired(false)
                  .HasConstraintName("FK_CompanyRole_AddedByParentCompanyRoleId_CompanyRole_ParentCompanyRoleId");

            });

            //bunu yapmazsak AspNetUserClaims yapar tablonun adını
            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.ToTable(name: "UserClaim");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");

                entity.Property(e => e.LastModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.AddedByUser)
                   .WithMany(p => p.UserClaimAddedByUsers)
                   .HasForeignKey(d => d.AddedByUserId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_UserClaim_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                  .WithMany(p => p.UserClaimLastModifiedByUsers)
                  .HasForeignKey(d => d.LastModifiedByUserId)
                  .HasConstraintName("FK_UserClaim_LastModifiedByUserId_User_Id");
            });

            //bunu yapmazsak AspNetUserRoles yapar tablonun adını
            modelBuilder.Entity<CompanyRoleUser>(entity =>
            {
                entity.ToTable(name: "CompanyRoleUser");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");

                entity.Property(e => e.LastModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.AddedByUser)
                   .WithMany(p => p.CompanyRoleUserAddedByUsers)
                   .HasForeignKey(d => d.AddedByUserId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_CompanyRoleUser_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                  .WithMany(p => p.CompanyRoleUserLastModifiedByUsers)
                  .HasForeignKey(d => d.LastModifiedByUserId)
                  .HasConstraintName("FK_CompanyRoleUser_LastModifiedByUserId_User_Id");
            });

            //bunu yapmazsak AspNetUserLogins yapar tablonun adını
            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.ToTable(name: "UserLogin");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");

                entity.Property(e => e.LastModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.AddedByUser)
                   .WithMany(p => p.UserLoginAddedByUsers)
                   .HasForeignKey(d => d.AddedByUserId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_UserLogin_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                  .WithMany(p => p.UserLoginLastModifiedByUsers)
                  .HasForeignKey(d => d.LastModifiedByUserId)
                  .HasConstraintName("FK_UserLogin_LastModifiedByUserId_User_Id");
            });

            //bunu yapmazsak AspNetRoleClaims yapar tablonun adını
            modelBuilder.Entity<CompanyRoleClaim>(entity =>
            {
                entity.ToTable(name: "CompanyRoleClaim");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");

                entity.Property(e => e.LastModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.AddedByUser)
                   .WithMany(p => p.CompanyRoleClaimAddedByUsers)
                   .HasForeignKey(d => d.AddedByUserId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_CompanyRoleClaim_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                  .WithMany(p => p.CompanyRoleClaimLastModifiedByUsers)
                  .HasForeignKey(d => d.LastModifiedByUserId)
                  .HasConstraintName("FK_CompanyRoleClaim_LastModifiedByUserId_User_Id");

                entity.HasOne(d => d.ApiControllerDescriptionByCompanyRoleClaim)
                 .WithMany(p => p.CompanyRoleClaimsAddedByAdmins)
                 .HasForeignKey(d => d.ApiControllerDescriptionId)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("FK_CompanyRoleClaim_ApiControllerDescriptionId_ApiControllerDescription_Id");

                entity.HasOne(d => d.RestClientTypeByCompanyRoleClaim)
                .WithMany(p => p.CompanyRoleClaimByRestClientTypes)
                .HasForeignKey(d => d.RestClientTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CompanyRoleClaim_RestClientTypeId_RestClientType_Id");

                entity.HasOne(d => d.TicketStateRoleByCompanyRoleClaim)
                  .WithMany(p => p.CompanyRoleClaimByTicketStateRoles)
                  .HasForeignKey(d => d.TicketStateRoleId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK_CompanyRoleClaim_TicketStateRoleId_TicketStateRole_Id");
            });

            //bunu yapmazsak AspNetUserTokens yapar tablonun adını
            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.ToTable(name: "UserToken");

                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedTime).HasColumnType("datetime").HasDefaultValueSql("getdate()");

                entity.Property(e => e.LastModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.AddedByUser)
                   .WithMany(p => p.UserTokenAddedByUsers)
                   .HasForeignKey(d => d.AddedByUserId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_UserToken_AddedByUserId_User_Id");

                entity.HasOne(d => d.LastModifiedByUser)
                  .WithMany(p => p.UserTokenLastModifiedByUsers)
                  .HasForeignKey(d => d.LastModifiedByUserId)
                  .HasConstraintName("FK_UserToken_LastModifiedByUserId_User_Id");
            });


            //identity base tablolarının ve User tablosunun hepsi idn şemasında olsun
            modelBuilder.HasDefaultSchema("idn");

            OnModelCreatingPartial(modelBuilder);
        }
    }
}
