using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    public float energyRestoreMultiplier;

    private void Start()
    {
        energyRestoreMultiplier = GameManager.Instance.spellcraftRestoreMultiplier;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.changeSanity(GameManager.Instance.sanityMax);
            GameManager.Instance.changeSpellcraft(Time.fixedDeltaTime * energyRestoreMultiplier);
        }
    }

    private void FixedUpdate()
    {
        
    }
}
