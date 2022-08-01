using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIAS.UI.Helpers
{
    public class UserHelperDto
    {
        public string TcNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }      
        public string PhoneNumber { get; set; }
        public RoleHelperDto RoleHelper { get; set; }
        public int RoleHelperId { get; set; }
        public bool ActiveStatus { get; set; }
        public int AddedByUserId { get; set; }

    }
}
