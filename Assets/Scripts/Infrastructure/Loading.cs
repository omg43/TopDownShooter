using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image m_loading;

    private static Loading m_instance;

    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(m_instance.gameObject);
            m_instance = null;
        }

        m_instance = this;
        DontDestroyOnLoad(target: this);
        gameObject.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        m_loading.fillAmount = 0;
        const int steps = 10;
        const float maxProgress = 0.5f;
        for (var i = 0; i < steps; i++)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            m_loading.fillAmount += maxProgress / steps;
        }

        var operation = SceneManager.LoadSceneAsync(sceneName);

        yield return operation;
        yield return new WaitForEndOfFrame();

        m_loading.fillAmount = 1f;
        gameObject.SetActive(false);
    }
}

