using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] Text targetText;
    [SerializeField] Text nameText;
    [SerializeField] Image portrait;

    DialogueContainer currentDialogue;
    int currentLine;

    [Range(0f, 1f)]
    [SerializeField] float visibleTextPercent;
    [SerializeField] float timePerLetter = 0.05f;
    float totalTimeToType, currtentTime;
    string lineToShow;
    public bool doneTalk;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PushText();
        }
        TypeOutText();
    }

    private void TypeOutText()
    {
        if (visibleTextPercent >= 1f)
        {
            return;
        }
        currtentTime += Time.deltaTime;
        visibleTextPercent = currtentTime / totalTimeToType;
        visibleTextPercent = Mathf.Clamp(visibleTextPercent, 0, 1f);
        UpdateText();
    }

    private void UpdateText()
    {
        int letterCount = (int)(lineToShow.Length * visibleTextPercent);
        targetText.text = lineToShow.Substring(0, letterCount);
    }

    private void PushText()
    {
        if (visibleTextPercent < 1f)
        {
            visibleTextPercent = 1f;
            UpdateText();
            return;
        }

        if (currentLine >= currentDialogue.lines.Count)
        {
            Conclude();
        }
        else
        {
            CycleLine();
        }
    }

    private void CycleLine()
    {
        lineToShow = currentDialogue.lines[currentLine];
        totalTimeToType = lineToShow.Length * timePerLetter;
        currtentTime = 0;
        visibleTextPercent = 0;
        targetText.text = "";

        currentLine += 1;
    }

    public void Initialize(DialogueContainer dialogueContainer)
    {
        DisableControls.Instance.ButtonInventory(false).ButtonCraft(false).DisableControl();
        Show(true);
        currentDialogue = dialogueContainer;
        currentLine = 0;
        CycleLine();
        UpdatePortrait();
    }

    private void UpdatePortrait()
    {
        portrait.sprite = currentDialogue.actor.portrait;
        nameText.text = currentDialogue.actor.actorName;
    }

    private void Show(bool isTalking)
    {
        gameObject.SetActive(isTalking);
    }

    private void Conclude()
    {
        Debug.Log("The Dialogue has ended.");
        doneTalk = true;
        Show(false);
        DisableControls.Instance.ButtonInventory(true).ButtonCraft(true).EnableControl();
    }
}
