using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerController : MonoBehaviour
{
    private Collider _collider;
    [SerializeField] private bool _isDamage;
    [SerializeField] private int _damage = 4;

    private FragmentMapСustomizer _customizer;

    //private GameManager _gameManager;
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        _customizer = FindFirstObjectByType<FragmentMapСustomizer>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && _isDamage)
        {
            _customizer.TakeDamage(_damage);
   
            Debug.Log("TakeDamage");
        }
        else if (other.gameObject.tag == "Player")
        {
            _customizer.UpdateLevel();
            
        }
    }
}
