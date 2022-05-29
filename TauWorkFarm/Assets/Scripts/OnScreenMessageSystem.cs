using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using System;

public class OnScreenMessage
{
    //public GameObject gobj;
    public float timeToLive;
    public MessagePopup messagePopup;

    //public OnScreenMessage(GameObject gobj)
    //{
    //    this.gobj = gobj;
    //}
    public OnScreenMessage(MessagePopup messagePopup)
    {
        this.messagePopup = messagePopup;
    }
}

public class OnScreenMessageSystem : MonoBehaviour
{
    [SerializeField] GameObject textPrefab;

    [SerializeField] List<OnScreenMessage> onScreenMessageList;
    [SerializeField] List<OnScreenMessage> openList;

    [SerializeField] float horizontalScatter = 0.5f;
    [SerializeField] float verticalScatter = 1f;

    private void Awake()
    {
        onScreenMessageList = new List<OnScreenMessage>();
        openList = new List<OnScreenMessage>();
    }

    private void Update()
    {
        for (int i = onScreenMessageList.Count - 1; i >= 0; i--)
        {
            //onScreenMessageList[i].timeToLive -= Time.deltaTime;
            //if (onScreenMessageList[i].timeToLive < 0)
            //{
            //    onScreenMessageList[i].gobj.SetActive(false);
            //    openList.Add(onScreenMessageList[i]);
            //    onScreenMessageList.RemoveAt(i);
            //}
            if (!onScreenMessageList[i].messagePopup.CheckActive())
            {
                openList.Add(onScreenMessageList[i]);
                onScreenMessageList.RemoveAt(i);
            }
        }
    }
    public Transform GetPrefab()
    {
        return textPrefab.transform;
    }

    public void PostMessage(Vector3 worldPosition, string message)
    {
        worldPosition.z -= 1f;
        worldPosition.x += Random.Range(-horizontalScatter, horizontalScatter);
        worldPosition.y += Random.Range(-verticalScatter, verticalScatter);

        if (openList.Count > 0)
        {
            Debug.Log("reuse");
            ReuseObjectFromOpenList(worldPosition, message);                                         
        }
        else
        {
            Debug.Log("create new");
            CreateNewOnScreenMessageObject(worldPosition, message);
        }
    }

    private void ReuseObjectFromOpenList(Vector3 worldPosition, string message)
    {
        OnScreenMessage osm = openList[0];
        //osm.gobj.SetActive(true);
        //osm.timeToLive = timeToLive;
        //osm.gobj.GetComponent<TextMeshPro>().text = message;
        //osm.gobj.transform.position = worldPosition;
        osm.messagePopup.Setup(message).SetupPosition(worldPosition);
        openList.RemoveAt(0);
        onScreenMessageList.Add(osm);
    }

    private void CreateNewOnScreenMessageObject(Vector3 worldPosition, string message)
    {
        MessagePopup messagePopup = MessagePopup.Create(worldPosition, message);

        //GameObject textGobj = Instantiate(textPrefab, transform);
        //textGobj.transform.position = worldPosition;


        //MessagePopup messagePopup = textGobj.GetComponent<MessagePopup>();
        messagePopup.Setup(message).transform.SetParent(transform);//.SetupParent(transform);
        //TextMeshPro tmp = textGobj.GetComponent<TextMeshPro>();
        //tmp.text = message;

        //OnScreenMessage onScreenMessage = new OnScreenMessage(textGobj);
        OnScreenMessage onScreenMessage = new OnScreenMessage(messagePopup);
        //onScreenMessage.timeToLive = timeToLive;
        onScreenMessageList.Add(onScreenMessage);
    }
}
