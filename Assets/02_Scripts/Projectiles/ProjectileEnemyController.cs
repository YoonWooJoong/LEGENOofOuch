using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMaskEnemy; //적 설정
    [SerializeField] private LayerMask layerMaskWall; // 벽 설정
    [SerializeField] private LayerMask layerMaskTeam; // 아군 설정 및 총알설정 layer에 아군과 projectile 두개 넣어주면됨
    private Rigidbody2D rigidbody2D;
    private Vector3 direction; // 플레이어의 방향
    private int contactWall; // 벽과 충돌 횟수
    private int contactEnemy; // 적과 충돌 횟수
    private int contactWallCount; // 받아온 벽 충돌 횟수
    private int contactEnemyCount; // 받아온 적 충돌 횟수
    private float damage; // 데미지
    private float arrowDestoryTime = 0f; // 총알 파괴 시간
    private float arrowStayTime = 6f; // 총알 지속시간

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 총알 초기화 메서드
    /// </summary>
    /// <param name="_direction">방향</param>
    /// <param name="_contactwallCount">벽 충돌횟수, 적이 쏘면 0 </param>
    /// <param name="_contactEnemyCount">적 충돌횟수, 적이쏘면 0 </param>
    public void Init(Vector3 _direction, float _damage, int _contactwallCount = 0, int _contactEnemyCount = 0)
    {
        direction = _direction;
        RotationRojectile();
        contactWallCount = _contactwallCount;
        contactEnemyCount = _contactEnemyCount;
        damage = _damage;
    }

    void Update()
    {
        DirectionProjcetile();
        if (arrowDestoryTime >= arrowStayTime)
        {
            Destroy(this.gameObject);
        }
        else { arrowDestoryTime += Time.deltaTime; }
    }

    /// <summary>
    /// 총알진행 메서드
    /// </summary>
    public void DirectionProjcetile()
    {
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
        if ((layerMaskWall.value & (1 << collision.gameObject.layer)) != 0) // 벽과 충돌했을때
        {
            if (collision.contacts.Length > 0) // 적이 스킬쓸때를 대비해 남겨둔 코드
            {
                if (contactWall < contactWallCount) // 현재 충돌횟수가 받아온 충돌횟수보다 적다면
                {
                    var contact = collision.contacts[0]; // 충돌 지점

                    direction = Vector3.Reflect(direction, contact.normal); // 현재 진행방향과 충돌지점을 계산해 반사각을 구해줌
                    RotationRojectile();
                    contactWall += 1;
                }
            }
            if (contactWall >= contactWallCount) // 현재 충돌횟수가 받아온 충돌횟수와 같거나 크다면
                Destroy(this.gameObject);
        }
        else if (layerMaskEnemy.value == (layerMaskEnemy.value | (1 << collision.gameObject.layer))) // 플레이어와 충돌했을때
        {
            PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();
            player.ChangeHealth(-damage);
            Destroy(this.gameObject);

        }
        else if (layerMaskTeam.value == (layerMaskTeam.value | (1 << collision.gameObject.layer))) // 아군총알, 적총알, 적레이어 적용해서 무시 
        {
            Physics2D.IgnoreLayerCollision(this.gameObject.layer, collision.gameObject.layer);
        }
    }
}
