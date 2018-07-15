using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _camOffset = new Vector3(0, 0, -10);
    [Range(1f,10f)] [SerializeField] private float _smoothTimeX = 1f, _smoothTimeY = 1f, _smoothTimeZ = 1f;
    private Vector3 _velocity;
    public Camera Camera { get; private set; }

    void Awake()
    {
        Camera = GetComponent<Camera>();
        if (!Camera)
        {
            Debug.Log("Attach this component to a Camera object!");
        }
    }
    void FixedUpdate()
    {
        if (_target == null)
        {
            Debug.Log("There is no target to follow!");
        }else
        {
            float posX = Mathf.SmoothDamp(transform.position.x, _target.position.x + _camOffset.x, ref _velocity.x, 1/_smoothTimeX);
            float posY = Mathf.SmoothDamp(transform.position.y, _target.position.y + _camOffset.y, ref _velocity.y, 1/_smoothTimeY);
            float posZ = Mathf.SmoothDamp(transform.position.z, _target.position.z + _camOffset.z, ref _velocity.z, 1 / _smoothTimeZ);

            transform.position = (Vector3.right * posX) + (Vector3.up * posY)  + (Vector3.forward * posZ);
        }
    }

    public Transform Target() { return _target; }
    public Vector3 CamOffset() { return _camOffset; }
    public float SmoothTimeX() { return _smoothTimeX; }
    public float SmoothTimeY() { return _smoothTimeY; }
    public float SmoothTimeZ() { return _smoothTimeZ; }
}