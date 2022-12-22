using UnityEngine;

public class HitZone : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float damage_randomize = 5;
    [SerializeField] AudioClip hit_sound;

    private AudioSource audio_src;

    public bool isMob = true;


    private bool flag;

    private void Start()
    {
        audio_src = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        flag = true;
    }

    private void OnDisable()
    {
        flag = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isMob)
        {
            if (other.tag == "Player" && flag)
            {
                other.gameObject.SendMessage("DMG", damage + Random.Range(-damage_randomize, damage_randomize));
                flag = false;
            }
        } else
        {
            if (other.tag == "enemy" && flag)
            {
                other.gameObject.SendMessage("DMG", damage + Random.Range(-damage_randomize, damage_randomize));
                flag = false;
                audio_src.PlayOneShot(hit_sound);
            }
        }
    }
}
