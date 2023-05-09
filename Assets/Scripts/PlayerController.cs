using System;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        public float accelerationAmount = 1f;
        public float rotationSpeed = 1f;
        
        private Rigidbody2D _rigidbody2D;

        public float X = 1;
        public float Y = 1;
        public float Z = 0;

        private Vector2 _vector2;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            //get axis variables
            float x = Input.GetAxis("Horizontal")  * accelerationAmount;
            float y = Input.GetAxis("Vertical") * accelerationAmount;
            //transform.Translate(new Vector3(x, 0, z));
            var vector2 = new Vector2(x, y);
            transform.Translate(new Vector2(0,vector2.y)*accelerationAmount*Time.deltaTime);
            if (vector2.x>0.1)
            {
                transform.rotation =
                    Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z-rotationSpeed * Time.deltaTime);
            }
            if (vector2.x<-0.1)
            {
                transform.rotation =
                    Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z+rotationSpeed * Time.deltaTime);
            }

            //head turn fly
            /*w
            if (Input.GetKey(KeyCode.W))
            {
                _rigidbody2D.AddForce(transform.up * accelerationAmount * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                _rigidbody2D.AddForce((-transform.up) * accelerationAmount * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.C))
            {
                _rigidbody2D.angularVelocity = Mathf.Lerp(GetComponent<Rigidbody2D>().angularVelocity, 0,
                    rotationSpeed * 0.06f * Time.deltaTime);
                _rigidbody2D.velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, Vector2.zero,
                    accelerationAmount * 0.06f * Time.deltaTime);
            }
            
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
            {
                _rigidbody2D.AddForce((-transform.right) * accelerationAmount * 0.6f * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
            {
                _rigidbody2D.AddForce((transform.right) * accelerationAmount * 0.6f * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift))
            {
                _rigidbody2D.AddTorque(-rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift))
            {
                _rigidbody2D.AddTorque(rotationSpeed * Time.deltaTime);
            }
            */
        }
    }
}