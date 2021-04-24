using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelLoadTrigger : MonoBehaviour
{
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            if (!hasTriggered)
            {
                Debug.Log("Player triggering next level load, calling GM");
                hasTriggered = true;
                GameManager.Instance.generateNextLayer();
            }
        }
    }

}
