using System;
using UnityEngine;

namespace Player
{
    public class Bullet : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 3);
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }
    }
}