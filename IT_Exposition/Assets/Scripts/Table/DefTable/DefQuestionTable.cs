using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DefQuestionTable : IDefTable
{
    private Dictionary<int, Data> _dicData = new Dictionary<int, Data>();

    public int LastNextIndex = 1;

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

            if (data.Index >= LastNextIndex)
                LastNextIndex = data.Index + 1;
            _dicData.Add(data.Index, data);
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

        if (data.Index >= LastNextIndex)
            LastNextIndex = data.Index + 1;

        _dicData.Add(data.Index, data);
    }

    public void SetLastIndex()
    {
        while(_dicData.ContainsKey(LastNextIndex))
        {
            LastNextIndex++;                
        }
    }

    public class Data : DefData
    {
        private int _index;
        private string date;
        private string answer01;
        private string answer02;        
        
        private string age;

        private string receipt;
        private string gift;

        public override int Index => _index;
        public string Date => date;
        public string Answer01 => answer01;
        public string Answer02 => answer02;        

        public string Age => age;

        public string Receipt => receipt;
        public string Gift => gift;

        public override void Load(Dictionary<string, string> data)
        {
            CSVTableHelper.SetValue(ref _index, data["index"]);
            CSVTableHelper.SetValue(ref date, data["date"]);
            CSVTableHelper.SetValue(ref answer01, data["answer01"]);
            CSVTableHelper.SetValue(ref answer02, data["answer02"]);
            CSVTableHelper.SetValue(ref age, data["age"]);
            CSVTableHelper.SetValue(ref receipt, data["receipt"]);
            CSVTableHelper.SetValue(ref gift, data["gift"]);
        }
    }
}
