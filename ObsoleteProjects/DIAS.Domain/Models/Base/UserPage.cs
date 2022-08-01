using DIAS.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DIAS.Domain.Models.Base
{
    public class UserPage : AuditableBaseEntity
    {
        public string UserId { get; set; }
        public string PageId { get; set; }
    }
}
