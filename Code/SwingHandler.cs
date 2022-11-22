using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SwingHandler : MonoBehaviour
{
    public PlayerController pc;
    public SpriteRenderer sr;
    public BoxCollider2D bc;
    public float swingSpeed;
    public float swingStay;
    public bool swinging { get; private set; } = false;

    public void Swing()
    {
        transform.TransformVector(Vector3.zero);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        swinging = true;
        sr.enabled = true;
        bc.enabled = true;
        pc.canMove++;
        Invoke(nameof(EndSwing1), swingStay);
        Invoke(nameof(EndSwing2), swingSpeed);
    }

    private void EndSwing1()
    {
        sr.enabled = false;
        bc.enabled = false;
    }

    private void EndSwing2()
    {
        swinging = false;
        pc.canMove--;
        transform.TransformVector(new Vector3(1, 0, 0));
        pc.a.SetBool("Attack", false);
    }
}
