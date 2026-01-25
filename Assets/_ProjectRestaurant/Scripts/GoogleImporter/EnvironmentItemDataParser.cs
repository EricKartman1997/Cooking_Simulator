using UnityEngine;
using System;
using System.Globalization;

namespace GoogleSpreadsheets
{
    public class EnvironmentItemDataParser: IGoogleSheetParser
    {
        private readonly StorageData _storageData;
        private EnvironmentItemData _currentItemData;
        
        public EnvironmentItemDataParser(StorageData storageData)
        {
            _storageData = storageData;
        }
        
         public void Parse(string header, string token)
        {
            switch (header)
            {
                case "ID":
                    _currentItemData = new EnvironmentItemData
                    {
                        Id = Convert.ToInt32(token)
                    };
                    _storageData._itemsEnvironmentList.Add(_currentItemData);
                    break;
        
                case "Name":
                    _currentItemData.Name = token;
                    break;
        
                case "Position":
                    _currentItemData.Position = new Vector3Data(ParseVector3(token));
                    break;
                
                case "Rotation":
                    _currentItemData.Rotation = new Vector3Data(ParseVector3(token));
                    break;
                default:
                    throw new Exception($"Invalid header: {header}");
            }
        }

        private Vector3 ParseVector3(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                Debug.Log("ZERO-1");
                return Vector3.zero;
            }

            token = token.Replace(',', '.'); // если в Google Sheets числа через запятую

            var parts = token.Split(';');
            if (parts.Length != 3)
            {
                Debug.Log("ZERO-2");
                return Vector3.zero;
            }
            
            if (float.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float x) &&
                float.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float y) &&
                float.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out float z))
            {
                //Debug.Log($"X = {x}, Y = {y}, Z = {z}");
                return new Vector3(x, y, z);
            }
            Debug.Log("ZERO-3");
            return Vector3.zero;
        }
    }
}