using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Turret")]
    [SerializeField] private Transform turretPivot;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float turretRotationSpeed = 120f; // Градусів за секунду

    private void Update()
    {
        if (turretPivot == null || Camera.main == null)
            return;

        // 1. Пускаємо промінь із самого центру екрана (Viewport 0.5, 0.5)
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask))
        {
            Vector3 targetPoint = hit.point;

            // 2. Вираховуємо напрямок від башти до точки перетину в центрі екрана
            Vector3 direction = targetPoint - turretPivot.position;
            direction.y = 0f; // Обнуляємо висоту, щоб башта крутилася тільки в горизонтальній площині

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // 3. Плавне обертання до цілі
                turretPivot.rotation = Quaternion.RotateTowards(
                    turretPivot.rotation,
                    targetRotation,
                    turretRotationSpeed * Time.deltaTime
                );
            }
        }
    }
}