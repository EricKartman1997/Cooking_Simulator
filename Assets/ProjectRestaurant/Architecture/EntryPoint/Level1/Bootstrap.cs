using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private List<AssetReference> rawFoodList;
    [SerializeField] private List<AssetReference> cookedFoodList;
    [SerializeField] private List<AssetReference> otherFoodList;
    [SerializeField] private Slider progressSlider;
    
    private AssetReferencesDisposer _assetReferencesDisposer;
    private int _totalAssetsToLoad;
    private int _loadedAssetsCount;
    
    private void Awake()
    {
        _totalAssetsToLoad = rawFoodList.Count + cookedFoodList.Count + otherFoodList.Count;
        _loadedAssetsCount = 0;
        
        if (progressSlider != null)
        {
            progressSlider.minValue = 0;
            progressSlider.maxValue = _totalAssetsToLoad;
            progressSlider.value = 0;
        }
        
        _assetReferencesDisposer = new AssetReferencesDisposer(rawFoodList, cookedFoodList, otherFoodList);
        StartCoroutine(LoadAssetsWithProgress());
    }
    
    private IEnumerator LoadAssetsWithProgress()
    {
        yield return StartCoroutine(_assetReferencesDisposer.Initialize(OnAssetLoaded));
        yield return new WaitForSeconds(1); 
        SceneManager.LoadScene("SampleScene");
    }
    
    private void OnAssetLoaded()
    {
        _loadedAssetsCount++;
        if (progressSlider != null)
        {
            progressSlider.value = _loadedAssetsCount;
        }
        Debug.Log($"Загружено: {_loadedAssetsCount}/{_totalAssetsToLoad}");
    }
    

}
