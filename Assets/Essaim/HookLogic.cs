using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookLogic : MonoBehaviour {
    public Rigidbody2D ball;
    [HideInInspector] public Rigidbody2D fish;
    private HingeJoint2D hingeJoint2D;

    private Rigidbody2D body;

    private void OnCollisionEnter2D(Collision2D other1) {
        Hook(other1.rigidbody);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Hook(other.attachedRigidbody);
    }

    private void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (fish != null) body.MovePosition(fish.transform.position);
    }

    public void Hook(Rigidbody2D body) {
        if (fish == null) {
            fish = body;
            hingeJoint2D = gameObject.AddComponent<HingeJoint2D>();
            hingeJoint2D.autoConfigureConnectedAnchor = false;
            hingeJoint2D.connectedBody = ball;
            hingeJoint2D.anchor = Vector2.zero;
            hingeJoint2D.connectedAnchor = (body.position - ball.position) * 0.7f;
        }
    }

    public void Release() {
        fish = null;
        if (hingeJoint2D != null) Destroy(hingeJoint2D);
        hingeJoint2D = null;
    }
}