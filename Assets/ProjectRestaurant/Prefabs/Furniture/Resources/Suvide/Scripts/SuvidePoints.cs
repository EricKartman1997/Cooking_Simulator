using System;
using UnityEngine;

namespace SuvideFurniture
{
    public class SuvidePoints : IDisposable
    {
        private Transform _firstPointIngredient;
        private Transform _secondPointIngredient;
        private Transform _thirdPointIngredient;
        
        private Transform _firstPointResult;
        private Transform _secondPointResult;
        private Transform _thirdPointResult;
        
        public Transform PointIngredient1 => _firstPointIngredient;
    
        public Transform PointIngredient2 => _secondPointIngredient;
    
        public Transform PointIngredient3 => _thirdPointIngredient;
    
        public Transform PointResult1 => _firstPointResult;
    
        public Transform PointResult2 => _secondPointResult;
    
        public Transform PointResult3 => _thirdPointResult;
        
        public SuvidePoints(Transform firstPointIngredient, Transform secondPointIngredient, Transform thirdPointIngredient, Transform firstPointResult, Transform secondPointResult, Transform thirdPointResult)
        {
            _firstPointIngredient = firstPointIngredient;
            _secondPointIngredient = secondPointIngredient;
            _thirdPointIngredient = thirdPointIngredient;
            _firstPointResult = firstPointResult;
            _secondPointResult = secondPointResult;
            _thirdPointResult = thirdPointResult;
            
            Debug.Log("Создал объект: SuvidePoints");
        }
    
        public void Dispose()
        {
            Debug.Log("У объекта вызван Dispose : SuvidePoints");
        }
    }

}
