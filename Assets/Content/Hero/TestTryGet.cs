using System;
using System.Collections;
using System.Collections.Generic;
using Components;
using UnityEngine;
using Views;

[RequireComponent(typeof(CollisionProvider))]
public class TestTryGet : MonoBehaviour
{
    private CollisionProvider _collisionProvider;   
    private void Awake()
    {
        _collisionProvider = GetComponent<CollisionProvider>();
        _collisionProvider.TriggerEnter += OnTriggerEnterCustom;
        _collisionProvider.CollisionEnter += OnCollisionEnterCustom;
    }

    private void OnCollisionEnterCustom(Collision other)
    {
        if (other.gameObject.TryGetComponent(out InteractableObject interactable))
        {
            Debug.Log("OnCollisionEnterCustom");
            interactable.Interact();
            
        }
    }

    private void OnTriggerEnterCustom(Collider other)
    {
        if (other.gameObject.TryGetComponent(out InteractableObject interactable))
        {
            Debug.Log("OnTriggerEnterCustom");
            interactable.Interact();
        }
    }
}
