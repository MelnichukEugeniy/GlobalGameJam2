using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float _speed = 6.0f; // �������� ������
    public float _runSpeed = 12.0f; // �������� ���
    public float _jumpForce = 8.0f; // ���� �������
    public float _gravity = -30.0f; // ���������
    public float _crouchSpeed = 3.0f; // �������� ���������
    public float _proneSpeed = 1.5f; // �������� �������
    public float _transitionSpeed = 5.0f; // �������� �������� �������� �� �������

    private CharacterController _characterController;
    private float _verticalVelocity; // ����������� ��������
    private float _originalHeight; // ��������� ������ ���������
    private Vector3 _originalCenter; // ���������� ����� ���������
    private float _crouchHeight = 1.2f; // ������ ��������� ��� ��������
    private Vector3 _crouchCenter = new Vector3(0, 0.1f, 0); // ����� ��� ��������
    private float _proneHeight = 0.8f; // ������ ��������� ��� ������
    private Vector3 _proneCenter = new Vector3(0, 0.7f, 0); // ����� ��� ������

    private float _targetHeight; // ֳ����� ������ ���������
    private Vector3 _targetCenter; // ֳ������ ����� ���������
    private float _currentSpeed; // ������� �������� ���������

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        if (_characterController == null)
        {
            Debug.LogError("CharacterController is NULL");
            return;
        }
        _originalHeight = _characterController.height; // �������� ��������� ������
        _originalCenter = _characterController.center; // �������� ���������� �����
        _targetHeight = _originalHeight;
        _targetCenter = _originalCenter;
    }

    private void Update()
    {
        // ��������� ����
        if (Input.GetKey(KeyCode.C))
        {
            // �������
            _targetHeight = _proneHeight;
            _targetCenter = _proneCenter;
            _currentSpeed = _proneSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            // ���������
            _targetHeight = _crouchHeight;
            _targetCenter = _crouchCenter;
            _currentSpeed = _crouchSpeed;
        }
        else
        {
            // ��������� ����
            _targetHeight = _originalHeight;
            _targetCenter = _originalCenter;
            _currentSpeed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _speed;
        }

        // ������ ��������� ������ �� ������
        _characterController.height = Mathf.Lerp(_characterController.height, _targetHeight, Time.deltaTime * _transitionSpeed);
        _characterController.center = Vector3.Lerp(_characterController.center, _targetCenter, Time.deltaTime * _transitionSpeed);

        // ���
        float deltax = Input.GetAxis("Horizontal") * _currentSpeed;
        float deltaz = Input.GetAxis("Vertical") * _currentSpeed;

        Vector3 movement = new Vector3(deltax, 0, deltaz);

        // �������
        if (_characterController.isGrounded)
        {
            _verticalVelocity = -1f; // Գ����� �� ����

            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(_characterController.height - _originalHeight) < 0.1f)
            {
                // ������� �������� ����� � ���������� ����
                _verticalVelocity = _jumpForce;
            }
        }
        else
        {
            // ��������� �� ��� ������
            _verticalVelocity += _gravity * Time.deltaTime;
        }

        movement = Vector3.ClampMagnitude(movement, _currentSpeed);
        movement.y = _verticalVelocity; // ������ ������������ ���

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _characterController.Move(movement);
    }
}
