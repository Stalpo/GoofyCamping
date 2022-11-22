using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public ObjectManager om;
    public StatsManager sm;

    private static Dictionary<string, string> recipes = new Dictionary<string, string>()
    {
        { "stick+stick", "longStick" },
        { "longStick+stick", "kindling" },
        { "kindling+rock", "firePit" },
        { "stick+rock", "stickRock" },
        { "rock+rock", "stone" },
        { "axe+log", "axe+plank+plank" },
        { "log+plank", "woodWall" },
        { "stone+stone", "largeStone" },
        { "longStick+stone", "axe" },
        { "axe+stone", "pick" },
        { "longStick+largeStone", "hammer" },
        { "largeStone+stone", "stoneWall" },
        { "axe+plank", "axe+stick+stick+stick" },
        { "plank+plank", "trap" },
        { "stone+rock", "bearTrap" },
        { "largeStone+log", "rawMatter" },
        { "rawMatter+rawMatter", "beans" },
        { "rawMatter+axe", "weapon" },
        { "rawMatter+weapon", "WMD" }
    };

    public void RecipeOutput(string p1, string p2, GameObject o1, GameObject o2)
    {
        string[] split;
        string dicStr1 = p1 + "+" + p2, dicStr2 = p2 + "+" + p1;
        if (recipes.ContainsKey(dicStr1))
        {
            split = recipes.GetValueOrDefault(dicStr1).Split('+');
        }
        else if (recipes.ContainsKey(dicStr2))
        {
            split = recipes.GetValueOrDefault(dicStr2).Split('+');
        }
        else
        {
            return;
        }
        for (int i = 0; i < split.Length; i++)
        {
            sm.thingsCrafted++;
            if (split[i] == "trap" || split[i] == "bearTrap")
            {
                sm.trapsSet++;
            }
            if (i == 0)
            {
                om.Instantiate(split[i], o2.transform.position);
            }
            else
            {
                om.Instantiate(split[i], new Vector3(Random.Range(o2.transform.position.x - .5f, o2.transform.position.x + .5f), Random.Range(o2.transform.position.y - .5f, o2.transform.position.y + .5f), o2.transform.position.z));
            }
        }
        Destroy(o1);
        Destroy(o2);
    }
}
