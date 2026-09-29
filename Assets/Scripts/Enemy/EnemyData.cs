using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "GameData/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Основні характеристики")]
    public string enemyName;
    public int MaxHealth;
    public float speed;
    public int damage;

    public int Health;
    
    [Header("Візуал")]
    public Material enemyMat;
    public Color enemyColor = Color.white;

    public int SetHealth()
    {
        Health = Random.Range(0, MaxHealth);
        return Health;
    }
}
