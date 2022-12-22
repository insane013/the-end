using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickScreen : MonoBehaviour
{
    public GameObject cam;
    public Text show_name;
    public Text show_description;
    public GameObject accept, cancel, use, crosshair;
    public Light lit;

    [SerializeField] private GameObject[] item_prefabs;

    private Vector3 start_pos;
    private Quaternion start_rot;
    private GameObject start_point;
    private Vector3 pick_pos;
    private Quaternion pick_rot;
    private GameObject source_obj;
    private string itemName;
    private string itemDescription;
    private Item item;
    private Enumerations.ItemSlot itemSlot;
    private Player player;

    private bool picked = false;

    public void ChangeActive(bool a)
    {
        show_name.gameObject.SetActive(a);
        show_description.gameObject.SetActive(a);
        accept.SetActive(a);
        cancel.SetActive(a);
        use.SetActive(a);
        lit.gameObject.SetActive(a);
        this.gameObject.SetActive(a);
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ChangeActive(false);
    }
    void SetValues(Info i)
    {
        start_pos = i.point.transform.position;
        start_rot = i.point.transform.rotation;
        start_point = i.point.transform.gameObject;
        pick_pos = i.pos;
        pick_rot = i.rot;
        source_obj = i.g;
        itemName = i.item.item_name;
        itemDescription = i.item.item_description;
        itemSlot = i.item.item_slot;
        item = i.item;
    }

    void Pick()
    {
        player.gameObject.GetComponent<Move>().canControl = false;
        source_obj.transform.SetParent(cam.transform);
        source_obj.transform.localPosition = pick_pos;
        source_obj.transform.localRotation = pick_rot;
        source_obj.GetComponent<Rigidbody>().isKinematic = true;
        crosshair.SetActive(false);
        source_obj.layer = 11;
        show_name.text = item.item_name;
        show_description.text = item.item_description;
        picked = true;
    }
    public void UnPick()
    {
        if (picked)
        {
            player.gameObject.GetComponent<Move>().canControl = true;
            source_obj.transform.parent = start_point.transform;
            source_obj.transform.position = start_pos;
            source_obj.transform.rotation = start_rot;
            source_obj.GetComponent<Rigidbody>().isKinematic = false;
            source_obj.layer = start_point.layer;
            show_name.text = "";
            player.activation = false;
            crosshair.SetActive(true);
            picked = false;
            ChangeActive(false);
        }
    }
    void Equip(int code = 0)
    {
        source_obj.layer = 0;
        Item current_slot = player.inventory[(int)itemSlot];
        if (current_slot != null)
        {
            if (current_slot.ID == item.ID)
            {
                if (current_slot.count < current_slot.max) player.inventory[(int)itemSlot].count++;
            } else
            {
                Instantiate(item_prefabs[current_slot.ID], player.gameObject.transform.position + Vector3.forward, player.gameObject.transform.rotation);
                player.inventory[(int)itemSlot] = item;
            }
        }
        else
        {
            player.inventory[(int)itemSlot] = item;
        }
        if (code == 0)
        {
            source_obj.SetActive(false);
            player.gameObject.GetComponent<Move>().canControl = true;
            player.activation = false;
            ChangeActive(false);
        }
    }
    void Use()
    {
        Equip(1);
        player.inventory[(int)itemSlot].Use();
        source_obj.SetActive(false);
        player.gameObject.GetComponent<Move>().canControl = true;
        player.activation = false;
        ChangeActive(false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Equip();
        } else if(Input.GetMouseButtonDown(1))
        {
            UnPick();
        } else if(Input.GetKeyDown(KeyCode.Space))
        {
            Use();
        }
    }
}
