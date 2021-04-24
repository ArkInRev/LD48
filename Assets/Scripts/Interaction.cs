using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private int layer_mask;
    private bool tryInteract;

    public float interactDistance;

    private void Start()
    {
        layer_mask = LayerMask.GetMask("Interactable");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            tryInteract = true;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            tryInteract = false;
        }
    }



    private void FixedUpdate()
    {

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance,layer_mask))
        {
            Debug.Log(hit.transform.name + "Found!");

            if (tryInteract)
            {
                Debug.Log("Attempting to Interact with: " + hit.transform.name);
                if(hit.transform.GetComponent<IInteractable>() != null)
                {
                    hit.transform.GetComponent<IInteractable>().Interact();
                }

            }
        }


    }
}
