using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private int layer_mask;
    private bool tryInteract;
    private bool highlighting;
    private Transform lastTransformHit;

    public float interactDistance;

    private void Start()
    {
        layer_mask = LayerMask.GetMask("Interactable");
        highlighting = false;
        lastTransformHit = null;
    }

    void Update()
    {
        if ((Input.GetButtonDown("Fire2"))|| (Input.GetButtonDown("Fire1")))
        {
            tryInteract = true;
        }
        if ((Input.GetButtonUp("Fire2")) || (Input.GetButtonUp("Fire1")))
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
            hit.transform.GetComponent<MeshRenderer>().materials[1].SetFloat("Vector1_43C9FF66", 1);
            highlighting = true;
            lastTransformHit = hit.transform;

            if (tryInteract)
            {
                Debug.Log("Attempting to Interact with: " + hit.transform.name);
                if(hit.transform.GetComponent<IInteractable>() != null)
                {
                    hit.transform.GetComponent<IInteractable>().Interact();
                }

            }
        } else
        {
            if ((lastTransformHit != null) && (highlighting))
            {
                highlighting = false;
                lastTransformHit.GetComponent<MeshRenderer>().materials[1].SetFloat("Vector1_43C9FF66", 0);
                lastTransformHit = null;
            }
        }
                


    }
}
