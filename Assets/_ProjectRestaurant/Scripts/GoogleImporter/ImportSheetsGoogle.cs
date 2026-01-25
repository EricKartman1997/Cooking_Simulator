using Cysharp.Threading.Tasks;

namespace GoogleSpreadsheets
{
    public class ImportSheetsGoogle
    {
        private const string SPREADSHEET_ID = "1egofOmJAB6kx-TRCUNWvBhR6os8RO5oIzNOYsXNL3t8";
        private const string CREDENTIALS_PATH = "cookingsimulator-e8861e55a891.json";
        
        private const string ITEM_SHEETS_NAME_FURNITURE = "FurnitureDemoLevel";
        private const string ITEM_SHEETS_NAME_ENVIRONMENT = "EnvironmentDemoLevel";
        
        // public async UniTask LoadItemsSettings(FactoryEnvironment factoryEnvironment)
        // {
        //     var sheetsImporter = new GoogleSheetsImporter(CREDENTIALS_PATH, SPREADSHEET_ID);
        //     var itemParser = new FurnitureItemDataParser(factoryEnvironment);
        //     await sheetsImporter.DownloadAndParseSheet(ITEM_SHEETS_NAME,itemParser);
        // }
        
        public async UniTask LoadItemsSettingsFurniture(StorageData storageData)
        {
            var sheetsImporter = new GoogleSheetsImporter(CREDENTIALS_PATH, SPREADSHEET_ID);
            var itemParser = new FurnitureItemDataParser(storageData);
            await sheetsImporter.DownloadAndParseSheet(ITEM_SHEETS_NAME_FURNITURE,itemParser);
        }
        
        public async UniTask LoadItemsSettingsEnvironment(StorageData storageData)
        {
            var sheetsImporter = new GoogleSheetsImporter(CREDENTIALS_PATH, SPREADSHEET_ID);
            var itemParser = new EnvironmentItemDataParser(storageData);
            await sheetsImporter.DownloadAndParseSheet(ITEM_SHEETS_NAME_ENVIRONMENT,itemParser);
        }
    }
}

