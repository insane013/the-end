using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string item_name;
    public string item_description;
    public int count;
    public int max;
    public int ID;
    public Enumerations.ItemSlot item_slot;
    public Sprite item_ico;

    private hub HUB;
    void Start() => HUB = GameObject.FindGameObjectWithTag("HUB").GetComponent<hub>();

    virtual public void Use()
    {

    }
    public void DestroyItem()
    {
        Player pl = HUB.player.GetComponent<Player>();
        pl.inventory[(int)item_slot].count--;
        if (pl.inventory[(int)item_slot].count < 1)
        {
            pl.inventory[(int)item_slot] = null;
        }
    }
}
public class Info
{
    public GameObject point;
    public Vector3 pos;
    public Quaternion rot;
    public GameObject g;

    public Item item;

    public Info(GameObject p, Vector3 ps, Quaternion rt, GameObject gm, Item i)
    {
        point = p; pos = ps; rot = rt; g = gm; item = i;
    }
}

public class Enumerations
{
    public enum ItemSlot { weapon = 0, light = 1, drink = 2, food = 3, medicine = 4, psy = 5 };
}

[Serializable]
public class Debuff
{
    public float hp_damage = 1;
    public float psy_damage = 1;
    public float hp_period = 1f;
    public float psy_period = 1f;


    private float hp_timer;
    private float psy_timer;

    [HideInInspector] public bool activated = true;

    public void Update(Player player)
    {
        if (activated)
        {
            hp_timer += Time.deltaTime;
            psy_timer += Time.deltaTime;
        } else
        {
            hp_timer = 0f;
            psy_timer = 0f;
        }

        if (hp_timer >= hp_period)
        {
            player.DMG(hp_damage);
            hp_timer = 0;
        }
        if (psy_timer >= psy_period)
        {
            player.psyDMG(psy_damage);
            psy_timer = 0;
        }
    }
}

[Serializable]
public class triggerAction : MonoBehaviour
{


    public virtual void Action()
    {

    }
}

[Serializable]
public class ScenePlayData
{
    public Phrase[] texts;

    public bool skip = false;
    public bool fade = true;
    public bool looseControl = false;
    public bool stopTime = false;
    public bool hideUI = false;

    public float print_delay = 0.1f;

}

public static class DataHodler
{
    private static bool skipIntro = false;

    public static bool skipMainIntro
    {
        get
        {
            return skipIntro;
        }
        set
        {
            skipIntro = value;
        }
    }
}

