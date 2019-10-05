using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookLogic : MonoBehaviour {
    public Rigidbody2D ball;
    [HideInInspector] public Rigidbody2D fish;
    private HingeJoint2D hingeJoint2D;

    private void OnCollisionEnter2D(Collision2D other1) {
        Hook(other1.rigidbody);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Hook(other.attachedRigidbody);
    }


    private void FixedUpdate() {
        if (fish != null) this.transform.position = fish.position;
    }

    public void Hook(Rigidbody2D body) {
        if (fish == null) {
            fish = body;
            hingeJoint2D = fish.gameObject.AddComponent<HingeJoint2D>();
            hingeJoint2D.autoConfigureConnectedAnchor = true;
            hingeJoint2D.connectedBody = ball;
            hingeJoint2D.anchor = Vector2.zero;
        }
    }

    public void Release() {
        fish = null;
        if (hingeJoint2D != null) Destroy(hingeJoint2D);
        hingeJoint2D = null;
    }
}