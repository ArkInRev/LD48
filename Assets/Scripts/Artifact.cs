using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour, IInteractable
{
    public MeshRenderer mesh;
    private float artifactMinDrain;
    private float artifactMaxDrain;
    private float artifactTotalToDrain;
    private float artifactTotalDrained;
    private Color breakingColor;
    private Color lerpedBreakingColor;

    void Start()
    {
        breakingColor = GameManager.Instance.drainingColor;
        lerpedBreakingColor = Color.white;
        artifactMinDrain = GameManager.Instance.artifactMinDrain;
        artifactMaxDrain = GameManager.Instance.artifactMaxDrain;
        artifactTotalToDrain = Random.Range(artifactMinDrain, artifactMaxDrain);
        artifactTotalDrained = 0;


    }

    public void Interact()
    {
        artifactTotalDrained += Time.fixedDeltaTime;
        lerpedBreakingColor = Color.Lerp(Color.white, breakingColor, artifactTotalDrained / artifactTotalToDrain);
        mesh.materials[0].color = lerpedBreakingColor;

        GameManager.Instance.changeSpellcraft(Time.fixedDeltaTime);
        GameManager.Instance.changeSpellcraftTotal(Time.fixedDeltaTime);


        if (artifactTotalDrained >= artifactTotalToDrain)
        {
            GameManager.Instance.bookDestroyed();
            Destroy(gameObject);
        }
    }

    public void Update()
    {

    }
}
