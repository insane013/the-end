using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Weapon : Item
{
    public int weapon_id;
    override public void Use()
    {
        hub HUB = GameObject.FindGameObjectWithTag("HUB").GetComponent<hub>();
        HUB.player.GetComponent<Player>().UseWeapon(this);
        
    }
}
