using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WyswietlPukty : MonoBehaviour {

    Text scoreText;
    SesjaGry sesjaGry;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        sesjaGry = FindObjectOfType<SesjaGry>();
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = sesjaGry.GetScore().ToString();
	}
}
