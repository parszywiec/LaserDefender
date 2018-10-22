/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class niedzialaGameSession : MonoBehaviour {

    [SerializeField] int score;
    [SerializeField] Text scoreText;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
        scoreText.text = GetScore().ToString();
    }

//    private void Update()
//    {
//        scoreText.text = GetScore().ToString();
//    }

    private void SetUpSingleton()
    {
        int numberGameSession = FindObjectsOfType<GameSession>().Length;
        if (numberGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddToScore(int add)
    {
        score += add;
        scoreText.text = GetScore().ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }
}
*/
