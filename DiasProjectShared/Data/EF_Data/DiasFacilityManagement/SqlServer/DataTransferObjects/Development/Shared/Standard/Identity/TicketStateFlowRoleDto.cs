using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class TicketStateFlowRoleDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }

        public int TicketStateRoleId { get; set; }

        public int TicketStateTransitionFlowId { get; set; }

        public bool PermissionGranted { get; set; }
    }
}
