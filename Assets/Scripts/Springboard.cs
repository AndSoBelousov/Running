using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Runner.player
{
    public class Springboard : MonoBehaviour
    {
        private Material _springboardMaterial;
        private float _gravity = -9.81f;
        [SerializeField] private float _bounceForce = 15f; // Сила подбрасывания

        private Vector3 _velocity;
        private void OnTriggerEnter(Collider other)
        {
            CharacterController charController = other.GetComponent<CharacterController>();
            if (charController != null)
            {
                _velocity.y = Mathf.Sqrt(_bounceForce * -2 * _gravity);
                charController.Move(_velocity * Time.deltaTime);
            }
        }

    }
}

