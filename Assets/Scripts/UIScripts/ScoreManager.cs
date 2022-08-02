using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] Text CoinTextMenu;
    [SerializeField] Text CoinTextInsideGame;
    int coin = 0;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        coin = Prefs.COIN;
        CoinTextMenu.text = coin.ToString();
        CoinTextInsideGame.text = coin.ToString();
    }

    public void AddPoint()
    {
        coin++;
        Prefs.COIN = coin;
        CoinTextMenu.text = coin.ToString();
        CoinTextInsideGame.text = coin.ToString();
    }    
}
