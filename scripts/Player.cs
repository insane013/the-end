using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public KeyCode activate = KeyCode.E;
    public KeyCode use_item_weapon = KeyCode.Keypad1;
    public KeyCode use_item_light = KeyCode.Keypad2;
    public KeyCode use_item_drink = KeyCode.Keypad3;
    public KeyCode use_item_food = KeyCode.Keypad4;
    public KeyCode use_item_medicine = KeyCode.Keypad5;
    public KeyCode use_item_psy = KeyCode.Keypad6;

    [SerializeField] public Camera cam;
    [SerializeField] private Slider hp_shower;
    [SerializeField] private Slider psy_shower;
    [SerializeField] private Text  fps_shower;
    [SerializeField] private Image fading;

    [SerializeField] private GameObject hud;

    [SerializeField] private PickScreen pickSCR;
    [SerializeField] private pause_menu pause_menu;
    [SerializeField] private dead_menu dead_menu;

    [SerializeField] private GameObject player_zombie;

    [SerializeField] private Image[] item_slots = new Image[6];
    [SerializeField] private Sprite default_ico;
    [SerializeField] private GameObject activate_ico;

    [SerializeField] public GameObject respawn_point;
    [SerializeField] public GameObject tp;

    [SerializeField] private float _hp = 100f;
    [SerializeField] private float _psy = 100f;

    public float hp { 
        get { return _hp; } 
        set {
            if (value < 0)
            {
                _hp = 0;
                if (!dead)
                {
                    print("hp death");
                    print("Stop all coroutines");
                    StopAllCoroutines();
                    StartCoroutine(Death("hp"));
                }
            }
            else _hp = value;
        } 
    }

    public float psy
    {
        get { return _psy; }
        set
        {
            if (value < 0)
            {
                _psy = 0;
                if (!dead)
                {
                    print("psy death");
                    StartCoroutine(Death("psy"));
                }
            }
            else _psy = value;
        }
    }

    [SerializeField] public float activate_dist = 10f;

    [SerializeField] public Debuff debuff;

    public bool activation = false;
    public bool weapon_ready = false;

    private Move move;

    private GameObject hands;
    private hub HUB;

    public Item[] inventory = new Item[6];
    private Text[] item_slots_counters = new Text[6];

    public bool dead = false;

    private GameObject weapon;

    private TextMeshProUGUI activate_text;

    private void Start()
    {
        activate_text = activate_ico.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        move = GetComponent<Move>();
        for (int i = 0; i < 6; i++)
        {
            item_slots_counters[i] = item_slots[i].GetComponentInChildren<Text>();
        }
        hands = GameObject.FindGameObjectWithTag("hands");
    }

    public void DMG(float dmg)
    {
        hp -= dmg;
    }

    public void psyDMG(float dmg)
    {
        psy -= dmg;
    }

    public void UseWeapon(Weapon wp)
    {
        HUB = GameObject.FindGameObjectWithTag("HUB").GetComponent<hub>();
        weapon = HUB.weapons[wp.weapon_id];
        if (!weapon_ready)
        {
            hands.SetActive(false);
            weapon.SetActive(true);
            weapon_ready = true;
        } else
        {
            weapon.SetActive(false);
            hands.SetActive(true);
            weapon_ready = false;
        }

    }

    void Attack()
    {
        Animator wp_anim = weapon.GetComponent<Animator>();
        wp_anim.SetTrigger("hit");
    }

    private void Update()
    {
        debuff.Update(this);

        fps_shower.text = ((int)(1/Time.deltaTime)).ToString();
        hp_shower.value = (float)hp / 100;
        psy_shower.value = (float)psy / 100;


        if(Input.GetMouseButtonDown(0))
        {
            if (weapon_ready && move.canControl)
            {
                print("Hit");
                Attack();
            }

        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pause_menu.Activate("", "");
        }

        Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0));
        RaycastHit hit;
        if (activation)
        {
            activate_ico.SetActive(false);
        }

        if (Physics.SphereCast(ray, 1f, out hit, activate_dist)) {
            if (hit.distance < activate_dist && !activation)
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Playable"))
                {
                    activate_ico.SetActive(true);
                    activate_text.text = hit.transform.gameObject.GetComponent<ActiveTarget>().ActiveTargetName();
                    if (Input.GetKeyDown(activate))
                    {
                        activation = true;
                        hit.transform.gameObject.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
                    }
                } else
                {
                    activate_ico.SetActive(false);
                }
            }
        }
        #region inventory
        if (Input.GetKeyDown(use_item_weapon))
        {
            if (inventory[0] != null) inventory[0].Use();
        }
        if (Input.GetKeyDown(use_item_light))
        {
            if (inventory[1] != null) inventory[1].Use();
        }
        if (Input.GetKeyDown(use_item_drink))
        {
            if (inventory[2] != null) inventory[2].Use();
        }
        if (Input.GetKeyDown(use_item_food))
        {
            if (inventory[3] != null) { inventory[3].Use(); } else { print("Null"); }
        }
        if (Input.GetKeyDown(use_item_medicine))
        {
            if (inventory[4] != null) inventory[4].Use();
        }
        if (Input.GetKeyDown(use_item_psy))
        {
            if (inventory[5] != null) inventory[5].Use();
            print("press");
        }

        #region quick_use_panel
        if (inventory[0] != null)
        {
            item_slots[0].sprite = inventory[0].item_ico;
            item_slots_counters[0].text = inventory[0].count.ToString();
        }
        else
        {
            item_slots[0].sprite = default_ico;
            item_slots_counters[0].text = "";
        }
        if (inventory[1] != null)
        {
            item_slots[1].sprite = inventory[1].item_ico;
            item_slots_counters[1].text = inventory[1].count.ToString();
        }
        else
        {
            item_slots[1].sprite = default_ico;
            item_slots_counters[1].text = "";
        }
        if (inventory[2] != null)
        {
            item_slots[2].sprite = inventory[2].item_ico;
            item_slots_counters[2].text = inventory[2].count.ToString();
        }
        else
        {
            item_slots[2].sprite = default_ico;
            item_slots_counters[2].text = "";
        }
        if (inventory[3] != null)
        {
            item_slots[3].sprite = inventory[3].item_ico;
            item_slots_counters[3].text = inventory[3].count.ToString();
        }
        else
        {
            item_slots[3].sprite = default_ico;
            item_slots_counters[3].text = "";
        }
        if (inventory[4] != null)
        {
            item_slots[4].sprite = inventory[4].item_ico;
            item_slots_counters[4].text = inventory[4].count.ToString();
        }
        else
        {
            item_slots[4].sprite = default_ico;
            item_slots_counters[4].text = "";
        }
        if (inventory[5] != null)
        {
            item_slots[5].sprite = inventory[5].item_ico;
            item_slots_counters[5].text = inventory[5].count.ToString();
        }
        else
        {
            item_slots[5].sprite = default_ico;
            item_slots_counters[5].text = "";
        }
        #endregion

        #endregion

    }

    IEnumerator Death(string type)
    {
        dead = true;
        move.canControl = false;
        hud.SetActive(false);
        
        GameObject a = Instantiate(player_zombie, transform.position, transform.rotation) as GameObject;
        while (fading.color.a < 1)
        {
            float alpha = fading.color.a + 0.01f;
            fading.color = new Color(100, 0, 0, alpha);
            yield return new WaitForSecondsRealtime(0.05f);
        }
        Time.timeScale = 0;
        dead_menu.Activate(type, "");
        fading.color = new Color(0, 0, 0, 1);
    }

    public void Respawn()
    {
        StartCoroutine(ieRespawn());
    }

    private IEnumerator ieRespawn()
    {
        move.Teleport(respawn_point.transform.position);
        while (fading.color.a > 0.5f)
        {
            float alpha = fading.color.a - 0.01f;
            fading.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSecondsRealtime(0.05f);
        }
        hud.SetActive(true);
        hp = 50f;
        psy = 20f;
        dead = false;
        Time.timeScale = 1f;
        move.canControl = true;
    }

    public void Kill(string type)
    {
        if (!dead)
        {
            print("Stop all coroutines");
            StopAllCoroutines();
            StartCoroutine(Death(type));
        }
    }

    public void Sleep()
    {
        if (psy < 40f)
        {
            print("Stop all coroutines");
            StopAllCoroutines();
            StartCoroutine(Death("sleep"));
        }
        else
        {
            print("Stop all coroutines");
            StopAllCoroutines();
            StartCoroutine(ieSleep());
        }
    }

    IEnumerator ieSleep()
    {
        dead = true;
        move.canControl = false;
        hud.SetActive(false);

        GameObject a = Instantiate(player_zombie, transform.position, transform.rotation) as GameObject;
        while (fading.color.a < 1)
        {
            float alpha = fading.color.a + 0.01f;
            fading.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSecondsRealtime(0.05f);
        }
        Time.timeScale = 0;
        SceneManager.LoadScene("garage");
        fading.color = new Color(0, 0, 0, 1);
    }

}
