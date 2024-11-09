using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionTrigger : MonoBehaviour
{
    [SerializeField] private Light _light;
    void Start()
    {
        _light.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _light.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Heroik>())
        {
            _light.enabled = false;
        }
    }
}
