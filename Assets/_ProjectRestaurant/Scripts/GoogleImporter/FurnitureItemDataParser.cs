using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace GoogleSpreadsheets
{
    public class FurnitureItemDataParser: IGoogleSheetParser
    {
        private readonly FactoryEnvironment _factoryEnvironment;
        private FurnitureItemData _currentItemData;
        
        public FurnitureItemDataParser(FactoryEnvironment factoryEnvironment)
        {
            _factoryEnvironment = factoryEnvironment;
            _factoryEnvironment._itemsList = new List<FurnitureItemData>();
        }
        
        public void Parse(string header, string token)
        {
            switch (header)
            {
                case "ID":
                    _currentItemData = new FurnitureItemData
                    {
                        Id = Convert.ToInt32(token)
                    };
                    _factoryEnvironment._itemsList.Add(_currentItemData);
                    break;

                case "Name":
                    _currentItemData.Name = token;
                    break;

                case "Position":
                    _currentItemData.Position = ParseVector3(token);
                    break;
                
                case "Rotation": 
                    _currentItemData.Position = ParseVector3(token);
                    break;

                case "DecorationTableTop":
                    _currentItemData.DecorationTableTop = (EnumDecorationTableTop)Enum.Parse(typeof(EnumDecorationTableTop), token, true);
                    break;

                case "DecorationLowerSurface":
                    _currentItemData.DecorationLowerSurface = (EnumDecorationLowerSurface)Enum.Parse(typeof(EnumDecorationLowerSurface), token, true);
                    break;
                
                case "GiveFood":
                    _currentItemData.GiveFood = (EnumGiveFood)Enum.Parse(typeof(EnumGiveFood), token, true);
                    break;

                case "ViewFood":
                    _currentItemData.ViewFood = (EnumViewFood)Enum.Parse(typeof(EnumViewFood), token, true);
                    break;

                default:
                    throw new Exception($"Invalid header: {header}");
            }
        }

        private Vector3 ParseVector3(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return Vector3.zero;

            token = token.Replace(',', '.'); // если в Google Sheets числа через запятую

            var parts = token.Split(';');
            if (parts.Length != 3)
                return Vector3.zero;

            if (float.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float x) &&
                float.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float y) &&
                float.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out float z))
            {
                return new Vector3(x, y, z);
            }

            return Vector3.zero;
        }
    }
}