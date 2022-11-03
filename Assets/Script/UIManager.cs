using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("����������� ���������")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private float runningDuration;
    [SerializeField] private Color textTargetColor;

    private float runningTime;

    [Header("������")]
    [SerializeField] private float time;
    [SerializeField] private TMP_Text timeText;

    private float _timeLeft;

    /// <summary>
    /// ������ ���� ������� ���������� �� ����������� ������� � ������������� ���������
    /// </summary>
    private void Start()
    {
        runningTime = 0;
    }

    /// <summary>
    /// ������ ��� ������ ��������
    /// </summary>
    /// <returns>���)</returns>
    public IEnumerator OnChangeTimerValue(float time)
    {
        _timeLeft = time;
        while (_timeLeft > 0)
        {
            float minutes = Mathf.FloorToInt(_timeLeft / 60);
            float seconds = Mathf.FloorToInt(_timeLeft % 60);
            timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
            _timeLeft -= Time.deltaTime;
            yield return null;
        }
        ExamManager.singleton.OnEndGame();
    }

    /// <summary>
    /// ������������� ����������� ���������� ������ � ����������� �� ������ ��� ������ �� ���������
    /// </summary>
    /// <param name="text">����� ������� ����� �����������</param>
    /// <returns></returns>
    private IEnumerator OnTextAnimation(TMP_Text text)
    {
        while(runningTime < runningDuration) 
        { 
            runningTime += Time.deltaTime;

            float normalizeRuninngTime = runningTime / (runningDuration / 10);
            Color color = new Color(textTargetColor.r, textTargetColor.g, textTargetColor.b, textTargetColor.a * normalizeRuninngTime);
            text.GetComponent<TMP_Text>().color = color;

            yield return null;
        }
        yield return new WaitForSeconds(5);
        while(runningTime > 0)
        {
            runningTime -= Time.deltaTime;

            float normalizeRuninngTime = runningTime / (runningDuration / 15);
            Color color = new Color(textTargetColor.r, textTargetColor.g, textTargetColor.b, textTargetColor.a * normalizeRuninngTime);
            text.GetComponent<TMP_Text>().color = color;

            yield return null;
        }

        if(TutorialManager.singleton.currentStage == 0)
            TutorialManager.singleton.currentStage++;
            TutorialManager.singleton.OnChangeGameStage();
    }

    /// <summary>
    /// ����������� ��������� ��� ������ - ��������
    /// </summary>
    /// <param name="text">������������ �����</param>
    public void OnShowTip(string text)
    {
        title.GetComponent<TMP_Text>().text = text;
        if(text == "������ ��������" || text == "������ ���������")
            StartCoroutine(OnTextAnimation(title));
        else
            title.text = "������ " + text;
            StartCoroutine(OnTextAnimation(title));
    }
}
