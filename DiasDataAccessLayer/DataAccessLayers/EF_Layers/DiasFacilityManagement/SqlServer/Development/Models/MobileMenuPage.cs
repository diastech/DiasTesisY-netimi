using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    //TODO: Hiyerarşik tablo yapısına geçirtilecek
    public class MobileMenuPage : DevelopmentBaseEntity
    {
        public MobileMenuPage()
        {
            ParentMobileMenuPageByUsers = new HashSet<MobileMenuPage>();
        }

        public int Id { get; set; }

        public int HierarchicalOrder { get; set; }

        public int HierarchicalLevel { get; set; }

        public int? ParentId { get; set; }

        public string MenuText { get; set; }       

        public string MenuIcon { get; set; }

        public bool ExpandOnStart { get; set; }

        public string MenuImagePath { get; set; }

        public long AuthorizationCodeLevel { get; set; }

        public long AuthorizationCode { get; set; }


        public virtual User AddedByUser { get; set; }

        public virtual User LastModifiedByUser { get; set; }

        public virtual MobileMenuPage ParentAddedByUser { get; set; }

        public virtual ICollection<MobileMenuPage> ParentMobileMenuPageByUsers { get; set; }

    }
}
