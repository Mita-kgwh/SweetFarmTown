using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfiner : MonoBehaviour
{
    [SerializeField] CinemachineConfiner confiner;

    void Start()
    {
        UpdateBounds();
    }

    public void UpdateBounds()
    {
        GameObject gobj = GameObject.Find("CameraConfiner");
        if (gobj == null)
        {
            confiner.m_BoundingShape2D = null;
            return;
        }
        Collider2D bounds = gobj.GetComponent<Collider2D>();
        confiner.m_BoundingShape2D = bounds;
    }

    public void UpdateNewBounds(Collider2D collider2D)
    {
        confiner.m_BoundingShape2D = collider2D;
    }

}
