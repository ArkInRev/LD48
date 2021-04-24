using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSphere : MonoBehaviour, IInteractable
{
    public bool isActive = true;
   

    public void Interact()
    {
        if (isActive)
        {
            GameManager.Instance.interactWithCheckpoint(this.GetComponentInParent<Checkpoint>().playerSpawnPos, this.GetComponentInParent<Checkpoint>().thisCheckpointSpawn, this.gameObject);
            SetActiveState(false);
        }
    }

public void SetActiveState(bool activeState)
    {
        isActive = activeState;
    }
}
