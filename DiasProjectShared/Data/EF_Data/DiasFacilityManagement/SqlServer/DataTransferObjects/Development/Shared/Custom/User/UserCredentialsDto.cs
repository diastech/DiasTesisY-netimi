
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom
{
    public class UserCredentialsDto
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string Message { get; set; }

        public int ClientId = (int)(RemoteIncomingDomains.DiasTesisYonetimMobileClient);
    }
}
