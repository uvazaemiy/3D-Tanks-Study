using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public WeaponData[] availableWeapons;  // масив SO
    public WeaponData currentWeapon;

    void Update()
    {
        // Клавіші 1, 2, 3 — перемикання зброї
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipWeapon(2);
        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log($"Стріляємо з {currentWeapon.weaponName}! Урон: {currentWeapon.damage}");
    }

    void EquipWeapon(int index)
    {
        if (index < availableWeapons.Length)
        {
            currentWeapon = availableWeapons[index];
            Debug.Log($"Екіпіровано: {currentWeapon.weaponName}");
        }
    }
}