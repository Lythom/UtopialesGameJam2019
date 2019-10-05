using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishball : MonoBehaviour {
    public Rigidbody2D ball;
    public Rigidbody2D hook;
    public float grabLength = 1f;
    public float grabSpeed = 10f;

    private HookLogic hookL;

    void Start() {
        if (ball == null) throw new Exception("ball is required");
        if (hook == null) throw new Exception("hook is required");
        hookL = hook.transform.GetComponent<HookLogic>();
        hook.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var mousePos = Input.mousePosition;
            mousePos.z = Camera.main.transform.position.z - 0.5f;
            LaunchHook(Camera.main.ScreenToWorldPoint(mousePos));
        }

        if (!Input.GetMouseButton(0) || (hookL.fish == null && Vector2.Distance(ball.position, hook.position) > grabLength)) {
            hookL.Release();
            hook.gameObject.SetActive(false);
        }
    }

    private void LaunchHook(Vector2 targetPosition) {
        hook.gameObject.SetActive(true);
        var ballPos = ball.transform.position;
        Vector2 angle = (targetPosition - (Vector2) ballPos).normalized;
        hook.velocity = ball.velocity + angle * grabSpeed;
        hook.transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(angle, Vector2.up));
        hook.position = ballPos;
    }
}