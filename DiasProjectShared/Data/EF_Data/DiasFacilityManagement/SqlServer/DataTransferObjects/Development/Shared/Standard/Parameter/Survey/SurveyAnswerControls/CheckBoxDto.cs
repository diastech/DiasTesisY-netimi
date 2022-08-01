using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class CheckBoxDto
    {
        public string Question { get; set; }
        public string Label { get; set; }
        public List<string> Datas { get; set; }
        public bool isRequired { get; set; }
    }
}
