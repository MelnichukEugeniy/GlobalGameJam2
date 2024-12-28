using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float _speed = 6.0f; // Швидкість ходьби
    public float _runSpeed = 12.0f; // Швидкість бігу
    public float _jumpForce = 8.0f; // Сила стрибка
    public float _gravity = -30.0f; // Гравітація
    public float _crouchSpeed = 3.0f; // Швидкість присідання
    public float _proneSpeed = 1.5f; // Швидкість лягання
    public float _transitionSpeed = 5.0f; // Швидкість плавного переходу між станами

    private CharacterController _characterController;
    private float _verticalVelocity; // Вертикальна швидкість
    private float _originalHeight; // Початкова висота персонажа
    private Vector3 _originalCenter; // Початковий центр персонажа
    private float _crouchHeight = 1.2f; // Висота персонажа при присіданні
    private Vector3 _crouchCenter = new Vector3(0, 0.1f, 0); // Центр при присіданні
    private float _proneHeight = 0.8f; // Висота персонажа при ляганні
    private Vector3 _proneCenter = new Vector3(0, 0.7f, 0); // Центр при ляганні

    private float _targetHeight; // Цільова висота персонажа
    private Vector3 _targetCenter; // Цільовий центр персонажа
    private float _currentSpeed; // Поточна швидкість персонажа

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        if (_characterController == null)
        {
            Debug.LogError("CharacterController is NULL");
            return;
        }
        _originalHeight = _characterController.height; // Зберігаємо початкову висоту
        _originalCenter = _characterController.center; // Зберігаємо початковий центр
        _targetHeight = _originalHeight;
        _targetCenter = _originalCenter;
    }

    private void Update()
    {
        // Визначаємо стан
        if (Input.GetKey(KeyCode.C))
        {
            // Лягання
            _targetHeight = _proneHeight;
            _targetCenter = _proneCenter;
            _currentSpeed = _proneSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            // Присідання
            _targetHeight = _crouchHeight;
            _targetCenter = _crouchCenter;
            _currentSpeed = _crouchSpeed;
        }
        else
        {
            // Звичайний стан
            _targetHeight = _originalHeight;
            _targetCenter = _originalCenter;
            _currentSpeed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _speed;
        }

        // Плавне оновлення висоти та центру
        _characterController.height = Mathf.Lerp(_characterController.height, _targetHeight, Time.deltaTime * _transitionSpeed);
        _characterController.center = Vector3.Lerp(_characterController.center, _targetCenter, Time.deltaTime * _transitionSpeed);

        // Рух
        float deltax = Input.GetAxis("Horizontal") * _currentSpeed;
        float deltaz = Input.GetAxis("Vertical") * _currentSpeed;

        Vector3 movement = new Vector3(deltax, 0, deltaz);

        // Стрибок
        if (_characterController.isGrounded)
        {
            _verticalVelocity = -1f; // Фіксуємо на землі

            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(_characterController.height - _originalHeight) < 0.1f)
            {
                // Стрибок можливий тільки у звичайному стані
                _verticalVelocity = _jumpForce;
            }
        }
        else
        {
            // Гравітація під час падіння
            _verticalVelocity += _gravity * Time.deltaTime;
        }

        movement = Vector3.ClampMagnitude(movement, _currentSpeed);
        movement.y = _verticalVelocity; // Додаємо вертикальний рух

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _characterController.Move(movement);
    }
}
