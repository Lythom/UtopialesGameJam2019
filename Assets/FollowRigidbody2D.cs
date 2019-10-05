using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRigidbody2D : MonoBehaviour {
    public Rigidbody2D body;

    public Transform target;

    private void FixedUpdate() {
        body.MovePosition(target.position);
    }
}
