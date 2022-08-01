using DiasShared.InterfacesAbstracts.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom
{
    public class CustomLocationDto : IBaseDevelopmentCustomDto
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
        public string LocationName { get; set; }

    }
}
