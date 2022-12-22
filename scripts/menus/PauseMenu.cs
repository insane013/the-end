using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected Move player_move;

    [SerializeField] protected GameObject crosshair;

    protected bool opened = false;
    protected hub HUB;

    private void Start()
    {
        HUB = GameObject.FindGameObjectWithTag("HUB").GetComponent<hub>();
    }

    private void OnDestroy()
    {
        print("Destroyed");
    }

    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        crosshair.SetActive(false);

        Time.timeScale = 0f;
        player_move.canControl = false;
        opened = true;
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        crosshair.SetActive(true);

        player_move.canControl = true;
        Time.timeScale = 1.0f;
        opened = false;

    }

    public virtual void Activate(string title, string descr)
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }

    public virtual void Close()
    {
        Activate("", "");
        HUB.player.GetComponent<Player>().activation = false;
        this.gameObject.SetActive(false);
    }
}
