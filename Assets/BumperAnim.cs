using UnityEngine;
using AnimationHelper;

public class BumperAnim : MonoBehaviour
{
    public float animDurationInSeconds = 0.13f;
    
    void OnCollisionEnter2D(Collision2D other) {
        var parent = this.transform.parent;
        var z = parent.position.z;
        this.AnimateOverTime01(animDurationInSeconds, (i) =>
        {
            var pos = parent.position;
            pos.z = z - TweenCore.FloatTools.Yoyo(i, TweenCore.Easing.QuadIn);
            parent.position = pos;
        });
    }
}
