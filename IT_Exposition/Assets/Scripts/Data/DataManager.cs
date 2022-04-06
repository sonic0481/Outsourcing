using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private QuestionData _questionData;
    private GiftsData _giftsData;    

    public QuestionData QuestionData { get { return _questionData; } }
    public GiftsData GiftsData { get { return _giftsData; } }    

    protected DataManager() { }

   
    public void InitializeData()
    {
        if (null == _questionData)
            _questionData = new QuestionData();
        _questionData.AwakeInit();

        if (null == _giftsData)
            _giftsData = new GiftsData();
        _giftsData.AwakeInit();
    }
    

    public void ResetData()
    {
        _questionData.OnInit();
        _giftsData.OnInit();
    }
}
