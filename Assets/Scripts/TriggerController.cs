using Runner.player;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerController : MonoBehaviour
{
    private Collider _collider;
    [SerializeField] private bool _isDamage;
    [SerializeField] private int _damage = 4;

    private FragmentMapСustomizer _customizer;
    private PlayerMover _playerMover;


    private void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        _customizer = FindFirstObjectByType<FragmentMapСustomizer>();
        _playerMover = FindFirstObjectByType<PlayerMover>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && _isDamage)
        {
            _customizer.TakeDamage(_damage);
            _playerMover.SpeedBoost(-1f);
            Debug.Log("TakeDamage");
        }
        else if (other.gameObject.tag == "Player")
        {
            _customizer.UpdateLevel();
            _playerMover.SpeedBoost(0.1f);
        }
    }
}
