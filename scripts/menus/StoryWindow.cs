using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryWindow : PauseMenu
{
    public TextMeshProUGUI title_field;
    public TextMeshProUGUI description_field;
    
    public override void Activate(string title, string descr)
    {
        player_move.canControl = !player_move.canControl;
        if (opened)
        {
            Time.timeScale = 1.0f;
            opened = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Time.timeScale = 0f;
            opened = true;
            title_field.text = title;
            description_field.text = descr;
        }
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}
