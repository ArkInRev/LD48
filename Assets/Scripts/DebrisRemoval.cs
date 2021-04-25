using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisRemoval : MonoBehaviour, IInteractable
{
    public MeshRenderer mesh;

    private float minTimeToDestroyRange;
    private float maxTimeToDestroyRange;
    private float maxTimeToDestroy;
    private float layerAdditiveMultiplier;
    private float thisTimeToDestroy;
    private float destroyProgress;

    private Color breakingColor;
    private Color lerpedBreakingColor;

    public void Interact()
    {
        destroyProgress += Time.fixedDeltaTime;
        lerpedBreakingColor = Color.Lerp(Color.white, breakingColor,destroyProgress/thisTimeToDestroy);
        mesh.materials[0].color = lerpedBreakingColor;

        if (destroyProgress >= thisTimeToDestroy)
        {
            GameManager.Instance.debrisDestroyed();
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        breakingColor = GameManager.Instance.breakingColor;
        lerpedBreakingColor = Color.white;

        minTimeToDestroyRange = GameManager.Instance.minDebrisDestroyRange;
        maxTimeToDestroy = GameManager.Instance.maxDebrisDestroyRange;
        maxTimeToDestroy = GameManager.Instance.maxDebrisDestroy;
        layerAdditiveMultiplier = GameManager.Instance.debrisAdditiveLayerMultiplier;
        float randomTimeToDestroy = Random.Range(minTimeToDestroyRange, maxTimeToDestroyRange);
        float additiveLayer = GameManager.Instance.getCurrentLayer() * layerAdditiveMultiplier;
        thisTimeToDestroy = Mathf.Clamp(randomTimeToDestroy + additiveLayer, minTimeToDestroyRange, maxTimeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
