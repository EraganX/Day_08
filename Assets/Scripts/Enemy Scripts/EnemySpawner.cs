using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private GameObject enemy;

    private List<Vector3> locations = new List<Vector3>();

    private void Start()
    {
        foreach (Transform location in spawnLocations)
        {
            locations.Add(location.position);
        }

        StartCoroutine(SpawnEnemis());
    }



    IEnumerator SpawnEnemis()
    {
        while (GameManager.Instance.isGameOver==false)
        {
            yield return new WaitForSeconds(Random.Range(2f,10f));
            Instantiate(enemy, locations[Random.Range(0,locations.Count)],enemy.transform.rotation);
        }
    }
}
