using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class CSVReader
{
    public static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    public static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    public static char[] TRIM_CHARS = { '\"' };

    public static void Read(string file, System.Action<List<Dictionary<string, string>>> callback, Action onFailed)
    {
        var list = new List<Dictionary<string, string>>();

        //var asset = Resources.Load<TextAsset>(file);
        if(false == File.Exists(file))
        {
            onFailed?.Invoke();
            return;
        }

        var asset = File.ReadAllText(file);      
        
        if(null == asset)
        {
            onFailed?.Invoke();
            return;
        }

        var lines = Regex.Split(asset, LINE_SPLIT_RE);

        //if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 2; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, string>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

                value = value.Replace("<br>", "\n"); // 추가된 부분. 개행문자를 \n대신 <br>로 사용한다.
                value = value.Replace("<c>", ",");
                value = value.Replace("NONE", string.Empty);

                //object finalvalue = value;
                //int n;
                //float f;
                //if (int.TryParse(value, out n))
                //{
                //    finalvalue = n;
                //}
                //else if (float.TryParse(value, out f))
                //{
                //    finalvalue = f;
                //}
                entry[header[j]] = value; //finalvalue;
            }
            list.Add(entry);
        }

        callback(list);


            //Addressables.LoadAssetsAsync<TextAsset>(file,
            //(asset) =>
            //{
            //    if (asset == null)
            //    {
            //        onFailed();
            //        return;
            //    }

            //    var lines = Regex.Split(asset.text, LINE_SPLIT_RE);

            //    //if (lines.Length <= 1) return list;

            //    var header = Regex.Split(lines[0], SPLIT_RE);
            //    for (var i = 1; i < lines.Length; i++)
            //    {
            //        var values = Regex.Split(lines[i], SPLIT_RE);
            //        if (values.Length == 0 || values[0] == "") continue;

            //        var entry = new Dictionary<string, string>();
            //        for (var j = 0; j < header.Length && j < values.Length; j++)
            //        {
            //            string value = values[j];
            //            value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

            //            value = value.Replace("<br>", "\n"); // 추가된 부분. 개행문자를 \n대신 <br>로 사용한다.
            //            value = value.Replace("<c>", ",");
            //            value = value.Replace("NONE", string.Empty);

            //            //object finalvalue = value;
            //            //int n;
            //            //float f;
            //            //if (int.TryParse(value, out n))
            //            //{
            //            //    finalvalue = n;
            //            //}
            //            //else if (float.TryParse(value, out f))
            //            //{
            //            //    finalvalue = f;
            //            //}
            //            entry[header[j]] = value; //finalvalue;
            //        }
            //        list.Add(entry);
            //    }

            //    callback(list);
            //});
        //string filePath = string.Format("{0}/Table/{1}.csv", Application.streamingAssetsPath, file);

        //var sr = new StreamReader(filePath);
        //string data = sr.ReadToEnd();
        //sr.Close();
        //TextAsset data = Resources.Load(filePath) as TextAsset;


        //return list;
    }

    public static List<string> ReadStringFilter(string file)
    {
        var list = new List<string>();
        string filePath = string.Format("Filter/{0}", file);
        TextAsset data = Resources.Load(filePath) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);
        if (lines.Length < 1) return list;

        for (int i = 0; i < lines.Length; ++i)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0) continue;

            for (int j = 0; j < values.Length; ++j)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

                list.Add(value);
            }
        }

        return list;
    }
}
