using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameLayer
{
    public int thisLayerNumber;
    public int layerWidth;
    public int layerHeight;
    public Vector3 layerZeroZero;

    public Direction entranceWall;
    public int entranceWallLoc;
    public Vector3 entrancePos;
    public Direction exitWall;
    public int exitWallLoc;
    public Vector3 exitPos;
    public float exitLockNeeded;
    public float exitLockProgress;
    public GameObject[] basicRoom;
    public List<GameObject> unlockedRoom;
    public int artifactsOnLayer;
    public int artifactsSecured;
    public float collapsePercent;


}

public enum Direction
{
    NORTH,
    SOUTH,
    EAST,
    WEST
}

public enum Phobias
{
    ERGOPHOBIA, //fear of work, removing debris
    BIBLIOPHOBIA, //fear of books
    BATHMOPHOBIA, //fear of slopes or stairs
    NYCTOPHOBIA,   //fear of the dark
    ATAXOPHOBIA, // fear of a mess
    KOINONIPHOBIA // fear of rooms
}