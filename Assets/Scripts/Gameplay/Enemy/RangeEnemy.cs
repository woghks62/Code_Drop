using UnityEngine;
using UnityEditor;  // OnDrawGizmos

public class RangeEnemy : EnemyBase
{
    [SerializeField] private Bullet _enemyBullet;
    [SerializeField] private Transform _firePoint;

    public RangeChaseState RangeChaseState;
    public RangeAttackState RangeAttackState;
    
    //[SerializeField] private float angleRange;
    //[SerializeField] private float radius;
    
    private float _time;
    //[SerializeField] private float _framingInterval;

    private void OnEnable()
    {
        base.OnEnable();
        CacheComponents();
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if(IsDead)
        {
            return;
        }

        if (Target != null)
        {
            if ((Target.transform.position - transform.position).magnitude < 10 && SeeTarget)
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
        if (_time < _enemy.EnemyStats.FramingInterval)
        {
            return;
        }
        
        Vector3 spredDirection = GetSpredDirection((_firePoint.position - transform.position).normalized, 30);
        Vector3 spread = spredDirection + _firePoint.position;
        Bullet _bullet = Instantiate(_enemyBullet, spread, _firePoint.rotation);
        _time = 0;
    }
    
    private Vector3 GetSpredDirection(Vector3 forwardDirection, float spreadAngle)
    {
        float spreadX = Random.Range(-spreadAngle, spreadAngle);
        float spreadY = Random.Range(-spreadAngle, spreadAngle);

        Vector3 spreadDirection = Quaternion.Euler(spreadX, spreadY, 0) * forwardDirection;
        return spreadDirection;
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
        if (interV.magnitude <= _enemy.EnemyStats.Radius)
        {
            // '타겟-나 벡터'와 '내 정면 벡터'를 내적
            float dot = Vector3.Dot(interV.normalized, transform.forward);
            // 두 벡터 모두 단위 벡터이므로 내적 결과에 cos의 역을 취해서 theta를 구함
            float theta = Mathf.Acos(dot);
            // angleRange와 비교하기 위해 degree로 변환
            float degree = Mathf.Rad2Deg * theta;

            // 시야각 판별
            if (degree <= _enemy.EnemyStats.AngleRange / 2f)
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
    
    private void CacheComponents()
    {
        RangeChaseState = new RangeChaseState(this);
        RangeAttackState = new RangeAttackState(this);
    }
    
    // ------------------------------------ 에디터 확인용
    [SerializeField] bool _isCollision = false;
    
     Color _blue = new Color(0f, 0f, 1f, 0.2f);
     Color _red = new Color(1f, 0f, 0f, 0.2f);
     private void OnDrawGizmos()
     {
         Handles.color = SeeTarget ? _red : _blue;
         // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
         Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, _enemy.EnemyStats.AngleRange / 2, _enemy.EnemyStats.Radius);
         Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -_enemy.EnemyStats.AngleRange / 2, _enemy.EnemyStats.Radius);
     }
}
