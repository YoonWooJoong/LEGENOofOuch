using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DevilInteraction : MonoBehaviour
{
    public GameObject tradeUI;  // 거래 UI를 참조하는 변수


    /// <summary>
    /// 악마라운드에서 플레이어가 악마와 접촉했을때 trade진행
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 게임 오브젝트가 'Player' 태그를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 거래 UI 활성화
            tradeUI.SetActive(true);
            SoundManager.instance.PlaySFX("악마웃음소리 SFX안에 넣어놧습니다");
        }
    }
}
