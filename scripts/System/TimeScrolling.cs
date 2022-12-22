using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScrolling : MonoBehaviour
{
    public bool enable_scrolling = true;
    public Light sun, moon;
    public GameObject player;
    public float SecondsPerDay = 720f;

    [Range(0, 1)] public float time_now = 0.5f;

    /*public Material Skybox_day, Skybox_night;
    private Material Skybox_start, Skybox_next;*/

    [HideInInspector] public float current_second;

    private void Start()
    {
        current_second = SecondsPerDay * time_now;
        transform.localEulerAngles = new Vector3(360 * time_now, 0, 0);
        
        /*if (time_now >= 0.4f || time_now <= 0.1f ) { Skybox_start = Skybox_night;  }
        if (time_now >= 0.6f || time_now >= 0.9f) { Skybox_start = Skybox_day; }*/
    }
    void Update()
    {
        if (enable_scrolling)
        {
            if (time_now >= 1f) { time_now = 0; }
            if (current_second >= SecondsPerDay) { current_second = 0; }

            transform.Rotate(new Vector3(180 / (SecondsPerDay * 0.5f) * Time.deltaTime, 0, 0));
            current_second += Time.deltaTime;
            time_now = (current_second / (float)(SecondsPerDay * 0.01)) / 100;
        }

        this.gameObject.transform.position = player.transform.position;
        //RenderSettings.skybox.Lerp()

        if (time_now >= 0.5f)
        {
            sun.enabled = false;
            moon.enabled = true;
        }

        if (time_now <= 0.01f) {
            sun.enabled = true;
            moon.enabled = false;
        }
    }
}
