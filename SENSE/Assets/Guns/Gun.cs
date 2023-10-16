using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Gun : MonoBehaviour, IInteracatable
{
    private bool equipped = false;

    [SerializeField] private Camera cam;

    void Start()
    {

    }

    void Update()
    {

    }

    void Fire()
    {
        if (equipped)
        {
            Debug.Log("Fire");
        }
    }

    public void Interact(Transform interactor)
    {
        Debug.Log(interactor.name + " interacted");

        transform.parent = interactor;

        // move gun to character
        transform.localPosition = new Vector3(.5f, 0, 1);
        transform.localEulerAngles = Vector3.zero;

        equipped = true;
    }
}
