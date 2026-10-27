using UnityEngine;

[CreateAssetMenu (fileName = "New Tank Config", menuName = "Tanks/Tank Config")]
public class TankConfig : ScriptableObject
{
    [Header("Weapon")]
    [Min(1)]
    public int damage = 25;
    
    [Min(0.1f)]
    public float projectileSpeed = 18f;
    
    [Min(0.005f)]
    public float fireCooldown = 0.7f;
    
    [Min(0.1f)]
    public float projectileLifetime = 5f;
    
    public Projectile projectilePrefab;
}
