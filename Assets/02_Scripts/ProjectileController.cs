using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Vector2 playerDirection;
    public GameObject one;        //test용도
    public GameObject two;        //test용도
    private Vector2 direction;    //test용도
    private int contactWall = 0; // 벽과 충돌 횟수
    private int contactEnemy = 0; // 적과 충돌 횟수

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        // playerDirection = GameManager.Instance.Player.Direction; 
        direction = one.transform.position - two.transform.position;
        RotationRojectile();
    }
    void Update()
    {
        DirectionProjcetile();
    }
    
    /// <summary>
    /// 총알진행 메서드
    /// </summary>
    public void DirectionProjcetile()
    {
        //rigidbody2D.velocity = playerDirection * 10f;
        rigidbody2D.velocity = direction.normalized * 10f;

    }

    /// <summary>
    /// 총알 회전 메서드
    /// direction playerDirection으로 바꿔야함 테스트를 위해 direction으로 해놓은 것 
    /// </summary>
    public void RotationRojectile()
    {
        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //회전 각도 계산

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(180f, 0, -rotationZ);
        }
        else { transform.rotation = Quaternion.Euler(0, 0, rotationZ); }

    }

    /// <summary>
    /// 부딫혔을때
    /// </summary>
    /// <param name="collision">벽 혹은 적</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (contactWall < 2 && collision.gameObject.CompareTag("Wall")) // 임시로 wall로 작성 // 숫자에는 총알 튕기는 횟수변수 넣어주면됨
        {
            var contact = collision.contacts[0]; // 충돌 지점
            direction = Vector3.Reflect(direction, contact.normal); // 현재 진행방향과 충돌지점을 계산해 반사각을 구해줌
            RotationRojectile();
            contactWall += 1;
        }
        else if (contactWall >= 2 && collision.gameObject.CompareTag("Wall")) // 임시로 wall로 작성
        {
            Destroy(this.gameObject);
        }

        if (contactEnemy < 2 && collision.gameObject.CompareTag("Enemy")) // 임시로 Enemy로 작성 
        {
            var contact = collision.contacts[0]; // 충돌 지점
            direction = Vector3.Reflect(direction, contact.normal); // 현재 진행방향과 충돌지점을 계산해 반사각을 구해줌
            RotationRojectile();
            contactEnemy += 1;
        }
        else if (contactEnemy >= 2 && collision.gameObject.CompareTag("Enemy")) // 임시로 Enemy로 작성
        {
            Destroy(this.gameObject);
        }
    }
}
