using Microsoft.EntityFrameworkCore;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models
{
    public partial class IdentityManagement_DFMTemplateSqlServer
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //bunu yapmazsak AspNetUser yapar tablonun adını
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User");
                entity.Property(e => e.AccountLockTime).HasColumnType("datetime");
                entity.Property(e => e.AddedByUserId).HasDefaultValueSql("((1))");
                entity.Property(e => e.AddedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.MobilePhoneNumber)
                    .IsRequired()
                    .HasColumnType("text");

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
            });

            //identity base tablolarının ve User tablosunun hepsi idn şemasında olsun
            modelBuilder.HasDefaultSchema("idn");

            OnModelCreatingPartial(modelBuilder);
        }
    }
}
