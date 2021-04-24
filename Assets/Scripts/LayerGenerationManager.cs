using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LayerGenerationManager : MonoBehaviour
{

    private static LayerGenerationManager _instance;
    public static LayerGenerationManager Instance { get { return _instance; } }

    public GameLayer[] gameLayers;

    public GameObject[] basicRoom; // common/generic rooms
    public GameObject[] basicExit; // common/generic exits
    public GameObject[] basicEntrance; // common/generic entrances

    public float roomTileSize = 15f;
    public float roomTileHeight = 12f;


    public float[] rotations = { 0.0f, 90f, 180f, 270f }; // rotation of instantiated room
    private GameObject goInstantiated;

    private GameLayer curGameLayer;

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
        //goInstantiated = Instantiate(randomPrefab(basicRoom), new Vector3(0f,0f,0f), GetSpawnRotation(randomPrefabRotation()));

        curGameLayer = gameLayers[GameManager.Instance.getCurrentLayer()];

        curGameLayer.thisLayerNumber = GameManager.Instance.getCurrentLayer() + 1;

        curGameLayer.entranceWallLoc = findEntranceLoc(curGameLayer);
        curGameLayer.exitWall = findExitEdge(curGameLayer);
        curGameLayer.exitWallLoc = findExitLoc(curGameLayer);
        curGameLayer.layerZeroZero = findLayerZeroZero(curGameLayer);
        instantiateRooms(curGameLayer);
        curGameLayer.exitPos = getExitPosition(curGameLayer);
        placeExit(curGameLayer);
    }

    private int findEntranceLoc(GameLayer thisLayer)
    {
        int entranceLoc;
        int edgeLength;
        if ((thisLayer.entranceWall == Direction.NORTH) || (thisLayer.entranceWall == Direction.SOUTH))
        {
            edgeLength = thisLayer.layerWidth;
        } else
        {
            edgeLength = thisLayer.layerHeight;
        }
        entranceLoc = Random.Range((int)0, edgeLength);

        return entranceLoc;
    }

    private Direction findExitEdge(GameLayer thisLayer)
    {
        Direction thisDir;
        List<Direction> directions = new List<Direction> {Direction.NORTH, Direction.SOUTH, Direction.EAST, Direction.WEST };

        directions.Remove(thisLayer.entranceWall);

        int index = Random.Range((int)0, (int)directions.Count);
        thisDir = directions[index];

        return thisDir;
    }

    private int findExitLoc(GameLayer thisLayer)
    {
        int exitLoc;
        int edgeLength;
        if ((thisLayer.exitWall == Direction.NORTH) || (thisLayer.exitWall == Direction.SOUTH))
        {
            edgeLength = thisLayer.layerWidth;
        }
        else
        {
            edgeLength = thisLayer.layerHeight;
        }
        exitLoc = Random.Range((int)0, edgeLength);

        return exitLoc;
    }


    private Vector3 findLayerZeroZero(GameLayer thisLayer)
    {
        Vector3 layerZeroZero = new Vector3(0,roomTileHeight-(roomTileHeight*thisLayer.thisLayerNumber),0);
        switch (thisLayer.entranceWall)
        {
            case Direction.NORTH:
                layerZeroZero.x = thisLayer.entrancePos.x - (roomTileSize * thisLayer.entranceWallLoc);
                layerZeroZero.z = (thisLayer.entrancePos.z) - (roomTileSize * thisLayer.layerHeight);
//                Debug.Log("x " + layerZeroZero.x + " :: " + layerZeroZero.y + " :: " + layerZeroZero.z);
                break;
            case Direction.SOUTH:
                layerZeroZero.x = thisLayer.entrancePos.x - (roomTileSize * thisLayer.entranceWallLoc);
                layerZeroZero.z = (thisLayer.entrancePos.z) + (roomTileSize);
                break;
            case Direction.EAST:
                layerZeroZero.x = thisLayer.entrancePos.x - (roomTileSize * thisLayer.layerWidth);
                layerZeroZero.z = (thisLayer.entrancePos.z) + (roomTileSize * thisLayer.entranceWallLoc);
                break;
            case Direction.WEST:
                layerZeroZero.x = thisLayer.entrancePos.x + (roomTileSize);
                layerZeroZero.z = (thisLayer.entrancePos.z) - (roomTileSize * thisLayer.entranceWallLoc);
                break;
        }


        return layerZeroZero;
    }

    private void instantiateRooms(GameLayer thisLayer)
    {
        for(int j = 0; j < thisLayer.layerHeight; j++)
        {
            for(int i = 0; i < thisLayer.layerWidth; i++)
            {
                goInstantiated = Instantiate(randomPrefab(basicRoom), new Vector3(thisLayer.layerZeroZero.x+(i*roomTileSize), thisLayer.layerZeroZero.y, thisLayer.layerZeroZero.z+(j*roomTileSize)), GetSpawnRotation(randomPrefabRotation()));
            }
        }
    }

    private Vector3 getExitPosition(GameLayer thisLayer)
    {
        Vector3 layerExitPos = new Vector3(0, roomTileHeight - (roomTileHeight * thisLayer.thisLayerNumber), 0);
        switch (thisLayer.exitWall)
        {
            case Direction.NORTH:
                layerExitPos.x = thisLayer.layerZeroZero.x + (roomTileSize * thisLayer.exitWallLoc);
                layerExitPos.z = thisLayer.layerZeroZero.z + (roomTileSize * thisLayer.layerHeight);
                break;
            case Direction.SOUTH:
                layerExitPos.x = thisLayer.layerZeroZero.x + (roomTileSize * thisLayer.exitWallLoc);
                layerExitPos.z = thisLayer.layerZeroZero.z - (roomTileSize);
                break;
            case Direction.EAST:
                layerExitPos.x = thisLayer.layerZeroZero.x + (roomTileSize * thisLayer.layerWidth);
                layerExitPos.z = thisLayer.layerZeroZero.z + (roomTileSize * thisLayer.exitWallLoc);
                break;
            case Direction.WEST:
                layerExitPos.x = thisLayer.layerZeroZero.x - (roomTileSize);
                layerExitPos.z = thisLayer.layerZeroZero.z + (roomTileSize * thisLayer.exitWallLoc);
                break;
        }

        return layerExitPos;
    }

    private void placeExit(GameLayer thisLayer)
    {

        //should add layer to a layer list

        //instantiate exit based on rotation
        //instantiate landing/checkpoint
        //instantiate entrance to next level
        //set entrance information for the next gameLayer

        switch (thisLayer.exitWall)
        {
            case Direction.NORTH:
                goInstantiated = Instantiate(basicExit[0], thisLayer.exitPos, GetSpawnRotation(270));
                goInstantiated = Instantiate(basicExit[1], new Vector3(thisLayer.exitPos.x, thisLayer.exitPos.y - (roomTileHeight / 2), thisLayer.exitPos.z+roomTileSize), GetSpawnRotation(270));
                goInstantiated = Instantiate(basicEntrance[0], new Vector3(thisLayer.exitPos.x+roomTileSize, thisLayer.exitPos.y - roomTileHeight, thisLayer.exitPos.z + roomTileSize), GetSpawnRotation(270));
                gameLayers[thisLayer.thisLayerNumber].entrancePos = goInstantiated.transform.position;

                break;
            case Direction.SOUTH:
                goInstantiated = Instantiate(basicExit[0], thisLayer.exitPos, GetSpawnRotation(90));
                goInstantiated = Instantiate(basicExit[1], new Vector3(thisLayer.exitPos.x,thisLayer.exitPos.y-(roomTileHeight/2),thisLayer.exitPos.z-roomTileSize), GetSpawnRotation(90));
                goInstantiated = Instantiate(basicEntrance[0], new Vector3(thisLayer.exitPos.x-roomTileSize,thisLayer.exitPos.y-roomTileHeight,thisLayer.exitPos.z-roomTileSize), GetSpawnRotation(90));
                gameLayers[thisLayer.thisLayerNumber].entrancePos = goInstantiated.transform.position;               
                break;
            case Direction.EAST:
                goInstantiated = Instantiate(basicExit[0], thisLayer.exitPos, GetSpawnRotation(0));
                goInstantiated = Instantiate(basicExit[1], new Vector3(thisLayer.exitPos.x+roomTileSize, thisLayer.exitPos.y - (roomTileHeight / 2), thisLayer.exitPos.z), GetSpawnRotation(0));
                goInstantiated = Instantiate(basicEntrance[0], new Vector3(thisLayer.exitPos.x + roomTileSize, thisLayer.exitPos.y - roomTileHeight, thisLayer.exitPos.z - roomTileSize), GetSpawnRotation(0));
                gameLayers[thisLayer.thisLayerNumber].entrancePos = goInstantiated.transform.position;
                break;
            case Direction.WEST:
                goInstantiated = Instantiate(basicExit[0], thisLayer.exitPos, GetSpawnRotation(180));
                goInstantiated = Instantiate(basicExit[1], new Vector3(thisLayer.exitPos.x - roomTileSize, thisLayer.exitPos.y - (roomTileHeight / 2), thisLayer.exitPos.z), GetSpawnRotation(180));
                goInstantiated = Instantiate(basicEntrance[0], new Vector3(thisLayer.exitPos.x - roomTileSize, thisLayer.exitPos.y - roomTileHeight, thisLayer.exitPos.z + roomTileSize), GetSpawnRotation(180));
                gameLayers[thisLayer.thisLayerNumber].entrancePos = goInstantiated.transform.position;
                break;
        }

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
