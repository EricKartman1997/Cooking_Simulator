using System.Collections.Generic;

public interface IReadStorageData
{
    List<FurnitureItemData> ItemsFurnitureListRead { get; }
    List<FurnitureItemData> ItemsFurnitureTrainingListRead { get; }
    List<EnvironmentItemData> ItemsEnvironmentListRead { get; }
}
