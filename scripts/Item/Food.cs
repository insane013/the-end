using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Food : Item
{
    public float hp_restore = 10f;
    override public void Use()
    {
        hub HUB = GameObject.FindGameObjectWithTag("HUB").GetComponent<hub>();
        HUB.player.GetComponent<Player>().hp += hp_restore;
        DestroyItem();
    }
}
