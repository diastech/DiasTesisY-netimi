using DIAS.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DIAS.Domain.Models
{
    public class Staff : AuditableBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? BirthOfDate { get; set; }
        public string Duty { get; set; }
    }
}
