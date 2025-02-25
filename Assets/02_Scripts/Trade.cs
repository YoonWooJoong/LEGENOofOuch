using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour
{
    public Button acceptButton;
    public Button rejectButton;

    public void Awake()
    {

        acceptButton.onClick.AddListener(AcceptTrade);
        rejectButton.onClick.AddListener(RejectTrade);
        this.gameObject.SetActive(false);
    }

    public void AcceptTrade()
    {
        Debug.Log("거래를 수락했습니다.");
    }
    public void RejectTrade()
    {
        Debug.Log("거래를 거절했습니다.");
    }
}
