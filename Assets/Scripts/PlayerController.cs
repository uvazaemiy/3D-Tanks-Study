using UnityEngine;

public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _walkSpeed = 2f;
        [SerializeField] private float _runSpeed = 5f;
        [SerializeField] private float _smoothForce = 8f; 

        private const string XAXIS = "Horizontal";
        private const string YAXIS = "Vertical";

        private Vector3 _currentDirection;
        private Vector3 _targetDirection;
        private bool _isRunning;

        private void Update()
        {
            // 1. ВВІД (Завжди в Update)
            // Використовуємо GetAxisRaw, щоб уникнути подвійного згладжування
            float moveX = Input.GetAxisRaw(XAXIS);
            float moveZ = Input.GetAxisRaw(YAXIS);

            // Використовуємо GetKey, щоб біг працював поки кнопка затиснута
            _isRunning = Input.GetKey(KeyCode.LeftShift);

            // Нормалізуємо вектор, щоб по діагоналі гравець не рухався швидше
            _targetDirection = new Vector3(moveX, 0f, moveZ).normalized;
            
            SmoothDirection();
            DoAnim();
        }

        private void FixedUpdate()
        {
            // 2. ФІЗИКА (Завжди в FixedUpdate)
            DoMove();
        }

        private void SmoothDirection()
        {
            // Згладжуємо напрямок руху
            _currentDirection = Vector3.Lerp(_currentDirection, _targetDirection, Time.deltaTime * _smoothForce);
        }

        private void DoMove()
        {
            float currentSpeed = _isRunning ? _runSpeed : _walkSpeed;

            // Якщо напрямок по Z від'ємний (рух назад), зменшуємо швидкість удвічі
            if (_currentDirection.z < 0)
            {
                currentSpeed *= 0.5f;
            }

            // Задаємо швидкість напряму. Це прибирає ефект "ковзання на льоду"
            Vector3 targetVelocity = _currentDirection * currentSpeed;
    
            _rigidbody.linearVelocity = targetVelocity;
        }

        private void DoAnim()
        {
            // Зазвичай Z - це рух вперед/назад (Speed), а X - вліво/вправо (Strafe)
            // Додав множник для анімації бігу, якщо це підтримується твоїм Blend Tree
            float animSpeedMultiplier = _isRunning ? 2f : 1f;

            animator.SetFloat("Speed", _currentDirection.x );
            animator.SetFloat("Strafe", _currentDirection.z * animSpeedMultiplier);
        }
    }