using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public bool startedInScene = false;
    public GameObject player, slot1, slot2, slot3, slot4, slot5;
    public double holdSlotRange = .5f, playerHoldRangex = .5f, playerHoldRangey = .5f;
    public string itemName;
    public int canHit;
    public int d;
    public int fuel;
    public RecipeManager rm;
    public bool pressed { get; set; } = false;
    public int held = 0;
    private Vector2 offset;
    private SpriteRenderer sp { get; set; }
    private BoxCollider2D bc { get; set; }
    bool heldLast = false;
    public GameController gc;
    private Animator a;
    private ParticleSystem ps;

    private void Start()
    {
        a = gameObject.GetComponent<Animator>();
        ps = gameObject.GetComponent<ParticleSystem>();
        if (!startedInScene)
        {
            ps.Emit(25);
        }
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        var objects = GameObject.FindGameObjectsWithTag("Entity");
        foreach (var obj in objects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        sp = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        if (held == 1)
        {
            sp.sortingLayerName = "Slots";
            slot1.GetComponent<SlotHandler>().taken = true;
            bc.isTrigger = true;

        }
        else if (held == 2)
        {
            sp.sortingLayerName = "Slots";
            slot2.GetComponent<SlotHandler>().taken = true;
            bc.isTrigger = true;
        }
    }

    void Update()
    {
        if (pressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x + offset.x, mousePos.y + offset.y, -9);
        }
        switch (held)
        {
            case -1:
                if (gc.part1)
                {
                    transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.8f, -9);
                }
                else if (gc.part2)
                {
                    if (player.GetComponent<PlayerController>().isFacingLeft)
                    {
                        transform.position = new Vector3(player.transform.position.x - 0.7f, player.transform.position.y + 0.5f, -9);
                    }
                    else
                    {
                        transform.position = new Vector3(player.transform.position.x + 0.7f, player.transform.position.y + 0.5f, -9);
                    }
                }
                break;
            case 1:
                transform.position = new Vector3(slot1.transform.position.x, slot1.transform.position.y, -9);
                break;
            case 2:
                transform.position = new Vector3(slot2.transform.position.x, slot2.transform.position.y, -9);
                break;
            case 3:
                transform.position = new Vector3(slot3.transform.position.x, slot3.transform.position.y, -9);
                break;
            case 4:
                transform.position = new Vector3(slot4.transform.position.x, slot4.transform.position.y, -9);
                break;
            case 5:
                transform.position = new Vector3(slot5.transform.position.x, slot5.transform.position.y, -9);
                break;
        }
        if (Input.GetMouseButtonDown(0))
        {
            heldLast = false;
        }
    }

    private void OnMouseDown()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (held == -1)
        {
            offset = new Vector2(0, 0);
            bc.isTrigger = true;
            sp.enabled = true;
            pressed = true;
            sp.sortingLayerName = "Held";
            player.GetComponent<PlayerController>().holding = null;
            held = 0;
        }
        else if (player.GetComponent<PlayerController>().holding == null)
        {
            offset = new Vector2(transform.position.x - mousePos.x, transform.position.y - mousePos.y);
            bc.isTrigger = true;
            sp.enabled = true;
            pressed = true;
            sp.sortingLayerName = "Held";
            switch (held)
            {
                case 1:
                    slot1.GetComponent<SlotHandler>().taken = false;
                    break;
                case 2:
                    slot2.GetComponent<SlotHandler>().taken = false;
                    break;
                case 3:
                    slot3.GetComponent<SlotHandler>().taken = false;
                    break;
                case 4:
                    slot4.GetComponent<SlotHandler>().taken = false;
                    break;
                case 5:
                    slot5.GetComponent<SlotHandler>().taken = false;
                    break;
            }
            held = 0;
        }
    }

    private void OnMouseUp()
    {
        if (pressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pressed = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            heldLast = true;
            if (gc.part1 && Mathf.Abs(player.transform.position.x - mousePos.x) <= playerHoldRangex && Mathf.Abs(player.transform.position.y + 0.8f - mousePos.y) <= playerHoldRangey && player.GetComponent<PlayerController>().holding == null)
            {
                held = -1;
                sp.sortingLayerName = "Default";
                player.GetComponent<PlayerController>().holding = gameObject;
                if (itemName == "phone")
                {
                    GetComponent<PhoneController>().Call();
                }
            }
            else if (gc.part2 && player.GetComponent<PlayerController>().isFacingLeft && Mathf.Abs(player.transform.position.x - mousePos.x - 0.7f) <= playerHoldRangex && Mathf.Abs(player.transform.position.y + 0.5f - mousePos.y) <= playerHoldRangey && player.GetComponent<PlayerController>().holding == null)
            {
                held = -1;
                sp.sortingLayerName = "Default";
                player.GetComponent<PlayerController>().holding = gameObject;
                if (itemName == "phone")
                {
                    GetComponent<PhoneController>().Call();
                }
            }
            else if (gc.part2 && !player.GetComponent<PlayerController>().isFacingLeft && Mathf.Abs(player.transform.position.x - mousePos.x + 0.7f) <= playerHoldRangex && Mathf.Abs(player.transform.position.y + 0.5f - mousePos.y) <= playerHoldRangey && player.GetComponent<PlayerController>().holding == null)
            {
                held = -1;
                sp.sortingLayerName = "Default";
                player.GetComponent<PlayerController>().holding = gameObject;
                if (itemName == "phone")
                {
                    GetComponent<PhoneController>().Call();
                }
            }
            else if (slot1.GetComponent<SpriteRenderer>().enabled == true && Mathf.Abs(slot1.transform.position.x - mousePos.x) <= holdSlotRange && Mathf.Abs(slot1.transform.position.y - mousePos.y) <= holdSlotRange && slot1.GetComponent<SlotHandler>().taken == false)
            {
                held = 1;
                sp.sortingLayerName = "Slots";
                slot1.GetComponent<SlotHandler>().taken = true;
            }
            else if (slot2.GetComponent<SpriteRenderer>().enabled == true && Mathf.Abs(slot2.transform.position.x - mousePos.x) <= holdSlotRange && Mathf.Abs(slot2.transform.position.y - mousePos.y) <= holdSlotRange && slot2.GetComponent<SlotHandler>().taken == false)
            {
                held = 2;
                sp.sortingLayerName = "Slots";
                slot2.GetComponent<SlotHandler>().taken = true;
            }
            else if (slot3.GetComponent<SpriteRenderer>().enabled == true && Mathf.Abs(slot3.transform.position.x - mousePos.x) <= holdSlotRange && Mathf.Abs(slot3.transform.position.y - mousePos.y) <= holdSlotRange && slot3.GetComponent<SlotHandler>().taken == false)
            {
                held = 3;
                sp.sortingLayerName = "Slots";
                slot3.GetComponent<SlotHandler>().taken = true;
            }
            else if (slot4.GetComponent<SpriteRenderer>().enabled == true && Mathf.Abs(slot4.transform.position.x - mousePos.x) <= holdSlotRange && Mathf.Abs(slot4.transform.position.y - mousePos.y) <= holdSlotRange && slot4.GetComponent<SlotHandler>().taken == false)
            {
                held = 4;
                sp.sortingLayerName = "Slots";
                slot4.GetComponent<SlotHandler>().taken = true;
            }
            else if (slot5.GetComponent<SpriteRenderer>().enabled == true && Mathf.Abs(slot5.transform.position.x - mousePos.x) <= holdSlotRange && Mathf.Abs(slot5.transform.position.y - mousePos.y) <= holdSlotRange && slot5.GetComponent<SlotHandler>().taken == false)
            {
                held = 5;
                sp.sortingLayerName = "Slots";
                slot5.GetComponent<SlotHandler>().taken = true;
            }
            else
            {
                sp.sortingLayerName = "Default";
                bc.isTrigger = false;
                bc.enabled = false;
                bc.enabled = true;
                return;
            }
            bc.isTrigger = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ItemController ic;
        if (gc.part1 && collision.gameObject.TryGetComponent<ItemController>(out ic))
        {
            if (held == 0 && ic.held == 0 && heldLast)
            {
                heldLast = false;
                rm.RecipeOutput(itemName, ic.itemName, gameObject, collision.gameObject);
            }
        }
        else if (collision.gameObject.tag == "Entity")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnMouseEnter()
    {
        a.SetBool("Highlighted", true);
    }

    private void OnMouseExit()
    {
        a.SetBool("Highlighted", false);
    }

    public void Part2()
    {
        if (held != 0)
        {
            held = 0;
            transform.position = new Vector2(Random.Range(player.transform.position.x - 2, player.transform.position.x + 2), Random.Range(player.transform.position.y - 2, player.transform.position.y + 2));
            sp.sortingLayerName = "Default";
            bc.isTrigger = false;
            bc.enabled = false;
            bc.enabled = true;
            sp.enabled = true;
        }
        player.GetComponent<PlayerController>().holding = null;
    }
}
