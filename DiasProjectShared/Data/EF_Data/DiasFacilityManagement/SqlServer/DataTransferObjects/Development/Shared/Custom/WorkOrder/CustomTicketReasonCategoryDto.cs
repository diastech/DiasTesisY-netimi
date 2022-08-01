using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.InterfacesAbstracts.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom
{
    public class CustomTicketReasonCategoryDto : IBaseDevelopmentCustomDto
    {
        public int Id { get; set; }
        private string _Hierarchy;
        public string HierarchyId {
            get { return _Hierarchy; }
            set
            {
                _Hierarchy = value;
                var match = Regex.Match(value, @"\/[^\/]*\/", RegexOptions.RightToLeft);
                _ParentHierarchy = value.Substring(0, match.Success ? match.Index + 1 : 0);
                if (_ParentHierarchy == "")
                    _ParentHierarchy = null;
            }
        }
        private string _ParentHierarchy;
        public string ParentHierarchy
        {
            get { return _ParentHierarchy; }
        }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public bool IsReason { get; set; } = false;
        public string ResolutionTimeText { get; set; }
        public string ResponseTimeText { get; set; }
        public int ResolutionTime { get; set; }
        public int ResponseTime { get; set; }
    }
}
