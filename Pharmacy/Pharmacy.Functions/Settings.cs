using System;

namespace Pharmacy.Functions
{
    public class Settings
    {
        public static string AzureWebJobsStorage = Environment.GetEnvironmentVariable("AzureWebJobsStorage");

        public static string DocumentDB = Environment.GetEnvironmentVariable("CosmosDB_URI");
        public static string DocumentDBAuthKey = Environment.GetEnvironmentVariable("CosmosDB_AuthKey");
        public static string DatabaseId = Environment.GetEnvironmentVariable("CosmosDB_DatabaseId");
        public static string ProductsCollectionId = Environment.GetEnvironmentVariable("CosmosDB_ProductsCollection");
        public static string PharmaciesCollectionId = Environment.GetEnvironmentVariable("CosmosDB_PharmaciesCollection");
        public static string ProductAvailabilityCollectionId = Environment.GetEnvironmentVariable("CosmosDB_ProductAvailabilityCollection");

        public static string NotificationAccessSignature = Environment.GetEnvironmentVariable("NotificationHub_Access_Signature");
        public static string NotificationHubName = Environment.GetEnvironmentVariable("NotificationHub_Name");

        public static string CryptographyKey = Environment.GetEnvironmentVariable("Cryptography_Key");

        public static string MobileCenterID_Android = Environment.GetEnvironmentVariable("MobileCenterID_Android");

        public static string WebChatUrl = Environment.GetEnvironmentVariable("WebChat_Url");
        public static string FirebaseAppSenderId = Environment.GetEnvironmentVariable("FirebaseAppSenderId");
    }
}