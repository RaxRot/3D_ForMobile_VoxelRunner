using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]public bool canMove;
    private bool _gameStarted;
    
    [SerializeField] private float worldSpeed = 10;

    [SerializeField] private float timeToIncreaseSpeed;
    private float _increaseSpeedCounter;
    [SerializeField] private float speedMultiplier;
    private bool _canInreaseSpeed = true;
    [SerializeField] private float maxWorldSpeed = 25f;

    [SerializeField] private int coinsToContinueAfterDie = 100;

    [SerializeField] private PlayerController player;

    private int _currentCoin;
    
    [SerializeField] private GameObject[] models;
    [SerializeField] private GameObject defaultChar;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        canMove = false;

        _increaseSpeedCounter = timeToIncreaseSpeed;

        if (PlayerPrefs.HasKey(TagManager.COIN_PLAYER_PREFS))
        {
            _currentCoin = PlayerPrefs.GetInt(TagManager.COIN_PLAYER_PREFS);
        }
        UIManager.Instance.ShowCoinText(_currentCoin);
        
        SelectChar();
    }

    private void Update()
    {
        if (!_gameStarted && Input.GetMouseButtonDown(0))
        {
           GameStarted();
        }

        if (canMove && _gameStarted)
        {
            _increaseSpeedCounter -= Time.deltaTime;
            if (_increaseSpeedCounter<=0 && _canInreaseSpeed)
            {
                _increaseSpeedCounter = timeToIncreaseSpeed;
                worldSpeed *= speedMultiplier;

                if (worldSpeed>=maxWorldSpeed)
                {
                    worldSpeed = maxWorldSpeed;
                    _canInreaseSpeed = false;
                }
                
            }
        }
    }

    private void SelectChar()
    {
        for (int i = 0; i < models.Length; i++)
        {
            if (models[i].name==PlayerPrefs.GetString(TagManager.SELECTED_CHAR_NAME))
            {
                GameObject clone = Instantiate(models[i], player.modelHolder.position, player.modelHolder.rotation);
                clone.transform.parent = player.modelHolder;
                Destroy(clone.GetComponent<Rigidbody>());
                defaultChar.SetActive(false);
            }
        }
    }

    public float GetWorldSpeed()
    {
        return worldSpeed;
    }

    public void AddCoin()
    {
        _currentCoin++;
        AudioManager.Instance.PlayCoinSound();
        UIManager.Instance.ShowCoinText(_currentCoin);
        
    }

    public void EndGame()
    {
        canMove = false;
        PlayerPrefs.SetInt(TagManager.COIN_PLAYER_PREFS,_currentCoin);
        UIManager.Instance.ShowDeadPanel(true);
    }

    private void GameStarted()
    {
        UIManager.Instance.StartGame();
        
        _gameStarted = true;
        canMove = true;
    }

    public void ContinueGame()
    {
        if (_currentCoin>=coinsToContinueAfterDie)
        {
            _currentCoin -= coinsToContinueAfterDie;
           
            UIManager.Instance.ShowDeadPanel(false);
            UIManager.Instance.ShowCoinText(_currentCoin);
            
            player.ResetPlayer();
        }
        else
        {
            UIManager.Instance.ShowNotEnoughCoins();
        }
    }
}
