using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray interactRay = new Ray(playerCamera.position, playerCamera.forward);
            Debug.DrawRay(interactRay.origin, interactRay.direction);
            if (Physics.Raycast(interactRay, out RaycastHit hit))
            {
                var interactable = hit.transform.GetComponent<IInteracatable>();
                interactable.Interact();

                Debug.Log(hit.transform.name);
            }
        }
    }
}
