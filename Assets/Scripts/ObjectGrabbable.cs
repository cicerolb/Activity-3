using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody rigidBody;
    private MeshRenderer meshRenderer;
    private CapsuleCollider capsuleCollider;
    private BoxCollider boxCollider;
    PlayerScript playerScript;
    AudioSource missleAudio;

    private Transform objectGrabPointTransform;

    [SerializeField] private Transform objectLookPointTransform;
    [SerializeField] private GameObject objectlookPoint;
    [SerializeField] private bool fired;
    [SerializeField] private float projectileSpeed;

    public GameObject flame;
    public GameObject smoke;
    public GameObject explosion;
    public GameObject laser;

    private Collision collisionObject;





    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        
        if (capsuleCollider == null)
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
        }

        if (boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        missleAudio = GetComponent<AudioSource>();


        if (objectLookPointTransform == null)
        {
            objectlookPoint = GameObject.FindGameObjectWithTag("Look");

            if (objectlookPoint != null)
            {
                objectLookPointTransform = objectlookPoint.transform;
            }
        }

        fired = false;

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        rigidBody.useGravity = false;
        laser.SetActive(true);
        if (capsuleCollider != null)
        {
            capsuleCollider.enabled = false;
        }

        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }

    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        rigidBody.useGravity = true;
        laser.SetActive(false);
        if (capsuleCollider != null)
        {
            capsuleCollider.enabled = true;
        }

        if (boxCollider != null)
        {
            boxCollider.enabled = true;
        }
    }

    public void Fired()
    {

        this.objectGrabPointTransform = null;
        rigidBody.velocity = -transform.right * projectileSpeed;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        fired = true;

        flame.SetActive(true);
        smoke.SetActive(true);
        laser.SetActive(true);
        missleAudio.Play();
        if (capsuleCollider != null)
        {
            capsuleCollider.enabled = true;
        }

        if (boxCollider != null)
        {
            boxCollider.enabled = true;
        }





    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * 10f);
            rigidBody.MovePosition(newPosition);

            Quaternion rotationFix = objectLookPointTransform.rotation * Quaternion.Euler(new Vector3(0, 90, 0));

            transform.rotation = rotationFix;
        }

  
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && fired )
        {
            collisionObject = collision;
            StartCoroutine(EnemyDeath());
            meshRenderer.enabled = false;
            explosion.SetActive(true);
            playerScript.explosion.Play();



        }

        
    }

    public IEnumerator EnemyDeath()
    {
        if (collisionObject.transform.position != null)
        {
            yield return new WaitForSeconds(0);
            Instantiate(explosion, collisionObject.transform.position, Quaternion.identity);
            Destroy(collisionObject.gameObject);
            StartCoroutine(MissileDeSpawn());
            
            playerScript.kills += 1;
        }

    }

    public IEnumerator MissileDeSpawn()
    {
        if (capsuleCollider != null)
        {
            capsuleCollider.enabled = false;
        }
        laser.SetActive(false);
        flame.SetActive(false);
        smoke.SetActive(false);
        
        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }


}
