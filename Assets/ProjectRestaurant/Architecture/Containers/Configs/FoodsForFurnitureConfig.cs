using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FoodsForFurnitureConfig
{
    [SerializeField] private List<Product> listForFurniture;

    public List<Product> ListForFurniture => listForFurniture;
}
