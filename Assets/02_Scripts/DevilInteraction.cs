using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilInteraction : MonoBehaviour
{
    public GameObject tradeUI;  // 거래 UI를 참조하는 변수

    // 악마와 충돌이 발생했을 때 (2D 충돌 처리)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 게임 오브젝트가 'Player' 태그를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("부딫!");
            // 거래 UI 활성화
            tradeUI.SetActive(true);
        }
    }
}
