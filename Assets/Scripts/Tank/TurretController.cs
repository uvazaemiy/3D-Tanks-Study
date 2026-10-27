using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Turret")]
    [SerializeField] private Transform turretPivot;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float turretRotationSpeed = 120f; // Градусів за секунду

    private Vector3 targetPoint;
    private bool hasTarget = false;

    private void Update()
    {
        if (turretPivot == null || Camera.main == null)
            return;

        // 1. Зберігаємо останню точку перетину миші з шаром GroundMask при кліку
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            // Виправлено порядок аргументів: maxDistance (Mathf.Infinity) йде третім, а groundMask — четвертим
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask))
            {
                targetPoint = hit.point;
                hasTarget = true;
            }
        }

        // 2. Постійно повертаємо башту до останньої зафіксованої позиції
        if (hasTarget)
        {
            Vector3 direction = targetPoint - turretPivot.position;
            direction.y = 0f; // Обнуляємо висоту, щоб башта крутилася тільки горизонтально

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Плавне обертання до цілі
                turretPivot.rotation = Quaternion.RotateTowards(
                    turretPivot.rotation,
                    targetRotation,
                    turretRotationSpeed * Time.deltaTime
                );
            }
        }
    }
}