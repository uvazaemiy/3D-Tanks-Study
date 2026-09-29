using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "GameData/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Інформація")]
    public string weaponName;
    public string description;
    public Sprite icon;
    public ItemData itemData;
    public WeaponType weaponType;
    
    [Header("Характеристики")]
    public float damage;
    public float fireRate;       // постріли за секунду
    public float bulletSpeed;
    public int magazineSize;

    [Header("Звук та ефекти")]
    public AudioClip shootSound;
    public GameObject bulletPrefab;
    public GameObject muzzleFlashEffect;
}