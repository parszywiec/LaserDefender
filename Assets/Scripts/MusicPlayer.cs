using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private void Awake()
    {
        // dzaiala
        // int gameMusicCount = FindObjectsOfType<MusicPlayer>().Length;
        // if (gameMusicCount > 1) Destroy(gameObject);
        // alternatywnie - GetType() pobiera typ pliku w ktorym obecnie go uzywamy, wiec mozna ctrl+c i bedzie dzialal dla innych obiektow
        if (FindObjectsOfType(GetType()).Length > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }
}
