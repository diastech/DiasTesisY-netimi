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
        public string LocationNumber { get; set; }
        public string LocationName { 
            get{ return LocationWithName; } 
            set 
            {
                LocationWithName = value + " - " +LocationNumber;
            }
        }
        public string LocationWithName { get; set; }

        public string NFC_Code { get; set; }

        //Alt sorgularda, objenin rölatif olarak hangi seviyede olduğunu tutar
        //Mesela parenti x olanların aşağısını getir dediğimizde
        //x'in altındaki ilk seviye 1, onun altındakiler 2 olarak tutulur
        //Bu property getall'larda prensip olarak kullanılmamalıdır
        public int? RelativeLevel { get; set; }

        //TAblodaki gerçek location adını tutar
        public string LocationOriginalName { get; set; }

}
}
