using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public List<GameObject> enemyPrefabs; // Lista de prefabs, índice = tipo de enemigo

    [Header("Pool Settings")]
    public int poolSizePerType = 5;       // Cuántos de cada tipo instanciar
    private Dictionary<int, List<GameObject>> pool = new Dictionary<int, List<GameObject>>();

    void Awake()
    {
        // Crear pool de cada tipo de enemigo
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            pool[i] = new List<GameObject>();
            for (int j = 0; j < poolSizePerType; j++)
            {
                GameObject obj = Instantiate(enemyPrefabs[i], transform); // hijo del pool
                obj.SetActive(false);
                obj.name = enemyPrefabs[i].name + "_Clone_" + j;
                pool[i].Add(obj);
            }
        }
    }
    /// <summary>
    /// Activa un enemigo del tipo especificado desde el pool.
    /// Si no hay disponible, lo instancia y lo agrega al pool.
    /// </summary>
    public GameObject SpawnEnemyByType(int type)
    {
        if (!pool.ContainsKey(type))
        {
            Debug.LogWarning($"Enemy type {type} no existe en el pool.");
            return null;
        }

        // Buscar un enemigo inactivo
        foreach (GameObject enemy in pool[type])
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                // Reiniciar posición/rotación opcionalmente
                enemy.transform.position = Vector3.zero;
                enemy.transform.rotation = Quaternion.identity;
                return enemy;
            }
        }

        // Si no hay disponible, instanciar uno nuevo y agregarlo al pool
        GameObject newEnemy = Instantiate(enemyPrefabs[type], transform);
        newEnemy.name = enemyPrefabs[type].name + "_Clone_Extra";
        pool[type].Add(newEnemy);
        return newEnemy;
    }
}
