using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMaskEnemy; //적 설정
    [SerializeField] private LayerMask layerMaskWall; // 벽 설정
    [SerializeField] private LayerMask layerMaskTeam; // 아군 설정
    private Rigidbody2D rigidbody2D;
    private Collider2D arrowCollider;
    private Vector3 direction; // 플레이어의 방향
    private int contactWall; // 벽과 충돌 횟수
    private int contactEnemy; // 적과 충돌 횟수
    private int contactWallCount; // 받아온 벽 충돌 횟수
    private int contactEnemyCount; // 받아온 적 충돌 횟수
    private bool isDarkTouch;
    private bool isBlaze;
    private float arrowAttackPower;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        arrowCollider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// 총알 초기화 메서드
    /// </summary>
    /// <param name="_direction">방향</param>
    /// <param name="_contactwallCount">벽 충돌횟수, 적이 쏘면 0 </param>
    /// <param name="_contactEnemyCount">적 충돌횟수, 적이쏘면 0 </param>
    public void Init(Vector3 _direction, bool _isDarkTouch, bool _isBlaze, int _contactwallCount = 0, int _contactEnemyCount = 0)
    {
        direction = _direction;
        RotationRojectile();
        contactWallCount = _contactwallCount;
        contactEnemyCount = _contactEnemyCount;
        isDarkTouch = _isDarkTouch;
        isBlaze = _isBlaze;
        arrowAttackPower = GameManager.Instance.player.AttackPower;
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
        //if (collision.gameObject.GetComponent<ProjectileController>()) //다른 오브젝트가 ProjectileController를 가지고 있으면 총알이기 때문에
        //{ Physics2D.IgnoreLayerCollision(this.gameObject.layer, collision.gameObject.layer); } // 무시
        //else // 그외
        //{
        if (layerMaskWall.value == (layerMaskWall.value | (1 << collision.gameObject.layer))) // 벽과 충돌했을때
        {
            if (contactWall < contactWallCount) // 현재 충돌횟수가 받아온 충돌횟수보다 적다면
            {
                var contact = collision.contacts[0];
                // 충돌 지점
                direction = Vector3.Reflect(direction, contact.normal); // 현재 진행방향과 충돌지점을 계산해 반사각을 구해줌
                RotationRojectile();
                contactWall += 1;
                arrowAttackPower = GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetContactWallDecreaseDamage();
            }
            else if (contactWall >= contactWallCount) // 현재 충돌횟수가 받아온 충돌횟수와 같거나 크다면
                Destroy(this.gameObject);
        }
        else if (layerMaskEnemy.value == (layerMaskEnemy.value | (1 << collision.gameObject.layer))) // 적과 충돌했을때
        {
            if (contactEnemy < contactEnemyCount)
            {
                EnemyCharacter enemy = collision.gameObject.GetComponent<EnemyCharacter>();
                enemy.ChangeHealth(-arrowAttackPower); // 변수 바뀌면 적용
                if(enemy != null)
                {
                    if (isDarkTouch) // 어둠의 접촉 스킬 실행
                    {
                        StartCoroutine(DarkTouchDelay(enemy));
                    }
                    if (isBlaze) // 블레이즈 실행
                    {
                        StartCoroutine (BlazeDelay(enemy));
                    }
                }
                Physics2D.IgnoreCollision(arrowCollider, collision.collider);
                contactEnemy += 1;
                arrowAttackPower = GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetContactEnemyDecreaseDamage();

            }
            else if (contactEnemy >= contactEnemyCount)
            {
                EnemyCharacter enemy = collision.gameObject.GetComponent<EnemyCharacter>();
                enemy.ChangeHealth(-arrowAttackPower); // 변수 바뀌면 적용
                if (enemy != null)
                {
                    if (isDarkTouch) // 어둠의 접촉 스킬 실행
                    {
                        StartCoroutine(DarkTouchDelay(enemy));
                    }
                    if (isBlaze) // 블레이즈 실행
                    {
                        StartCoroutine(BlazeDelay(enemy));
                    }
                }
                Destroy(this.gameObject);
            }
        }
        else if (layerMaskTeam.value == (layerMaskTeam.value | (1 << collision.gameObject.layer))) // 같은 팀일 경우
        {
            Physics2D.IgnoreLayerCollision(this.gameObject.layer, collision.gameObject.layer);
        }
        // }

        //if (contactWall < 2 && collision.gameObject.CompareTag("Wall")) // 임시로 wall로 작성 // 숫자에는 총알 튕기는 횟수변수 넣어주면됨
        //{
        //    var contact = collision.contacts[0]; // 충돌 지점
        //    direction = Vector3.Reflect(direction, contact.normal); // 현재 진행방향과 충돌지점을 계산해 반사각을 구해줌
        //    RotationRojectile();
        //    contactWall += 1;
        //}
        //else if (contactWall >= 2 && collision.gameObject.CompareTag("Wall")) // 임시로 wall로 작성
        //{
        //    Destroy(this.gameObject);
        //}
        //else if (contactEnemy < 2 && collision.gameObject.CompareTag("Enemy")) // 임시로 Enemy로 작성 
        //{
        //    var contact = collision.contacts[0]; // 충돌 지점
        //    direction = Vector3.Reflect(direction, contact.normal); // 현재 진행방향과 충돌지점을 계산해 반사각을 구해줌
        //    RotationRojectile();
        //    contactEnemy += 1;
        //}
        //else if (contactEnemy >= 2 && collision.gameObject.CompareTag("Enemy")) // 임시로 Enemy로 작성
        //{
        //    Destroy(this.gameObject);
        //}
    }


    /// <summary>
    /// 어둠의 접촉 스킬 부현부
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    IEnumerator DarkTouchDelay(EnemyCharacter enemy)
    {
        yield return new WaitForSeconds(1);
        enemy.ChangeHealth(-GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetDarkTouchDecreaseDamage());
        List<EnemyCharacter> list = GameManager.Instance.MonsterManager.spawnedEnemys;
        float nearDir = 5000f;
        EnemyCharacter nearEnemy = null;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].transform.position != enemy.transform.position)
            {
                if (nearDir > Vector3.Distance(list[i].transform.position, enemy.transform.position))
                {
                    nearDir = Vector3.Distance(list[i].transform.position, enemy.transform.position);
                    nearEnemy = list[i];
                }
            }
        }
        if (nearEnemy == null)
        {
            Debug.Log("Projectile의 DarkTouchDelay의 nearEnemy가 null입니다!");
        }
        nearEnemy.ChangeHealth(-GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetDarkTouchDecreaseDamage());
    }

    /// <summary>
    /// 블레이즈 스킬 구현부
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    IEnumerator BlazeDelay(EnemyCharacter enemy)
    {
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.25f);
            enemy.ChangeHealth(-GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetBlazeDecresaseDamage());
        }
        yield return new WaitForSeconds(0.25f);
    }
    
}
