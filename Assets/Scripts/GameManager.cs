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
    public List<PhobiasSO> phobias;
    public float spellcraftRestoreMultiplier;
    //artifact
    public float bookMinDrain;
    public float bookMaxDrain;
    public float artifactMinDrain;
    public float artifactMaxDrain;
    public float artifactChanceBase;
    public float artifactAdditionPerLayer;
    public float artifactSaturation;
    // Phobias
    public List<PhobiasSO> allPhobias;
    public List<PhobiasSO> availablePhobias;

    public float phobiaPenaltyDiv;

    public bool hasAtaxMess;
    public bool hasBathSlope;
    public bool hasBiblioBook;
    public bool hasErgoWork;
    public bool hasKoinRoom;
    public bool hasNyctDark;

    private bool triggeringAtaxMess;
    private bool triggeringBathSlope;
    private bool triggeringBiblioBook;
    private bool triggeringErgoWork;
    private bool triggeringKoinRoom;
    private bool triggeringNyctDark;

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
        //        availablePhobias.Add(Phobias.ATAXOPHOBIA);
        //        availablePhobias.Add(Phobias.BATHMOPHOBIA);
        //        availablePhobias.Add(Phobias.BIBLIOPHOBIA);
        //       availablePhobias.Add(Phobias.ERGOPHOBIA);
        //        availablePhobias.Add(Phobias.KOINONIPHOBIA);
        //        availablePhobias.Add(Phobias.NYCTOPHOBIA);
        resetAvailablePhobias();
        changeSpellcraft(0);
        changeSpellcraftTotal(0);




    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {

    }

    private void FixedUpdate()
    {
        // Lower sanity over time
        float sanityLost = Time.fixedDeltaTime;
        changeSanity(-1 * sanityLost);
        // handle running out of sanity
        if (sanity <= 0)
        {
            sanity = sanityMax;
            teleportPlayerToStart();
            if (phobias.Count >= 3)
            {
                //do game over
                gameOver();
            } else
            {
                gainRandomPhobia();
            }

        }

        if (triggeringAtaxMess) doPhobiaDamage();



    }



    #region game data functions

    public int getCurrentLayer()
    {
        return currentGameLayer;
    }

    #endregion

    #region Gameplay

    private void doPhobiaDamage()
    {
        changeSanity(-1*Time.fixedDeltaTime / phobiaPenaltyDiv);
    }

    public void teleportPlayerToStart()
    {
        player.transform.position = playerReturnTransform.position;
    }

    public void gainRandomPhobia()
    {
        PhobiasSO selectedPhobia;

        int index = UnityEngine.Random.Range((int)0, (int)availablePhobias.Count);
        selectedPhobia = availablePhobias[index];
        availablePhobias.RemoveAt(index);
        phobias.Add(selectedPhobia);
        phobiaGained(selectedPhobia);
        switch (selectedPhobia.phobiaID)
        {
            case Phobias.ATAXOPHOBIA:
                hasAtaxMess = true;
                break;
            case Phobias.BATHMOPHOBIA:
                hasBathSlope = true;
                break;
            case Phobias.BIBLIOPHOBIA:
                hasBiblioBook = true;
                break;
            case Phobias.ERGOPHOBIA:
                hasErgoWork = true;
                break;
            case Phobias.NYCTOPHOBIA:
                hasNyctDark = true;
                break;
        }

    }

    public void resetAvailablePhobias()
    {
        availablePhobias = new List<PhobiasSO>();
        for(int i = 0; i < allPhobias.Count; i++)
        {
            availablePhobias.Add(allPhobias[i]);
        }
    }

    public void handleTooManyPhobia()
    {

    }


    public void startGameOver()
    {

    }

    public void triggeringPhobia(Phobias phob)
    {
        switch (phob)
        {

        
        case Phobias.ATAXOPHOBIA:
                triggeringAtaxMess = true;
        break;
            case Phobias.BATHMOPHOBIA:
                triggeringBathSlope = true;
        break;
            case Phobias.BIBLIOPHOBIA:
                triggeringBiblioBook = true;
        break;
            case Phobias.ERGOPHOBIA:
                triggeringErgoWork = true;
        break;
            case Phobias.NYCTOPHOBIA:
                triggeringNyctDark = true;
        break;
        }
    }

    public void stopTriggeringPhobia(Phobias phob)
    {
        switch (phob)
        {


            case Phobias.ATAXOPHOBIA:
                triggeringAtaxMess = false;
                break;
            case Phobias.BATHMOPHOBIA:
                triggeringBathSlope = false;
                break;
            case Phobias.BIBLIOPHOBIA:
                triggeringBiblioBook = false;
                break;
            case Phobias.ERGOPHOBIA:
                triggeringErgoWork = false;
                break;
            case Phobias.NYCTOPHOBIA:
                triggeringNyctDark = false;
                break;
        }
    }

    public void interactWithCheckpoint(Transform playerCheckpoint, GameObject checkpointSpawn, GameObject checkpointSphere)
    {
        playerCheckpointTransform = playerCheckpoint;
        checkpointChanged(checkpointSpawn, checkpointSphere);
    }

    public void interactWithReturnArtifact(GameObject returnArtifactGO)
    {
        teleportPlayerToStart();
        //spellcraft = spellcraftMax;
        //changedSpellcraft();
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

    public void changeSanity(float amount)
    {
        sanity = Mathf.Clamp(sanity + amount, 0, sanityMax);
        changedSanity();
    }

    public void changeSanityTotal(float amount)
    {
        sanityMax += amount;
        changedSanityTotal();
    }

    public void triggerTutorial(string message)
    {
        tutorialTriggered(message);
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

    public event Action onChangedSanity;
    public void changedSanity()
    {
        if (onChangedSanity != null)
        {
            onChangedSanity();
        }
    }

    public event Action onChangedSanityTotal;
    public void changedSanityTotal()
    {
        if (onChangedSanityTotal != null)
        {
            onChangedSanityTotal();

        }
    }

    public event Action<string> onTutorialTriggered;
    public void tutorialTriggered(string msg)
    {
        if (onTutorialTriggered != null)
        {
            onTutorialTriggered(msg);

        }
    }

    public event Action<PhobiasSO> onPhobiaGained;
    public void phobiaGained(PhobiasSO phob)
    {
        if (onPhobiaGained != null)
        {
            onPhobiaGained(phob);

        }
    }
    #endregion

    #region Game State Events

    public event Action onGameOver;
    public void gameOver()
    {
        if (onGameOver != null)
        {
            onGameOver();

        }
    }

    #endregion



}
