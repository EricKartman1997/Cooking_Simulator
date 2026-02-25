using Cysharp.Threading.Tasks;
using System.IO;
using UnityEngine;

namespace GoogleSpreadsheets
{
    public class ImportSheetsGoogle
    {
        private const string SPREADSHEET_ID = "1egofOmJAB6kx-TRCUNWvBhR6os8RO5oIzNOYsXNL3t8";
        private const string CREDENTIALS_FILE = "cookingsimulator-144f6e89dc6c.json";
        
        private string CredentialsPath =>
            Path.Combine(Application.streamingAssetsPath, CREDENTIALS_FILE);

        public async UniTask LoadItemsSettings<T>(IStorageJson jsonHandler) where T : ItemDataParser, new()
        {
            var sheetsImporter = new GoogleSheetsImporter(CredentialsPath, SPREADSHEET_ID);
            var itemParser = new T();
            itemParser.Init(jsonHandler);
            await sheetsImporter.DownloadAndParseSheet(itemParser.NameSheet, itemParser);
        }
    }
}