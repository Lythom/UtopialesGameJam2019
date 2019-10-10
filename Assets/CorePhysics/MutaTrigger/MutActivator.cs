using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class MutActivator : MonoBehaviour
{
    private bool activated = false;
    public GameObject despawnEffect;

    public float despawnDuration = 1.0f;
    private float exitTime = -1;
    
    void OnCollisionEnter2D(Collision2D other)
    {
        GetComponent<Light>().intensity = 2;
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeActivationStatus(other.gameObject.transform.parent.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (exitTime != -1)
        {
            MoveAway();
            if (Time.time > exitTime)
                Destroy(transform.parent.gameObject);
        }
    }

    public void SetMoving()
    {
        exitTime = Time.time + despawnDuration;
        Instantiate(despawnEffect, transform.position + Vector3.forward, transform.rotation, transform);
    }

    void MoveAway()
    {
        transform.parent.Translate(0, 0, (-1 / despawnDuration) * Time.deltaTime);
    }

    void ChangeActivationStatus(GameObject player)
    {
        //Visual effect
        activated = !activated;
        GetComponent<Light>().intensity = activated ? 2 : 0;
        //Update MutatorTriggers
        player.GetComponent<MutablePlayer>().UpdateBonuses( player.GetComponent<MutablePlayer>().WALL_BREAKER_TYPE, activated ? 1 : -1);
    }
}
