using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationHelper;
using TweenCore;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class OnTriggerWin : MonoBehaviour {
    public new Camera camera;
    public GameObject particles;
    public Vector3 destinationPos = new Vector3(0, 8.4f, 5);
    public Vector3 destinationAngle = new Vector3(310, 180, 0);
    public string targetSceneName;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            var attachedRigidbody = other.attachedRigidbody;
            attachedRigidbody.isKinematic = true;
            attachedRigidbody.velocity = Vector2.zero;
            var initalPos = camera.transform.position;
            var initalAngle = camera.transform.eulerAngles;
            particles.SetActive(true);

            this.AnimateOverTime01(2, (i) =>
            {
                camera.transform.position = Vector3.Lerp(initalPos, destinationPos, i.CubicIn());
                camera.transform.eulerAngles = Vector3.Lerp(initalAngle, destinationAngle, i.CubicIn());
                if (i >= 1) {
                    var overlay = camera.GetComponentInChildren<ScreenOverlay>();
                    overlay.blendMode = ScreenOverlay.OverlayBlendMode.Additive;
                    overlay.intensity = 0;
                    this.AnimateOverTime01(4f, j =>
                    {
                        overlay.intensity = - Mathf.Lerp(0, j * 7, j.ExpoIn());
                        if (j >= 1) {
                            SceneManager.LoadScene(targetSceneName);
                        }
                    });
                }
            });
        }
    }
}