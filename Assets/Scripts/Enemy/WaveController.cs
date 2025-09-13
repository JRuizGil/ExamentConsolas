using UnityEngine;
using System.Collections; // para IEnumerator
public class WaveController : MonoBehaviour
{
    public float waveStartDelay = 3f;
    public WaveCollection WaveData; // WaveCollection es la clase que tiene List<WaveData> Waves
    public EnemyPool EnemyPool;     // Referencia al pool
    public WaveManager WaveManager; // Referencia al WaveManager

    void Start()
    {
        Invoke("StartWaves", waveStartDelay);
    }

    void StartWaves()
    {
        if (WaveManager != null)
        {
            WaveManager.StartCoroutine(WaveManager.SpawnAllWaves());
        }
        else
        {
            Debug.LogWarning("WaveManager no asignado.");
        }
    }
    
    



}