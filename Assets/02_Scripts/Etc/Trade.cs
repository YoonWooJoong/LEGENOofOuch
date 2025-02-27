using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour
{
    public Button acceptButton;
    public Button rejectButton;
    public GameObject devil;
    public void Awake()
    {

        acceptButton.onClick.AddListener(AcceptTrade);
        rejectButton.onClick.AddListener(RejectTrade);
        this.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        devil = GameObject.FindGameObjectWithTag("Devil");//거래완료후 악마 오브젝트를 처리하기위해
    }
    /// <summary>
    /// 플레이어가 거래버튼을 눌렀을때 게임매니저에게 수락을 보내준다.
    /// 게임매니저는 플레이어에게 extralife를 주고 생명력 30을 가져온다.
    /// 악마오브젝트 삭제
    /// </summary>
    public void AcceptTrade()
    {
        Debug.Log("거래를 수락했습니다.");
        //클릭사운드/공용클릭사운드 없으면 찾아오겠습니다
        SoundManager.instance.PlaySFX("");
        GameManager.Instance.Trade();
        Achievements.TriggerFirstTrade();
        Destroy(devil);
        //GameManager.instance.Trade();
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// ui비활성화
    /// 악마오브젝트 삭제
    /// </summary>
    public void RejectTrade()
    {
        Debug.Log("거래를 거절했습니다.");
        //클릭사운드/공용클릭사운드 없으면 찾아오겠습니다
        SoundManager.instance.PlaySFX("");
        Destroy(devil);
        this.gameObject.SetActive(false);
    }
}
