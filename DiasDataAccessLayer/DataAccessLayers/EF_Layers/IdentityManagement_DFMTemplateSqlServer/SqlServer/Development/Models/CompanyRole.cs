using Microsoft.AspNetCore.Identity;
using System;
using DiasDataAccessLayer.InterfacesAbstracts.Interfaces.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models
{
    public class CompanyRole : IdentityRole<int>, IDevelopmentBaseEntity //C# iki classtan türetmeye izin vermediği için IDevelopmentBaseEntity kullanıyoruz
    {    
        public int ParentCompanyRoleId { get; set; }

        #region IDevelopmentBaseEntityProperties
        public int AddedByUserId { get; set; } = 1; //NonIdentityProperty
        public DateTime AddedTime { get; set; } = DateTime.Now; //NonIdentityProperty
        public int? LastModifiedByUserId { get; set; } //NonIdentityProperty
        public DateTime? LastModifiedTime { get; set; } //NonIdentityProperty
        public bool? IsActive { get; set; } = true; //NonIdentityProperty
        public bool IsDeleted { get; set; } = false; //NonIdentityProperty
        #endregion IDevelopmentBaseEntityProperties

    }
}
