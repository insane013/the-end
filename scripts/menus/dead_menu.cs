using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class dead_menu : PauseMenu
{
    [SerializeField] private string hp_dead_message;
    [SerializeField] private string psy_dead_message;
    [SerializeField] private string story_dead_message;
    [SerializeField] private string sleep_dead_message;

    [SerializeField] private TextMeshProUGUI dead_reason;

    public override void Activate(string type, string empty) 
    {
        this.gameObject.SetActive(true);
        switch (type)
        {
            case "hp":
                dead_reason.text = hp_dead_message;
                break;
            case "psy":
                dead_reason.text = psy_dead_message;
                break;
            case "story":
                dead_reason.text = story_dead_message;
                break;
            case "sleep":
                dead_reason.text = sleep_dead_message;
                break;
            default:
                break;
        }
    }

    private void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void Respawn()
    {
        FindObjectOfType<Player>().Respawn();
        Deactivate();
    }
}
