using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Essaim {
    public class FishGroup : MonoBehaviour {
        [Header("Flocking Rules")] public Transform target;
        public GameObject prefab;
        [Range(1.0f, 10.0f)] public float neighbourDistance;
        [Range(0.0f, 5.0f)] public float rotationSpeed;

        [Header("Fish Settings")] [Range(0.0f, 5.0f)]
        public float minSpeed;

        [Range(0.0f, 10.0f)] public float maxSpeed;
        [Range(1, 300)] public int count = 30;

        public FishBehaviour[] all;
        private int initializedCount;

        void Start() {
            if (target == null) throw new Exception("target is required");
            if (prefab == null) throw new Exception("prefab is required");
            initializedCount = count;
            all = new FishBehaviour[initializedCount];
            for (int i = 0; i < initializedCount; i++) {
                GameObject go = GameObject.Instantiate(prefab, this.transform);
                go.transform.position = initializedCount * 0.02f * Random.insideUnitSphere;
                FishBehaviour fb = go.AddComponent<FishBehaviour>();
                fb.group = this;
                all[i] = fb;
            }
        }

        void Update() {
        }
    }
}