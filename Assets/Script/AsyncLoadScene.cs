using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncLoadScene : MonoBehaviour
{
    /// <summary>
    /// Запуск корутины AsyncSceneLoad
    /// </summary>
    /// <param name="sceneIndex">индекс сцены необходимой для загрузки</param>
    public void OnLoad(int sceneIndex)
    {
        StartCoroutine(AsyncSceneLoad(sceneIndex));
    }

    /// <summary>
    /// Ассинхронная загрузка выбранной сцены
    /// </summary>
    /// <param name="sceneIndex">выбранная сцена</param>
    /// <returns>нул)</returns>
    private IEnumerator AsyncSceneLoad(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while(!operation.isDone)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Выход из приложения
    /// </summary>
    public void OnExitGame()
    {
        Application.Quit();
    }
}
