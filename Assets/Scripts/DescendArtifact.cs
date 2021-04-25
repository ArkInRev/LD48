using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescendArtifact : MonoBehaviour, IInteractable
{
    public GameObject tutorialTrigger;
    public MeshRenderer mesh;

    public void Interact()
    {
        //teleport player to start
        GameManager.Instance.interactWithDescendArtifact();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onCheckpointChanged += OnCheckpointChanged;
    }

    private void OnCheckpointChanged(GameObject spawn, GameObject orb)
    {
        //enable mesh, box collider, change layer
        mesh.enabled = true;
        this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<BoxCollider>().enabled = true;
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
        tutorialTrigger.SetActive(true);
        //Unregister, only need to know the first
        GameManager.Instance.onCheckpointChanged -= OnCheckpointChanged;
    }
}
