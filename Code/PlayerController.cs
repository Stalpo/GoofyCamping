using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineOrbitalTransposer;

public class PlayerController : MonoBehaviour
{
    public int mhealth;
    public float speed;
    public float damageWait;
    public int canMove { get; set; } = 0;
    public Rigidbody2D rb;
    public GameObject holding { get; set; } = null;
    Vector2 movement;
    public ObjectManager om;
    public SwingHandler sh;
    private int health;
    public Sprite wolfSprite;
    public SpriteRenderer[] hearts;
    public bool isFacingLeft { get; private set; } = false;
    private bool alive = true;
    public StatsManager sm;
    public Animator a { get; set; }
    private ParticleSystem ps;
    private Dictionary<string, int> animationDic = new Dictionary<string, int>()
    {
        { "", 0 },
        { "stick", 1 },
        { "longStick", 2 },
        { "kindling", 3 },
        { "rock", 4 },
        { "stickRock", 5 },
        { "log", 6 },
        { "plank", 7 },
        { "stone", 8 },
        { "largeStone", 9 },
        { "axe", 10 },
        { "pick", 11 },
        { "hammer", 12 },
        { "lighter", 13 },
        { "phone", 14 },
        { "beans", 15 },
        { "beansOpen", 16 },
        { "rawMatter", 17 },
        { "weapon", 18 },
        { "WMD", 19 }
    };

    private void Start()
    {
        health = mhealth;
        a = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (canMove == 0 && alive)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement.x = 0;
            movement.y = 0;
        }
        if (Input.GetMouseButtonDown(0) && holding != null && !sh.swinging && alive)
        {
            holding.GetComponent<SpriteRenderer>().enabled = false;
            a.SetInteger("Holding", animationDic[holding.GetComponent<ItemController>().itemName]);
            a.SetBool("Attack", true);
            a.SetTrigger("Attack 0");
            if (holding.GetComponent<ItemController>().itemName == "beansOpen")
            {
                Heal(2);
                sm.beansAte++;
                Destroy(holding);
                holding = null;
                canMove++;
                Invoke(nameof(EndEat), sh.swingSpeed);
            }
            else
            {
                sh.Swing();
                Invoke(nameof(removeWMD), sh.swingSpeed / 2);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        if ((movement.x > 0 && isFacingLeft) || (movement.x < 0 && !isFacingLeft))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            isFacingLeft = !isFacingLeft;
        }
        if (movement.x != 0 || movement.y != 0)
        {
            if (!a.GetBool("Moving"))
            {
                a.SetBool("Moving", true);
            }
        }
        else
        {
            if (a.GetBool("Moving"))
            {
                a.SetBool("Moving", false);
            }
        }
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    public void Damage(int d)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i * 2 < health && i * 2 >= health - d)
            {
                hearts[i].enabled = false;
                hearts[i].gameObject.GetComponent<ParticleSystem>().Emit(25);
            }
        }
        health -= d;
        if (alive)
        {
            ps.Emit(25);
        }
        if (health <= 0)
        {
            Die();
        }
        canMove++;
        Invoke(nameof(AllowMove), damageWait);
    }

    private void Die()
    {
        Debug.Log("You Died");
        if (isFacingLeft)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            isFacingLeft = !isFacingLeft;
        }
        a.SetTrigger("Dead");
        alive = false;
        sm.Die();
    }

    public void NoMove(float time)
    {
        Invoke(nameof(AllowMove), time);
    }

    public void AllowMove()
    {
        canMove--;
    }

    public void EndEat()
    {
        canMove--;
        a.SetBool("Attack", false);
    }

    public void Heal(int h)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i * 2 >= health && i * 2 < health + h)
            {
                hearts[i].enabled = true;
                hearts[i].gameObject.GetComponent<ParticleSystem>().Emit(25);
            }
        }
        health += h;
        if (health > mhealth)
        {
            health = mhealth;
        }
    }

    public void removeWMD()
    {
        ItemController ic;
        if (holding != null)
        {
            if (holding.TryGetComponent<ItemController>(out ic))
            {
                if (ic.itemName == "WMD")
                {
                    Destroy(holding);
                    holding = null;
                }
            }
        }
    }

    public void EnableItemRender()
    {
        SpriteRenderer sr;
        if (holding != null)
        {
            if (holding.TryGetComponent<SpriteRenderer>(out sr))
            {
                sr.enabled = true;
            }
        }
    }

    public void Part2()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.transform.Translate(new Vector2(0, -1));
        }
        a.SetTrigger("moveParts");
        a.SetBool("Part2", true);
    }
}
