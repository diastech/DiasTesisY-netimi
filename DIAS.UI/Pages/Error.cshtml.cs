using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DIAS_UI.Pages {

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel {
        public enum eExceptionCode
        {
            System = 500,
            DataBase = 501,
            SetValue = 502,
            StatusCode = 503,
            Validation = 504
        }
        public int? Code
        { get; set; }

        public string Message
        { get; set; }

        public string StackTrace
        { get; set; }

        public string Sql
        { get; set; }

        public string MethodName
        { get; set; }

        public eExceptionCode ExceptionCode
        { get; set; }

        public void OnGet(int? code)
        {
            Code = code;
        }

        public void InsertErrorLog(string appName, string methodName, string message, string messageDetail, string sql, decimal userId, string machineIp)
        {
            var a = "a";
        }
    }
}
