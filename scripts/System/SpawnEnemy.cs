using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : triggerAction
{
    [SerializeField] GameObject enemy;
    
    public override void Action()
    {
        enemy.SetActive(true);
    }

    public void Spawn()
    {
        enemy.SetActive(true);
    }
}
