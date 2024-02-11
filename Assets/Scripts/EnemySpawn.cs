using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject enemyPlane;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemyPlane, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(7f);
        }
        
    }

}
