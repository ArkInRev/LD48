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

    #region Layer Generation Events

    public event Action onGenerateNextLayer;
    public void generateNextLayer()
    {
        if (onGenerateNextLayer != null)
        {
            onGenerateNextLayer();
        }

    }

    #endregion


    #region Interaction Events

    #endregion

    #region Player Events

    #endregion



}
