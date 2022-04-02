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

    

    public class Data : DefData
    {
        private int _index;
        private string name;
        private int totalCount;
        private int giveCount;
        public override int Index => _index;
        public string Name => name;
        public int TotalCount => totalCount;
        public int GiveCount => giveCount;
        
        public override void Load(Dictionary<string, string> data)
        {
            CSVTableHelper.SetValue(ref _index, data["index"]);
            CSVTableHelper.SetValue(ref name, data["name"]);
            CSVTableHelper.SetValue(ref totalCount, data["totalCount"]);
            CSVTableHelper.SetValue(ref giveCount, data["giveCount"]);
        }
    }
}
