using System;
using System.Collections;
using System.Collections.Generic;
using LMS.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] Button btn;
    GameObject fadeObj;

    private static FadeManager instance = null;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static FadeManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called before the first frame update
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIn());
    }
    
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(FadeOut(() => SceneManager.LoadScene(sceneIndex)));
        if(sceneIndex == 0)
            btn.gameObject.SetActive(true);
    }

    public void OnFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    //페이드 인
    IEnumerator FadeIn(Action action = null)
    {
        Color color = Color.black;
        color.a = 1f;
        while (color.a > 0f)
        {
            color.a -= 0.01f;
            fadeImage.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        action?.Invoke();
    }
    
    //페이드 아웃
    public IEnumerator FadeOut(Action action = null)
    {
        Color color = Color.black;
        color.a = 0f;
        while (color.a < 1f)
        {
            color.a += 0.01f;
            fadeImage.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        action?.Invoke();
    }
}
