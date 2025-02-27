using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundController : MonoBehaviour
{
    private float circleRadius = 1f; // 반지름
    private float deg = 0; // 각도
    private float speed = 40f;  // 이동 속도
    private float timeSinceLastAttack = 0; //공격 시간
    private float AttackDelay = 5f; // 공격 딜레이
    private bool IsAttacking = true; // 공격 여부

    /// <summary>
    /// 각도 받아옴
    /// </summary>
    /// <param name="chagedeg"></param>
    public void Init(float chagedeg = 0)
    {
        deg = chagedeg;
    }
    

    /// <summary>
    /// 5초마다 공격하고 플레이어 주변을 돌아다님
    /// </summary>
    void Update()
    {
        SurroundPosition();
        if (timeSinceLastAttack <= AttackDelay) // 딜레이 
            timeSinceLastAttack += Time.deltaTime;
        else if (IsAttacking)
        {
            timeSinceLastAttack = 0;
            if (GameManager.Instance.player.target == null)
            {
                Debug.Log("요정이 공격할 대상이 없습니다.");
            }
            else { GameManager.Instance.ProjectileManager.ShootFairy(this.transform.position, (GameManager.Instance.player.target.position - this.transform.position)); }
        }

    }

    /// <summary>
    /// 생성시 플레이어 주변을 회전시키는 메서드
    /// </summary>
    private void SurroundPosition()
    {
        deg += Time.deltaTime * speed;
        if (deg < 360)
        {
            float rad = Mathf.Deg2Rad * (deg); //1도(degree)를 라디안으로 변환하는 상수(π / 180) 이므로, deg 값에 곱하면 라디안 값을 얻을 수 있음, rad는해당 각도에서의 위치를 계산하기 위한 라디안 값
            float x = circleRadius * Mathf.Cos(rad); // x와 y위치를 바꾸면 시계방향으로 회전
            float y = circleRadius * Mathf.Sin(rad);
            if (GameManager.Instance.player == null)
            {
                Destroy(this.gameObject);
            }
            else
                this.transform.position = GameManager.Instance.player.transform.position + new Vector3(x, y, 0);
        }
        else { deg = 0; }
    }
}
