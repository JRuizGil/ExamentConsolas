using UnityEngine;
using System.Collections;
using UnityEngine.Networking; // para IEnumerator

public class WaveManager : MonoBehaviour
{
    public string jsonUrl = "https://kev-games-development.net/Services/WavesTest.json";
    public EnemyPool EnemyPool; // referencia al pool
    private EnemyWave[] waves;

    void Start()
    {
        StartCoroutine(LoadWaves());
    }

    IEnumerator LoadWaves()
    {
        UnityWebRequest www = UnityWebRequest.Get(jsonUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            waves = JsonUtility.FromJson<WaveList>(json).Waves;
        }
        else
        {
            Debug.LogError("Error al cargar el JSON: " + www.error);
        }
    }

    public IEnumerator SpawnAllWaves()
    {
        if (waves == null || waves.Length == 0) yield break;

        for (int i = 0; i < waves.Length; i++)
        {
            EnemyWave wave = waves[i];

            foreach (EnemySpawn spawn in wave.Enemies)
            {
                yield return new WaitForSeconds(spawn.Time);
                EnemyPool.SpawnEnemyByType(spawn.Enemy);
            }

            yield return new WaitForSeconds(2f); // espera entre oleadas
        }
    }
}