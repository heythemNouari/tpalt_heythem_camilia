using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace heythem_Demo.Service
{
    public class BlobService
    {
        public CloudBlobClient GetConnection()
        {
           
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=tpaltstorage;AccountKey=D2+WI/HsEupjVHvsWKSzcHHa+PQ4o/0DoT1A0glGLj7EAIBGuRgXqerNCyx6eWS2iZs//jFyfiSpxBdqvBmy3Q==;EndpointSuffix=core.windows.net");
            return cloudStorageAccount.CreateCloudBlobClient();

        }
    }
}
