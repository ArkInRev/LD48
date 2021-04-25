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

    public Slider spellcraftSlider;
    public Slider sanitySlider;

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

        // Testing popup text
        anxietyAttackText.text = "In case your mouse goes crazy, press a number key to adjust sensitivity. 1 is the least sensitive, 9 is more sensitive.";
        StartCoroutine(PopupFade(anxietyAttackCG, fadeInTime, keepOnTime+5, fadeOutTime));

    }

    private void OnDestroy()
    {
        GameManager.Instance.onExitDoorDestroyed -= OnExitDoorDestroyed;
        GameManager.Instance.onChangedSpellcraft -= OnChangedSpellcraft;
        GameManager.Instance.onChangedSpellcraftTotal -= OnChangedSpellcraftTotal;
    }

    private void OnChangedSpellcraftTotal()
    {
        spellcraftSlider.value = GameManager.Instance.spellcraft / GameManager.Instance.spellcraftMax;

    }

    private void OnChangedSpellcraft()
    {
        spellcraftSlider.value = GameManager.Instance.spellcraft / GameManager.Instance.spellcraftMax;

    }

    private void OnExitDoorDestroyed()
    {
        layerNumCG.alpha = 1f;
        layerNum.text = GameManager.Instance.getCurrentLayer().ToString();


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
            cg.alpha += Time.fixedDeltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOutText(float t, CanvasGroup cg)
    {
        while (cg.alpha > 0.0f)
        {
            cg.alpha -= Time.fixedDeltaTime;
            yield return null;
        }
    }
}
