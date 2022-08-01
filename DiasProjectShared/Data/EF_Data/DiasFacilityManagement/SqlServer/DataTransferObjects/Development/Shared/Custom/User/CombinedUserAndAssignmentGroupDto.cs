using DiasShared.InterfacesAbstracts.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom
{
    public class CombinedUserAndAssignmentGroupDto : IBaseDevelopmentCustomDto
    {
        public int? UserId { get; set; }

        /// <summary>
        /// Eğer öğe kullanıcı ve bir gruba bağlı ise grup idsi, değilse null
        /// Eper öğe grup ise kendi idsi
        /// </summary>
        public int? AssignmentGroupId { get; set; }

        /// <summary>
        /// Grup içinde kullanıcı içinde ortaktır
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// false -> grup, true -> kullanıcı
        /// </summary>
        public bool isUserOrGroup { get; set; }

        /// <summary>
        /// Eğer kullanıcı bir gruba ait ise grup adı, değilse null
        /// </summary>
        public string UserGroupName { get; set; }

    }
}
