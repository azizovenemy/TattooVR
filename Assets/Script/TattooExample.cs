using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TattooExample : MonoBehaviour
{
    [SerializeField] 
    private List<GameObject> examplesList;

    /// <summary>
    /// Ну тут спавн рандомного примера татуировки, пока так)
    /// </summary>
    public void OnSpawnExample()
    {
        int i = Random.Range(0, examplesList.Count);
        GameObject currentExample = Instantiate(examplesList[i] /*  тут где спавнить или чота на уровне */);

        currentExample.AddComponent<Valve.VR.InteractionSystem.Interactable>();
        currentExample.AddComponent<Valve.VR.InteractionSystem.Throwable>();
        currentExample.AddComponent<Collider>();

    }
}
