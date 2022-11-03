using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public int currentStage = 0;
    public static TutorialManager singleton { get; private set; }

    [SerializeField] private GameObject tattooMachinePrefab;
    [SerializeField] private GameObject printerPrefab;
    [SerializeField] private GameObject razorPrefab;
    [SerializeField] private GameObject gelPrefab;
    [SerializeField] private List<GameObject> examplePrefabs;
    [SerializeField] private Vector3 spawnPoint;

    private UIManager UIManager;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
        UIManager.OnShowTip("Начало туториала");
        currentStage = 0;
    }

    public void OnChangeGameStage()
    {
        switch (currentStage)
        {
            case 1:
                UIManager.OnShowTip("бритву");
                GameObject razor = Instantiate(razorPrefab, spawnPoint, Quaternion.identity);
                razor.AddComponent<RazorAndGel>();
                break;
            case 2:
                UIManager.OnShowTip("гель");
                GameObject gel = Instantiate(gelPrefab, spawnPoint, Quaternion.identity);
                gel.AddComponent<RazorAndGel>();
                break;
            case 3:
                UIManager.OnShowTip("эскиз татуировки из принтера");
                GameObject printer = Instantiate(printerPrefab, spawnPoint, Quaternion.identity);
                printer.AddComponent<Printer>();
                printer.GetComponent<Printer>().OnSpawnExample(examplePrefabs);
                break;
            case 4:
                UIManager.OnShowTip("тату машинку");
                GameObject tattooMachine = Instantiate(tattooMachinePrefab, spawnPoint, Quaternion.identity);
                tattooMachine.AddComponent<TattooMachine>();
                break;
        }
    }
}
