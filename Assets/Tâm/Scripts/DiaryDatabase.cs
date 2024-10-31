using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Diary
{
    public string diaryName;
    public Sprite diaryPicture;
    public string diaryLines;
}

public class DiaryDatabase : MonoBehaviour
{
    public List<Diary> diaryList;
    public Diary GetDiary(int diaryIndex)
    {
        return diaryList[diaryIndex];
    }
}
