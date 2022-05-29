using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MessagePopup : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float slowdownSpeed = 8f;

    [SerializeField] float timeToLive = 3f;
    [SerializeField] float disapearSpeed = 1f;
    [SerializeField] float scaleAmount = 1f;
    private TextMeshPro textMesh;
    private float timer;
    private Color orgColor;
    private Color textColor;
    private Vector3 moveVector;
    
    public static MessagePopup Create(Vector3 position, string mess)
    {
        Transform messPopupTf = Instantiate(GamesManager.Instance.messageSystem.GetPrefab(), position, Quaternion.identity);
        MessagePopup messagePopup = messPopupTf.GetComponent<MessagePopup>();
        messagePopup.Setup(mess);

        return messagePopup;
    }

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        orgColor = textMesh.color;
    }
    public MessagePopup Setup(string mess)
    {
        textMesh.SetText(mess);
        textColor = orgColor;
        textMesh.color = orgColor;
        transform.localScale = Vector3.one;
        timer = timeToLive;
        gameObject.SetActive(true);
        moveVector = new Vector3(.7f, 1f) * moveSpeed;
        return this;
    }

    //public MessagePopup SetupMoveVector(Vector3 vector)
    //{
    //    moveVector = vector;
    //    return this;
    //}

    public void SetupPosition(Vector3 position)
    {
        transform.position = position;
    }

    public bool CheckActive()
    {
        return textColor.a > 0;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * slowdownSpeed * Time.deltaTime;
        //Debug.Log("active");
        if (timer > timeToLive*.5f)
        {
            transform.localScale += Vector3.one * scaleAmount * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * scaleAmount * Time.deltaTime;
        }

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            textColor.a -= disapearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
