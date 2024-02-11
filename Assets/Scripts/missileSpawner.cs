using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject missile;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MissileSpawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MissileSpawn()
    {
        while (true)
        {   
            Instantiate(missile, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(3);
        }
        
    }
}
