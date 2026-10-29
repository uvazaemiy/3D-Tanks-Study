using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target & Following")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetOffset = new Vector3(0f, 2f, 0f);
    [SerializeField] private float smoothSpeed = 20f; // Трохи збільшено для швидкого слідування

    [Header("Orbit Rotation")]
    [SerializeField] private float rotationSpeed = 200f;

    [Header("Zoom (Mouse Scroll)")]
    [SerializeField] private float distance = 10f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 25f;
    [SerializeField] private float zoomSpeed = 10f;

    private float currentYaw = 0f;
    private float currentPitch = 20f;

    private void Start()
    {
        Cursor.visible = false;
        Vector3 angles = transform.eulerAngles;
        currentYaw = angles.y;
        currentPitch = angles.x;
    }

    // Використовуємо FixedUpdate замість LateUpdate, щоб синхронізувати рух з фізикою об'єкта
    private void FixedUpdate()
    {
        if (target == null)
            return;
        
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;

        currentYaw += mouseX; 
        currentPitch -= mouseY;

        // Керування зумом (працює і в FixedUpdate через Time.fixedDeltaTime для стабільності)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            distance -= scroll * zoomSpeed * 1.5f; // Компенсація fixedDeltaTime для скролу
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
        }

        // Розрахунок повороту та цільової позиції
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        Vector3 targetCenter = target.position + targetOffset;
        Vector3 targetPosition = targetCenter - (rotation * Vector3.forward * distance);

        // Застосовуємо поворот напряму
        transform.rotation = rotation;

        // Плавне переміщення камери до цільової точки з використанням Time.fixedDeltaTime
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            1f - Mathf.Exp(-smoothSpeed * Time.fixedDeltaTime)
        );
    }
}