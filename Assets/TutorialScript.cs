using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {
    public Sprite off;
    public Sprite on;
    public SpriteRenderer renderer;


    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(0)) {
            renderer.sprite = @on;
        } else {
            renderer.sprite = @off;
        }
    }
}