using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Medicine : Item
{
    public float hp_restore = 70f;
    override public void Use()
    {
        print("Medicine used");
        hub HUB = GameObject.FindGameObjectWithTag("HUB").GetComponent<hub>();
        HUB.player.GetComponent<Player>().hp += hp_restore;
        DestroyItem();
    }
}
