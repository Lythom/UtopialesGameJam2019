using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAlgues : MonoBehaviour
{
    private int[] offsets;

    public float speed = 5f;
    public float amplitude = 4f;
    // Start is called before the first frame update
    void Start() {
        offsets = new int[] {
            5, 8, 9, 1, 3, 5, 4, 7, 8, 3, 1, 2, 4, 9, 12
        };
    }

    // Update is called once per frame
    void Update() {
        int i = 0;
        foreach (Transform child in transform) {
            var rot = child.transform.rotation.eulerAngles;
            rot.y = -90 + Mathf.Cos(Time.time * speed + offsets[i]) * amplitude;
            child.transform.eulerAngles = rot;
            i++;
        }
    }
}
