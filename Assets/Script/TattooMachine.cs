using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Valve.VR.InteractionSystem.Throwable))]
[RequireComponent(typeof(Valve.VR.InteractionSystem.Interactable))]
[RequireComponent(typeof(BoxCollider))]
//[RequireComponent(typeof(Rigidbody))]
public class TattooMachine : MonoBehaviour
{
    private Example example;

    private void Start()
    {
        example = FindObjectOfType<Example>();
    }

    private void Update()
    {
       OnRazorRaycast();
    }

    private IEnumerator OnRazorRaycast() //здесь IEnumerator только для того чтобы была какая-то задержка потому что рисовалка не работает(
    {
        RaycastHit hit;
        Ray ray = new Ray(gameObject.transform.GetChild(0).transform.position, transform.forward);
        LayerMask layerMask = LayerMask.GetMask("Player");
        Debug.DrawRay(gameObject.transform.GetChild(0).transform.position, transform.forward, Color.green, 100f);

        if (!Physics.Raycast(ray, out hit, 100f, layerMask))
            yield return null;

        Renderer render = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        //if (render == null || render.sharedMaterial == null || render.sharedMaterial.mainTexture == null || meshCollider == null)
        //    return;

        yield return new WaitForSeconds(5);
        TutorialManager.singleton.currentStage++;
        TutorialManager.singleton.OnChangeGameStage();
        Destroy(gameObject);
        Destroy(example);
        OnDrawingTexture(render, hit);
    }

    private void OnDrawingTexture(Renderer render, RaycastHit hit)
    {
        //if (!SteamVR_Actions._default.GrabGrip.GetStateDown(SteamVR_Input_Sources.Any))
        //    return;

        Texture2D texture = render.sharedMaterial.mainTexture as Texture2D;
        Vector2 pixelsUV = hit.textureCoord;

        pixelsUV.x *= texture.width;
        pixelsUV.y *= texture.height;

        texture.filterMode = FilterMode.Bilinear;
        texture.wrapMode = TextureWrapMode.Clamp;

        texture.SetPixel((int)pixelsUV.x, (int)pixelsUV.y, Color.black);
        texture.Apply();
    }
}
