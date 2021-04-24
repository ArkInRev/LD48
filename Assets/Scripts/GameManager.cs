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

    #endregion

    #region Player Events

    #endregion



}
