using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] private Light light_source;

    public float min_fade_duration = 0.1f;
    public float max_fade_duration = 3f;
    public float min_light_duration = 0.1f;
    public float max_light_duration = 3f;

    private bool isBlinkNow = false;

    private void Update()
    {
        if (!isBlinkNow)
        {
            light_source.enabled = false;
            StartCoroutine(wait());
        }
    }

    private IEnumerator wait()
    {
        isBlinkNow = true;
        yield return new WaitForSeconds(Random.Range(min_fade_duration, max_fade_duration));
        light_source.enabled = true;
        yield return new WaitForSeconds(Random.Range(min_light_duration, max_light_duration));
        isBlinkNow = false;
    }
}
