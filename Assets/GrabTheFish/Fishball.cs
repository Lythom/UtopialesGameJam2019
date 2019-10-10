using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishball : MonoBehaviour {
    public new Camera camera;
    public Rigidbody2D ball;
    public Transform ball3D;
    public Rigidbody2D hook;
    public float grabLength = 1f;
    public float grabSpeed = 10f;

    public AudioSource launchSound;
    
    private HookLogic hookL;

    void Start() {
        if (ball == null) throw new Exception("ball is required");
        if (hook == null) throw new Exception("hook is required");
        if (camera == null) throw new Exception("camera is required");
        hookL = hook.transform.GetComponent<HookLogic>();
        hook.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (hookL.gameObject.activeSelf && (!Input.GetMouseButton(0) || (hookL.fish == null && Vector2.Distance(ball.position, hook.position) > grabLength))) {
            hookL.Release();
            hook.gameObject.SetActive(false);
            Debug.Log("rewind " + (hookL.fish == null ? "no fish" : "hooked!" + ":") + Vector2.Distance(ball.position, hook.position));
        }
        
        if (Input.GetMouseButtonDown(0)) {
            var mousePos = Input.mousePosition;
            mousePos.z = camera.transform.position.z - 0.5f;
            LaunchHook(camera.ScreenToWorldPoint(mousePos));
            Debug.Log("launch");
        }

        ball3D.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(ball.velocity * new Vector2(1, -1), Vector2.up));
        ball3D.position = ball.transform.position;
    }

    private void LaunchHook(Vector2 targetPosition) {
        launchSound.Play();
        hook.gameObject.SetActive(true);
        var ballPos = ball.transform.position;
        Vector2 angle = (targetPosition - (Vector2) ballPos).normalized;
        hook.velocity = ball.velocity + angle * grabSpeed;
        hook.rotation = Vector2.SignedAngle(-hook.velocity * new Vector2(1, -1), Vector2.up);
        hook.position = ballPos;
    }
}