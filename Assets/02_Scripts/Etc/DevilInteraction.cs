using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DevilInteraction : MonoBehaviour
{
    public GameObject tradeUI; 


    /// <summary>
    /// 악마라운드에서 플레이어가 악마와 접촉했을때 trade진행
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tradeUI.SetActive(true);
            SoundManager.instance.PlaySFX("악마웃음소리 SFX안에 넣어놧습니다");
        }
    }
}
