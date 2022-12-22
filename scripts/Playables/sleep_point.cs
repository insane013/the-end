using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sleep_point : ActiveTarget
{
    public override void Activate()
    {
        hub HUB = GameObject.FindGameObjectWithTag("HUB").GetComponent<hub>();
        HUB.sleepScreen.Activate("", "");
    }
}
