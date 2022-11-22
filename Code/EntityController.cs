using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public GameObject player;
    public string entityName;
    public bool animal;
    public int type;
    public bool breakObjects;
    public bool trapable;
    public float speed;
    public float attackDistance;
    public float attackSpeed;
    public float stunDelay;
    public int d;
    public int mhealth;
    public float stalkDistance;
    public float stalkSpeed;
    public float sprintSpeed;
    public float retreatSpeed;
    private int health;
    private bool attacking = false;
    private bool sprint = false;
    private bool prepForAttack = false;
    private bool retreat = false;
    private Rigidbody2D rb;
    private Vector2 movement;
    private int canMove = 0;
    public GameController gc;
    public ObjectManager om;
    private bool isFacingLeft = false;
    private Animator a;
    private ParticleSystem ps;

    private void Start()
    {
        a = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();
        var objects = GameObject.FindGameObjectsWithTag("Item");
        foreach (var obj in objects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        health = mhealth;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        switch (type)
        {
            case 0:
                if (!attacking && Vector2.Distance(transform.position, player.transform.position) > attackDistance && canMove == 0)
                {
                    movement = (player.transform.position - transform.position).normalized;
                    rb.velocity = movement * speed;
                    CheckFacing();
                }
                else if (!attacking && canMove == 0)
                {
                    Attack();
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
                break;
            case 1:
                if (retreat)
                {
                    movement = (transform.position - player.transform.position).normalized;
                    rb.velocity = movement * sprintSpeed;
                    CheckFacing();
                }
                else if (!prepForAttack && !attacking && Vector2.Distance(transform.position, player.transform.position) > stalkDistance && canMove == 0)
                {
                    movement = (player.transform.position - transform.position).normalized;
                    rb.velocity = movement * speed;
                    CheckFacing();
                }
                else if (!prepForAttack && !attacking && canMove == 0)
                {
                    prepForAttack = true;
                    a.SetTrigger("Stalk");
                    Invoke(nameof(SprintAtPlayer), stalkSpeed);
                }
                else if (sprint)
                {
                    movement = (player.transform.position - transform.position).normalized;
                    rb.velocity = movement * sprintSpeed;
                    CheckFacing();
                    if (Vector2.Distance(transform.position, player.transform.position) <= attackDistance)
                    {
                        sprint = false;
                        prepForAttack = false;
                        Attack();
                    }
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
                break;
        }
    }

    private void CheckFacing()
    {
        if ((movement.x > 0 && isFacingLeft) || (movement.x < 0 && !isFacingLeft))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            isFacingLeft = !isFacingLeft;
        }
    }

    private void SprintAtPlayer()
    {
        sprint = true;
        a.SetTrigger("Sprint");
    }

    public void EndRetreat()
    {
        a.SetTrigger("Walk");
        sprint = false;
        prepForAttack = false;
        retreat = false;
    }

    private void Attack()
    {
        attacking = true;
        player.GetComponent<PlayerController>().Damage(d);
        rb.bodyType = RigidbodyType2D.Kinematic;
        Invoke(nameof(EndAttack), attackSpeed);
        a.SetTrigger("Attack");
    }

    private void EndAttack()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ItemController ic = player.GetComponent<PlayerController>().holding.GetComponent<ItemController>();
            TakeDamage(ic.d);
        }
    }

    public void TakeDamage(int d)
    {
        health -= d;
        ps.Emit(25);
        if (health <= 0)
        {
            if (entityName == "sasquatch")
            {
                gc.Part2();
                for (int i = 0; i < 10; i++)
                {
                    om.Instantiate("beans", new Vector3(Random.Range(transform.position.x - .5f, transform.position.x + .5f), Random.Range(transform.position.y - .5f, transform.position.y + .5f), transform.position.z));
                }
            }
            if (Random.Range(0, 15) == 0)
            {
                om.Instantiate("beans", new Vector3(Random.Range(transform.position.x - .5f, transform.position.x + .5f), Random.Range(transform.position.y - .5f, transform.position.y + .5f), transform.position.z));
            }
            if (animal)
            {
                gc.sm.animalsDefeated++;
            }
            else
            {
                gc.sm.humansDefeated++;
            }
            Destroy(gameObject);
        }
        CantMove();
    }

    public void CantMove()
    {
        a.SetTrigger("Damage");
        canMove++;
        rb.bodyType = RigidbodyType2D.Kinematic;
        Invoke(nameof(CanMove), stunDelay);
    }

    public void CantMove(float t)
    {
        a.SetTrigger("Damage");
        canMove++;
        rb.bodyType = RigidbodyType2D.Kinematic;
        Invoke(nameof(CanMove), t);
    }

    private void CanMove()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        canMove--;
        if (type == 1)
        {
            a.SetTrigger("Retreat");
            retreat = true;
            Invoke(nameof(EndRetreat), retreatSpeed);
        }
    }

    public void Part2()
    {
        Destroy(gameObject);
    }
}
