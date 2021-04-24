using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LayerGenerationManager : MonoBehaviour
{

    private static LayerGenerationManager _instance;
    public static LayerGenerationManager Instance { get { return _instance; } }

    public GameObject[] basicRoom; // common/generic rooms
    public float[] rotations = { 0.0f, 90f, 180f, 270f }; // rotation of instantiated room
    private GameObject goInstantiated;

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
    public void Start()
    {
        GameManager.Instance.onGenerateNextLayer += OnGenerateNextLayer;
    }

    public void OnDestroy()
    {
        GameManager.Instance.onGenerateNextLayer -= OnGenerateNextLayer;
    }

    public void OnGenerateNextLayer()
    {
        goInstantiated = Instantiate(randomPrefab(basicRoom), new Vector3(0f,0f,0f), GetSpawnRotation(randomPrefabRotation()));
    }

    GameObject randomPrefab(GameObject[] goArray)
    {
        GameObject randomGO;
        int randIndex = Random.Range((int)0, goArray.Length);
        randomGO = goArray[randIndex];

        return randomGO;
    }

    float randomPrefabRotation()
    {
        float randomRot;
        int randIndex = Random.Range((int)0, rotations.Length);
        randomRot = rotations[randIndex];

        return randomRot;
    }
    Quaternion GetSpawnRotation(float rotationAngle)
    {
        Quaternion rotationQ = Quaternion.identity;
        rotationQ.eulerAngles = new Vector3(0, rotationAngle, 0);
        
        return rotationQ;
    }
}
