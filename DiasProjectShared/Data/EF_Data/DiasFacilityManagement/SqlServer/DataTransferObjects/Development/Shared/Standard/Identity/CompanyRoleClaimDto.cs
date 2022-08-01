using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class CompanyRoleClaimDto : BaseDevelopmentStandartDto
    {
        //
        // Summary:
        //     Gets or sets the identifier for this role claim.
        public int Id { get; set; }
        //
        // Summary:
        //     Gets or sets the of the primary key of the role associated with this claim.
        public int RoleId { get; set; }
        //
        // Summary:
        //     Gets or sets the claim type for this claim.
        public string ClaimType { get; set; }
        //
        // Summary:
        //     Gets or sets the claim value for this claim.
        public string ClaimValue { get; set; }

        public int? ApiControllerDescriptionId { get; set; }

        /// <summary>
        ///TODO: Web UI da MenuPageV2'e geçiş olduğunda bu sütun silinecek
        /// </summary>
        public int? WebMenuPageLevel { get; set; }

        /// <summary>
        /// Web UI da MenuPageV2'e geçiş olmadan bu sütun kullanılmayacak, geçiş olunca default null kaldırılacak
        /// </summary>
        public int? WebMenuPageV2Level { get; set; }

        public int? MobilMenuPageLevel { get; set; }

        public int RestClientTypeId { get; set; }
         
        public int? TicketStateRoleId { get; set; }
    }
}
