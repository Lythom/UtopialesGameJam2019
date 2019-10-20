﻿using System;
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

        [Header("Fish Settings")] [Range(0.0f, 10.0f)]
        public float minSpeed;

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
                var transform1 = this.transform;
                var position = transform1.position;
                var pos = path.nodes[0] + position + initializedCount * 0.02f * Random.insideUnitSphere;
                pos.z = position.z;
                go.transform.position = pos;
                FishBehaviour fb = go.transform.GetChild(0).gameObject.AddComponent<FishBehaviour>();
                fb.group = this;
                all[i] = fb;
            }
        }

        void FixedUpdate() {
            var position = transform.position;
            Vector3 nextPoint = path.nodes[nextPathPoint] + position;
            nextPoint.z = position.z;
            float distanceToNextPoint = Vector3.Distance(target.transform.position, nextPoint);
            if (distanceToNextPoint < maxSpeed * 1.5f * Time.deltaTime) {
                nextPathPoint = (nextPathPoint + 1) % path.nodes.Count;
            } else {
                Quaternion targetLookAt = Quaternion.LookRotation(nextPoint - target.position, Vector3.forward);
                target.rotation = Quaternion.Lerp(target.rotation, targetLookAt, 0.15f);
                target.Translate(0, 0, maxSpeed * targetSpeedRatio * Time.deltaTime);
            }
        }
    }
}