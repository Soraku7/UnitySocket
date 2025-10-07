using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        private bool _island;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();
            Jump();
        }

        private void Move()
        {
            float h = Input.GetAxis("Horizontal");
            if (h != 0)
            {
                _rigidbody.linearVelocity = new Vector2(h * 10, _rigidbody.linearVelocity.y);
            }
        }

        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.W) && _island)
            {
                Debug.Log("Jump");
                _rigidbody.AddForce(Vector2.up * 400);
            }
        }
        
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _island = true;
            }
        }
        
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _island = false;
            }
        }
    }
}