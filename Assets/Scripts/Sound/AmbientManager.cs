using UnityEngine;

using System.Collections;

public class AmbientManager : MonoBehaviour

{
    public AudioSource forestAmbient;

    public AudioSource windAmbient;

    public AudioSource riverAmbient;
// Час плавного переходу в секундах
    public float crossfadeDuration = 2.0f;
    
    private AudioSource currentaudioSource;

    private void Start()
    {
        currentaudioSource = forestAmbient;
    }

    // Викликай цей метод при вході в підземелля
    public void TransitionToDungeon()
    {
        StartCoroutine(Crossfade(currentaudioSource, windAmbient));
    }

    public void TransitionToForest()
    {
        StartCoroutine(Crossfade(currentaudioSource, forestAmbient));
    }
    
    public void TransitionToRiver()
    {
        StartCoroutine(Crossfade(currentaudioSource, riverAmbient));
    }

// Корутина: одночасно зменшує гучність одного і збільшує іншого
    private IEnumerator Crossfade(AudioSource fadeOut, AudioSource fadeIn)
    {
        float startVolumeFadeOut = fadeOut.volume;
        float startVolumeFadeIn = 0;
    
        // Починаємо відтворення нового шару з нульової гучності
        fadeIn.volume = 0;
        fadeIn.Play();
        Debug.Log("Почав грати: " + fadeIn.name);

        currentaudioSource = fadeIn;
    
        float elapsed = 0;
    
        while (elapsed < crossfadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / crossfadeDuration; // Від 0 до 1
            // Зменшуємо один, збільшуємо інший
            fadeOut.volume = Mathf.Lerp(startVolumeFadeOut, 0, t);
            fadeIn.volume = Mathf.Lerp(startVolumeFadeIn, 0.5f, t);
        
            yield return null; // Чекаємо наступний кадр
        }
    
        // Зупиняємо попередній шар після завершення переходу
        fadeOut.Stop();
        Debug.Log("Зупинили: " + fadeOut.name);
    }
}