using Cysharp.Threading.Tasks;
using System.IO;
using UnityEngine;

namespace GoogleSpreadsheets
{
    public class ImportSheetsGoogle
    {
        private const string SPREADSHEET_ID = "1egofOmJAB6kx-TRCUNWvBhR6os8RO5oIzNOYsXNL3t8";
        private const string CREDENTIALS_FILE = "cookingsimulator-144f6e89dc6c.json";
        
        private const string ITEM_SHEETS_NAME_FURNITURE = "FurnitureDemoLevel";
        private const string ITEM_SHEETS_NAME_FURNITURE_TRAINING = "FurnitureTrainingLevel";
        private const string ITEM_SHEETS_NAME_ENVIRONMENT = "EnvironmentDemoLevel";
        
        private string CredentialsPath =>
            Path.Combine(Application.streamingAssetsPath, CREDENTIALS_FILE);

        
        public async UniTask LoadItemsSettingsFurniture(StorageData storageData)
        {
            var sheetsImporter = new GoogleSheetsImporter(CredentialsPath, SPREADSHEET_ID);
            var itemParser = new FurnitureItemDataParser(storageData);
            await sheetsImporter.DownloadAndParseSheet(ITEM_SHEETS_NAME_FURNITURE, itemParser);
        }
        
        public async UniTask LoadItemsSettingsFurnitureTraining(StorageData storageData)
        {
            var sheetsImporter = new GoogleSheetsImporter(CredentialsPath, SPREADSHEET_ID);
            var itemParser = new FurnitureTrainingItemDataParser(storageData);
            await sheetsImporter.DownloadAndParseSheet(ITEM_SHEETS_NAME_FURNITURE_TRAINING, itemParser);
        }
        
        public async UniTask LoadItemsSettingsEnvironment(StorageData storageData)
        {
            var sheetsImporter = new GoogleSheetsImporter(CredentialsPath, SPREADSHEET_ID);
            var itemParser = new EnvironmentItemDataParser(storageData);
            await sheetsImporter.DownloadAndParseSheet(ITEM_SHEETS_NAME_ENVIRONMENT, itemParser);
        }
    }
}