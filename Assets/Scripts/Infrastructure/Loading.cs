using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image m_loading;
    public void LoadScene(string nameScene)
    {
        StartCoroutine(LoadScenAsyn(nameScene));
    }

    internal void DespawnAll()
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);

    }

    private IEnumerator LoadScenAsyn(string scenName)
    {
        m_loading.fillAmount = 0;

        AsyncOperation operation =  SceneManager.LoadSceneAsync(scenName);
        yield return operation;

        m_loading.fillAmount = 0.5f;
        const int steps = 10;
        var delta = 1 - m_loading.fillAmount;

        for (var i = 0; i < steps; i++)
        {
            yield return new WaitForSeconds(0.5f);
            m_loading.fillAmount += delta / steps;
        }

        gameObject.SetActive(true);
    }
}
