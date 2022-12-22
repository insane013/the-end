using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tp_to_spawn : MonoBehaviour
{
    public GameObject player;
    private void Update()
    {
        player.transform.position = player.GetComponent<Player>().respawn_point.transform.position;
        gameObject.SetActive(false);
    }

}
