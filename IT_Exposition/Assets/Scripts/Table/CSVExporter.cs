using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVExporter
{
    /*
    public static void QuestionTableWrite(string path)
    {
        //List<string> dataList = new List<string>();

        Dictionary<string, string> dicData = new Dictionary<string, string>();

        CSVTableManager.Instance.GetTable<DefQuestionTable>().SetLastIndex();
        dicData.Add("index", CSVTableManager.Instance.GetTable<DefQuestionTable>().LastNextIndex.ToString());
        dicData.Add("date", DateTime.Today.ToString("yyMMdd"));        
        
        dicData.Add("age", DataManager.Instance.QuestionData.GetAgeText());

        for (QUESTION q = QUESTION.Q1; q < QUESTION.Q_END; ++q)
        {
            string text = DataManager.Instance.QuestionData.GetAnswerText(q);

            dicData.Add(string.Format("answer{0:D2}", (int)q + 1), text);
        }

        dicData.Add("gift", DataManager.Instance.GiftsData.GetUserGiftText());
        dicData.Add("receipt", DataManager.Instance.GiftsData.GetUserReceiptText());        

        string delimiter = ",";
        StringBuilder sb = new StringBuilder();

        sb.Append(string.Join(delimiter, dicData.Values));

        Stream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
        StreamWriter outStream = new StreamWriter(fs, Encoding.UTF8);//File.AppendText(path);
        outStream.WriteLine(sb);
        outStream.Close();

        CSVTableManager.Instance.GetTable<DefQuestionTable>().AddData(dicData);
    }
    */
    
    public static void UpdateGiftsData(int index)
    {
        
    }
}
