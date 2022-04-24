using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour
{
    [SerializeField] GameObject highlighter;

    GameObject curTarget;
    public void Highlight(GameObject target)
    {
        if (curTarget == target)
        {
            return;
        }
        curTarget = target;
        Vector3 position = target.transform.position;
        Highlight(position);
    }
    public void Highlight(Vector3 position)
    {
        highlighter.SetActive(true);
        highlighter.transform.position = position + Vector3.up * 0.5f;
    }

    public void Hide()
    {
        curTarget = null;   
        highlighter.SetActive(false);
    }

}
