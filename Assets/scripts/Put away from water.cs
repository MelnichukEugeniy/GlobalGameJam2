using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionTrigger : MonoBehaviour
{
    public AudioClip resetSound; // Аудіо-кліп для програвання звуку
    private AudioSource _audioSource;

    private Queue<Vector3> positionHistory = new Queue<Vector3>(); // Історія позицій персонажа
    public float trackTime = 3f; // Час у секундах, на який зберігається історія позицій
    private float recordInterval = 0.1f; // Інтервал оновлення історії

    private void Start()
    {
        // Додаємо або використовуємо існуючий AudioSource
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Починаємо збереження позиції
        StartCoroutine(TrackPlayerPosition());
    }

    private IEnumerator TrackPlayerPosition()
    {
        while (true)
        {
            if (positionHistory.Count > trackTime / recordInterval)
            {
                positionHistory.Dequeue(); // Видаляємо старі позиції
            }

            // Додаємо поточну позицію
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                positionHistory.Enqueue(GameObject.FindGameObjectWithTag("Player").transform.position);
            }

            yield return new WaitForSeconds(recordInterval); // Чекаємо перед наступним записом
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Перевіряємо, чи торкнувся тригера об'єкт із тегом "Player"
        {
            CharacterController characterController = other.GetComponent<CharacterController>();

            // Програвання звуку
            if (resetSound != null && _audioSource != null)
            {
                _audioSource.PlayOneShot(resetSound);
            }

            // Відправляємо персонажа в позицію 3 секунди тому
            if (positionHistory.Count > 0)
            {
                Vector3 pastPosition = positionHistory.Peek();

                if (characterController != null)
                {
                    characterController.enabled = false; // Відключаємо контролер
                    other.transform.position = pastPosition;
                    characterController.enabled = true; // Включаємо контролер
                }
                else
                {
                    other.transform.position = pastPosition; // Якщо CharacterController відсутній
                }
            }
        }
    }
}
