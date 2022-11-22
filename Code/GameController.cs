using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int wave = 0;
    private float time = 0;
    private bool halfway = false;
    public int dayTime;
    private List<string> entityQueue;
    private List<string> tempQueue;
    private float spawnSpeed;
    public EntityManager em;
    public bool part1 = true;
    public bool part2 = false;
    public Animator blackBackground;
    public AudioSource waveSound;
    public StatsManager sm { get; set; }

    private void Start()
    {
        sm = gameObject.GetComponent<StatsManager>();
    }

    private void Update()
    {
        if (part1 || part2)
        {
            time += Time.deltaTime;
            sm.timeSurvived += Time.deltaTime;
            if (time >= dayTime / 2 && !halfway)
            {
                halfway = true;
                wave++;
                if (part2 || sm.wavesSurvived < 10)
                {
                    sm.wavesSurvived++;
                }
                StartWave(wave);
            }
            else if (time >= dayTime)
            {
                halfway = false;
                time = time - dayTime;
                wave++;
                if (part2 || sm.wavesSurvived < 10)
                {
                    sm.wavesSurvived++;
                }
                StartWave(wave);
            }
        }
    }

    public void StartWave(int w)
    {
        Debug.Log("wave" + w);
        if (part1)
        {
            switch (w)
            {
                case 1:
                    entityQueue = new List<string>()
                    { "wolf", "wolf", "wolf" };
                    spawnSpeed = 3;
                    break;
                case 2:
                    entityQueue = new List<string>()
                    { "wolf", "wolf", "wolf", "wolf", "wolf" };
                    spawnSpeed = 1;
                    break;
                case 3:
                    entityQueue = new List<string>()
                    { "snake", "wolf", "wolf", "wolf", "snake", "wolf", "wolf", "wolf", "snake" };
                    spawnSpeed = 1;
                    break;
                case 4:
                    entityQueue = new List<string>()
                    { "snake", "snake", "wolf", "wolf", "snake", "wolf", "wolf", "snake", "snake", "wolf", "wolf", "wolf" };
                    spawnSpeed = .9f;
                    break;
                case 5:
                    entityQueue = new List<string>()
                    { "bear", "skip", "wolf", "wolf", "wolf" };
                    spawnSpeed = 1;
                    break;
                case 6:
                    entityQueue = new List<string>()
                    { "bear", "wolf", "snake", "wolf", "bear", "wolf", "snake" };
                    spawnSpeed = .9f;
                    break;
                case 7:
                    entityQueue = new List<string>()
                    { "wolf", "snake", "wolf", "wolf", "snake", "wolf", "wolf", "snake", "wolf", "wolf", "snake", "wolf", "wolf", "snake", "wolf", "wolf", "snake", "wolf", "skip", "skip", "skip", "skip", "skip", "skip", "skip", "skip", "skip", "skip", "bear" };
                    spawnSpeed = .7f;
                    break;
                case 8:
                    entityQueue = new List<string>()
                    { "bear", "bear", "wolf", "wolf", "wolf", "wolf", "wolf", "snake", "snake", "snake", "bear", "bear" };
                    spawnSpeed = 1;
                    break;
                case 9:
                    entityQueue = new List<string>()
                    { "bear", "wolf", "snake", "skip", "skip", "skip", "bear", "wolf", "snake", "skip", "skip", "skip", "bear", "wolf", "snake", "skip", "skip", "skip", "bear", "wolf", "snake" };
                    spawnSpeed = .3f;
                    break;
                case 10:
                    entityQueue = new List<string>()
                    { "sasquatch", "skip", "skip", "skip", "skip", "skip", "wolf", "skip", "skip", "snake", "skip", "skip", "wolf", "skip", "skip", "snake", "skip", "wolf", "skip", "snake", "skip", "wolf", "skip", "snake", "skip", "wolf", "snake", "wolf", "snake", "wolf", "snake", "wolf", "snake", "wolf", "bear", "skip", "skip", "skip", "skip", "skip", "bear" };
                    spawnSpeed = 1;
                    break;
                default:
                    return;
            }
            waveSound.Play();
            em.InstantiateWave(entityQueue, spawnSpeed, 1);
        }
        else if (part2)
        {
            switch (w)
            {
                case 1:
                    entityQueue = new List<string>()
                    { "person", "person", "person" };
                    spawnSpeed = 3;
                    break;
                case 2:
                    entityQueue = new List<string>()
                    { "person", "person", "person", "person", "person" };
                    spawnSpeed = 1;
                    break;
                case 3:
                    entityQueue = new List<string>()
                    { "shortPerson", "person", "person", "person", "shortPerson", "person", "person", "person", "shortPerson" };
                    spawnSpeed = 1;
                    break;
                case 4:
                    entityQueue = new List<string>()
                    { "shortPerson", "shortPerson", "person", "person", "shortPerson", "person", "person", "shortPerson", "shortPerson", "person", "person", "person" };
                    spawnSpeed = .9f;
                    break;
                case 5:
                    entityQueue = new List<string>()
                    { "bulldozer", "skip", "person", "person", "person" };
                    spawnSpeed = 1;
                    break;
                default:
                    switch (w % 5)
                    {
                        case 1:
                            entityQueue = new List<string>();
                            tempQueue = new List<string>()
                            { "person", "shortPerson", "person", "person", "shortPerson", "person", "person", "shortPerson", "person" };
                            for (int i = 0; i < (w - 1) / 5; i++)
                            {
                                entityQueue.AddRange(tempQueue);
                            }
                            spawnSpeed = 1 / ((w - 1) / 5);
                            break;
                        case 2:
                            entityQueue = new List<string>();
                            tempQueue = new List<string>()
                            { "person", "person", "person", "shortPerson", "shortPerson", "shortPerson", "skip", "skip", "skip", "person", "person", "person", "shortPerson", "shortPerson", "shortPerson", "skip", "skip", "skip" };
                            for (int i = 0; i < (w - 1) / 5; i++)
                            {
                                entityQueue.AddRange(tempQueue);
                            }
                            spawnSpeed = .8f / ((w - 1) / 5);
                            break;
                        case 3:
                            entityQueue = new List<string>();
                            tempQueue = new List<string>()
                            { "bulldozer", "person", "shortPerson", "person", "person", "shortPerson", "person", "person", "shortPerson", "person", "skip", "skip", "skip" };
                            for (int i = 0; i < (w - 1) / 5; i++)
                            {
                                entityQueue.AddRange(tempQueue);
                            }
                            spawnSpeed = 1 / ((w - 1) / 5);
                            break;
                        case 4:
                            entityQueue = new List<string>();
                            tempQueue = new List<string>()
                            { "person", "person", "shortPerson", "shortPerson", "person", "person", "shortPerson", "shortPerson", "skip", "skip", "skip", "person", "person", "shortPerson", "shortPerson", "person", "person", "shortPerson", "shortPerson", "skip", "skip", "skip" };
                            for (int i = 0; i < (w - 1) / 5; i++)
                            {
                                entityQueue.AddRange(tempQueue);
                            }
                            spawnSpeed = .7f / ((w - 1) / 5);
                            break;
                        case 0:
                            entityQueue = new List<string>();
                            tempQueue = new List<string>()
                            { "bulldozer", "person", "person", "shortPerson", "shortPerson", "person", "person", "shortPerson", "shortPerson", "skip", "skip", "skip", "bulldozer", "person", "person", "shortPerson", "shortPerson", "person", "person", "shortPerson", "shortPerson", "skip", "skip", "skip" };
                            for (int i = 0; i < (w - 1) / 5; i++)
                            {
                                entityQueue.AddRange(tempQueue);
                            }
                            spawnSpeed = 1 / ((w - 1) / 5);
                            break;
                    }
                    break;
            }
            waveSound.Play();
            em.InstantiateWave(entityQueue, spawnSpeed, 2);
        }
    }

    public void Part2()
    {
        Debug.Log("Part 1 Over");
        part1 = false;
        blackBackground.SetTrigger("Part2");
    }

    public void StartPart2()
    {
        part2 = true;
        sm.gotToPart2 = true;
        wave = 0;
        time = 0;
        var objects = GameObject.FindGameObjectsWithTag("Item");
        foreach (var obj in objects)
        {
            obj.GetComponent<ItemController>().Part2();
        }
        objects = GameObject.FindGameObjectsWithTag("Entity");
        foreach (var obj in objects)
        {
            obj.GetComponent<EntityController>().Part2();
        }
        objects = GameObject.FindGameObjectsWithTag("UI");
        foreach (var obj in objects)
        {
            SlotHandler sh;
            if (obj.TryGetComponent<SlotHandler>(out sh))
            {
                sh.Part2();
            }
        }
        objects = GameObject.FindGameObjectsWithTag("Player");
        foreach (var obj in objects)
        {
            PlayerController pc;
            if (obj.TryGetComponent<PlayerController>(out pc))
            {
                pc.Part2();
            }
        }
    }
}
