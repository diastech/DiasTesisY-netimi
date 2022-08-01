using Microsoft.AspNetCore.Identity;
using System;
using DiasDataAccessLayer.InterfacesAbstracts.Interfaces.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models
{
    public class UserToken : IdentityUserToken<int>, IDevelopmentBaseEntity //C# iki classtan türetmeye izin vermediği için IDevelopmentBaseEntity kullanıyoruz
    {
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
    }
}
