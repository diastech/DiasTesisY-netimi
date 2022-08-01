using System.ComponentModel;

namespace DiasShared.Enums.Standart
{
    public class LogEnums
    {
        public enum ApplicationNLogEnvironment
        {
            [Description("Development")]
            Development,

            [Description("Test")]
            Test,

            [Description("Live")]
            Live
        }
    }
}
