using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _objectToFollow = null;

    Vector3 _objectOffset;
    private void Awake()
    {
        // create an offset bw this position and obj's position
        _objectOffset = this.transform.position - _objectToFollow.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        // apply the offset every frame, to reposition this obj
        this.transform.position = _objectToFollow.position + _objectOffset;
    }
}
