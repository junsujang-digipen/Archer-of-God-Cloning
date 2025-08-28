using System;
using TMPro;
using UnityEngine;

public class UpdateHP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Character character;
    void Update()
    {
        hpText.text = "HP: " + Math.Ceiling(character.HP);
    }
}
