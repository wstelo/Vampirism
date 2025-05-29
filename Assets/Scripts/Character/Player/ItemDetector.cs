using UnityEngine;

[RequireComponent(typeof(CoinWallet))]
public class ItemDetector : MonoBehaviour
{
    [SerializeField] private CoinWallet _coinWallet;
    [SerializeField] private Health _health;

    private VisitorCollector _visitorCollector;

    private void Awake()
    {
        _visitorCollector = new VisitorCollector(_coinWallet,_health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IItemable item))
        {

            item.Accept(_visitorCollector);
        }
    }
}
