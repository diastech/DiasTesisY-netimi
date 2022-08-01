using DevExtreme.AspNet.Mvc;

namespace DiasShared.Data.JsonData.InputOutput.Filters.Web.DevExpress.Development
{
    public class CustomDataSourceLoadOptionDto 
    {
        public CustomDataSourceLoadOptionDto() {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">muhakkak deep copy yapılmış DataSourceLoadOptions olmalıdır</param>
        /// <param name="filterStr"></param>
        public CustomDataSourceLoadOptionDto(DataSourceLoadOptions options)
        {
            DataSourceLoadOption = options;
        }


        public DataSourceLoadOptions DataSourceLoadOption { get; set; }

       
       
    }
}
