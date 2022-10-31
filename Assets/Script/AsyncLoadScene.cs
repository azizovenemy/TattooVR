using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncLoadScene : MonoBehaviour
{
    /// <summary>
    /// ������ �������� AsyncSceneLoad
    /// </summary>
    /// <param name="sceneIndex">������ ����� ����������� ��� ��������</param>
    public void OnLoad(int sceneIndex)
    {
        StartCoroutine(AsyncSceneLoad(sceneIndex));
    }

    /// <summary>
    /// ������������ �������� ��������� �����
    /// </summary>
    /// <param name="sceneIndex">��������� �����</param>
    /// <returns>���)</returns>
    private IEnumerator AsyncSceneLoad(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while(!operation.isDone)
        {
            yield return null;
        }
    }

    /// <summary>
    /// ����� �� ����������
    /// </summary>
    public void OnExitGame()
    {
        Application.Quit();
    }
}
