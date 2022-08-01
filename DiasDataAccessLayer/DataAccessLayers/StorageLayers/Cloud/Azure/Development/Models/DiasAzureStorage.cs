using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DiasDataAccessLayer.InterfacesAbstracts.Interfaces.StorageLayers.Cloud.Azure.Models;
using DiasDataAccessLayer.Shared.Configuration;
using DiasShared.Operations.EnumOperations;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using static DiasShared.Enums.ApplicationEnums;

namespace DiasDataAccessLayer.DataAccessLayers.StorageLayers.Cloud.Azure.Development.Models
{
    public class DiasAzureStorage : IDiasAzureStorage
    {
        //Bu property içeriden üretilecektir
        private BlobServiceClient BlobServiceClientObj { get; set; }

        //Bu property içeriden üretilecektir
        private BlobContainerClient BlobContainerClientObj { get; set; }

        //Bu property içeriden üretilecektir
        private BlobClient BlobClientObj { get; set; }

        //Bu property içeriden üretilecektir
        private BlobDownloadInfo BlobDownloadInfoObj { get; set; }

        public DiasAzureStorage()
        {
            IConfiguration settings = ConfigurationHelper.GetConfig();

            string conString = String.Empty;

            //Azure storage'ın connection stringini al
            switch (ConfigurationHelper.GetAzureStorageEnvironment(settings))
            {
                case AzureStorageEnvironment.Development:
                    {
                        conString = settings.GetSection("ConnectionStrings").
                            GetSection(AzureStorageServiceName.DiasAzureStorage.DescriptionAttr()).GetSection("DevelopmentConnection").Value;
                        break;
                    }

                case AzureStorageEnvironment.Test:
                    {
                        conString = settings.GetSection("ConnectionStrings").
                             GetSection(AzureStorageServiceName.DiasAzureStorage.DescriptionAttr()).GetSection("TestConnection").Value;
                        break;
                    }

                case AzureStorageEnvironment.Live:
                    {
                        conString = settings.GetSection("ConnectionStrings").
                             GetSection(AzureStorageServiceName.DiasAzureStorage.DescriptionAttr()).GetSection("LiveConnection").Value;
                        break;
                    }

                default:
                    {
                        conString = settings.GetSection("ConnectionStrings").
                             GetSection(AzureStorageServiceName.DiasAzureStorage.DescriptionAttr()).GetSection("DevelopmentConnection").Value;
                        break;
                    }
            }

            // Container client nesnesini üretebilmemiz için BlobServiceClient nesnesini üret
            BlobServiceClientObj = new BlobServiceClient(conString);

            //Diğer initializelar
            BlobContainerClientObj = null;
            BlobClientObj = null;
            BlobDownloadInfoObj = null;
        }

        public DiasAzureStorage(AzureStorageBlobContainers azureStorageBlobContainer)
        {
            if ((azureStorageBlobContainer.DescriptionAttr() == null) ||
                    (azureStorageBlobContainer.DescriptionAttr() == ""))
            {
                //hata
                BlobServiceClientObj = null;
                BlobContainerClientObj = null;
                BlobClientObj = null;
                BlobDownloadInfoObj = null;
            }
            else
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                string conString = String.Empty;

                //Azure storage'ın connection stringini al
                switch (ConfigurationHelper.GetAzureStorageEnvironment(settings))
                {
                    case AzureStorageEnvironment.Development:
                        {
                            conString = settings.GetSection("ConnectionStrings").GetSection("DiasAzureStorage").GetSection("DevelopmentConnection").Value;
                            break;
                        }

                    case AzureStorageEnvironment.Test:
                        {
                            conString = settings.GetSection("ConnectionStrings").GetSection("DiasAzureStorage").GetSection("TestConnection").Value;
                            break;
                        }

                    case AzureStorageEnvironment.Live:
                        {
                            conString = settings.GetSection("ConnectionStrings").GetSection("DiasAzureStorage").GetSection("LiveConnection").Value;
                            break;
                        }

                    default:
                        {
                            conString = settings.GetSection("ConnectionStrings").GetSection("DiasAzureStorage").GetSection("DevelopmentConnection").Value;
                            break;
                        }
                }

                // Container client nesnesini üretebilmemiz için BlobServiceClient nesnesini üret
                BlobServiceClientObj = new BlobServiceClient(conString);

                BlobContainerClientObj = BlobServiceClientObj.GetBlobContainerClient(azureStorageBlobContainer.DescriptionAttr());

                if (BlobContainerClientObj == null)
                {
                    //hata
                    BlobServiceClientObj = null;
                    BlobContainerClientObj = null;
                    BlobClientObj = null;
                    BlobDownloadInfoObj = null;
                }
                else
                {
                    BlobClientObj = null;
                    BlobDownloadInfoObj = null;
                }
            }
        }

        public async Task<Tuple<ErrorCodes, object>> UploadFileToBlobContainerAsync(byte[] fileContent, string fileName)
        {
            if ((fileContent == null) || (fileContent.Length == 0) || (fileName == null) || (fileName == ""))
            {
                return new Tuple<ErrorCodes, object>(ErrorCodes.UnknownError, null);
            }
            else if ((BlobServiceClientObj == null) || (BlobContainerClientObj == null))
            {
                BlobServiceClientObj = null;
                BlobContainerClientObj = null;

                return new Tuple<ErrorCodes, object>(ErrorCodes.UnknownError, null);
            }
            else
            {
                byte[] returnContentByteArr;

                try
                {
                    //bloba referans al
                    BlobClientObj = BlobContainerClientObj.GetBlobClient(fileName);

                    using (MemoryStream stream = new MemoryStream(fileContent, writable: false))
                    {
                        await BlobClientObj.UploadAsync(stream);
                        returnContentByteArr = stream.ToArray();
                    }
                }
                //Azure tarafındaki hatalar
                catch (RequestFailedException)
                {
                    return new Tuple<ErrorCodes, object>(ErrorCodes.UnknownError, null);
                }
                catch (Exception)
                {
                    return new Tuple<ErrorCodes, object>(ErrorCodes.UnknownError, null);
                }

                return new Tuple<ErrorCodes, object>(ErrorCodes.None, (object)returnContentByteArr);
            }
        }

        public async Task<Tuple<ErrorCodes, object>> DownloadFileFromBlobContainerToByteArrayAsync(string fileName)
        {
            if ((fileName == null) || (fileName == ""))
            {
                return new Tuple<ErrorCodes, object>(ErrorCodes.UnknownError, null);
            }
            else if ((BlobServiceClientObj == null) || (BlobContainerClientObj == null))
            {
                BlobServiceClientObj = null;
                BlobContainerClientObj = null;

                return new Tuple<ErrorCodes, object>(ErrorCodes.UnknownError, null);
            }
            else
            {
                byte[] returnContentByteArr;

                try
                {
                    //bloba referans al
                    BlobClientObj = BlobContainerClientObj.GetBlobClient(fileName);

                    //Blob içeriğini download et
                    BlobDownloadInfoObj = await BlobClientObj.DownloadAsync();

                    //Download edlimiş içeriği byte arraye çevir
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await BlobDownloadInfoObj.Content.CopyToAsync(ms);
                        returnContentByteArr = ms.ToArray();
                    }
                }
                //Azure tarafındaki hatalar
                catch (RequestFailedException)
                {
                    return new Tuple<ErrorCodes, object>(ErrorCodes.UnknownError, null);
                }
                catch (Exception)
                {
                    return new Tuple<ErrorCodes, object>(ErrorCodes.UnknownError, null);
                }

                return new Tuple<ErrorCodes, object>(ErrorCodes.None, (object)returnContentByteArr);
            }
        }

        public async Task<Tuple<ErrorCodes, object>> DeleteFileFromBlobContainerAsync(string fileName)
        {
            if ((fileName == null) || (fileName == ""))
            {
                return new Tuple<ErrorCodes, object>(ErrorCodes.UnknownError, false);
            }
            else if ((BlobServiceClientObj == null) || (BlobContainerClientObj == null))
            {
                BlobServiceClientObj = null;
                BlobContainerClientObj = null;

                return new Tuple<ErrorCodes, object>(ErrorCodes.UnknownError, false);
            }
            else
            {
                try
                {
                    //bloba referans al
                    BlobClientObj = BlobContainerClientObj.GetBlobClient(fileName);

                    //Blob içeriğini sil et
                    Response<bool> resultDeleteOperation = await BlobClientObj.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

                    if (!(resultDeleteOperation.Value))
                    {
                        return new Tuple<ErrorCodes, object>(ErrorCodes.UnknownError, false);

                    }
                    else if (resultDeleteOperation.GetRawResponse().Status != 202)//Not Accepted
                    {
                        return new Tuple<ErrorCodes, object>(ErrorCodes.UnknownError, false);
                    }
                    else
                    {
                        return new Tuple<ErrorCodes, object>(ErrorCodes.None, (object)resultDeleteOperation.Value);
                    }
                }
                //Azure tarafındaki hatalar
                catch (RequestFailedException)
                {
                    return new Tuple<ErrorCodes, object>(ErrorCodes.UnknownError, false);
                }
                catch (Exception)
                {
                    return new Tuple<ErrorCodes, object>(ErrorCodes.UnknownError, false);
                }

            }
        }
    }
}
