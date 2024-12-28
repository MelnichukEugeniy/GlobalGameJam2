using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ϳ�������� SceneManager

public class LevelManager : MonoBehaviour
{
    // ����� ��� �������� �� ��������� �����
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // �������� ������ ������� �����
        int nextSceneIndex = currentSceneIndex + 1; // ��������� �����

        // ��������, �� � �������� �����
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // ����������� �������� �����
        }
        else
        {
            Debug.Log("�� ������� �����!"); // ���� ����� ����������
        }
    }

    // ����� ��� ����������� ��������� ����
    public void ReloadCurrentLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

  
}
