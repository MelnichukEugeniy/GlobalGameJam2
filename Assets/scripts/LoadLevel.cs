using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Підключаємо SceneManager

public class LevelManager : MonoBehaviour
{
    // Метод для переходу на наступний рівень
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Отримуємо індекс поточної сцени
        int nextSceneIndex = currentSceneIndex + 1; // Наступний рівень

        // Перевірка, чи є наступна сцена
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Завантажуємо наступну сцену
        }
        else
        {
            Debug.Log("Це останній рівень!"); // Якщо сцени закінчились
        }
    }

    // Метод для перезапуску поточного рівня
    public void ReloadCurrentLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

  
}
