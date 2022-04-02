using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDefTable
{
    void SetData(List<Dictionary<string, string>> csvTable);
}

public abstract class DefData
{
    public abstract int Index { get; }
    public abstract void Load(Dictionary<string, string> data);
}
