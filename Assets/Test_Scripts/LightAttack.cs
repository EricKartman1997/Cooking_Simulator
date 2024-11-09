using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightAttack : MonoBehaviour
{
    public Material defaultMaterial;
    public Material attackMaterial;
    public Material inactionMaterial;
    public Material specialMaterial;
    public void ChangeColorShere(Material material)
    {
        GetComponent<MeshRenderer>().material = material;
    }

    private void Start()
    {
        ChangeColorShere(inactionMaterial);
    }
}
