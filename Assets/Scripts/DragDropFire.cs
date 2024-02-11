using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropFire : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;
    [SerializeField] LayerMask pickUpLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private bool isGrabbing;


    private ObjectGrabbable objectGrabbable;
    private float pickUpDistance = 4f;
    private RaycastHit raycastHit;

    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject drop;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject grab;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cameraPosition.position, cameraPosition.forward,out raycastHit, pickUpDistance, pickUpLayerMask))
        {
            indicator.SetActive(true);
            if (isGrabbing)
            {
                drop.SetActive(true);
                fire.SetActive(true);
                grab.SetActive(false);

            }
            else
            {
                fire.SetActive(false);
                grab.SetActive(true);
                drop.SetActive(false);
            }
        }
        else if (!Physics.Raycast(cameraPosition.position, cameraPosition.forward, out raycastHit, pickUpDistance, pickUpLayerMask)&& !isGrabbing)
        {
            indicator.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (objectGrabbable == null)
            {
                if (Physics.Raycast(cameraPosition.position, cameraPosition.forward, out raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                        isGrabbing = true;
                    }
                }
            }
            else
            {
                objectGrabbable.Drop();
                objectGrabbable = null;
                isGrabbing = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isGrabbing && objectGrabbable != null)
            {
                objectGrabbable.Fired();
                isGrabbing = false;

            }
        }
    }
}
