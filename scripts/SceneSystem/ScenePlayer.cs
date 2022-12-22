using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScenePlayer : MonoBehaviour
{
    [SerializeField] private GameObject hud;
    [SerializeField] private Image fading;
    [SerializeField] private Text subtitres;
    [SerializeField] private GameObject subtitres_zone;

    [SerializeField] private Move player_move;
    [SerializeField] private Player player;

    private float alpha = 0.5f;
    private bool new_row = true;
    private string writing_string = "";

    bool isPlayingNow = false;

    private ScenePlayData data;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    public bool Action(ScenePlayData import_data)
    {
        if (!isPlayingNow)
        {
            data = import_data;

            if (!data.skip)
            {
                if (data.fade)
                {
                    StartCoroutine(FadeIn());
                }

                if (data.looseControl) player_move.canControl = false;

                if (data.stopTime) Time.timeScale = 0;

                if (data.hideUI) hud.SetActive(false);

                subtitres_zone.SetActive(true);
                StartCoroutine(PlayScene(data.texts));
                isPlayingNow = true;
                return true;
            }
            return true;
        } else
        {
            Reset();
            return false;
        }
    }

    IEnumerator PlayScene(Phrase[] text_array)
    {
        subtitres.text = "";

        //print("Play scene begin");
        //print("String count: " + text_array.Length);

        foreach (Phrase p in text_array)
        {
            //print("Writing string +[" + p.text + "]");
            StartCoroutine(PrintString(p.text));
            yield return new WaitForSecondsRealtime(p.duration);
        }

        subtitres.text = "";
        subtitres_zone.SetActive(false);

        hud.SetActive(true);


        StartCoroutine(Ending());
        yield return null;

    }

    private IEnumerator PrintString(string s1)
    {
        //print("String print begin");

        if (new_row) writing_string = "";

        if (s1.Substring(s1.Length - 1, 1) == "/")
        {
            s1 = s1.Substring(0, s1.Length - 2);
            new_row = false;
        }
        else
        {
            new_row = true;
        }

        for (int i = 0; i < s1.Length; i++)
        {
            if (player.dead) Reset();
            writing_string += s1.Substring(i, 1);
            subtitres.text = writing_string;
            //print("Printed: " + writing_string + " " + "Interation: " + i + " " + "Interation remain: " + (s1.Length - i - 1));
            //print("Waiting for " + data.print_delay);
            yield return new WaitForSecondsRealtime(0.1f);
            //print("End of cycle");
        }

        yield return null;

    }

    IEnumerator Ending()
    {
        //print("Ending scene");

        StartCoroutine(FadeOut());

        if (data.hideUI) hud.SetActive(true);

        if (data.looseControl) player_move.canControl = true;
        if (data.stopTime) Time.timeScale = 1.0f;
        isPlayingNow = false;
        yield return null;
    }

    private void Reset()
    {
        StopAllCoroutines();
        subtitres_zone.SetActive(false);
        fading.color = new Color(0, 0, 0, 0.5f);
        if (data.hideUI) hud.SetActive(true);
        Time.timeScale = 1.0f;
        player_move.canControl = true;
        isPlayingNow = false;
    }

    IEnumerator FadeIn()
    {
        while (alpha < 1)
        {
            //print(alpha);
            alpha += 0.01f;
            fading.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
    IEnumerator FadeOut()
    {
        while (alpha > 0.5f)
        {
            alpha -= 0.01f;
            fading.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}
