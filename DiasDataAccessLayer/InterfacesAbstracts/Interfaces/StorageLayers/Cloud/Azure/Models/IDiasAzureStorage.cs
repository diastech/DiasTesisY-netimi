using System;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;

namespace DiasDataAccessLayer.InterfacesAbstracts.Interfaces.StorageLayers.Cloud.Azure.Models
{
    public interface IDiasAzureStorage
    {
        public Task<Tuple<ErrorCodes, object>> UploadFileToBlobContainerAsync(byte[] fileContent, string fileName);

        public Task<Tuple<ErrorCodes, object>> DownloadFileFromBlobContainerToByteArrayAsync(string fileName);

        public Task<Tuple<ErrorCodes, object>> DeleteFileFromBlobContainerAsync(string fileName);
    }
}
