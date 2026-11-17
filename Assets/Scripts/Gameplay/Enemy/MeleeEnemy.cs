using System;
using NUnit.Framework;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    [Header("Enemy 무기 관련")]
    [SerializeField] private MeleeKnife _meleeKnife;
    
    public MeleeAttackState MeleeAttackState{get; set;}
    public MeleeChaseState MeleeChaseState{get; set;}
    public MeleeKnife MeleeKnife => _meleeKnife;
    
    public void Attack()
    {
        // 애니메이션 Event (애니메이션 시작할 때 실행)
        Debug.Log("공격 애니메이션 재생");
        IsAttack = true;
        MeleeKnife.Attack();
    }

    public void AttackFinish()
    {
        // 애니메이션 Event (애니메이션 끝날때 실행)
        Debug.Log("공격 애니메이션 종료");
        IsAttack = false;
        MeleeKnife.Finish();
    }

    public override void Start()
    {
        base.Start();
        MeleeCacheComponents();
    }

    private void Update()
    {
        if(IsDead)
        {
            return;
        }

        if (GameManager.Instance.GameData.IsCallEscape)
        {
            if (Target == null)
            {
                Target = FindAnyObjectByType<PlayerHealth>().gameObject;
            }
            
            if ((Target.transform.position - transform.position).magnitude < AttackRange)
            {
                EnemyStateMachine.ChangeState(MeleeAttackState);
            }
            else
            {
                EnemyStateMachine.ChangeState(MeleeChaseState);    
            }
        }
        else if (Target != null)
        {
            if ((Target.transform.position - transform.position).magnitude < AttackRange)
            {
                EnemyStateMachine.ChangeState(MeleeAttackState);
            }
            else
            {
                CheckPlayer();
                IsAttack = false;
            }
        }
        
        
        EnemyStateMachine.Execute();
    }
    
    
    private void CheckPlayer()
    {
        if (EnemyStateMachine.CurrentState == MeleeChaseState)
        {
            return;
        }
        if (Target == null)
        {
            SeeTarget = false;
            return;
        }
        
        Vector3 interV = Target.gameObject.transform.position - transform.position;

        // target과 나 사이의 거리가 radius 보다 작다면
        if (interV.magnitude <= Radius)
        {
            // '타겟-나 벡터'와 '내 정면 벡터'를 내적
            float dot = Vector3.Dot(interV.normalized, transform.forward);
            // 두 벡터 모두 단위 벡터이므로 내적 결과에 cos의 역을 취해서 theta를 구함
            float theta = Mathf.Acos(dot);
            // angleRange와 비교하기 위해 degree로 변환
            float degree = Mathf.Rad2Deg * theta;

            // 시야각 판별
            if (degree <= AngleRange / 2f)
            {
                SeeTarget = true;
                EnemyStateMachine.ChangeState(MeleeChaseState);
            }
            else
            {
                SeeTarget = false;
                EnemyStateMachine.ChangeState(EnemyPatrolState);

            }
        }
        else
        {
            SeeTarget = false;
            EnemyStateMachine.ChangeState(EnemyPatrolState);
        }
    }
    
    // ----------------------------------- 초기화
    
    private void MeleeCacheComponents()
    {
        MeleeChaseState =  new MeleeChaseState(this);
        MeleeAttackState = new MeleeAttackState(this);
    }

}
