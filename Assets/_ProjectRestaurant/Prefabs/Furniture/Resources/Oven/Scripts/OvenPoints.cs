using System;
using UnityEngine;

namespace OvenFurniture
{
    public class OvenPoints : IDisposable
    {
        private Transform _pointUp;
        private Transform _positionIngredient;
        
        public Transform PointUp => _pointUp;
        public Transform PositionIngredient => _positionIngredient;
    
        internal OvenPoints(Transform pointUp, Transform positionIngredient)
        {
            _pointUp = pointUp;
            _positionIngredient = positionIngredient;
            
            Debug.Log("Создан объект: OvenPoints");
        }
    
        public void Dispose()
        {
            Debug.Log("У объекта вызван Dispose : OvenPoints");
        }
    }
}

