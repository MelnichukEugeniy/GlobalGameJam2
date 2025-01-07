using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionTrigger : MonoBehaviour
{
    public AudioClip resetSound; // ����-��� ��� ����������� �����
    private AudioSource _audioSource;

    private Queue<Vector3> positionHistory = new Queue<Vector3>(); // ������ ������� ���������
    public float trackTime = 3f; // ��� � ��������, �� ���� ���������� ������ �������
    private float recordInterval = 0.1f; // �������� ��������� �����

    private void Start()
    {
        // ������ ��� ������������� �������� AudioSource
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        // �������� ���������� �������
        StartCoroutine(TrackPlayerPosition());
    }

    private IEnumerator TrackPlayerPosition()
    {
        while (true)
        {
            if (positionHistory.Count > trackTime / recordInterval)
            {
                positionHistory.Dequeue(); // ��������� ���� �������
            }

            // ������ ������� �������
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                positionHistory.Enqueue(GameObject.FindGameObjectWithTag("Player").transform.position);
            }

            yield return new WaitForSeconds(recordInterval); // ������ ����� ��������� �������
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ����������, �� ��������� ������� ��'��� �� ����� "Player"
        {
            CharacterController characterController = other.GetComponent<CharacterController>();

            // ����������� �����
            if (resetSound != null && _audioSource != null)
            {
                _audioSource.PlayOneShot(resetSound);
            }

            // ³���������� ��������� � ������� 3 ������� ����
            if (positionHistory.Count > 0)
            {
                Vector3 pastPosition = positionHistory.Peek();

                if (characterController != null)
                {
                    characterController.enabled = false; // ³�������� ���������
                    other.transform.position = pastPosition;
                    characterController.enabled = true; // �������� ���������
                }
                else
                {
                    other.transform.position = pastPosition; // ���� CharacterController �������
                }
            }
        }
    }
}
