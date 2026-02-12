using System;
using UnityEngine;

namespace CuttingTableFurniture
{
    public class CuttingTablePoints : IDisposable
    {
        private Transform _positionIngredient1; 
        private Transform _positionIngredient2; 
        private Transform _positionResult;   
    
        internal CuttingTablePoints(Transform positionIngredient1, Transform positionIngredient2, Transform positionResult)
        {
            _positionIngredient1 = positionIngredient1;
            _positionIngredient2 = positionIngredient2;
            _positionResult = positionResult;
            
            //Debug.Log("Создать объект: CuttingTablePoints");
        }
        
        public void Dispose()
        {
            Debug.Log("У объекта вызван Dispose : CuttingTablePoints");
        }
    
        public Transform PositionIngredient1 => _positionIngredient1;
    
        public Transform PositionIngredient2 => _positionIngredient2;
    
        public Transform PositionResult => _positionResult;
        
    }

}
