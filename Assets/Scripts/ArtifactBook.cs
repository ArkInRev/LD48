using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactBook : MonoBehaviour, IInteractable
{
    public MeshRenderer mesh;
    private float bookMinDrain;
    private float bookMaxDrain;
    private float bookTotalToDrain;
    private float bookTotalDrained;
    private Color breakingColor;
    private Color lerpedBreakingColor;

    void Start()
    {
        breakingColor = GameManager.Instance.drainingColor;
        lerpedBreakingColor = Color.white;
        bookMinDrain = GameManager.Instance.bookMinDrain;
        bookMaxDrain = GameManager.Instance.bookMaxDrain;
        bookTotalToDrain = Random.Range(bookMinDrain, bookMaxDrain);
        bookTotalDrained = 0;


    }

    public void Interact()
    {
        bookTotalDrained += Time.fixedDeltaTime;
        lerpedBreakingColor = Color.Lerp(Color.white, breakingColor, bookTotalDrained / bookTotalToDrain);
        mesh.materials[0].color = lerpedBreakingColor;

        GameManager.Instance.changeSpellcraft(Time.fixedDeltaTime);
        GameManager.Instance.changeSpellcraftTotal(Time.fixedDeltaTime);


        if (bookTotalDrained >= bookTotalToDrain)
        {
            GameManager.Instance.bookDestroyed();
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        
    }
}
