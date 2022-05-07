using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZDepth : MonoBehaviour
{
    Transform trans;
    [SerializeField] bool stationary = true; // if obj dont change position,
                                             // so we dont need this zdept cucalate everyframe
                                             // just caculate 1 first frame and true to destroy

    void Start()
    {
        trans = transform;
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.z = pos.y * 0.0001f;
        transform.position = pos;

        if (stationary)
        {
            Destroy(this);
        }
    }
}
