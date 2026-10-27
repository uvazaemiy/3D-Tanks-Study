using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float rotationSpeed = 100f;
    
    [Header("Acceleration")]
    [SerializeField] private float acceleration = 3f;
    [SerializeField] private float deceleration = 5f;
    [SerializeField] private float turnAcceleration = 6f;
    
    private Rigidbody tankRigidbody;

    private float targetMoveInput;
    private float targetTurnInput;
    private float currentMoveInput;
    private float currentTurnInput;
    
    private void Awake()
    {
        tankRigidbody = GetComponent<Rigidbody>();
        
        // Рекомендація: бажано заблокувати обертання через фізику, 
        // щоб танк не перевертався від зіткнень
        tankRigidbody.freezeRotation = true;
    }

    private void Update()
    {
        // 1. Отримуємо сирий вхід та записуємо як цільові значення
        targetMoveInput = Input.GetAxisRaw("Vertical");
        targetTurnInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        SmoothInput();
        MoveTank();
    }

    private void SmoothInput()
    {
        // Вибираємо швидкість зміни залежно від того, чи натиснута кнопка руху
        float moveChangeSpeed = (targetMoveInput == 0f) ? deceleration : acceleration;

        // Плавно інтерполюємо поточний рух до цільового
        currentMoveInput = Mathf.MoveTowards(
            currentMoveInput,
            targetMoveInput,
            moveChangeSpeed * Time.fixedDeltaTime
        );

        // Плавно інтерполюємо поворот
        currentTurnInput = Mathf.MoveTowards(
            currentTurnInput,
            targetTurnInput,
            turnAcceleration * Time.fixedDeltaTime
        );
    }

    private void MoveTank()
    {
        // Розрахунок повороту
        float turn = currentTurnInput * rotationSpeed * Time.fixedDeltaTime;
        Quaternion rotationDelta = Quaternion.Euler(0f, turn, 0f);

        // Виправлено множення кватерніонів
        Quaternion nextRotation = tankRigidbody.rotation * rotationDelta;
        
        // Виправлено множення кватерніона на напрямок
        Vector3 moveDirection = nextRotation * Vector3.forward;

        // Розрахунок позиції
        Vector3 nextPosition = tankRigidbody.position + moveDirection * currentMoveInput * moveSpeed * Time.fixedDeltaTime;

        // Застосування через Rigidbody для коректної взаємодії з фізикою
        tankRigidbody.MoveRotation(nextRotation);
        tankRigidbody.MovePosition(nextPosition);
    }
}