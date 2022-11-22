using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject[] items;
    private Dictionary<string, GameObject> objectD;
    private Dictionary<string, GameObject> itemD;
    public GameObject player, slot1, slot2, slot3, slot4, slot5;
    public RecipeManager rm;
    public GameController gc;

    private void Start()
    {
        objectD = new Dictionary<string, GameObject>()
        {
            { "firePit", objects[0] },
            { "tree", objects[1] },
            { "rock", objects[2] },
            { "woodWall", objects[3] },
            { "stoneWall", objects[4] },
            { "trap", objects[5] },
            { "bearTrap", objects[6] }
        };
        itemD = new Dictionary<string, GameObject>()
        {
            { "stick", items[0] },
            { "longStick", items[1] },
            { "kindling", items[2] },
            { "rockItem", items[3] },
            { "stickRock", items[4] },
            { "log", items[5] },
            { "plank", items[6] },
            { "stone", items[7] },
            { "largeStone", items[8] },
            { "axe", items[9] },
            { "pick", items[10] },
            { "hammer", items[11] },
            { "lighter", items[12] },
            { "phone", items[13] },
            { "beans", items[14] },
            { "beansOpen", items[15] },
            { "rawMatter", items[16] },
            { "weapon", items[17] },
            { "WMD", items[18] }
        };
    }

    public void Instantiate(string str, Vector3 p)
    {
        if (objectD.ContainsKey(str))
        {
            InstantiateObject(objectD[str], p);
        }
        else if(itemD.ContainsKey(str))
        {
            InstantiateItem(itemD[str], p);
        }
        else
        {
            Debug.Log("item/object: " + str + " not stored in database");
        }
    }

    public void InstantiateItem(GameObject o, Vector3 p)
    {
        o = Instantiate(o, p, Quaternion.identity);
        ItemController ic = o.GetComponent<ItemController>();
        ic.player = player;
        ic.slot1 = slot1;
        ic.slot2 = slot2;
        ic.slot3 = slot3;
        ic.slot4 = slot4;
        ic.slot5 = slot5;
        ic.rm = rm;
        ic.gc = gc;
    }

    public void InstantiateObject(GameObject o, Vector3 p)
    {
        o = Instantiate(o, p, Quaternion.identity);
        ObjectHandler oh = o.GetComponent<ObjectHandler>();
        oh.om = this;
        oh.pc = player.GetComponent<PlayerController>();
    }
}
