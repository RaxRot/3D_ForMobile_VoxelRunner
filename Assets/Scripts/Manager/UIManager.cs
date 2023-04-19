using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TMP_Text tapToStartText;
    [SerializeField] private TMP_Text coinText;

    [SerializeField] private GameObject deadPanel;
    [SerializeField] private TMP_Text notEnoughCoins;
    [SerializeField] private float timeToShowWarning = 3f;

    [SerializeField] private Image fadeImage;
    private bool _shouldFadeIn;
    private bool _shouldFadeOut=true;
    [SerializeField] private float fadeSpeed = 2f;

    [SerializeField] private GameObject pausePanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        tapToStartText.gameObject.SetActive(true);
        deadPanel.SetActive(false);
        notEnoughCoins.gameObject.SetActive(false);
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        ControlFade();
    }

    private void ControlFade()
    {
        if (_shouldFadeIn)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b,
                Mathf.MoveTowards(fadeImage.color.a, 1f, fadeSpeed * Time.deltaTime));
        }else if (_shouldFadeOut)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b,
                Mathf.MoveTowards(fadeImage.color.a, 0, fadeSpeed * Time.deltaTime));
        }
    }

    public void FadeIn()
    {
        _shouldFadeIn = true;
        _shouldFadeOut = false;
    }

    public void FadeOut()
    {
        _shouldFadeIn = false;
        _shouldFadeOut = true;
    }

    public void StartGame()
    {
        tapToStartText.gameObject.SetActive(false);
    }

    public void ShowCoinText(int coinsToShow)
    {
        coinText.text = "Coins: " + coinsToShow;
    }

    public void ShowDeadPanel(bool shouldShowDeadScreen)
    {
        deadPanel.SetActive(shouldShowDeadScreen);
    }
    

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(TagManager.MAIN_MENU_NAME);
        Time.timeScale = 1f;
    }

    public void ContinueGame()
    {
        GameManager.Instance.ContinueGame();
    }

    public void ShowNotEnoughCoins()
    {
        StartCoroutine(nameof(_ShowNotEnoughCoinsCO));
    }
    
    private IEnumerator _ShowNotEnoughCoinsCO()
    {
        notEnoughCoins.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(timeToShowWarning);
        
        notEnoughCoins.gameObject.SetActive(false);
    }

    public void ShowPausePanel(bool showPausePanel)
    {
        if (showPausePanel)
        {
            pausePanel.SetActive(showPausePanel);
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.SetActive(showPausePanel);
            Time.timeScale = 1;
        }
    }
    
}
    
