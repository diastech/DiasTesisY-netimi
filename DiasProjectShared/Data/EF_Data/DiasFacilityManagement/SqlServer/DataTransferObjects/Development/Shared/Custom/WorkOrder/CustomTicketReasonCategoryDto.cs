using DiasShared.InterfacesAbstracts.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using System.Text.RegularExpressions;

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

        //UI dan gelen ReasonName
        public string ReasonNameFromUI { get; set; }
        //UI dan gelen ReasonResolutionTime 
        public int ReasonResolutionTimeFromUI { get; set; }
        //UI dan gelen ReasonResponseTime
        public int ReasonResponseTimeFromUI { get; set; }
        //UI dan gelen ParentHierarchy
        public string ParentHierarchyFromUI { get; set; }
        //UI ya giden CategoryName
        public string CategoryNameToUI { get; set; }
    }
}
