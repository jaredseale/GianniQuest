using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class DialogueFetcher : MonoBehaviour
{
    [SerializeField] TextAsset textFile;

    ArrayList tempArrayList = new ArrayList();
    string[] dialogueLines;
    string key = null;
    public Dictionary<string, string[]> dialogueDict;

    private void Start() {
        CreateDictionary();
        Debug.Log(dialogueDict);
    }

    public void CreateDictionary() {
        string fileString = textFile.text;
        Debug.Log(fileString);
        string[] fileLines = Regex.Split(fileString, "\n|\r|\r\n");
        Debug.Log("filelines = " + fileLines);

        for (int i = 0; i < fileLines.Length; i++) {

            if (fileLines[i].Contains("@")) {

                if (tempArrayList.Count > 0) {

                    dialogueLines = (string[])tempArrayList.ToArray(typeof(string));
                    dialogueDict.Add(key, dialogueLines);
                    tempArrayList.Clear();
                }

                key = fileLines[i].Trim('@');
            } else {

                tempArrayList.Add(fileLines[i]);
            }
        }

    }


}
