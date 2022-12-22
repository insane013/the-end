using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireLit : MonoBehaviour
{
    [SerializeField] private GameObject lit;
    [SerializeField] private GameObject CampFireOBJ;
    public bool isFlameNow = true;

    private void Update()
    {
        switch(isFlameNow)
        {
            case true:
                lit.SetActive(true); CampFireOBJ.SetActive(true); break;
            case false:
                lit.SetActive(false); CampFireOBJ.SetActive(false); break;
        }
    }

    void Activate()
    {
        isFlameNow = !isFlameNow;
    }
}
