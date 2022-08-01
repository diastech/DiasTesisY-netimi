using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models
{
    /// <summary>
    /// Bu context base bir contextdir yani kendine ait bir veritabanı yoktur
    /// Bu context sadece DiasFacilityManagement in kendisinden türetilmesi için yazılmıştır
    /// Başka bir contextde kullanmayın
    /// Eğer Microsoft contextlere abstract olabilme izni verse idi o şekilde yapılacaktı
    /// </summary>
    public partial class IdentityManagement_DFMSqlServer : IdentityDbContext<User, CompanyRole, int,
                                                               UserClaim, CompanyRoleUser, UserLogin,
                                                                CompanyRoleClaim, UserToken>
    {
        public IdentityManagement_DFMSqlServer()
        {
        }

        public IdentityManagement_DFMSqlServer(DbContextOptions<IdentityManagement_DFMSqlServer> options)
            : base(options)
        {
        }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual DbSet<User> Users { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword

        public virtual DbSet<CompanyRole> CompanyRoles { get; set; }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual DbSet<UserClaim> UserClaims { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword

        public virtual DbSet<CompanyRoleUser> CompanyRoleUsers { get; set; }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual DbSet<UserLogin> UserLogins { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword

        public virtual DbSet<CompanyRoleClaim> CompanyRoleClaims { get; set; }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual DbSet<UserToken> UserTokens { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword

    }
}


