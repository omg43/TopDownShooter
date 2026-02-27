using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [Header("Компонент изображения")]
    [SerializeField] private Image targetImage;

    [Header("Настройки цвета")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private bool changeRed = true;
    [SerializeField] private bool changeGreen = true;
    [SerializeField] private bool changeBlue = true;
    [SerializeField] private bool changeAlpha = false;

    [Header("Режим изменения")]
    [SerializeField] private ColorMode colorMode = ColorMode.SmoothRGB;

    private float time;
    private Color startColor;
    private Color targetColor;

    public enum ColorMode
    {
        SmoothRGB,        // Плавное RGB с синусоидой
        Rainbow,          // Радужный цикл
        Pulse,            // Пульсация
        RandomTransition, // Случайные переходы
        CustomColors      // Пользовательские цвета
    }

    void Start()
    {
        // Автоматически находим Image, если не назначен
        if (targetImage == null)
            targetImage = GetComponent<Image>();

        if (targetImage == null)
        {
            Debug.LogError("Image компонент не найден!");
            enabled = false;
            return;
        }

        startColor = targetImage.color;

        if (colorMode == ColorMode.RandomTransition)
        {
            StartCoroutine(RandomColorTransition());
        }
    }

    void Update()
    {
        if (targetImage == null) return;

        time += Time.deltaTime * speed;

        switch (colorMode)
        {
            case ColorMode.SmoothRGB:
                SmoothRGB();
                break;
            case ColorMode.Rainbow:
                Rainbow();
                break;
            case ColorMode.Pulse:
                Pulse();
                break;
            case ColorMode.CustomColors:
                // Обрабатывается в корутине
                break;
        }
    }

    void SmoothRGB()
    {
        float r = targetImage.color.r;
        float g = targetImage.color.g;
        float b = targetImage.color.b;
        float a = targetImage.color.a;

        // Плавное изменение с помощью синуса
        if (changeRed)
            r = (Mathf.Sin(time) + 1f) / 2f;

        if (changeGreen)
            g = (Mathf.Sin(time + 2f) + 1f) / 2f;

        if (changeBlue)
            b = (Mathf.Sin(time + 4f) + 1f) / 2f;

        if (changeAlpha)
            a = (Mathf.Sin(time * 2f) + 1f) / 2f;

        targetImage.color = new Color(r, g, b, a);
    }

    void Rainbow()
    {
        // Радужный цикл (HSV)
        float hue = (time * 0.5f) % 1f;
        Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);

        float a = targetImage.color.a;
        if (changeAlpha)
            a = (Mathf.Sin(time * 2f) + 1f) / 2f;

        targetImage.color = new Color(
            changeRed ? rainbowColor.r : targetImage.color.r,
            changeGreen ? rainbowColor.g : targetImage.color.g,
            changeBlue ? rainbowColor.b : targetImage.color.b,
            a
        );
    }

    void Pulse()
    {
        // Пульсирующий эффект
        float pulse = (Mathf.Sin(time * 2f) + 1f) / 2f;

        if (changeRed)
            targetImage.color = new Color(pulse, targetImage.color.g, targetImage.color.b, targetImage.color.a);
        if (changeGreen)
            targetImage.color = new Color(targetImage.color.r, pulse, targetImage.color.b, targetImage.color.a);
        if (changeBlue)
            targetImage.color = new Color(targetImage.color.r, targetImage.color.g, pulse, targetImage.color.a);
        if (changeAlpha)
            targetImage.color = new Color(targetImage.color.r, targetImage.color.g, targetImage.color.b, pulse);
    }

    IEnumerator RandomColorTransition()
    {
        while (true)
        {
            // Генерируем случайный целевой цвет
            targetColor = new Color(
                changeRed ? Random.value : targetImage.color.r,
                changeGreen ? Random.value : targetImage.color.g,
                changeBlue ? Random.value : targetImage.color.b,
                changeAlpha ? Random.value : targetImage.color.a
            );

            // Плавно переходим к новому цвету
            float duration = 2f / speed;
            float elapsed = 0f;
            Color startColor = targetImage.color;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                targetImage.color = Color.Lerp(startColor, targetColor, t);
                yield return null;
            }

            // Небольшая пауза
            yield return new WaitForSeconds(0.5f / speed);
        }
    }

    // Публичный метод для смены режима
    public void SetColorMode(ColorMode newMode)
    {
        colorMode = newMode;
        time = 0f;

        if (colorMode == ColorMode.RandomTransition)
        {
            StopAllCoroutines();
            StartCoroutine(RandomColorTransition());
        }
    }

    // Публичный метод для установки конкретного цвета
    public void SetColor(Color newColor)
    {
        targetImage.color = newColor;
    }

    // Публичный метод для установки скорости
    public void SetSpeed(float newSpeed)
    {
        speed = Mathf.Max(0.1f, newSpeed);
    }
}
