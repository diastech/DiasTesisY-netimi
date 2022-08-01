using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
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
    public class CustomLocationDto : BaseDevelopmentStandartDto, IBaseDevelopmentCustomDto
    {    
        public int Id { get; set; }
        private string _Hierarchy;
        public string HierarchyId {
            get { return _Hierarchy; }
            set
            {
                if (value != null)
                {
                    _Hierarchy = value;
                    var match = Regex.Match(value, @"\/[^\/]*\/", RegexOptions.RightToLeft);
                    _ParentHierarchy = value.Substring(0, match.Success ? match.Index + 1 : 0);
                    if (_ParentHierarchy == "")
                        _ParentHierarchy = null;
                }
                else
                {
                    _Hierarchy = null;
                    _ParentHierarchy = null;
                }
            }
        }
        private string _ParentHierarchy;
        public string ParentHierarchy
        {
            get { return _ParentHierarchy; }
        }

        public string LocationNumber { get; set; }

        ////UI dan gelen LocationName
        //public string LocationNameFromUI { get; set; }
        ////UI dan gelen LocationNumber
        //public int LocationNumberFromUI { get; set; }      

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

        //Tablodaki gerçek location adını tutar
        public string LocationOriginalName { get; set; }
        
        //Client tarafından gönderilen parent hiyerarşi Idsi
        //Sadece requestlerde bulunur
        //Responselarda boştur
        public string ParentHierarchyFromUI { get; set; }

        public string LocationDescription { get; set; }
    }
}
