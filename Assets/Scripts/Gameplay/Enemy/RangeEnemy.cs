using UnityEngine;

public class RangeEnemy : EnemyBase
{
    [Header("Enemy 무기 관련")]
    [SerializeField] private Bullet _enemyBullet;
    [SerializeField] private Transform _firePoint;
    
    private float _time;
    
    public RangeChaseState RangeChaseState;
    public RangeAttackState RangeAttackState;
    
    public override void Start()
    {
        base.Start();
        RangeCacheComponents();
    }

    private void Update()
    {
        _time += Time.deltaTime;
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
                EnemyStateMachine.ChangeState(RangeAttackState);
            }
            else
            {
                EnemyStateMachine.ChangeState(RangeChaseState);    
            }
        }
        else if (Target != null)
        {
            if ((Target.transform.position - transform.position).magnitude < AttackRange && SeeTarget)
            {
                EnemyStateMachine.ChangeState(RangeAttackState);
            }
            else
            {
                CheckPlayer();
                IsAttack = false;
            }
        }
        
        EnemyStateMachine.Execute();
    }

    public void Shoot()
    {
        transform.LookAt(Target.transform);
        if (_time < FramingInterval)
        {
            return;
        }
        
        Vector3 direction = Target.transform.GetComponent<Collider>().bounds.center - _firePoint.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        Bullet _bullet = Instantiate(_enemyBullet, _firePoint.position, rotation);
        _bullet.Damage = Damage;
        _time = 0;
    }
    private void CheckPlayer()
    {
        if (EnemyStateMachine.CurrentState == RangeChaseState)
        {
            return;
        }
        /*if (Target == null)
        {
            SeeTarget = false;
            return;
        }*/
        
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
                EnemyStateMachine.ChangeState(RangeChaseState);
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
    
    private void RangeCacheComponents()
    {
        RangeChaseState = new RangeChaseState(this);
        RangeAttackState = new RangeAttackState(this);
    }
    
}
