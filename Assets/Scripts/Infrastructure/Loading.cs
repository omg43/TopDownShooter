using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class Loading : MonoBehaviour
{
    [SerializeField] private Image m_loading;

    private string _nameScene;
    private static Loading m_instance;

    private void Awake()
    {
        if (m_instance is not null)
        {
            Destroy(m_instance.gameObject);
            m_instance = null;
        }

        m_instance = this;
        gameObject.SetActive(false);
        DontDestroyOnLoad(target: this);
    }

    public void LoadScene(string nameScene)
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadSceneAsync(nameScene));
    }

    private IEnumerator LoadSceneAsync(string nameScene)
    {
        m_loading.fillAmount = 0;

        const int steps = 10;
        const float maxProgress = 0.5f;

        for (var i = 0; i < steps; i++)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            m_loading.fillAmount += maxProgress / steps;
        }

        var operation = SceneManager.LoadSceneAsync(nameScene);

        yield return operation;
        yield return new WaitForEndOfFrame();

        m_loading.fillAmount = 1f;
        gameObject.SetActive(false);
    }
}