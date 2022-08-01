using System.ComponentModel;

namespace DiasShared.Enums
{
    public class ApplicationEnums
    {
        public enum ApplicationDatabaseType
        {
            [Description("SqlServer")]
            SqlServer
        }

        public enum DatabaseEnvironment
        {
            [Description("Development")]
            Development,

            [Description("Test")]
            Test,

            [Description("Live")]
            Live,

            [Description("MobileDevelopment")]
            DevelopmentMobile,

            [Description("MobileTest")]
            TestMobile,

            [Description("MobileLive")]
            LiveMobile

        }

        public enum AzureStorageEnvironment
        {
            [Description("Development")]
            Development,

            [Description("Test")]
            Test,

            [Description("Live")]
            Live
        }
             

        public enum AzureStorageServiceName
        {
            [Description("DiasAzureStorage")]
            DiasAzureStorage
        }

        public enum AzureStorageBlobContainers
        {
            [Description("content")]
            Content,

            [Description("userdoc")]
            UserDocument,          
        }

        public enum AzureStorageBlobContainerVirtualFolders
        {
            [Description("certificate/")]
            Certificate,

            [Description("identity_document/")]
            IdentityDocument,

            [Description("photo/")]
            photo

        }
    }
}
