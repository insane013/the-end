using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetItem : ActiveTarget
{
    public Item item;

    [SerializeField] private GameObject point;
    [SerializeField] private Vector3 screen_pick_pos;
    [SerializeField] private Vector3 screen_pick_rot_eul;
    
    private Quaternion screen_pick_rot;

    private hub HUB;
    private GameObject pickScreen;

    private void Start()
    {
        HUB = GameObject.FindGameObjectWithTag("HUB").GetComponent<hub>();
        pickScreen = HUB.pickScreen;
        if (pickScreen == null)
        {
            print("PickScreen not found!");
        }

        target_name = item.item_name;
    }

    public override void Activate()
    {
        //pickScreen = GameObject.Find("pickScreen") as GameObject;
        screen_pick_rot = Quaternion.Euler(screen_pick_rot_eul);
        Info i = new Info(point, screen_pick_pos, screen_pick_rot, this.gameObject, item);
        pickScreen.SetActive(true);
        pickScreen.GetComponent<PickScreen>().ChangeActive(true);
        pickScreen.SendMessage("SetValues", i, SendMessageOptions.DontRequireReceiver);
        pickScreen.SendMessage("Pick", SendMessageOptions.DontRequireReceiver);
    }
}
