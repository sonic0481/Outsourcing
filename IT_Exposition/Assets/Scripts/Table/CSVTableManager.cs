using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

public class CSVTableManager : MonoSingleton<CSVTableManager>
{
    private Dictionary<Type, object> _dicTable = new Dictionary<Type, object>();

    private DefQuestionTable _questionTable;
    public DefQuestionTable QuestionTable => _questionTable;

    private DefGiftsListTable _giftsListTable;
    public DefGiftsListTable GiftsListTable => _giftsListTable;


    private int checkCount = 1;
    private int loadCount = 0;
    public bool IsTableLoadClear { get { return checkCount <= loadCount; } }

    protected CSVTableManager() { }

    public void LoadTable<T>(string path, Action loadTableCallback = null) where T : new()
    {

        T t = new T();
        string fullPath = $"{PersistentDataPath}/{path}";//$"{StreamingAssetsPath}{path}";

        CSVReader.Read(fullPath, (data) => 
        {
            IDefTable table = (IDefTable)t;
            table.SetData(data);

            if (_dicTable.ContainsKey(typeof(T)))
            {
                loadCount--;
                _dicTable.Remove(typeof(T));
            }

            _dicTable.Add(typeof(T), t);
            loadCount++;

            if (IsTableLoadClear)
            {
                loadTableCallback?.Invoke();
            }                
        }, () => {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("index,date,age,answer01,answer02,gift,receipt");
            sb.AppendLine("번호,날짜,나이,Q1,Q2,수령경품,수령여부");

            //StreamWriter outStream =  File.CreateText(fullPath);            
            Stream fs = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write);
            StreamWriter outStream = new StreamWriter(fs, Encoding.UTF8);
            
            outStream.Write(sb);
            outStream.Close();

            _dicTable.Add(typeof(T), t);
            loadCount++;
            if (IsTableLoadClear)
            {
                loadTableCallback?.Invoke();
            }
        });
    }    

    public void InitTable(Action callback = null)
    {
        loadCount = 0;
        
        LoadTable<DefQuestionTable>("QuestionTable.csv", callback);
    }

    //private void SetTableData<T>(string path) where T : new()
    //{
        
    //}

    ///테이블 가져오기
    public T GetTable<T>()
    {
        if (_dicTable.ContainsKey(typeof(T)))
        {
            return (T)_dicTable[typeof(T)];
        }

        return default;
    }

    public void AddTableData()
    {
        CSVExporter.QuestionTableWrite($"{PersistentDataPath}/QuestionTable.csv");
    }


    public string StreamingAssetsPath
    {
        get
        {
#if UNITY_EDITOR
            return $"{Application.dataPath}/StreamingAssets/";
#elif UNITY_ANDROID
            return $"jar:file://{Application.dataPath}!/assets/";
#elif UNITY_IOS
            return $"{Application.dataPath}/Raw/";
#else
            return $"{Application.dataPath}/StreamingAssets/";
#endif
        }
    }

    public string PersistentDataPath
    {
        get
        {
            return Application.persistentDataPath;
        }
    }
}
