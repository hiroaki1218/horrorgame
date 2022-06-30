using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))] 
public class CollisionDetector : MonoBehaviour
{

    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();

    private void OnTriggerStay(Collider other)
    {
        onTriggerStay.Invoke(other);
    }

    [Serializable] 
    public class TriggerEvent : UnityEvent<Collider>
    {
    }
}
