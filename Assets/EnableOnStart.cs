using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnStart : MonoBehaviour {
    public GameObject[] enableMe;
    void Start() {
        foreach (var o in enableMe) {
            o.SetActive(true);
        }
    }

}
