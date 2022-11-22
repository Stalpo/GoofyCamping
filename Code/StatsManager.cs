using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public Animator blackScreen;
    public TextMeshProUGUI statsText;

    [HideInInspector]
    public bool gotToPart2 = false;
    [HideInInspector]
    public float timeSurvived = 0;
    [HideInInspector]
    public int wavesSurvived = 0;
    [HideInInspector]
    public int animalsDefeated = 0;

    // hide before part 2
    [HideInInspector]
    public int humansDefeated = 0;

    [HideInInspector]
    public int thingsCrafted = 0;
    [HideInInspector]
    public int beansAte = 0;
    [HideInInspector]
    public int firesStarted = 0;
    [HideInInspector]
    public int treesCutDown = 0;
    [HideInInspector]
    public int rocksMined = 0;
    [HideInInspector]
    public int otherStuffBroken = 0;
    [HideInInspector]
    public int trapsSet = 0;

    // hide unless acheived
    [HideInInspector]
    public bool secretFound = false;

    public void Die()
    {
        blackScreen.SetTrigger("FadeToBlack");
        LoadStats();
    }

    public void LoadStats()
    {
        statsText.text = "Part\n";
        if (gotToPart2)
        {
            statsText.text += "2/2";
        }
        else
        {
            statsText.text += "1/2";
        }
        statsText.text += "\n\nTime Survived\n";
        if ((int)((timeSurvived % 60) / 1) < 10)
        {
            statsText.text += (int)(timeSurvived / 60) + ":0" + (int)((timeSurvived % 60) / 1);
        }
        else
        {
            statsText.text += (int)(timeSurvived / 60) + ":" + (int)((timeSurvived % 60) / 1);
        }
        statsText.text += "\n\nWaves Survived\n";
        statsText.text += wavesSurvived;
        statsText.text += "\n\nAnimals Defeated\n";
        statsText.text += animalsDefeated;
        if (gotToPart2)
        {
            statsText.text += "\n\nHumans Defeated\n";
            statsText.text += humansDefeated;
        }
        statsText.text += "\n\nThings Crafted\n";
        statsText.text += thingsCrafted;
        statsText.text += "\n\nCans of Beans Consumed\n";
        statsText.text += beansAte;
        statsText.text += "\n\nFires Started\n";
        statsText.text += firesStarted;
        statsText.text += "\n\nTrees Chopped\n";
        statsText.text += treesCutDown;
        statsText.text += "\n\nRocks Mined\n";
        statsText.text += rocksMined;
        statsText.text += "\n\nOther Stuff Destroyed\n";
        statsText.text += otherStuffBroken;
        statsText.text += "\n\nTraps Set\n";
        statsText.text += trapsSet;
        if (Random.value <= .01)
        {
            statsText.text += "\n\nMr Bones Delux\n";
            statsText.text += "Good Job! (?)";
        }
    }
}
