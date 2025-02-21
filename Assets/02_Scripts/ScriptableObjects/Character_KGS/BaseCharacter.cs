using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] protected float maxHp;
    [SerializeField] protected float speed;
    [SerializeField] protected float attackPower, attackSpeed;

    protected float CurHp { get => CurHp; set => Mathf.Clamp(value, 0, maxHp); }
    protected bool isMove;
    protected Transform target;

    protected Rigidbody2D rig;
    protected Vector2 lookDir, moveDir;

    protected virtual void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    protected virtual void Move()
    {

    }
}
