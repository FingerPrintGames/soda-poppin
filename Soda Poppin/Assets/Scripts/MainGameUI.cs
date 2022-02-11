using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;

    CoinCounter coinCounter;

    void Start()
    {
        coinCounter = FindObjectOfType<CoinCounter>();
    }

    void Update()
    {
        UpdateCoinUI();
    }

    void UpdateCoinUI()
    {
        coinText.text = coinCounter.GetCoinAmount().ToString();
    }
}
