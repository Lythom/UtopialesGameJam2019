using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnHitWithMutatator : MonoBehaviour {
    public GameObject despawnEffect;
    public float despawnDuration = 1.0f;
    private float exitTime = -1;

    private void OnCollisionEnter2D(Collision2D other) {
        if (!other.gameObject.CompareTag("Player") ||
            !other.transform.parent.gameObject.GetComponent<MutablePlayer>().wallBreaker) return;
        exitTime = Time.time + despawnDuration;
        var thisTransform = transform;
        Instantiate(despawnEffect, thisTransform.position + Vector3.forward * 2, thisTransform.rotation, thisTransform.parent);
        other.transform.parent.gameObject.GetComponent<MutablePlayer>().DisableWallBreaker();
    }

    private GameObject exl1;
    private GameObject exl2;

    private void FixedUpdate() {
        if (exitTime < 0) return;
        var transform1 = transform;
        transform1.parent.Translate(0, 0, (-2 / despawnDuration) * Time.deltaTime);
        
        if (exl1 == null && Time.time > exitTime - despawnDuration * 0.75)
        {
            exl1 = Instantiate(despawnEffect, transform1.position + Vector3.left + Vector3.forward * 2, transform1.rotation, transform1.parent);
        }

        if (exl2 == null && Time.time > exitTime - despawnDuration * 0.50)
        {
            exl2 = Instantiate(despawnEffect, transform1.position + Vector3.right + Vector3.forward * 2, transform1.rotation, transform1.parent);
        }

        if (Time.time > exitTime) Destroy(transform1.parent.gameObject);
    }
}