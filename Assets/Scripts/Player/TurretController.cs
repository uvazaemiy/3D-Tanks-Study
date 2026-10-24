using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Turret")]
    [SerializeField] private Transform turretPivot;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float turretRotationSpeed = 12f;

    private void Update()
    {
        AimTurret();
    }
    
    private void AimTurret()
    {
        if (turretPivot == null || Camera.main == null)
            return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 200f, groundMask))
        {
            Vector3 direction = hit.point - turretPivot.position;
            direction.y = 0f;
            if (direction.sqrMagnitude < 0.01f)
                return;
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            turretPivot.rotation = Quaternion.RotateTowards(
                turretPivot.rotation,
                targetRotation,
                turretRotationSpeed * Time.deltaTime
            );
        }
    }
}
