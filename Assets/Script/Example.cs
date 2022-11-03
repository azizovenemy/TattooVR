using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Valve.VR.InteractionSystem.Interactable))]
[RequireComponent(typeof(Valve.VR.InteractionSystem.Throwable))]
[RequireComponent(typeof(BoxCollider))]
//[RequireComponent(typeof(Rigidbody))]
public class Example : MonoBehaviour
{
    //тут все удалилось(
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == 8)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
