using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOrbController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMaskEnemy;
    private float circleRadius = 1f; // 반지름
    private float deg = 0; // 각도
    private float speed = 40f;  // 이동 속도

    public void Init(float chagedeg = 0)
    {
        deg = chagedeg;
    }

    void Update()
    {
        SurroundPosition();
    }

    /// <summary>
    /// 생성시 플레이어 주변 회전하는 메서드
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


    /// <summary>
    /// 적과 접촉했을때 구현
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (layerMaskEnemy.value == (layerMaskEnemy.value | (1 << collision.gameObject.layer)))
        {
            EnemyCharacter enemy = collision.gameObject.GetComponent<EnemyCharacter>();
            enemy.ChangeHealth(-(GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetFireOrbDecreaseDamage())); 
        }
    }

}
