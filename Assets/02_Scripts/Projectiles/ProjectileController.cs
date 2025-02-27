using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMaskEnemy; //적 설정
    [SerializeField] private LayerMask layerMaskWall; // 벽 설정
    [SerializeField] private LayerMask layerMaskTeam; // 아군 설정 및 총알 두개 레이어 넣어주면됨
    private Rigidbody2D rigidbody2D;
    private Collider2D arrowCollider;
    private Vector3 direction; // 플레이어의 방향
    private int contactWall =0; // 벽과 충돌 횟수
    private int contactEnemy= 0; // 적과 충돌 횟수
    private int contactWallCount; // 받아온 벽 충돌 횟수
    private int contactEnemyCount; // 받아온 적 충돌 횟수
    private bool isDarkTouch; // 어둠의 접촉 on off
    private bool isBlaze; // 블레이즈 on off
    private float arrowAttackPower; // 화살 공격력
    private float arrowDestoryTime = 0f; // 화살 파괴 시간
    private float arrowStayTime = 6f; // 화살 유지 시간


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
    public void Init(Vector3 _direction, bool _isDarkTouch, bool _isBlaze, float _attackPower ,int _contactwallCount = 0, int _contactEnemyCount = 0)
    {
        direction = _direction;
        RotationRojectile();
        contactWallCount = _contactwallCount;
        contactEnemyCount = _contactEnemyCount;
        isDarkTouch = _isDarkTouch;
        isBlaze = _isBlaze;
        arrowAttackPower = _attackPower * GameManager.Instance.ProjectileManager.GetFinalDecreaseDamage();
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
            if (contactWall < contactWallCount) // 현재 충돌횟수가 받아온 충돌횟수보다 적다면
            {
                if (collision.contacts.Length > 0)
                {
                    var contact = collision.contacts[0];// 충돌 지점
                    direction = Vector3.Reflect(direction, contact.normal); // 현재 진행방향과 충돌지점을 계산해 반사각을 구해줌
                    RotationRojectile();
                    contactWall += 1;
                    arrowAttackPower = GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetContactWallDecreaseDamage();
                }
            }
            if (contactWall >= contactWallCount) // 현재 충돌횟수가 받아온 충돌횟수와 같거나 크다면
                Destroy(this.gameObject);
        }
        if (layerMaskEnemy.value == (layerMaskEnemy.value | (1 << collision.gameObject.layer))) // 적과 충돌했을때
        {
            if (contactEnemy < contactEnemyCount)
            {
                EnemyCharacter enemy = collision.gameObject.GetComponent<EnemyCharacter>();
                CirticalPer(enemy); 
                if (enemy != null)
                {
                    if (isDarkTouch) // 어둠의 접촉 스킬 실행
                    {
                        GameManager.Instance.StartCoroutine(DarkTouchDelay(enemy));
                    }
                    if (isBlaze) // 블레이즈 실행
                    {
                        GameManager.Instance.StartCoroutine (BlazeDelay(enemy));
                    }
                }
                Physics2D.IgnoreCollision(arrowCollider, collision.collider);
                contactEnemy += 1;
                arrowAttackPower = GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetContactEnemyDecreaseDamage();

            }
            else if (contactEnemy >= contactEnemyCount)
            {
                EnemyCharacter enemy = collision.gameObject.GetComponent<EnemyCharacter>();
                CirticalPer(enemy); 
                if (enemy != null)
                {
                    if (isDarkTouch) // 어둠의 접촉 스킬 실행
                    {
                        GameManager.Instance.StartCoroutine(DarkTouchDelay(enemy));
                    }
                    if (isBlaze) // 블레이즈 실행
                    {
                        GameManager.Instance.StartCoroutine(BlazeDelay(enemy));
                    }
                }
                Destroy(this.gameObject);
            }
        }
        if (layerMaskTeam.value == (layerMaskTeam.value | (1 << collision.gameObject.layer))) // 같은 팀일 경우
        {
            Physics2D.IgnoreLayerCollision(this.gameObject.layer, collision.gameObject.layer);
        }
    }

    /// <summary>
    /// 크리티컬 계산
    /// </summary>
    /// <param name="enemy"></param>
    private void CirticalPer(EnemyCharacter enemy)
    {
        if (GameManager.Instance.player.CriChance < Random.Range(0.01f, 1))
            enemy.ChangeHealth(-(arrowAttackPower + (GameManager.Instance.player.CriDmg)*arrowAttackPower));
        else
            enemy.ChangeHealth(-arrowAttackPower);
    }


    /// <summary>
    /// 어둠의 접촉 스킬 부현부
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    IEnumerator DarkTouchDelay(EnemyCharacter enemy)
    {
        yield return new WaitForSeconds(1);
        if (enemy != null)
        {
            enemy.ChangeHealth(-GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetDarkTouchDecreaseDamage());

            List<EnemyCharacter> list = GameManager.Instance.MonsterManager.spawnedEnemys; // 적의 리스트 가져옴
            float nearDir = 5000f;
            EnemyCharacter nearEnemy = null;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].transform.position != enemy.transform.position)
                {
                    if (nearDir > Vector3.Distance(list[i].transform.position, enemy.transform.position)) // 저장한 근처 거리보다 적과의 거리가 더 가까우면 실행
                    {
                        nearDir = Vector3.Distance(list[i].transform.position, enemy.transform.position); //가까운 거리에 더가까운 거리를 넣고 해당 몬스터를 가져옴
                        nearEnemy = list[i];
                    }
                }
            }

            if (nearEnemy == null)
            {
            }
            else { nearEnemy.ChangeHealth(-GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetDarkTouchDecreaseDamage()); }
        }
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
            if (enemy == null) { break; }
            enemy.ChangeHealth(-GameManager.Instance.player.AttackPower * GameManager.Instance.ProjectileManager.GetBlazeDecresaseDamage());
        }
        yield return new WaitForSeconds(0.25f);
    }
    
}
