using Essaim;
using UnityEngine;

public class HookLogic : MonoBehaviour {
    public Rigidbody2D ball;
    [HideInInspector] public FishBehaviour fish;
    private SpringJoint2D joint2D;
    public AudioSource grabSound;
    public LineRenderer line;
    public GameObject arrow;


    public Transform hookTail;
    private Rigidbody2D body;

    private void Start() {
        body = this.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other1) {
        Hook(other1.rigidbody);
    }

    void Update() {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, ball.gameObject.transform.position);
        line.enabled = true;
    }

    private void FixedUpdate() {
        if (hookTail != null && hookTail.gameObject.activeSelf) {
            var scale = hookTail.localScale;
            var position = transform.position;
            scale.y = Vector2.Distance(ball.position, position) * 2.4f;
            hookTail.localScale = scale;
        }

        bool hooked = fish != null;
        if (hooked) {
            if (joint2D != null) joint2D.connectedAnchor = fish.@group.target.position;
            body.MovePosition(fish.vcenter);
        }
    }

    public void Hook(Rigidbody2D body) {
        if (fish == null && joint2D == null) {
            grabSound.Play();
            fish = body.transform.parent.GetComponentInChildren<FishBehaviour>();
            joint2D = ball.gameObject.AddComponent<SpringJoint2D>();
            joint2D.autoConfigureDistance = false;
            joint2D.distance = 3.5f;
            joint2D.anchor = Vector2.zero;
            joint2D.dampingRatio = 1;
            joint2D.frequency = 3;
            joint2D.breakForce = Mathf.Infinity;
            ball.drag = 3f;
            arrow.SetActive(true);
        }
    }

    public void Release() {
        fish = null;
        if (joint2D != null) Destroy(joint2D);
        joint2D = null;
        line.enabled = false;
        ball.drag = 0;
        ball.AddForce(ball.velocity * 0.5f, ForceMode2D.Impulse);
        arrow.SetActive(false);
    }
}