using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform playerSpawnPos;
    public GameObject thisCheckpointSpawn;
    public GameObject thisCheckpointSphere;
    public Light thisOrbLight;
    public Light thisSpawnLight;

    public Material activeMat;
    public Material inactiveMat;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onCheckpointChanged += OnCheckpointChanged;
        GameManager.Instance.onDescendArtifactUsed += OnDescendArtifactUsed;

        playerSpawnPos = this.transform.GetChild(0).transform;
        thisCheckpointSpawn = this.transform.GetChild(1).gameObject;
        thisCheckpointSphere = this.transform.GetChild(2).gameObject;
    }

    private void OnCheckpointChanged(GameObject checkpointSpawn, GameObject checkpointSphere)
    {
        //Debug.Log("Listener stepping through changed checkpoint.");
        if(checkpointSpawn == thisCheckpointSpawn)
        {
            //Debug.Log("change to active material and increase the spawn light");
            thisCheckpointSpawn.GetComponent<MeshRenderer>().material= activeMat;
            thisSpawnLight.enabled = true;
        }
        else
        {
            //Debug.Log("change to inactive material and decrease the spawn light");
            thisCheckpointSpawn.GetComponent<MeshRenderer>().material = inactiveMat;
            thisSpawnLight.enabled = false;
        }

        if (checkpointSphere == thisCheckpointSphere)
        {
            //Debug.Log("change to the inactive material and decrease the sphere light");
            thisCheckpointSphere.GetComponent<MeshRenderer>().material = inactiveMat;
            thisCheckpointSphere.GetComponent<CheckpointSphere>().SetActiveState(false);
            thisOrbLight.enabled = false;
        }
        else
        {
            //Debug.Log("change to the active material and increase the sphere light");
            thisCheckpointSphere.GetComponent<MeshRenderer>().material = activeMat;
            thisCheckpointSphere.GetComponent<CheckpointSphere>().SetActiveState(true);
            thisOrbLight.enabled = true;
        }


    }

    private void OnDescendArtifactUsed()
    {
        if(GameManager.Instance.playerCheckpointTransform == playerSpawnPos)
        {
            Debug.Log("Should teleport to this location and do an effect.");
        }
    }
}
