namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom
{
    public class CustomSubLocationFilterRequestDto
    {
        public string[] HierarchyIdArr { get; set; }

        //Eğer HierarchyIdArr aynı seviye mahaller ise, filtre sonuç listesinde unique kontrolüne gerek yok
        //true -> unique kontrolüne gerek yok, false -> unique kontrolüne gerek var
        public bool OnTheSameLevel { get; set; }
    }
}
