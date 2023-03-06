using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float _rotationSpeed = 5f;
    public float sensitivit = 100f;
    private Vector3 _lastMousePosition;
    public bool canRotate = true;

    void LateUpdate()
    {
        if (!canRotate)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {

            Vector3 delta = Input.mousePosition - _lastMousePosition;
            
            transform.Rotate(Vector3.up * ( - delta.x * _rotationSpeed * Time.deltaTime) / sensitivit , Space.World);
            _lastMousePosition = Input.mousePosition;
        }
    }
}
