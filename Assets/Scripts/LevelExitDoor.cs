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
        thisExitLockNeeded = GameManager.Instance.exitDoorBaseOpeningTime + (GameManager.Instance.exitDoorMultiplierPerLayer * GameManager.Instance.getCurrentLayer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {
        //throw new System.NotImplementedException();
        if (GameManager.Instance.spellcraft >= Time.fixedDeltaTime)
        {
            thisExitLockProgress += Time.fixedDeltaTime;
            GameManager.Instance.changeSpellcraft(-1 * Time.fixedDeltaTime);
            doorProgressSlider.value = (thisExitLockProgress / thisExitLockNeeded);
        }




        if (thisExitLockProgress >= thisExitLockNeeded)
        {
            GameManager.Instance.destroyedExitDoor();
            Destroy(gameObject);
        }



    }



}
