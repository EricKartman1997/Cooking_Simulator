using Cysharp.Threading.Tasks;

namespace GoogleSpreadsheets
{
    public class ImportSheetsGoogle
    {
        private const string SPREADSHEET_ID = "1egofOmJAB6kx-TRCUNWvBhR6os8RO5oIzNOYsXNL3t8";
        private const string ITEM_SHEETS_NAME = "DemoLevel";
        private const string CREDENTIALS_PATH = "cookingsimulator-e8861e55a891.json";
        
        public async UniTask LoadItemsSettings(FactoryEnvironment factoryEnvironment)
        {
            var sheetsImporter = new GoogleSheetsImporter(CREDENTIALS_PATH, SPREADSHEET_ID);
            var itemParser = new FurnitureItemDataParser(factoryEnvironment);
            await sheetsImporter.DownloadAndParseSheet(ITEM_SHEETS_NAME,itemParser);
        }
    }
}

