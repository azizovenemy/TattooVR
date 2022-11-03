using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Printer : MonoBehaviour
{
    public void OnSpawnExample(List<GameObject> examples)
    {
        int i = Random.Range(1, examples.Count);
        GameObject example = Instantiate(examples[i], gameObject.transform.position += new Vector3(0f, 0.3f, 0f), Quaternion.identity);
        example.AddComponent<Example>();
    }
}
