using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    // Use this for initialization
    // Coroutine metody Start - pewnie bede to wykorzystywac, wiec warto pamietac
    IEnumerator Start ()
    {
        do
        {
            // Coroutine wywoluje metode Coroutinowa
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
	}

    // loop w loopie
    private IEnumerator SpawnAllWaves()
    {
        for (int wavesIndex = 0; wavesIndex < waveConfigs.Count; wavesIndex++)
        {
            var currentWave = waveConfigs[wavesIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));

        }
    }

	// loop w loopie loopie :)
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = startingWave; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                                waveConfig.GetEnemyPrefab(),
                                waveConfig.GetWaypoints()[0].transform.position,
                                Quaternion.identity); // <- oznacza to, zacznij roatcje od nowa - w skrocie 
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
