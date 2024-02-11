using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody rigidBody;
    PlayerScript playerScript;


    [SerializeField] private bool kill = false;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed;

    [SerializeField] private GameObject explosion;


    

    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
       
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        transform.LookAt(player.transform.position);

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            playerScript.isGameOver = true;
            playerScript.explosion.Play();
        }
    }
}
