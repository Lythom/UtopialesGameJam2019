using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using AnimationHelper;
using TweenCore;
using UnityStandardAssets.ImageEffects;

public class OnTriggerTeleport : MonoBehaviour
{
    public string targetSceneName;
    public new Camera camera;
    public GameObject particles;

    public GameObject gameObjectForDestinationPos;
    public Vector3 destinationPos;// = new Vector3(12, -3.45f, 5);
    public Vector3 destinationAngle;// = new Vector3(310, 180, 0);

    void Start()
    {
        destinationPos = new Vector3(12, -3.45f, 5);
        destinationAngle = new Vector3(310, 180, 0);
        
        if (particles != null)
        {
            particles.SetActive(false);
        }

        if (gameObjectForDestinationPos == null) return;
        var gameObjetForDestiantionPosTransform = gameObjectForDestinationPos.transform;
        destinationPos = gameObjetForDestiantionPosTransform.position;
        destinationAngle = gameObjetForDestiantionPosTransform.eulerAngles;
        Debug.Log(gameObjetForDestiantionPosTransform.position);
        Debug.Log(gameObjetForDestiantionPosTransform.eulerAngles);
        Debug.Log(gameObjetForDestiantionPosTransform.localEulerAngles);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            var attachedRigidbody = other.attachedRigidbody;
            attachedRigidbody.isKinematic = true;
            attachedRigidbody.velocity = new Vector2(.1f,.1f);
            var cameraTransform = camera.transform;
            var initalPos = cameraTransform.position;
            var initalAngle = cameraTransform.eulerAngles;

            if (particles != null)
            {
                particles.SetActive(true);
            }

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
