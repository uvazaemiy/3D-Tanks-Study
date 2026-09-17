using UnityEngine;

using UnityEngine.UI; // Потрібно для доступу до компонента Slider

public class VolumeControl : MonoBehaviour

{

    // Слайдер, який перетягнемо з Hierarchy в Inspector

    public Slider volumeSlider;
// AudioSource, гучністю якого керуємо
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        // Встановлюємо початкове положення слайдера = поточній гучності AudioSource
        // Так слайдер відображатиме реальний стан, а не стрибатиме в 0 при старті
        float savedVolume = PlayerPrefs.GetFloat("volume", 1);
        
        volumeSlider.value = savedVolume;
        audioSource.volume = savedVolume;
    }

// Цей метод підключимо до події On Value Changed слайдера
// Він буде викликатися КОЖНОГО разу, коли гравець рухає повзунок
    public void ChangeVolume()
    {
        audioSource.volume = volumeSlider.value;
        
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }
}