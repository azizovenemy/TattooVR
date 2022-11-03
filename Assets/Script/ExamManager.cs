using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Callbacks;
using UnityEngine;

public class ExamManager : MonoBehaviour
{
    public static ExamManager singleton { get; private set; }

    [Header("Время для таймера")]
    [SerializeField]
    [Range(30f, 180f)]
    private float timeToReach;
    [Header("Префабы")]
    [SerializeField] 
    private Vector3 spawnPoint;
    [SerializeField]
    private List<GameObject> examplesList;
    [SerializeField] private GameObject tattooMachinePrefab;
    [SerializeField] private GameObject printerPrefab;
    [SerializeField] private GameObject razorPrefab;
    [SerializeField] private GameObject gelPrefab;

    private UIManager UIManager;

    private void Awake()
    {
        singleton = this;
    }

    private IEnumerator Start()
    {
        UIManager = FindObjectOfType<UIManager>();
        Instantiate(razorPrefab, spawnPoint, Quaternion.identity).AddComponent<RazorAndGel>();
        Instantiate(gelPrefab, spawnPoint, Quaternion.identity).AddComponent<RazorAndGel>();
        Instantiate(tattooMachinePrefab, spawnPoint, Quaternion.identity).AddComponent<TattooMachine>();
        GameObject printer = Instantiate(printerPrefab, spawnPoint, Quaternion.identity);
        printer.AddComponent<TattooMachine>();
        printer.GetComponent<Printer>().OnSpawnExample(examplesList);
        UIManager.OnShowTip("Начало экзамена");
        yield return new WaitForSeconds(5);
        StartCoroutine(UIManager.OnChangeTimerValue(timeToReach));
    }

    /// <summary>
    /// Ну выиграть нельзя
    /// </summary>
    public void OnEndGame()
    {
        print("lose");
    }
}
