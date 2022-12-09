using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("UI組件")]
    public Text textLabel;
    public Image dialogBackground;
    [Header("劇本文件")]
    public TextAsset textFile;
    public int index;
    public float delay;

    bool textFinished;
    public bool isStopped;

    List<string> textList = new List<string>();

    void Start()
    {
        GetText(textFile);
        index = 0;
        textFinished = true;
        isStopped = false;
    }

    void Update()
    {
        if (textFinished && !isStopped)
        {
            StartCoroutine(SetTextUI());
        }
    }

    void GetText(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineDate = file.text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }

    public IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";

        switch (textList[index])
        {
            case "a"://奶奶說話
                textLabel.color = new Color32(235, 235, 235, 255);
                isStopped = false;
                index++;
                break;
            case "b"://劉昕說話
                textLabel.color = new Color32(20, 20, 20, 255);
                isStopped = false;
                index++;
                break;
            case "t"://等吃到線索
                textLabel.text = textList[index - 1];
                isStopped = true;
                break;
        }

        for (int i = 0; i < textList[index].Length; i++)
        {
            if (textList[index] != "t")
            {
                textLabel.text += textList[index][i];
            }
            switch (textList[index][i])
            {
                case '，':
                    delay = 0.5f;
                    break;
                case '？':
                    delay = 0.8f;
                    break;
                case '。':
                    delay = 0.8f;
                    break;
                case '！':
                    delay = 0.8f;
                    break;
                case '.':
                    delay = 0.3f;
                    break;
                default:
                    delay = 0.1f;
                    break;
            }

            if (textList[index].Length == i)
            {
                if (textList[index + 1] == "t")
                {
                    delay = 0;
                }
                else
                {
                    delay = 12f;
                }
            }
            yield return new WaitForSeconds(delay);
        }
        textFinished = true;
        index++;
    }
}