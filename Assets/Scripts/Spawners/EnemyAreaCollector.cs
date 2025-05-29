using UnityEngine;

public class EnemyAreaCollector : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PointCollector _pointCollector;

    public Transform SpawnPoint => _spawnPoint;
    public PointCollector PointCollector => _pointCollector;
}
