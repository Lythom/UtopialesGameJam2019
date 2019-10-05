using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulse : MonoBehaviour
{
    public long impulsion; // 10 was good
    private Rigidbody2D rb;

    void OnCollisionEnter2D(Collision2D other)
    {
        rb = other.gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(-other.GetContact(0).normal * impulsion, ForceMode2D.Impulse);
    }
}
