using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    Text healtText;
    Player playerHealth;

	// Use this for initialization
	void Start () {
        healtText = GetComponent<Text>();
        playerHealth = FindObjectOfType<Player>();
        
	}
	
	// Update is called once per frame
	void Update () {
        healtText.text = playerHealth.GetPlayerHealth().ToString();
	}
}
