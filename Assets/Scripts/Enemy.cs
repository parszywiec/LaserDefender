using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyLaser;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject ExplosionVFX;
    [SerializeField] int ScoreForEnemy = 100;
    [Header("Sounds")]
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float laserVolume = 0.3f;
    [SerializeField] AudioClip destroySound;
    [SerializeField] [Range(0, 1)] float destroyVolume = 0.8f;

    // Use this for initialization
    void Start () {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
	}
	
	// Update is called once per frame
	void Update () {
        CountDownAndShoot();
	}

    private void CountDownAndShoot()
    {
        // odejmujemy czas ostatniej klatki
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            
            Fire();
            // inicjacja za ile kolejny strzal, ktory wykonuje sie w Fire() - dajemu mu ponownie wartosc, aby nie strzelal bez przerwy
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            // PlayEnemyShootingSFK();
        }
    }

    private void Fire()
    {
        // inicujemy obiekt laseru (jaki obiekt, skad zaczyna, rotacja - tutaj brak (identity - readonly wg. manuala)
        GameObject laser = Instantiate(enemyLaser, transform.position, Quaternion.identity) as GameObject;
        // velocity gdzie leca w tym wypadku naboje 0 na y, ujemne wartosci na x leca w dol
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        PlayEnemyShootingSFK();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // tworzenie obiektu zadajacego dmg
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        // jesli trafimy w obiekt nie posiadajacy scriptu DamageDealer, to wroc nie wykonuj sie dalej, zabezpierczenie przed brakiem obiektu
        if (!damageDealer) return;
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        // ile dmg zadaje naboj, ktorym dostal obiekt
        health -= damageDealer.GetDamage();
        // niszczy naboj/kule/laser
        damageDealer.Hit();
        if (health <= 0)
        {
            FindObjectOfType<SesjaGry>().AddToScore(ScoreForEnemy);
            Destroy(gameObject);
            PlayEnemyDestorySFK();
            TriggerExplosionVFX();
        }
    }
    private void TriggerExplosionVFX()
    {
        GameObject explosion = Instantiate(ExplosionVFX, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
    }

    private void PlayEnemyShootingSFK()
    {
        AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserVolume);
    }

    private void PlayEnemyDestorySFK()
    {
        AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position, destroyVolume);
    }
}
