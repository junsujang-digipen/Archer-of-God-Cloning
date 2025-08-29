using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndCheck : MonoBehaviour
{
    Character[] _characters;
    [SerializeField] GameObject UI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _characters = FindObjectsByType<Character>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var character in _characters)
        {
            if (character.HP <= 0)
            {
                GameTime.TimeScale = 0;

                UI.SetActive(true);
                if (GlobalVariables.PlayerCharacter.HP <= 0)
                { 
                    UI.GetComponentInChildren<TextMeshProUGUI>().text = "You Lose";
                }else
                {
                    UI.GetComponentInChildren<TextMeshProUGUI>().text = "You Win";
                }
                Debug.Log("Game Over");
                break;
            }
        }
    }
    public void Restart()
    {
        GameTime.TimeScale = 1;
        SceneManager.LoadScene("Temp", LoadSceneMode.Single);
    }
}
