using System;
using UnityEngine;

public class cutscene : MonoBehaviour
{
    public bool MainIntro = false;

    [SerializeField] private ScenePlayData ScenePlayOptions;
    [SerializeField] private ScenePlayer scene_player;

    [SerializeField] private triggerAction[] action;

    private void Start()
    {
        if (MainIntro)
        {
            ScenePlayOptions.skip = DataHodler.skipMainIntro;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (scene_player.Action(ScenePlayOptions))
            {
                if (action != null && !ScenePlayOptions.skip)
                {
                    foreach (var item in action)
                    {
                        item.Action();

                    }
                }
                this.gameObject.SetActive(false);
            }
        }
    }
}

[Serializable]
public class Phrase
{
    public string text;
    public float duration;

    public Phrase(string txt, float dur)
    {
        text = txt;
        duration = dur;
    }
}
