using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnArtifact : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        //teleport player to start
        GameManager.Instance.interactWithReturnArtifact(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onReturnArtifactUsed += OnReturnArtifactUsed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameManager.Instance.onReturnArtifactUsed -= OnReturnArtifactUsed;

    }

    private void OnReturnArtifactUsed(GameObject usedArtifact)
    {
       // if this one was used, do some effect
    }
}
