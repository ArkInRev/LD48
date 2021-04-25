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
    public Color drainingColor;
    // Exit Doors
    public float exitDoorBaseOpeningTime;
    public float exitDoorMultiplierPerLayer;
    // Debris
    public float minDebrisDestroyRange;
    public float maxDebrisDestroyRange;
    public float maxDebrisDestroy;
    public float debrisAdditiveLayerMultiplier;
    public float debrisChanceBase;
    public float debrisAdditionPerLayer;
    public float debrisSaturation;
    // Player
    public float spellcraftMax;
    public float spellcraft;
    public float sanityMax;
    public float sanity;
    public List<Phobias> phobias;
    //artifact
    public float bookMinDrain;
    public float bookMaxDrain;
    public float artifactMinDrain;
    public float artifactMaxDrain;
    public float artifactChanceBase;
    public float artifactAdditionPerLayer;
    public float artifactSaturation;
    // Phobias
    public List<Phobias> availablePhobias;
    #endregion

    #region Phobias enum

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
        availablePhobias.Add(Phobias.ATAXOPHOBIA);
        availablePhobias.Add(Phobias.BATHMOPHOBIA);
        availablePhobias.Add(Phobias.BIBLIOPHOBIA);
        availablePhobias.Add(Phobias.ERGOPHOBIA);
        availablePhobias.Add(Phobias.KOINONIPHOBIA);
        availablePhobias.Add(Phobias.NYCTOPHOBIA);

        changeSpellcraft(0);
        changeSpellcraftTotal(0);




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
        spellcraft = spellcraftMax;
        changedSpellcraft();
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

    public void changeSpellcraft(float amount)
    {
        spellcraft = Mathf.Clamp(spellcraft + amount, 0, spellcraftMax);
        changedSpellcraft();
    }

    public void changeSpellcraftTotal(float amount)
    {
        spellcraftMax += amount;
        changedSpellcraftTotal();
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
            //Debug.Log("Debris destroyed...");
        }
    }

    public event Action onExitDoorDestroyed;
    public void destroyedExitDoor()
    {
        if (onExitDoorDestroyed != null)
        {
            onExitDoorDestroyed();
            //Debug.Log("Exit Door Destroyed...");
        }
    }


    public event Action onBookDestroyed;
    public void bookDestroyed()
    {
        if (onBookDestroyed != null)
        {
            onBookDestroyed();
            //Debug.Log("Debris destroyed...");
        }
    }

    public event Action onChangedSpellcraft;
    public void changedSpellcraft()
    {
        if (onChangedSpellcraft != null)
        {
            onChangedSpellcraft();
        }
    }

    public event Action onChangedSpellcraftTotal;
    public void changedSpellcraftTotal()
    {
        if (onChangedSpellcraftTotal != null)
        {
            onChangedSpellcraftTotal();

        }
    }
    #endregion

    #region Player Events

    #endregion



}
