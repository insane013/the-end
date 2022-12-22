using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class LightSource : Item
{
    override public void Use()
    {
        print("Lighting used");
        DestroyItem();
    }
}
