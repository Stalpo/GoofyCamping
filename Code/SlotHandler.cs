using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHandler : MonoBehaviour
{
    public bool taken { get; set; } = false;

    public void Part2()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
