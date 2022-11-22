using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackBackground : MonoBehaviour
{
    public Animator stats;
    private bool skippable = false;
    public AudioSource deathMusic;
    public AudioSource gameMusic;
    public GameController gc;

    private void Update()
    {
        if (skippable)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
            {
                stats.SetTrigger("Skip");
            }
        }
    }

    public void Part2()
    {
        gc.StartPart2();
    }

    public void FinishedFading()
    {
        deathMusic.Play();
        gameMusic.Stop();
        var objects = GameObject.FindGameObjectsWithTag("Entity");
        foreach (var obj in objects)
        {
            Destroy(obj);
        }
        objects = GameObject.FindGameObjectsWithTag("Item");
        foreach (var obj in objects)
        {
            Destroy(obj);
        }
        objects = GameObject.FindGameObjectsWithTag("Object");
        foreach (var obj in objects)
        {
            Destroy(obj);
        }
        objects = GameObject.FindGameObjectsWithTag("Player");
        foreach (var obj in objects)
        {
            Destroy(obj);
        }
        objects = GameObject.FindGameObjectsWithTag("UI");
        foreach (var obj in objects)
        {
            Destroy(obj);
        }
        objects = GameObject.FindGameObjectsWithTag("GameController");
        foreach (var obj in objects)
        {
            Destroy(obj);
        }
        stats.SetTrigger("RollStats");
        skippable = true;
    }

    public void BackToMenu()
    {
        Debug.Log("LLL");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
