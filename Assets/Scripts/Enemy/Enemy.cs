using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Перетягни сюди потрібний EnemyData.asset в Inspector
    public EnemyData data;

    private int currentHealth;
    private MeshRenderer meshRenderer;

    void Start()
    {
        // Перевіряємо чи підключені дані
        if (data == null)
        {
            Debug.LogError("EnemyData не підключений до " + gameObject.name);
            return;
        }

        // Ініціалізуємо ворога з даних SO
        
        currentHealth = data.SetHealth();;

        // Підтягуємо спрайт якщо є
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null && data.enemyMat != null)
        {
            meshRenderer.material = data.enemyMat;
        }

        // Називаємо GameObject за ім'ям з SO
        gameObject.name = data.enemyName;

        Debug.Log("Створено ворога: " + data.enemyName + " HP: " + currentHealth + " Speed: " + data.speed);
    }

    void Update()
    {
        // Простий рух вперед з швидкістю з SO
        transform.Translate(Vector3.left * data.speed * Time.deltaTime);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{data.enemyName} отримав {amount} шкоди. Залишилось HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{data.enemyName} знищений!");
        Destroy(gameObject);
    }
}