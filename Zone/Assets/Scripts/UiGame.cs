
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class UiGame : MonoBehaviour
{
    public static UiGame Instance;

    public TMP_Text countText;
    public Image blackPanel;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GetCountText(Manger.Instance.count);
    }
     public void GetCountText(int count)
    {
        if (PlayerPrefs.HasKey(GameString.Access))
            count = PlayerPrefs.GetInt(GameString.Access);
        countText.text = "Count / " + count.ToString();
    }

    public void ShowBlackPanel()
    {
        blackPanel.DOFade(1f, 2f).SetEase(Ease.Linear);
    }
    public void HideBlackPanel()
    {
        blackPanel.DOFade(0, 2f).SetEase(Ease.Linear);
    }
}
