using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    public bool startedInScene = false;
    public int mhealth;
    public int otype;
    public int health;
    public string[] drops;
    public string Objectname;
    public float burnTime;
    public ObjectManager om;
    public PlayerController pc;
    private bool used = false;
    private int fuel = 15;
    private Animator a;
    private SpriteRenderer sr;
    private ParticleSystem ps;

    private void Start()
    {
        a = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
        if (!startedInScene)
        {
            ps.Emit(25);
        }
        health = mhealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ItemController ic = pc.holding.GetComponent<ItemController>();
            if (ic.canHit == otype)
            {
                TakeDamage(ic.d);
            }
            else if (ic.canHit == 4 && Objectname == "firePit" && fuel > 0)
            {
                if (a.GetBool("Fueled"))
                {
                    om.gameObject.GetComponent<StatsManager>().firesStarted++;
                    StartFirePit();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            ItemController ic = collision.gameObject.GetComponent<ItemController>();
            if (ic.fuel > 0 && !ic.pressed && ic.held == 0 && Objectname == "firePit")
            {
                FuelFirePit(ic.fuel);
                Destroy(collision.gameObject);
            }
            else if (ic.itemName == "beans" && Objectname == "firePit")
            {
                if (a.GetBool("Lit"))
                {
                    Destroy(collision.gameObject);
                    om.Instantiate("beansOpen", new Vector3(Random.Range(transform.position.x - .5f, transform.position.x + .5f), Random.Range(transform.position.y - .5f, transform.position.y + .5f), transform.position.z));
                }
            }
        }
        else if (collision.gameObject.tag == "Entity")
        {
            EntityController ec = collision.gameObject.GetComponent<EntityController>();
            if (Objectname == "trap" && !used)
            {
                Trap(ec);
            }
            else if (Objectname == "bearTrap" && !used)
            {
                TrapAttack(ec);
            }
            else if (ec.breakObjects)
            {
                ec.CantMove();
                DestroyObject();
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            if (Objectname == "trap" && !used)
            {
                Trap();
            }
            else if (Objectname == "bearTrap" && !used)
            {
                TrapAttack();
            }
        }
    }

    private void TakeDamage(int d)
    {
        health -= d;
        ps.Emit(25);
        if (health <= 0)
        {
            if (Objectname == "tree")
            {
                om.gameObject.GetComponent<StatsManager>().treesCutDown++;
            }
            else if (Objectname == "rock")
            {
                om.gameObject.GetComponent<StatsManager>().rocksMined++;
            }
            else
            {
                om.gameObject.GetComponent<StatsManager>().otherStuffBroken++;
            }
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        for (int i = 0; i < drops.Length; i++)
        {
            om.Instantiate(drops[i], new Vector3(Random.Range(transform.position.x - .5f, transform.position.x + .5f), Random.Range(transform.position.y - .5f, transform.position.y + .5f), transform.position.z));
        }
        Destroy(gameObject);
    }

    public void FuelFirePit(int f)
    {
        fuel += f;
        if (a.GetBool("Out"))
        {
            a.SetBool("Fueled", true);
            a.SetBool("Out", false);
        }
    }

    public void StartFirePit()
    {
        fuel--;
        a.SetBool("Lit", true);
        a.SetBool("Fueled", false);
        Invoke(nameof(EndFirePit), 1f);
    }

    public void EndFirePit()
    {
        if (fuel > 0)
        {
            StartFirePit();
        }
        else
        {
            a.SetBool("Out", true);
            a.SetBool("Lit", false);
        }
    }

    public void TrapAttack(EntityController ec)
    {
        a.SetTrigger("Use");
        if (ec.trapable)
        {
            ec.TakeDamage(5);
        }
        Invoke(nameof(BreakTrap), 1f);
        used = true;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), ec.gameObject.GetComponent<Collider2D>());
    }

    public void TrapAttack()
    {
        if (om.gameObject.GetComponent<GameController>().part2)
        {
            a.SetTrigger("Use");
            pc.Damage(5);
            Invoke(nameof(BreakTrap), 1f);
            used = true;
        }
    }

    public void Trap(EntityController ec)
    {
        a.SetTrigger("Use");
        if (ec.trapable)
        {
            ec.CantMove(5f);
        }
        Invoke(nameof(BreakTrap), 5f);
        used = true;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), ec.gameObject.GetComponent<Collider2D>());
    }

    public void Trap()
    {
        if (om.gameObject.GetComponent<GameController>().part2)
        {
            a.SetTrigger("Use");
            pc.canMove++;
            pc.NoMove(5f);
            Invoke(nameof(BreakTrap), 5f);
            used = true;
        }
    }

    public void BreakTrap()
    {
        Destroy(gameObject);
    }
}
