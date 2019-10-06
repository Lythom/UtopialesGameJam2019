using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookLogic : MonoBehaviour {
    public Rigidbody2D ball;
    [HideInInspector] public Rigidbody2D fish;
    private HingeJoint2D hingeJoint2D;

    public Transform hookTail;


    private void OnCollisionEnter2D(Collision2D other1) {
        Hook(other1.rigidbody);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Hook(other.attachedRigidbody);
    }


    private void FixedUpdate() {
        if (fish != null) this.transform.position = fish.position;
        if (hookTail != null && hookTail.gameObject.activeSelf) {
            var scale = hookTail.localScale;
            var position = transform.position;
            scale.y = Vector2.Distance(ball.position, position) * 2.4f;
            hookTail.localScale = scale;
        }
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