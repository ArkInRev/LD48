using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhobiaArea : MonoBehaviour
{
    public Phobias affectedPhobia;

    private void Start()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // tell the game manager that the phobia is in effect
            GameManager.Instance.triggeringPhobia(affectedPhobia);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // tell the game manager that the phobia is in effect
            GameManager.Instance.stopTriggeringPhobia(affectedPhobia);
        }
    }

    public void OnDestroy()
    {
        GameManager.Instance.stopTriggeringPhobia(affectedPhobia);
    }

    private void FixedUpdate()
    {

    }
}
