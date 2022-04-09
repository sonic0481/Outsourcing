using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftItem : MonoBehaviour
{
    [SerializeField] Text _no;
    [SerializeField] Text _name;
    [SerializeField] Text _allCount;
    [SerializeField] Text _giveCount;
    [SerializeField] Text _remainCount;

    public void SetGift(GIFTS gift)
    {
        GiftsData data = DataManager.Instance.GiftsData;

        _no.text = ((int)gift + 1).ToString();
        _name.text = data.GetName(gift);
        _allCount.text = data.Get_Total(gift).ToString();
        _giveCount.text = data.Get_Give(gift).ToString();
        _remainCount.text = data.Get_Remain(gift).ToString();
    }
}
