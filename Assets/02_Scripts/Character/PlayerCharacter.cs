using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] float criticalDamage = 0.2f, criticalChance;
    [SerializeField] int level = 1;
    [SerializeField] int exp;

    [Header("")]
    [SerializeField] PlayerClassEnum pClass;
    [SerializeField] Slider expbar;
    [SerializeField] TextMeshProUGUI levelTxt;
    [SerializeField] ParticleSystem healParticle;

    //버프수치: 20%증가시 0.2f 입력
    public float MaxHpBuf { get; set; }
    public float SpeedBuf { get; set; }
    public float AtkBuf { get; set; }
    public float AsBuf { get; set; }
    public float CriDmgBuf { get; set; }
    public float CriChanceBuf { get; set; }

    public override float MaxHp => base.MaxHp * (1 + MaxHpBuf);
    public override float Speed => base.Speed * (1 + SpeedBuf);
    public override float AttackPower => base.AttackPower * (1 + AtkBuf);
    public override float AttackSpeed => base.AttackSpeed * (1 + AsBuf);
    public float CriDmg => criticalDamage + CriDmgBuf;
    public float CriChance => criticalChance + CriChanceBuf;

    public bool GodMod = false;
    public int life = 1;

    // 멀티샷
    public bool isMultiShot = false;

    //플레이어의 일시정지상태를 확인하는 변수입니다.
    bool playerPaused = false;
    public bool PlayerPaused
    {
        get => playerPaused;
        set
        {
            moveDir = Vector2.zero;
            playerPaused = value;
            var collider = GetComponent<BoxCollider2D>();
            var rigi = GetComponent<Rigidbody2D>();
            collider.enabled = !playerPaused;
            rigi.simulated = !playerPaused;
        }
    }

    /// <summary>
    /// 키보드 입력으로 이동방향을 결정합니다.
    /// </summary>
    protected override void HandleAction()
    {
        base.HandleAction();

        // 키 바인딩에서 사용자 설정 키 가져오기
        bool moveUp = Input.GetKey(OptionManager.instance.GetKey("Up"));
        bool moveDown = Input.GetKey(OptionManager.instance.GetKey("Down"));
        bool moveLeft = Input.GetKey(OptionManager.instance.GetKey("Left"));
        bool moveRight = Input.GetKey(OptionManager.instance.GetKey("Right"));

        moveDir.Set((moveRight ? 1 : 0) - (moveLeft ? 1 : 0),
                    (moveUp ? 1 : 0) - (moveDown ? 1 : 0));

        moveDir.Normalize();
        SearchTarget();
    }

    /// <summary>
    /// 현제 활성화된 살아있는 적들 중 가장 가까운 적을 목표로 삼습니다.
    /// </summary>
    void SearchTarget()
    {
        target = null;
        var enemys = FindObjectsOfType<EnemyCharacter>();
        foreach (var enemy in enemys)
        {
            var enemyCharacter = enemy.GetComponent<EnemyCharacter>();
            float distance = (enemyCharacter.gameObject.transform.position - transform.position).magnitude;

            if (distance < TargetDis && enemyCharacter.GetCurHp() != 0)
                target = enemy.GameObject().transform;
        }
    }

    /// <summary>
    /// 무적상태일 경우 체력회복만 가능합니다.
    /// </summary>
    /// <param name="change">변경할 수치입니다. 데미지면 음수, 회복이면 양수값을 입력합니다.</param>
    public override void ChangeHealth(float change)
    {
        if (!GodMod || change > 0)
        {
            base.ChangeHealth(change);

            // 체력 회복일 때만 파티클 실행
            if (change > 0 && healParticle != null)
            {
                healParticle.Play();
            }
        }
    }

    /// <summary>
    /// 죽음에 달하면 목숨이 하나 줄고, 목숨이 다하면 사망합니다.
    /// </summary>
    protected override void Death()
    {
        if (--life > 0)
        {
            ChangeHealth(MaxHp);
            return;
        }
        //사망시 게임종료 로직 실행
        Achievements.TriggerFirstDeath();
        base.Death();
        GameManager.Instance.EndGame();
    }

    protected override void Attack()
    {
        base.Attack();

        GameManager.Instance.AbilityManager.UseAbility();
        if (GameManager.Instance.AbilityManager.GetMultiShotOn())
        {
            StartCoroutine(AttackWithDelay(0.1f));
        }
    }

    IEnumerator AttackWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.AbilityManager.UseAbility();
    }

    /// <summary>
    /// 이동시에는 스킬을 사용하지 않습니다.
    /// </summary>
    protected override void Move()
    {
        base.Move();

        if (IsMove)
            StopAllCoroutines();
    }

    public void GetExp(int exp)
    {
        this.exp += exp;

        int upLv = this.exp / 100;

        level += upLv;
        if (upLv > 0)
            Achievements.TriggerFirstLevelUp();
        for (int i = 0; i < upLv; i++)
            ChangeHealth(MaxHp / 10);
        this.exp %= 100;

        expbar.value = this.exp / 100f;
        levelTxt.text = level.ToString();
    }

    public PlayerClassEnum GetPlayerClass()
    {
        return pClass;
    }

    /// <summary>
    /// 가지고 있는 모든 증감스텟을 0으로 하고 체력을 최대로 합니다.
    /// </summary>
    public void ClearPlayerBuf()
    {
        MaxHpBuf = SpeedBuf = AtkBuf = AsBuf = CriDmgBuf = CriChanceBuf = 0;
        GodMod = isMultiShot = false;
        life = 1;
        PlayerPaused = false;
        CurHp = MaxHp;
        exp = 0;
        level = 1;
    }

    /// <summary>
    /// 선택된 직업을 반영해줍니다.
    /// </summary>
    /// <param name="pClass">현제 플레이어의 직업입니다.</param>
    public void SetClass(PlayerClassEnum pClass)
    {
        this.pClass = pClass;
        var pForm = GetComponent<PlayerFormChange>();
        pForm.FormChange(pClass);
    }
}
