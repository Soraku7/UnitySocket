using UnityEngine;

namespace Player
{
    public class GunController : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        
        private GameObject _bulletPref;
        private Transform _firePosition;
        
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _bulletPref = Resources.Load<GameObject>("Prefabs/Bullet") as GameObject;
            _firePosition = transform.Find("Fire");
        }  
        
        private void Update()
        {
            LookAt();
            Fire();
        }
        
        
        private void LookAt()
        {
            Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            _spriteRenderer.flipY = transform.eulerAngles.z is >= 90 and <= 270;
        }

        private void Fire()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                
                float fireAngle = Vector3.Angle(mousePos - transform.position , Vector3.up);

                if (mousePos.x > transform.position.x)
                {
                    fireAngle = -fireAngle;
                }
                
                GameObject bullet = Instantiate(_bulletPref, _firePosition.position, Quaternion.identity);
                bullet.transform.eulerAngles = new Vector3(0, 0, fireAngle + 90);
                Vector2 velocity = (mousePos - transform.position).normalized * 20;
                bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(velocity.x, velocity.y);
            }
            
            
        }
    }
}