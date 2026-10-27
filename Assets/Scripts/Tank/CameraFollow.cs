using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target & Following")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetOffset = new Vector3(0f, 2f, 0f);
    [SerializeField] private float smoothSpeed = 8f;

    [Header("Orbit Rotation & Limits")]
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float minPitch = -20f;  // Мінімальний кут нахилу вниз
    [SerializeField] private float maxPitch = 80f;   // Максимальний кут нахилу вгору

    [Header("Zoom (Mouse Scroll)")]
    [SerializeField] private float distance = 10f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 25f;
    [SerializeField] private float zoomSpeed = 10f;

    private float currentYaw = 0f;
    private float currentPitch = 20f;

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Вільне обертання камери (жодних кнопок затискати не потрібно)
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        currentYaw += mouseX; 
        currentPitch -= mouseY;

        // Жорстко обмежуємо нахил камери у заданих межах (працює завжди)
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        // Керування віддаленням / наближенням через колесо миші
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            distance -= scroll * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
        }

        // Розрахунок позиції камери
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        Vector3 lookAtTarget = target.position + targetOffset;
        Vector3 targetPosition = lookAtTarget - (rotation * Vector3.forward * distance);

        // Плавне переслідування цілі
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        // Постійне спрямування камери на ціль
        transform.LookAt(lookAtTarget);
    }
}