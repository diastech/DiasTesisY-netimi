using Microsoft.AspNetCore.Identity;
using System;
using DiasDataAccessLayer.InterfacesAbstracts.Interfaces.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models
{
    public class CompanyRoleClaim : IdentityRoleClaim<int>, IDevelopmentBaseEntity //C# iki classtan türetmeye izin vermediği için IDevelopmentBaseEntity kullanıyoruz
    {
        #region IDevelopmentBaseEntityProperties
        public int AddedByUserId { get; set; } = 1; //NonIdentityProperty
        public DateTime AddedTime { get; set; } = DateTime.Now; //NonIdentityProperty
        public int? LastModifiedByUserId { get; set; } //NonIdentityProperty
        public DateTime? LastModifiedTime { get; set; } //NonIdentityProperty
        public bool? IsActive { get; set; } = true; //NonIdentityProperty
        public bool IsDeleted { get; set; } = false; //NonIdentityProperty
        #endregion IDevelopmentBaseEntityProperties

        public int? ApiControllerDescriptionId { get; set; }

        /// <summary>
        ///TODO: Web UI da MenuPageV2'e geçiş olduğunda bu sütun silinecek
        /// </summary>
        public int? WebMenuPageLevel { get; set; }

        /// <summary>
        /// Web UI da MenuPageV2'e geçiş olmadan bu sütun kullanılmayacak, geçiş olunca default null kaldırılacak
        /// </summary>
        public int? WebMenuPageV2Level { get; set; } = null;

        public int? MobilMenuPageLevel { get; set; }     

        public int RestClientTypeId { get; set; } = 1;

        public int? TicketStateRoleId { get; set; } = 1;

        public virtual User AddedByUser { get; set; }

        public virtual User LastModifiedByUser { get; set; }

        public virtual ApiControllerDescription ApiControllerDescriptionByCompanyRoleClaim { get; set; }

        public virtual RestClientType RestClientTypeByCompanyRoleClaim { get; set; }

        public virtual TicketStateRole TicketStateRoleByCompanyRoleClaim { get; set; }
    }
}
