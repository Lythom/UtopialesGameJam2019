﻿using System;
using System.Collections;
using System.Collections.Generic;
using Essaim;
using UnityEngine;

public class HookLogic : MonoBehaviour {
    public Rigidbody2D ball;
    [HideInInspector] public FishBehaviour fish;
    private SpringJoint2D joint2D;
    public AudioSource grabSound;


    public Transform hookTail;
    private Rigidbody2D body;

    private void Start() {
        body = this.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other1) {
        Hook(other1.rigidbody);
    }

    private void FixedUpdate() {
        if (hookTail != null && hookTail.gameObject.activeSelf) {
            var scale = hookTail.localScale;
            var position = transform.position;
            scale.y = Vector2.Distance(ball.position, position) * 2.4f;
            hookTail.localScale = scale;
        }

        bool hooked = fish != null;
        if (hooked) {
            if(joint2D != null) joint2D.connectedAnchor = fish.vcenter;
            body.MovePosition(fish.vcenter);
        }
    }

    public void Hook(Rigidbody2D body) {
        if (fish == null && joint2D == null) {
            grabSound.Play();
            fish = body.transform.parent.GetComponentInChildren<FishBehaviour>();
            joint2D = ball.gameObject.AddComponent<SpringJoint2D>();
            joint2D.autoConfigureDistance = false;
            joint2D.distance = 2.5f;
            joint2D.anchor = Vector2.zero;
            joint2D.dampingRatio = 1;
            joint2D.frequency = 10;
            joint2D.breakForce = Mathf.Infinity;
        }
    }

    public void Release() {
        fish = null;
        if (joint2D != null) Destroy(joint2D);
        joint2D = null;
    }
}