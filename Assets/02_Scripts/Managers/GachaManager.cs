using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaManager : MonoBehaviour
{   
    public static GachaManager Instance { get; private set; }

    public Gacha gacha;
    public GachaHandler gachaHandler;
    public int[] selectedAbility;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        StartGacha();
    }
    public void StartGacha()
    {
        gacha.SelectRandomAbility();
        gachaHandler.StartGacha();
    }


}
