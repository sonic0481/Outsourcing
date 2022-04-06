using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DefQuestionTable : IDefTable
{
    private Dictionary<int, Data> _dicData = new Dictionary<int, Data>();
    private Dictionary<EVENT, List<Data>> _dicEventData = new Dictionary<EVENT, List<Data>>();
    

    public Dictionary<int, Data> DicData { get { return _dicData; } }
    
    public void SetData(List<Dictionary<string, string>> csvTable)
    {
        for (int i = 0; i < csvTable.Count; ++i)
        {
            Data data = new Data();
            data.Load(csvTable[i]);

            // index dictionary 
            if (_dicData.ContainsKey(data.Index))
            {
                Debug.LogErrorFormat("{0} Table {1} Index duplicated", GetType(), data.Index);
                continue;
            }

            if(false == _dicData.ContainsKey(data.Index))
                _dicData.Add(data.Index, data);
            if (false == _dicEventData.ContainsKey(data.EventType))
                _dicEventData.Add(data.EventType, new List<Data>());
            _dicEventData[data.EventType].Add(data);
            //_dicFileData.Add(data.ItemID.Trim().ToLower(), data);            
        }
    }

    public void AddData(Dictionary<string, string> csv)
    {
        Data data = new Data();
        data.Load(csv);

        if (_dicData.ContainsKey(data.Index))
        {
            Debug.LogErrorFormat("{0} Table {1} Index duplicated", GetType(), data.Index);
            return;
        }        

        _dicData.Add(data.Index, data);
    }
    
    public List<Data> GetDataByEvent(EVENT eventType)
    {
        if (false == _dicEventData.ContainsKey(eventType))
            return null;

        return _dicEventData[eventType];
    }

    public List<Data> GetDataByEvent_RandomQuestion(EVENT eventType, int randomCount = 3)
    {
        if (false == _dicEventData.ContainsKey(eventType))
            return null;

        List<Data> tempList = _dicEventData[eventType];
        List<Data> dataList = new List<Data>();

        for(int i = 0; i < randomCount; ++i)
        {
            int index = UnityEngine.Random.Range(0, tempList.Count);

            dataList.Add(tempList[index]);
            tempList.RemoveAt(index);
        }

        return dataList;
    }

    public class Data : DefData
    {
        private int         _index;
        private EVENT       eventType;
        private string      question;
        private string      answerList_1;
        private string      answerList_2;
        private string      answerList_3;
        private string      answerList_4;
        private ANSWER      answer;                

        public override int Index => _index;
        public EVENT EventType => eventType;
        public string Question => question;
        public string AnswerList_1 => answerList_1;
        public string AnswerList_2 => answerList_2;
        public string AnswerList_3 => answerList_3;
        public string AnswerList_4 => answerList_4;
        public ANSWER Answer => answer;

        public override void Load(Dictionary<string, string> data)
        {
            CSVTableHelper.SetValue(ref _index, data["index"]);
            CSVTableHelper.SetValue<EVENT>(ref eventType, data["type"]);
            CSVTableHelper.SetValue(ref question, data["question"]);
            CSVTableHelper.SetValue(ref answerList_1, data["answer_1"]);
            CSVTableHelper.SetValue(ref answerList_2, data["answer_2"]);
            CSVTableHelper.SetValue(ref answerList_3, data["answer_3"]);
            CSVTableHelper.SetValue(ref answerList_4, data["answer_4"]);
            CSVTableHelper.SetValue<ANSWER>(ref answer, data["answer"]);
        }
    }
}
