using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform playerSpawnPos;
    public GameObject thisCheckpointSpawn;
    public GameObject thisCheckpointSphere;

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
        Debug.Log("Listener stepping through changed checkpoint.");
        if(checkpointSpawn == thisCheckpointSpawn)
        {
            Debug.Log("change to active material and increase the spawn light");
            thisCheckpointSpawn.GetComponent<MeshRenderer>().material= activeMat;
        }
        else
        {
            Debug.Log("change to inactive material and decrease the spawn light");
            thisCheckpointSpawn.GetComponent<MeshRenderer>().material = inactiveMat;
        }

        if (checkpointSphere == thisCheckpointSphere)
        {
            Debug.Log("change to the inactive material and decrease the sphere light");
            thisCheckpointSphere.GetComponent<MeshRenderer>().material = inactiveMat;
            thisCheckpointSphere.GetComponent<CheckpointSphere>().SetActiveState(false);
        }
        else
        {
            Debug.Log("change to the active material and increaase the sphere light");
            thisCheckpointSphere.GetComponent<MeshRenderer>().material = activeMat;
            thisCheckpointSphere.GetComponent<CheckpointSphere>().SetActiveState(true);
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
