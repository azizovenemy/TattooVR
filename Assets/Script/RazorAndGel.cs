using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Valve.VR.InteractionSystem.Interactable))]
[RequireComponent(typeof(Valve.VR.InteractionSystem.Throwable))]
[RequireComponent(typeof(BoxCollider))]
//[RequireComponent(typeof(Rigidbody))]
public class RazorAndGel : MonoBehaviour
{
    private float timeElapsed = 0;
    private float timeToStop = 10f;

    private void FixedUpdate()
    {
        if(timeElapsed > timeToStop)
        {
            TutorialManager.singleton.currentStage++;
            TutorialManager.singleton.OnChangeGameStage();
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == 8)
        {
            timeToStop += Time.deltaTime;
        }
    }
}
