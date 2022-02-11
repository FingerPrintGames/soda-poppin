using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] int coins = 0;
    [SerializeField] List<bool> coinsCollected;

    static CoinCounter instance;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetCoinAmount()
    {
        return coins;
    }

    public void AddACoin()
    {
        coins++;
    }

    public void CheckCoinOffOfList(int value)
    {
        coinsCollected[value] = true;
    }

    public bool CoinCollected(int value)
    {
        return coinsCollected[value];
    }
}
