using System;
using System.Collections.Generic;

[Serializable]
public class WaveData
{
    public int Waves;                 // Número de oleada
    public List<EnemySpawn> Enemies; // Lista de enemigos a spawnear
}

