using System;
using System.Collections;
using System.Collections.Generic;
//using TMPro;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private int currentGameLayer = 0;
    public Transform playerCheckpointTransform;
    public Transform playerReturnTransform;
    public GameObject player;

    #region Setting adjustments
    [Header("Game Settings")]
    public bool staticFloors;
    public Color breakingColor;
    // Exit Doors
    public float exitDoorBaseOpeningTime;
    public float exitDoorMultiplierPerLayer;
    //Debris
    public float minDebrisDestroyRange;
    public float maxDebrisDestroyRange;
    public float maxDebrisDestroy;
    public float debrisAdditiveLayerMultiplier;
    public float debrisChanceBase;
    public float debrisAdditionPerLayer;
    public float debrisSaturation;
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region game data functions

    public int getCurrentLayer()
    {
        return currentGameLayer;
    }

    #endregion

    #region Gameplay

    public void interactWithCheckpoint(Transform playerCheckpoint, GameObject checkpointSpawn, GameObject checkpointSphere)
    {
        playerCheckpointTransform = playerCheckpoint;
        checkpointChanged(checkpointSpawn, checkpointSphere);
    }

    public void interactWithReturnArtifact(GameObject returnArtifactGO)
    {
        player.transform.position = playerReturnTransform.position;
        returnArtifactUsed(returnArtifactGO);
    }

    public void interactWithDescendArtifact()
    {
        if (playerCheckpointTransform != null)
        {
            player.transform.position = playerCheckpointTransform.position;
            descendArtifactUsed();
        }
    }

    public void interactWithExitDoor()
    {

    }

    #endregion

    #region Layer Generation Events

    public event Action onGenerateNextLayer;
    public void generateNextLayer()
    {
        if (onGenerateNextLayer != null)
        {
            onGenerateNextLayer();
        }
        currentGameLayer += 1;
    }

    #endregion


    #region Interaction Events


    public event Action<GameObject,GameObject> onCheckpointChanged;
    public void checkpointChanged(GameObject checkpointSpawn, GameObject checkpointSphere)
    {
        if (onCheckpointChanged != null)
        {
            onCheckpointChanged(checkpointSpawn,checkpointSphere);
           /// Debug.Log("Changing Checkpoint...");
        }
    }

    public event Action<GameObject> onReturnArtifactUsed;
    public void returnArtifactUsed(GameObject artifactUsed)
    {
        if (onReturnArtifactUsed != null)
        {
            onReturnArtifactUsed(artifactUsed);
            //Debug.Log("Returning Home...");
        }
    }

    public event Action onDescendArtifactUsed;
    public void descendArtifactUsed()
    {
        if (onDescendArtifactUsed != null)
        {
            onDescendArtifactUsed();
            //Debug.Log("Returning Deeper and Deeper...");
        }
    }

    public event Action onDebrisDestroyed;
    public void debrisDestroyed()
    {
        if (onDebrisDestroyed != null)
        {
            onDebrisDestroyed();
            Debug.Log("Debris destroyed...");
        }
    }

    #endregion

    #region Player Events

    #endregion



}
