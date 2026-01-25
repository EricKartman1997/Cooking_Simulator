using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReadStorageData
{
    List<FurnitureItemData> ItemsFurnitureListRead { get; }
    List<EnvironmentItemData> ItemsEnvironmentListRead { get; }
}
