using System;
using System.Collections;
using System.Collections.Generic;
using UDEV;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Actor : MonoBehaviour
{
    public ActorStats statData;
    [LayerList]
    [SerializeField] private int m_invincibleLayer;
    [LayerList]
    [SerializeField] private int m_normalLayer;

    public Weapon weapon;
    protected bool m_isKnockback;
    protected bool m_isInvincible;
    private bool m_isDead;
    private float m_curHp;

    protected Rigidbody2D m_rb;
    protected Animator m_anim;

    private string currentAnim;

    protected Coroutine m_stopKnockbackCo;
    protected Coroutine m_invincibleCo;

    [Header("Event:")]

    public UnityEvent OnInit;
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDead;

    
    public bool IsDead { get => m_isDead; set => m_isDead = value; }
    public float CurHp
    {
        get => m_curHp;
        set => m_curHp = value;


    }

    protected virtual void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        Init();
        OnInit?.Invoke();
    }
    public virtual void Init()
    {

    }

    public virtual void TakeDamage(float damage)
    {
        if (damage < 0 || m_isInvincible) return;//neu dam nho hon k hoac dang o trang thai bat bai thì k nhan sat thuong,ngat code
        m_curHp -= damage;
        Knockback();
        if (m_curHp <= 0)
        {
            m_curHp = 0;
            Die();
        }
        OnTakeDamage?.Invoke();
    }

    protected virtual void Die()
    {
        m_isDead = true;
        m_rb.velocity = Vector3.zero;
        OnDead?.Invoke();
        Destroy(gameObject, 0.5f);
    }

    protected void Knockback()
    {
       if(m_isInvincible|| m_isKnockback||m_isDead) return;//m_isKnockback= đánh trả 
        m_isKnockback = true;
        m_stopKnockbackCo= StartCoroutine(StopKnockback());
    }

    protected void InVincible (float invincibleTime){
        m_isKnockback = false;
        m_isInvincible = true;//invinsible=bat kha chien bai

        gameObject.layer = m_invincibleLayer;

        m_invincibleCo= StartCoroutine(StopInvincible(invincibleTime));
    }
    private IEnumerator StopKnockback()
    {
        yield return new WaitForSeconds(statData.knockbackTime);
        InVincible(statData.invincibleTime);
       
    }
     private IEnumerator StopInvincible(float invincibleTime)
     {
        yield return new WaitForSeconds(invincibleTime);
        m_isInvincible = false;
        gameObject.layer = m_normalLayer;
     }

    protected virtual void Move()
    {

    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            m_anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            m_anim.SetTrigger(currentAnim);
        }
    }

}
