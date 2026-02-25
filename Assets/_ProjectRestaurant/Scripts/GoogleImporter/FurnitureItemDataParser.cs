using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace GoogleSpreadsheets
{
    public class FurnitureItemDataParser: ItemDataParser
    {
        private const string NAMESHEET = "FurnitureDemoLevel";
        private FurnitureItemData _currentItemData;
        private List<FurnitureItemData> _itemsList = new List<FurnitureItemData>();

        public override string NameSheet => NAMESHEET;

        public override void Parse(string header, string token)
        {
            switch (header)
            {
                case "ID":
                    _currentItemData = new FurnitureItemData
                    {
                        Id = Convert.ToInt32(token)
                    };
                    _itemsList.Add(_currentItemData);
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


                case "DecorationTableTop":
                    _currentItemData.DecorationTableTop = (CustomFurnitureName)Enum.Parse(typeof(CustomFurnitureName), token, true);
                    break;

                case "DecorationLowerSurface":
                    _currentItemData.DecorationLowerSurface = (CustomFurnitureName)Enum.Parse(typeof(CustomFurnitureName), token, true);
                    break;
                
                case "GiveFood":
                    _currentItemData.GiveFood = (IngredientName)Enum.Parse(typeof(IngredientName), token, true);
                    break;

                case "ViewFood":
                    _currentItemData.ViewFood = (ViewDishName)Enum.Parse(typeof(ViewDishName), token, true);
                    break;

                default:
                    throw new Exception($"Invalid header: {header}");
            }
        }
        
        public override void Save()
        {
            FurnitureItems saveObj = new FurnitureItems(_itemsList);
            JsonHandler.Save(JsonPathName.FURNITURE_ITEMS_PATH,saveObj);
        }

        private Vector3 ParseVector3(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                Debug.Log("ZERO-1");
                return Vector3.zero;
            }

            token = token.Replace(',', '.');

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
                return new Vector3(x, y, z);
            }
            Debug.Log("ZERO-3");
            return Vector3.zero;
        }
    }
}