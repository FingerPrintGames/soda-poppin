using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    [SerializeField] Vector3 rotationSpeed;

    void Awake()
    {
        CoinCounter coinCounter = FindObjectOfType<CoinCounter>();
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (coinCounter.CoinCollected(currentScene) && gameObject.name != "UICoin")
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
