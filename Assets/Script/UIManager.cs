using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Всплывающие подсказки")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private float runningDuration;
    [SerializeField] private Color textTargetColor;

    private float runningTime;

    [Header("Таймер")]
    [SerializeField] private float time;
    [SerializeField] private TMP_Text timeText;

    private float _timeLeft;

    /// <summary>
    /// Запуск двух корутин отвечающих за отображения таймера и определенного заголовка
    /// </summary>
    private void Start()
    {
        runningTime = 0;
    }

    /// <summary>
    /// Таймер для режима экзамена
    /// </summary>
    /// <returns>нул)</returns>
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
    /// Анимированное отображение стартового текста в зависимости от режима или текста от подсказки
    /// </summary>
    /// <param name="text">текст который нужно анимировать</param>
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
    /// Отображение подсказки для режима - туториал
    /// </summary>
    /// <param name="text">передаваемый текст</param>
    public void OnShowTip(string text)
    {
        title.GetComponent<TMP_Text>().text = text;
        if(text == "Начало экзамена" || text == "Начало туториала")
            StartCoroutine(OnTextAnimation(title));
        else
            title.text = "Возьми " + text;
            StartCoroutine(OnTextAnimation(title));
    }
}
