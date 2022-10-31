using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("����������� �����")]
    public TMP_Text titleText;
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
        _timeLeft = time;
        runningTime = 0;
        StartCoroutine(OnChangeTimerValue());
        StartCoroutine(OnTextAnimation());
    }
    /// <summary>
    /// ������ ��� ������ ��������
    /// </summary>
    /// <returns>���)</returns>
    private IEnumerator OnChangeTimerValue()
    {
        while (_timeLeft > 0)
        {
            float minutes = Mathf.FloorToInt(_timeLeft / 60);
            float seconds = Mathf.FloorToInt(_timeLeft % 60);
            timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
            _timeLeft -= Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// ������ ����������� ���������� ������ (���������) � ����������� �� ������
    /// </summary>
    /// <returns></returns>
    private IEnumerator OnTextAnimation()
    {
        while(runningTime < runningDuration) 
        { 
            runningTime += Time.deltaTime;

            float normalizeRuninngTime = runningTime / runningDuration;
            Color color = new Color(textTargetColor.r, textTargetColor.g, textTargetColor.b, textTargetColor.a * normalizeRuninngTime);
            titleText.GetComponent<TMP_Text>().color = color;

            yield return null;
        }

        yield return new WaitForSeconds(5);

        while(runningTime > 0)
        {
            runningTime -= Time.deltaTime;

            float normalizeRuninngTime = runningTime / (runningDuration / 2);
            Color color = new Color(textTargetColor.r, textTargetColor.g, textTargetColor.b, textTargetColor.a * normalizeRuninngTime);
            titleText.GetComponent<TMP_Text>().color = color;

            yield return null;
        }
    }
}
