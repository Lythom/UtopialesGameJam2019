using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Essaim {
    public class FishGroup : MonoBehaviour {
        public Polyline path;
        public float targetSpeedRatio = 1.1f;
        [Header("Flocking Rules")] public Transform target;
        public GameObject prefab;
        [Range(1.0f, 10.0f)] public float neighbourDistance;
        [Range(0.0f, 10.0f)] public float avoidDistance;
        [Range(0.0f, 5.0f)] public float rotationSpeed;

        [Header("Fish Settings")] 
        [Range(0.0f, 10.0f)]public float minSpeed;
        [Range(0.0f, 10.0f)] public float maxSpeed;
        [Range(1, 300)] public int count = 30;

        public FishBehaviour[] all;
        private int initializedCount;
        private int nextPathPoint = 0;

        void Start() {
            if (target == null) throw new Exception("target is required");
            if (prefab == null) throw new Exception("prefab is required");
            if (path == null) throw new Exception("path is required");
            initializedCount = count;
            all = new FishBehaviour[initializedCount];
            for (int i = 0; i < initializedCount; i++) {
                GameObject go = GameObject.Instantiate(prefab, this.transform);
                go.transform.position = path.nodes[0] + initializedCount * 0.02f * Random.insideUnitSphere;
                FishBehaviour fb = go.transform.GetChild(0).gameObject.AddComponent<FishBehaviour>();
                fb.group = this;
                all[i] = fb;
            }
        }

        void Update() {
            Vector3 nextPoint = path.nodes[nextPathPoint];
            float distanceToNextPoint = Vector3.Distance(target.transform.position, nextPoint);
            if (distanceToNextPoint < minSpeed * Time.deltaTime) {
                nextPathPoint = (nextPathPoint + 1) % path.nodes.Count;
            } else {
                target.transform.position = Vector3.MoveTowards(target.transform.position, nextPoint, maxSpeed * targetSpeedRatio * Time.deltaTime);
            }
        }
    }
}