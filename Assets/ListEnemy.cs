using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ListEnemy : MonoBehaviour
{
    [SerializeField] private LightAttack lightAttack;
    private List<GameObject> _enemies = new List<GameObject>(); // лист
    [SerializeField] private GameObject[] _enemiesArray;        // масив
    void Start()
    {
        _enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");
        // ArrayToList(_enemiesArray);
        // foreach (var enemy in _enemies)
        // {
        //     Debug.Log(enemy.gameObject.name);
        // }
    }

    public void TakeEnemiesArray()
    {
        foreach (var enemy in _enemiesArray)
        {
            enemy.gameObject.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
            //enemy.GetComponent<MeshRenderer>().material = lightAttack.specialMaterial;
            _enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");
        }
    }

    public List<GameObject> ArrayToList(GameObject[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            _enemies.Add(array[i]);
        }

        return _enemies;
    }

    public GameObject[] GetEnemiesArray()
    {
        return _enemiesArray;
    }

    
}
