using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    public CanvasGroup layerNumCG;
    public TMP_Text layerNum;

    public CanvasGroup anxietyAttackCG;
    public TMP_Text anxietyAttackText;
    public float fadeInTime;
    public float keepOnTime;
    public float fadeOutTime;

    public CanvasGroup phobiaAfflictionCG;
    public TMP_Text phobiaAfflictionText;

    public Slider spellcraftSlider;
    public Slider sanitySlider;

    public GameObject phobiasGainedPrefab;
    public GameObject phobiaPanel;

    public CanvasGroup GameOverPanel;
    public CanvasGroup MainUIPanel;

    public CanvasGroup GameOverMessage;
    public CanvasGroup GameOverLayerText;
    public CanvasGroup GameOverLayerNum;
    public TMP_Text GameOverLayerNumTMP;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        GameManager.Instance.onExitDoorDestroyed += OnExitDoorDestroyed;
        GameManager.Instance.onChangedSpellcraft += OnChangedSpellcraft;
        GameManager.Instance.onChangedSpellcraftTotal += OnChangedSpellcraftTotal;
        GameManager.Instance.onChangedSanity += OnChangedSanity;
        GameManager.Instance.onChangedSanityTotal += OnChangedSanityTotal;
        GameManager.Instance.onTutorialTriggered += OnTutorialTriggered;
        GameManager.Instance.onPhobiaGained += OnPhobiaGained;
        GameManager.Instance.onGameOver += OnGameOver;
        // Testing popup text
        anxietyAttackText.text = "In case your mouse goes crazy, press a number key to adjust sensitivity. 1 is the least sensitive, 9 is more sensitive.";
        StartCoroutine(PopupFade(anxietyAttackCG, fadeInTime, keepOnTime+5, fadeOutTime));

    }

    private void OnDestroy()
    {
        GameManager.Instance.onExitDoorDestroyed -= OnExitDoorDestroyed;
        GameManager.Instance.onChangedSpellcraft -= OnChangedSpellcraft;
        GameManager.Instance.onChangedSpellcraftTotal -= OnChangedSpellcraftTotal;
        GameManager.Instance.onChangedSanity -= OnChangedSanity;
        GameManager.Instance.onChangedSanityTotal -= OnChangedSanityTotal;
        GameManager.Instance.onPhobiaGained -= OnPhobiaGained;
        GameManager.Instance.onGameOver -= OnGameOver;
    }

    private void OnChangedSpellcraftTotal()
    {
        spellcraftSlider.value = GameManager.Instance.spellcraft / GameManager.Instance.spellcraftMax;

    }

    private void OnChangedSpellcraft()
    {
        spellcraftSlider.value = GameManager.Instance.spellcraft / GameManager.Instance.spellcraftMax;

    }

    private void OnChangedSanityTotal()
    {
        sanitySlider.value = GameManager.Instance.sanity / GameManager.Instance.sanityMax;

    }

    private void OnChangedSanity()
    {
        sanitySlider.value = GameManager.Instance.sanity / GameManager.Instance.sanityMax;

    }

    private void OnExitDoorDestroyed()
    {
        layerNumCG.alpha = 1f;
        layerNum.text = GameManager.Instance.getCurrentLayer().ToString();
        GameOverLayerNumTMP.text = GameManager.Instance.getCurrentLayer().ToString();


    }

    private void OnTutorialTriggered(string message)
    {
        anxietyAttackText.text = message;
        StartCoroutine(PopupFade(anxietyAttackCG, fadeInTime, keepOnTime, fadeOutTime));


    }


    private void OnPhobiaGained(PhobiasSO phob)
    {
        // create phobia line item from prefab
        // send phobia ui element to function for display
        GameObject phobiaToInstantiate = Instantiate(phobiasGainedPrefab, new Vector3(0,0,0), Quaternion.identity, phobiaPanel.transform);
        TMP_Text phobiaAddedTMP = phobiaToInstantiate.GetComponent<TMP_Text>();
        CanvasGroup phobiaCG = phobiaToInstantiate.GetComponent<CanvasGroup>();
        phobiaAddedTMP.text = phob.name + " - " + phob.description;

        phobiaAfflictionText.text = phob.developed;

        StartCoroutine(FadeInText(fadeInTime+6,phobiaCG));
        StartCoroutine(PopupFade(phobiaAfflictionCG, fadeInTime, keepOnTime+3, fadeOutTime));
    }

    private void OnGameOver()
    {
        StartCoroutine(FadeOutText(fadeOutTime, MainUIPanel));
        StartCoroutine(FadeInText(fadeInTime + 3, GameOverPanel));
        StartCoroutine(GameOverFadeIn(GameOverMessage, GameOverLayerText, GameOverLayerNum));

    }




    void Update()
    {
        
    }














    private IEnumerator PopupFade(CanvasGroup fadingText, float timeIn, float timeOn, float timeOut)
    {
        yield return StartCoroutine(FadeInText(timeIn, fadingText));
        yield return new WaitForSeconds(timeOn);
        yield return StartCoroutine(FadeOutText(timeOut, fadingText));
    }

    private IEnumerator FadeInText(float t, CanvasGroup cg)
    {
        while (cg.alpha < 1.0f)
        {
            cg.alpha += Time.fixedDeltaTime/t;
            yield return null;
        }
    }

    private IEnumerator FadeOutText(float t, CanvasGroup cg)
    {
        while (cg.alpha > 0.0f)
        {
            cg.alpha -= Time.fixedDeltaTime/t;
            yield return null;
        }
    }


    private IEnumerator GameOverFadeIn(CanvasGroup message, CanvasGroup layerText, CanvasGroup layerNum)
    {
        yield return StartCoroutine(FadeInText(5, message));
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(FadeInText(3, layerText));
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(FadeInText(3, layerNum));

    }
}
