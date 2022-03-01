using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartGame : MonoBehaviour
{
    public int timer;

    void Start()
    {
        GoToGame();
    }

    void GoToGame()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() => { UiGame.Instance.HideBlackPanel(); });
        seq.AppendInterval(timer);
        seq.AppendCallback(() => { SceneManger.Instance.LoadScene(GameString.MainScene); });
        
    }
  
}
