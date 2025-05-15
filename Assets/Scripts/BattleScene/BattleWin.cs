using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using WandComponentNS;

public class BattleWin : MonoBehaviour
{
    public GameObject typeComponent;
    public GameObject modifierComponent;
    public List<WandComponentSO> reward1 = new List<WandComponentSO>();
    public List<WandComponentSO> reward2 = new List<WandComponentSO>();
    public List<WandComponentSO> reward3 = new List<WandComponentSO>();

    public TMP_Text reward1Text;
    public TMP_Text reward2Text;
    public TMP_Text reward3Text;
    public TMP_Text selectText;

    public RewardDisplay display1;
    public RewardDisplay display2;
    public RewardDisplay display3;

    public ItemInspector itemInspector1;
    public ItemInspector itemInspector2;
    public ItemInspector itemInspector3;

    public Image backgroud;
    public GameObject skipButton;

    public float fadeDuration = 2.0f; // Duration of the fade-in effect
    private Image fadeImage;
    private Color fadeColor;

    void Start()
    {
        display1.gameObject.SetActive(false);
        display2.gameObject.SetActive(false);
        display3.gameObject.SetActive(false);
        selectText.gameObject.SetActive(false);
        skipButton.SetActive(false);
        fadeImage = GetComponentInChildren<Image>();
        fadeColor = fadeImage.color;
        fadeColor.a = 1.0f; // Start fully opaque
        fadeImage.color = fadeColor;
        StartCoroutine(FadeOutEffect());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator FadeOutEffect()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(0f + (elapsedTime / fadeDuration));
            fadeImage.color = fadeColor;
            yield return null;
        }

        fadeColor.a = .975f; // Ensure it's fully visible at the end
        fadeImage.color = fadeColor;
        display1.gameObject.SetActive(true);
        display2.gameObject.SetActive(true);
        display3.gameObject.SetActive(true);
        selectText.gameObject.SetActive(true);
        skipButton.SetActive(true);
    }

    public void GetRandomRewards()
    {
        gameObject.SetActive(true);
        RewardSO rewardSO = RewardDB.Instance.GetSequentialReward();
        reward1 = rewardSO.rewards;
        reward1Text.text = rewardSO.rewardName;
        foreach (WandComponentSO item in reward1)
        {
            display1.AddReward(item);
        }
        rewardSO = RewardDB.Instance.GetSequentialReward();
        reward2 = rewardSO.rewards;
        reward2Text.text = rewardSO.rewardName;
        foreach (WandComponentSO item in reward2)
        {
            display2.AddReward(item);
        }
        rewardSO = RewardDB.Instance.GetSequentialReward();
        reward3 = rewardSO.rewards;
        reward3Text.text = rewardSO.rewardName;
        foreach (WandComponentSO item in reward3)
        {
            display3.AddReward(item);
        }
    }

    public void GetReward1()
    {
        GlobalController.Instance.gemInventory.AddRange(reward1);
        SkipRewards();
    }
    public void GetReward2()
    {
        GlobalController.Instance.gemInventory.AddRange(reward2);
        SkipRewards();
    }
    public void GetReward3()
    {
        GlobalController.Instance.gemInventory.AddRange(reward3);
        SkipRewards();
    }
    public void SkipRewards()
    {
        display1.gameObject.SetActive(false);
        display2.gameObject.SetActive(false);
        display3.gameObject.SetActive(false);
        selectText.gameObject.SetActive(false);
        skipButton.SetActive(false);
        backgroud.gameObject.SetActive(false);
    }
}
