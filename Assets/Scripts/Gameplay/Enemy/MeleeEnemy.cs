using System;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;  // OnDrawGizmos

public class MeleeEnemy : EnemyBase
{
    public MeleeAttackState MeleeAttackState{get; set;}
    public MeleeChaseState MeleeChaseState{get; set;}
    
    [SerializeField] private float angleRange;
    [SerializeField] private float radius;
    
    bool _isCollision = false;
    
    private void Awake()
    {
        base.Awake();
        CacheComponents();
    }

    private void Update()
    {
        if(IsDead)
        {
            return;
        }

        if (Target != null)
        {
            if ((Target.transform.position - transform.position).magnitude < 2)
            {
                EnemyStateMachine.ChangeState(MeleeAttackState);
            }
            else
            {
                CheckPlayer();    
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
            _isCollision = false;
            return;
        }
        
        Vector3 interV = Target.gameObject.transform.position - transform.position;

        // target과 나 사이의 거리가 radius 보다 작다면
        if (interV.magnitude <= radius)
        {
            // '타겟-나 벡터'와 '내 정면 벡터'를 내적
            float dot = Vector3.Dot(interV.normalized, transform.forward);
            // 두 벡터 모두 단위 벡터이므로 내적 결과에 cos의 역을 취해서 theta를 구함
            float theta = Mathf.Acos(dot);
            // angleRange와 비교하기 위해 degree로 변환
            float degree = Mathf.Rad2Deg * theta;

            // 시야각 판별
            if (degree <= angleRange / 2f)
            {
                _isCollision = true;
                EnemyStateMachine.ChangeState(MeleeChaseState);
            }
            else
            {
                _isCollision = false;
                EnemyStateMachine.ChangeState(EnemyPatrolState);

            }
        }
        else
        {
            _isCollision = false;
            EnemyStateMachine.ChangeState(EnemyPatrolState);
        }
    }
    
    // ----------------------------------- 초기화
    
    private void CacheComponents()
    {
        MeleeChaseState =  new MeleeChaseState(this);
        MeleeAttackState = new MeleeAttackState(this);
    }
    
    // ------------------------------------ 에디터 확인용
    
    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);
    private void OnDrawGizmos()
    {
        Handles.color = _isCollision ? _red : _blue;
        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    }
}
