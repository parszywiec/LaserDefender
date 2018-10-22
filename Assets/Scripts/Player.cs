using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player")] // tworzy naglowek dla pol, jak wskazuje nazwa
    [SerializeField] float moveSpeed = 10f;
    // zmienna okresla jak daleko od granicy ekranu moze byc nasz "samolot" (obiekt player)
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 200;
    [Header("Projectile")]
    [SerializeField] GameObject playerLaser;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [Header("Sounds")]
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float laserVolume = 0.1f;
    [SerializeField] AudioClip destroySound;
    [SerializeField] [Range(0, 1)] float destroyVolume = 1f;

    Coroutine firingCoroutine;

    float xMin, xMax, yMin, yMax;

    // Use this for initialization
    void Start () {
        SetUpMoveBondaries();

        // Coroutine - czyli metoda do robienia czegos w sposob ciagly do spelnienia jakiegos warunku - np strzelaj puki spacja jest przycisnieta 
        // StartCoroutine(WaitForThreeSec());
	}

    // Update is called once per frame
    void Update () {
        Move();
        Fire();

    }

    // sa dwie metody Trigger i Collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // tworzenie obiektu typu DamageDealer - ktory na razie jest laserem, ktory trafil w cos i start metody ponizej
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        GotHit(damageDealer);
    }

    private void GotHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        PlayPlayerDestorySFK();
    }

    private void Fire()
    {
        // strzelanie w sposob ciagly
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        // koniec strzelania, gdy button idzie w gore, czyli w tym wypadku gdy puszczam spacje
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    // metoda typu Coroutine, czyli taka ktora sie wykonuje do spelnienia yielda
    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(playerLaser, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            PlayPlayerShootingSFK();
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
        // poruszanie sie horyzontalne(poziom / x) i verticalne (pionowe / y)
        // Time.deltaTime podaje ile czasu trwal ostatni frame (fps)
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        // Debug.Log(deltaX);
        // dobra funkcja/metoda - pamietaj o niej... (Mathf.Clamp) daa! 
        // w SetUpMove... przypisywane wartosci, za ktore nie mozna wyjsc, dzieki metodzie
        var newXPosit = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPosit = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2 (newXPosit, newYPosit);
    }

    private void SetUpMoveBondaries()
    {
        Camera gameCamera = Camera.main;
        // w tym wypadku zamyka ekran na wartosciach naszej kamery, sam ma zakresy od 0 do 1, czyli 0 dla x to wartosc lewej granicy kamery, a wiec tego
        // co widzimy w grze
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        // w wektorze y i z jest w tym wypadku bez znaczenia, bo bierzemy tylko wartosc x
        // czyli bierze 1 - to jest skrajna wartosc x dla camery i podstawia wartosc z naszej plaszczyzny gry atm ~5.62
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        // Debug.Log(xMin);
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 1)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void PlayPlayerDestorySFK()
    {
        AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position, destroyVolume);
    }
    
    private void PlayPlayerShootingSFK()
    {
        AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserVolume);
    }

    public int GetPlayerHealth()
    {
        return health;
    }

/*    // Playin with Coroutine
    private IEnumerator WaitForThreeSec()
    {
        // Debug.Log(Time.time);
        yield return new WaitForSeconds(3);
        // Debug.Log(Time.time);
    }
*/        
}
