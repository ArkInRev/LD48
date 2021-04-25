using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebris : MonoBehaviour
{
    public GameObject layerParent;
    public GameObject debris;


    public GameObject goInstantiated;
    private float debrisChance;

    private void Start()
    {
        debrisChance = Mathf.Clamp(GameManager.Instance.debrisChanceBase + (GameManager.Instance.debrisAdditionPerLayer * GameManager.Instance.getCurrentLayer()),0,GameManager.Instance.debrisSaturation);
        float debrisRoll = Random.Range(0f, 1f);
        Debug.Log("Trying to instantiate Debris... " + debrisChance.ToString() + " chance against a " + debrisRoll.ToString() + " roll.");
        if (debrisRoll <= debrisChance)
        {
            Debug.Log("In the debris instantiation...");
            goInstantiated = Instantiate(debris, gameObject.transform.position, GetSpawnRotation(Random.Range(0,360)), LayerGenerationManager.Instance.currentLayerParent.transform);
        }
        Destroy(gameObject);
    }

    Quaternion GetSpawnRotation(float rotationAngle)
    {
        Quaternion rotationQ = Quaternion.identity;
        rotationQ.eulerAngles = new Vector3(0, rotationAngle, 0);

        return rotationQ;
    }
}
