using System.Collections.Generic;
using UnityEngine;

public class BlenderRecipes : MonoBehaviour
{
    [SerializeField]private GameObject _wildBerryCocktail;
    [SerializeField]private GameObject _freshnessCocktail;
    [SerializeField]private GameObject _rubbish;
    [SerializeField] private List<GameObject> _requiredFreshnessCocktail;
    [SerializeField] private List<GameObject> _requiredWildBerryCocktail;

    public void Initialize(GameObject _wildBerryCocktail,GameObject _freshnessCocktail,GameObject _rubbish,
        List<GameObject> _requiredFreshnessCocktail,List<GameObject> _requiredWildBerryCocktail)
    {
        this._wildBerryCocktail = _wildBerryCocktail;
        this._freshnessCocktail = _freshnessCocktail;
        this._rubbish = _rubbish;
        this._requiredFreshnessCocktail = _requiredFreshnessCocktail;
        this._requiredWildBerryCocktail = _requiredWildBerryCocktail;
        
    }

    public GameObject GetWildBerryCocktail()
    {
        return _wildBerryCocktail;
    }
    public GameObject GetFreshnessCocktail()
    {
        return _freshnessCocktail;
    }
    public GameObject GetRubbish()
    {
        return _rubbish;
    }
    public List<GameObject> GetRequiredFreshnessCocktail()
    {
        return _requiredFreshnessCocktail;
    }
    public List<GameObject> GetRequiredWildBerryCocktail()
    {
        return _requiredWildBerryCocktail;
    }
    
}
