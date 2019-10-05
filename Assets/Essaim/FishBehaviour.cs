using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Essaim {
    public class FishBehaviour : MonoBehaviour {
        [Range(1.0f, 10.0f)] public float neighbourDistance;
        [Range(0.0f, 5.0f)] public float rotationSpeed;
        public FishGroup group;

        private float speed;

        private float nDistance;
        private float gSpeed;

        void Start() {
            if (group == null) throw new Exception("group is required");
            speed = Random.Range(group.minSpeed, group.maxSpeed);
        }

        void Update() {
            transform.Translate(0, 0, Time.deltaTime * speed);
            Vector3 vcenter = Vector3.zero;
            Vector3 vavoid = Vector3.zero;
            foreach (FishBehaviour fish in group.all) {
                if (fish != this) {
                    nDistance = Vector3.Distance(fish.transform.position, this.transform.position);
                    vcenter += fish.transform.position;

                    if (nDistance < 1.0f) {
                        vavoid = vavoid + (this.transform.position - fish.transform.position);
                    }

                    gSpeed = gSpeed + fish.speed;
                }
            }

            vcenter = vcenter / group.all.Length;
            gSpeed = gSpeed / group.all.Length;

            Vector3 direction = (vcenter + vavoid + group.target.position) * 0.5f - transform.position;

            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(direction),
                    group.rotationSpeed * Time.deltaTime);
        }
    }
}