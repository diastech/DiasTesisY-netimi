using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public class MenuPage : DevelopmentBaseEntity
    {
        public MenuPage()
        {
            ParentMenuPageByUsers = new HashSet<MenuPage>();
        }

        [Column(Order = 0)]
        public int Id { get; set; }

        [Column(Order = 1)]
        public int HierarchicalOrder { get; set; }

        [Column(Order = 2)]
        public int HierarchicalLevel { get; set; }

        [Column(Order = 3)]
        public int? ParentId { get; set; }

        [Column(Order = 4)]
        public string MenuText { get; set; }

        [Column(Order = 5)]
        public string UrlPath { get; set; }

        [Column(Order = 6)]
        public string MenuIcon { get; set; }

        [Column(Order = 7)]
        public bool ExpandOnStart { get; set; }

        [Column(Order = 8)]
        public string MenuImagePath { get; set; }

        [Column(Order = 9)]
        public long AuthorizationCodeLevel { get; set; }

        [Column(Order = 10)]
        public long AuthorizationCode { get; set; }
       

        public virtual User AddedByUser { get; set; }

        public virtual User LastModifiedByUser { get; set; }

        public virtual MenuPage ParentAddedByUser { get; set; }

        public virtual ICollection<MenuPage> ParentMenuPageByUsers { get; set; }
    }
}
