using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] entities;
    private Dictionary<string, GameObject> entityD;
    private int c;
    List<string> elist = new List<string>();
    List<int> plist = new List<int>();
    public float distanceFromCamera;
    public GameController gc;
    public ObjectManager om;

    private void Start()
    {
        entityD = new Dictionary<string, GameObject>()
        {
            { "wolf", entities[0] },
            { "snake", entities[1] },
            { "bear", entities[2] },
            { "sasquatch", entities[3] },
            { "shortPerson", entities[4] },
            { "person", entities[5] },
            { "bulldozer", entities[6] }
        };
    }

    public void InstantiateWave(List<string> e, float speed, int part)
    {
        if (c < elist.Count)
        {
            List<string> newelist = new List<string>();
            List<int> newplist = new List<int>();
            c = 0;
            for (int i = 0; i < elist.Count; i++)
            {
                newelist.Add(elist[i]);
                newplist.Add(plist[i]);
            }
            for (int i = 0; i < e.Count; i++)
            {
                newelist.Add(e[i]);
                newplist.Add(part);
            }
        }
        else
        {
            elist = e;
            plist.Clear();
        }
        c = 0;
        for (int i = 0; i < e.Count; i++)
        {
            plist.Add(part);
            Invoke(nameof(InstatiateWaveEnemy), speed * i);
        }
    }

    public void InstatiateWaveEnemy()
    {
        if ((gc.part1 && plist[c] == 1) || (gc.part2 && plist[c] == 2))
        {
            Vector2 pos = Vector2.zero;
            int r = Random.Range(0, 4);
            Camera cam = Camera.main;
            float height = cam.orthographicSize;
            float width = height * cam.aspect;
            switch (r)
            {
                case 0:
                    pos = new Vector2(player.transform.position.x - width - distanceFromCamera, Random.Range(player.transform.position.y - height - distanceFromCamera, player.transform.position.y + height + distanceFromCamera));
                    break;
                case 1:
                    pos = new Vector2(player.transform.position.x + width + distanceFromCamera, Random.Range(player.transform.position.y - height - distanceFromCamera, player.transform.position.y + height + distanceFromCamera));
                    break;
                case 2:
                    pos = new Vector2(Random.Range(player.transform.position.x - width - distanceFromCamera, player.transform.position.x + width + distanceFromCamera), player.transform.position.y - height - distanceFromCamera);
                    break;
                case 3:
                    pos = new Vector2(Random.Range(player.transform.position.x - width - distanceFromCamera, player.transform.position.x + width + distanceFromCamera), player.transform.position.y + height + distanceFromCamera);
                    break;
            }
            if (elist[c] != "skip")
            {
                InstantiateEntity(elist[c], pos);
            }
            c++;
        }
    }

    public void InstantiateEntity(string str, Vector3 p)
    {
        GameObject entity;
        if (entityD.TryGetValue(str, out entity))
        {
            entity = Instantiate(entity, p, Quaternion.identity);
            EntityController ec = entity.GetComponent<EntityController>();
            ec.player = player;
            ec.gc = gc;
            ec.om = om;
        }
    }
}
