using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    internal static PlayerController Instance;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed = 120f;

    float vertical;
    float horizontal;
    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        PlayerInput();
        PlayAnimation();
    }
    private void FixedUpdate()
    {
        PlayerMoving();
    }

    private void PlayerMoving()
    {
        rigidbody.velocity = new Vector3(Horizontal(), rigidbody.velocity.y, Vertical());
    }
    void PlayAnimation()
    {
        if (IsMoving())
        {
            animator.SetFloat(GameString.Horizontal, Horizontal());
            animator.SetFloat(GameString.Vertical, Vertical());
        }
    }
   
    bool IsMoving()
    {
        return Horizontal() != 0 || vertical != 0;
    }
  
    void PlayerInput()
    {
        vertical = Input.GetAxis(GameString.Vertical);
        horizontal = Input.GetAxis(GameString.Horizontal);
    }

    float Horizontal()
    {
        return horizontal * movementSpeed * Time.fixedDeltaTime;
    }
    float Vertical()
    {
        return vertical * movementSpeed * Time.fixedDeltaTime;
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == GameString.Zone)
        {
            animator.SetTrigger(GameString.InDancingAnimation);
        }
        else if (other.gameObject.tag == GameString.InRoom)
        {
            GoToRoom();
        }
        else if (other.gameObject.tag == GameString.OutRoom)
        {

            GoOutRoom();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == GameString.Zone)
        {
            animator.SetTrigger(GameString.OutDancingAnimation);
        }
       
    }
    void StopMove()
    {
        movementSpeed = 0;
    }
    void GoToRoom()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() => { UiGame.Instance.ShowBlackPanel();});
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() => { StopMove(); });
        seq.AppendCallback(() => { StartCoroutine(Data.Instance.GetText());});
        seq.AppendInterval(2f);
        seq.AppendCallback(() => {  });
        seq.AppendCallback(() => { SceneManger.Instance.LoadScene(GameString.RoomScene); });
        seq.AppendCallback(() => { UiGame.Instance.HideBlackPanel(); });
    }

   void GoOutRoom()
    {
        Sequence seq = DOTween.Sequence();
       
       
        seq.AppendCallback(() => { UiGame.Instance.ShowBlackPanel(); });
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() => {StopMove(); });
        seq.AppendInterval(2f);
        seq.AppendCallback(() => { SceneManger.Instance.LoadScene(GameString.MainScene); });
        seq.AppendCallback(() => { UiGame.Instance.HideBlackPanel(); });
    }

   
}
