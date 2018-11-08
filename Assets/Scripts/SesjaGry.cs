using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesjaGry : MonoBehaviour {

    int score = 0;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<SesjaGry>().Length;
        if (numberGameSessions > 1)
        {
            // roziwazanie problemu z naliczaniem, lub nie punktow, poniewaz destroy (unity - manual - stany awake, init itd) jest na koncu framea, to istnieja dwa obiekty przez jego dlugosc
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int add)
    {
        score += add;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
