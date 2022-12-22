using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PSY : Item
{
    public float psy_restore = 30f;
    override public void Use()
    {
        print("psy used");
        hub HUB = GameObject.FindGameObjectWithTag("HUB").GetComponent<hub>();
        HUB.player.GetComponent<Player>().psy += psy_restore;
        DestroyItem();
    }
}