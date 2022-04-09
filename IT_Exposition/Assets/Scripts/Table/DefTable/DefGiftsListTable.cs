using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefGiftsListTable : IDefTable
{
    private Dictionary<int, Data> _dicData = new Dictionary<int, Data>();

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
            
            _dicData.Add(data.Index, data);
            //_dicFileData.Add(data.ItemID.Trim().ToLower(), data);            
        }
    }

    public Data GetData(GIFTS gift)
    {
        int key = (int)gift;
        if (_dicData.ContainsKey(key))
            return _dicData[key];

        return null;
    }

    public IEnumerator<Data> GetDataList()
    {
        return _dicData.Values.GetEnumerator();
    }    

    public class Data : DefData
    {
        private int _index;
        private string name;
        private string fullName;
        private int win_0;
        private int win_1;
        private int win_2;
        private int win_3;
        public override int Index => _index;
        public string Name => name;
        public string FullName => fullName;
        public int Win_0 => win_0;
        public int Win_1 => win_1;
        public int Win_2 => win_2;
        public int Win_3 => win_3;
        
        public override void Load(Dictionary<string, string> data)
        {
            CSVTableHelper.SetValue(ref _index, data["index"]);
            CSVTableHelper.SetValue(ref name, data["name"]);
            CSVTableHelper.SetValue(ref fullName, data["fullName"]);
            CSVTableHelper.SetValue(ref win_0, data["win_0"]);
            CSVTableHelper.SetValue(ref win_1, data["win_1"]);
            CSVTableHelper.SetValue(ref win_2, data["win_2"]);
            CSVTableHelper.SetValue(ref win_3, data["win_3"]);            
        }        
    }
}
