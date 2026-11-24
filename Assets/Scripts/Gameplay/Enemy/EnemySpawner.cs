using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private BoxCollider _spawnZone;
    [SerializeField] private Transform _referencePoint;
    [SerializeField] private int _spawnCount = 5;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        NavMeshPath path = new NavMeshPath();

        for (int i = 0; i < _spawnCount; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                Vector3 randomPosition = GetRandomPosition();

                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPosition, out hit, 2f, NavMesh.AllAreas))
                {
                    if (NavMesh.CalculatePath(_referencePoint.position, hit.position, NavMesh.AllAreas, path))
                    {
                        if (path.status == NavMeshPathStatus.PathComplete)
                        {
                            Instantiate(_enemyPrefab, hit.position, Quaternion.identity);
                            break;
                        }
                    }
                }
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        Bounds bounds = _spawnZone.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, bounds.center.y, z);
    }
}
