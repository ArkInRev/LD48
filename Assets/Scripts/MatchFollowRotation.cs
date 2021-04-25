using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFollowRotation : MonoBehaviour
{
    public GameObject thingToMatch;

    private void FixedUpdate()
    {
        this.transform.localEulerAngles = thingToMatch.transform.localEulerAngles;
    }

}
