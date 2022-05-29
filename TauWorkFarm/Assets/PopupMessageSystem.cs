using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMessageSystem : MonoBehaviour
{
    [SerializeField] Transform pfMessagePopup;

    private static PopupMessageSystem instance;
    public static PopupMessageSystem Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PopupMessageSystem>();
            return instance;
        }
    }

    public Transform GetPfMessagePopup()
    {
        return pfMessagePopup;
    }
}
