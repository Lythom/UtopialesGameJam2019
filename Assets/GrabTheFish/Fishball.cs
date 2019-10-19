using System;
using System.Collections;
using System.Collections.Generic;
using Essaim;
using UnityEngine;

public class Fishball : MonoBehaviour {
    public new Camera camera;
    public Rigidbody2D ball;
    public Transform ball3D;
    public Rigidbody2D hook;
    public GameObject targetVisual;
    public float grabLength = 1f;
    public float grabSpeed = 10f;

    public AudioSource launchSound;

    private HookLogic hookL;
    private FishGroup[] fishGroups;

    void Start() {
        if (ball == null) throw new Exception("ball is required");
        if (hook == null) throw new Exception("hook is required");
        if (camera == null) throw new Exception("camera is required");
        hookL = hook.transform.GetComponent<HookLogic>();
        hook.gameObject.SetActive(false);
        fishGroups = GameObject.FindObjectsOfType<FishGroup>();
    }

    void Update() {
        if (hookL.gameObject.activeSelf && (!Input.GetMouseButton(0) || (hookL.fish == null && Vector2.Distance(ball.position, hook.position) > grabLength))) {
            hookL.Release();
            hook.gameObject.SetActive(false);
        }

        Vector2 closestGroup = Vector2.zero;
        float closestDistance = 999999;
        foreach (FishGroup fishGroup in fishGroups) {
            if (fishGroup.all.Length > 0) {
                var groupPos = fishGroup.all[0].vcenter;
                var dist = Vector2.Distance(groupPos, ball.position);
                if (dist < closestDistance) {
                    closestDistance = dist;
                    closestGroup = groupPos;
                }
            }
        }

        if (closestDistance < grabLength && !hookL.gameObject.activeSelf) {
            targetVisual.SetActive(true);
            targetVisual.transform.position = (Vector3) closestGroup + Vector3.forward * 1.5f;
        } else {
            targetVisual.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0)) {
            LaunchHook(closestGroup);
        }
    }

    void FixedUpdate() {
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