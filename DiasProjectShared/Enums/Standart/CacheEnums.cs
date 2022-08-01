using System.ComponentModel.DataAnnotations;

namespace DiasShared.Enums.Standart
{
    public class CacheEnums
    {
        public enum RedisDtoDefinitions
        {
            [Display(Name = "CustomLocation")]
            CustomLocationDto = 1,

            [Display(Name = "CombinedUserAndAssignmentGroup")]
            CombinedUserAndAssignmentGroupDto = 2,

            [Display(Name = "User")]
            UserDto = 3,

            [Display(Name = "TicketReasonCategory")]
            TicketReasonCategoryDto = 4,

            [Display(Name = "CustomTicketReasonCategory")]
            CustomTicketReasonCategoryDto = 5
        }
    }
}
