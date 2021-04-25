using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArtifact : MonoBehaviour
{
    public GameObject layerParent;
    public GameObject[] artifacts;


    public GameObject goInstantiated;
    private float artifactChance;

    private void Start()
    {
        artifactChance = Mathf.Clamp(GameManager.Instance.artifactChanceBase + (GameManager.Instance.artifactAdditionPerLayer * GameManager.Instance.getCurrentLayer()), 0, GameManager.Instance.artifactSaturation);
        float artifactRoll = Random.Range(0f, 1f);
        Debug.Log("Trying to instantiate artifact... " + artifactChance.ToString() + " chance against a " + artifactRoll.ToString() + " roll.");
        if (artifactRoll <= artifactChance)
        {
            Debug.Log("In the artifact instantiation...");
            goInstantiated = Instantiate(artifacts[(int)Random.Range(0,artifacts.Length)], gameObject.transform.position, GetSpawnRotation(Random.Range(0, 360)), LayerGenerationManager.Instance.currentLayerParent.transform);
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
