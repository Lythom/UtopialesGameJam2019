﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class MutablePlayer : MonoBehaviour
{

    public GameObject isWallBreakerOn = null;
    private float sizeMultiplier = 0.0f;

    public int WALL_BREAKER_TYPE = 1;
    int wallBreakerActivatorsNumber;
    private int wallBreakerActivatorsTriggered = 0;
    private static bool sizeActivatorStatus = false;
    public bool wallBreaker => isWallBreakerOn != null && isWallBreakerOn.activeSelf;

    //Start
    void Start()
    {
        wallBreakerActivatorsNumber = GameObject.FindGameObjectsWithTag("WallBreakerActivator").Length;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Called by OnTriggerEnter of bonuses activators
    public void UpdateBonuses(int type, int value)
    {
        if (type == WALL_BREAKER_TYPE)
            wallBreakerActivatorsTriggered += value;
        if (wallBreakerActivatorsNumber == wallBreakerActivatorsTriggered)
        {
            //TODO Launch animation or sound effect meaning "you can break fragile walls"
            isWallBreakerOn.SetActive(true);
            GameObject[] activators = GameObject.FindGameObjectsWithTag("WallBreakerActivator");
            foreach (var entity in activators)
            {
                entity.GetComponent<MutActivator>().SetMoving();
            }
        }
    }


    public void DisableWallBreaker() {
        isWallBreakerOn.SetActive(false);
    }
}