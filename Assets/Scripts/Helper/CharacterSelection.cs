using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    private int _characterIndex;

    [SerializeField] private TMP_Text coins;
    private int _currentCoins;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject unlockButton;
    [SerializeField] private GameObject closePersonImage;

    
    private void Start()
    {
        //1 true 0 false
        PlayerPrefs.SetInt(characters[0].name, 1);
        UpdateCharacters();
        ShowCoins();
    }

    private void ShowCoins()
    {
        if (PlayerPrefs.HasKey(TagManager.COIN_PLAYER_PREFS))
        {
            _currentCoins = PlayerPrefs.GetInt(TagManager.COIN_PLAYER_PREFS);
            _currentCoins = 700;
            coins.text = "Coins: " +_currentCoins;
        }
        else
        {
            coins.text = "Coins:" + 0;
        }
    }

    private void UpdateCharacters()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
        }
        characters[_characterIndex].SetActive(true);
        
        ShowCharInfo();
    }

    private void ShowCharInfo()
    {
        if (PlayerPrefs.GetInt(characters[_characterIndex].name)==1)
        {
            playButton.SetActive(true);
            unlockButton.SetActive(false);
            closePersonImage.SetActive(false);
        }
        else
        {
            playButton.SetActive(false);
            unlockButton.SetActive(true);
            closePersonImage.SetActive(true);
            unlockButton.GetComponentInChildren<TextMeshProUGUI>().text =
                "Price:\n" + characters[_characterIndex].GetComponent<Chatacter>().price + "\nCoins";
        }
    }

    public void UnlockPlayer()
    {
        if (_currentCoins>=characters[_characterIndex].GetComponent<Chatacter>().price)
        {
            _currentCoins -= characters[_characterIndex].GetComponent<Chatacter>().price;
            coins.text = "Coins: " +_currentCoins;
            PlayerPrefs.SetInt(TagManager.COIN_PLAYER_PREFS,_currentCoins);
            PlayerPrefs.SetInt(characters[_characterIndex].name,1);
            
            UpdateCharacters();
        }
        else
        {
            Debug.Log("No  money");
        }
    }

    public void Play()
    {
        PlayerPrefs.SetString(TagManager.SELECTED_CHAR_NAME,characters[_characterIndex].name);
        SceneManager.LoadScene("Game");
    }

    public void MoveLeft()
    {
        _characterIndex--;
        if (_characterIndex<=0)
        {
            _characterIndex = 0;
        }
        
        UpdateCharacters();
    }

    public void MoveRight()
    {
        _characterIndex++;
        if (_characterIndex>=characters.Length)
        {
            _characterIndex = characters.Length - 1;
        }
        
        UpdateCharacters();
    }
}
