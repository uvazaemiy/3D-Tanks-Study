using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "GameData/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public ItemType itemType;
    public WeaponType weaponType;
    public int value;           // ціна або потужність ефекту
    public bool isStackable;    // чи можна складати в стопку
}