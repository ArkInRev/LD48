using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelExitDoor : MonoBehaviour, IInteractable
{

    public Slider doorProgressSlider;

    public float thisExitLockNeeded;
    private float thisExitLockProgress = 0f;

    // Start is called before the first frame update
    void Start()
    {
        thisExitLockNeeded = 1+(2*GameManager.Instance.getCurrentLayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        //throw new System.NotImplementedException();
        thisExitLockProgress += Time.fixedDeltaTime;
        doorProgressSlider.value = (thisExitLockProgress / thisExitLockNeeded);
        
        
        
        if(thisExitLockProgress >= thisExitLockNeeded)
        {
            Destroy(gameObject);
        }
        


    }
}
