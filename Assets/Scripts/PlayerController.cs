using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        public float accelerationAmount = 1;
        public float rotationSpeed = 1f;
        public float currentSpeed;
        private Vector2 _lastPosition;
        private Vector2 _vector2;
        private float _timer;

        private void Start()
        {
            _lastPosition = transform.position;
        }

        private void FixedUpdate()
        {
            Move();

            _timer += Time.deltaTime;
            if (_timer > 0.2)
            {
                ComputationSpeed(_timer);
                _timer = 0;
            }
        }

        private void Move()
        {
            //get axis variables
            float x = Input.GetAxis("Horizontal") * accelerationAmount;
            float y = Input.GetAxis("Vertical") * accelerationAmount;
            //transform.Translate(new Vector3(x, 0, z));
            var vector2 = new Vector2(x, y);
            if (vector2.y > 0)
            {
                transform.Translate(new Vector2(0, vector2.y) * accelerationAmount * Time.deltaTime);
            }

            if (vector2.x > 0.1)
            {
                transform.rotation =
                    Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - rotationSpeed * Time.deltaTime);
            }

            if (vector2.x < -0.1)
            {
                transform.rotation =
                    Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotationSpeed * Time.deltaTime);
            }
        }

        private void ComputationSpeed(float duration)
        {
            var position = transform.position;
            currentSpeed = Vector2.Distance(_lastPosition, position) / duration;
            _lastPosition = position;
        }
    }
}