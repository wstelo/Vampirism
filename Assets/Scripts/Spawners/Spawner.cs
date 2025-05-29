using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : Item ,IItemable

{
    [SerializeField] private T _prefab;
    [SerializeField] private PointCollector _objectWithPoints;

    private void Start()
    {
        CreateObjects(_prefab);
    }

    private void CreateObjects(T prefab)
    {
        for (int i = 0; i < _objectWithPoints.TargetPoints.Count; i++)
        {
            var currentObject = Instantiate(prefab, _objectWithPoints.TargetPoints[i].transform.position, Quaternion.identity);
            currentObject.Collected += DestroyObject;
        }
    }

    private void DestroyObject(Item item)
    {
        item.Collected -= DestroyObject;
        Destroy(item.gameObject);
    }
}
