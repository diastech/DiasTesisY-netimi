using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models
{
    /// <summary>
    /// Bu context base bir contextdir yani kendine ait bir veritabanı yoktur
    /// Bu context sadece DiasFacilityManagementTemplate in kendisinden türetilmesi için yazılmıştır
    /// Başka bir contextde kullanmayın
    /// Eğer Microsoft contextlere abstract olabilme izni verse idi o şekilde yapılacaktı
    /// </summary>
    public partial class IdentityManagement_DFMTemplateSqlServer : IdentityDbContext<User, CompanyRole, int,
                                                               IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>,
                                                                IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public IdentityManagement_DFMTemplateSqlServer()
        {
        }

        public IdentityManagement_DFMTemplateSqlServer(DbContextOptions<IdentityManagement_DFMTemplateSqlServer> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<CompanyRole> CompanyRoles { get; set; }
    }
}


