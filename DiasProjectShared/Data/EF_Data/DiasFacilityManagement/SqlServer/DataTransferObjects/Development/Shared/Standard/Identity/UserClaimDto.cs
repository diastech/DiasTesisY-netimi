using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class UserClaimDto : BaseDevelopmentStandartDto
    {
        //
        // Summary:
        //     Gets or sets the identifier for this user claim.
        public int Id { get; set; }
        //
        // Summary:
        //     Gets or sets the primary key of the user associated with this claim.
        public int UserId { get; set; }
        //
        // Summary:
        //     Gets or sets the claim type for this claim.
        public string ClaimType { get; set; }
        //
        // Summary:
        //     Gets or sets the claim value for this claim.
        public string ClaimValue { get; set; }

    }
}
