using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArm : MonoBehaviour
{
    [Header("Camera Controls")]
    [SerializeField]
    float _minXRotation = -45f;
    [SerializeField]
    float _maxXRotation = 60f;
    [SerializeField]
    [Range(0f, 1f)]
    float _sensitivity = 0.085f;

    [Header("Player Reference")]
    [SerializeField]
    Transform _player;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _player.Rotate(Vector3.up * mouseX * _sensitivity);

        float newRotationX = transform.eulerAngles.x - mouseY * _sensitivity;
        newRotationX = Mathf.Clamp(newRotationX, _minXRotation, _maxXRotation);

        transform.localRotation = Quaternion.Euler(newRotationX, transform.eulerAngles.y, 0f);
    }
}
