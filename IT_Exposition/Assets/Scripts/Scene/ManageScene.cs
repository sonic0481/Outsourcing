using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageScene : SceneBase
{
    [SerializeField] Button _homeBtn;

    [SerializeField] Transform _giftContent;
    [SerializeField] GameObject _itemPrefab;

    [Header("Gifts/SELECT")]
    [SerializeField] InputField _indexField;
    [SerializeField] Button _selectBtn;

    [SerializeField] InputField _totalField;
    [SerializeField] InputField _giveField;
    [SerializeField] Button _saveBtn;

    Dictionary<GIFTS, GiftItem> _dicGifts = new Dictionary<GIFTS, GiftItem>();
    GIFTS _currentSelectGift = GIFTS.NONE;
    string _strIndexField = string.Empty;
    string _strTotalField = string.Empty;
    string _strGiveField = string.Empty;

    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);

        _homeBtn.onClick.AddListener(() => {
            _sceneMgr.ForceScene(SCENE.TITLE);
        });

        var table = CSVTableManager.Instance.GetTable<DefGiftsListTable>();
        var dataEnumerator = table.GetDataList();

        while (dataEnumerator.MoveNext())
        {
            var data = dataEnumerator.Current;
            GIFTS key = (GIFTS)data.Index;

            GiftItem giftItem;

            if (false == _dicGifts.TryGetValue(key, out giftItem))
            {
                GameObject obj = Instantiate(_itemPrefab, _giftContent);
                giftItem = obj.GetComponent<GiftItem>();
                giftItem.SetGift(key);
                obj.SetActive(true);

                _dicGifts.Add(key, giftItem);
            }
            else
            {
                giftItem.SetGift(key);
                giftItem.transform.SetParent(_giftContent);
                giftItem.gameObject.SetActive(true);
            }            
        }

        _indexField.onValueChanged.AddListener(IndexValueChanged);
        _selectBtn.onClick.AddListener(OnClickSelect);

        _totalField.onValueChanged.AddListener(TotalValueChanged);
        _giveField.onValueChanged.AddListener(GiveValueChanged);
        _saveBtn.onClick.AddListener(OnClickUpdate);
    }

    public override void Init()
    {
        
    }

    public override void On()
    {
        _currentSelectGift = GIFTS.NONE;
        _indexField.text = string.Empty;
        _totalField.text = string.Empty;
        _giveField.text = string.Empty;

        _strIndexField = string.Empty;
        _strTotalField = string.Empty;
        _strGiveField = string.Empty;

        _totalField.interactable = false;
        _giveField.interactable = false;
        _totalField.text = string.Empty;
        _giveField.text = string.Empty;

        UpdateGiftsData();
    }

    #region Gifts Method
    private void UpdateGiftsData()
    {
        foreach (var iter in _dicGifts)
        {
            iter.Value.SetGift(iter.Key);
        }
    }

    private void IndexValueChanged(string strIndex)
    {
        _strIndexField = strIndex;
    }

    private void OnClickSelect()
    {
        if (string.IsNullOrEmpty(_strIndexField))
            return;

        int index = System.Convert.ToInt32(_strIndexField);
        int giftsIndex = index - 1;

        if (0 > giftsIndex || GIFTS.END <= (GIFTS)giftsIndex)
        {
            return;
        }

        _currentSelectGift = (GIFTS)giftsIndex;

        _totalField.interactable = true;
        _giveField.interactable = true;
        _totalField.text = DataManager.Instance.GiftsData.Get_Total(_currentSelectGift).ToString();
        _giveField.text = DataManager.Instance.GiftsData.Get_Give(_currentSelectGift).ToString();
    }

    private void TotalValueChanged(string strTotal)
    {
        _strTotalField = strTotal;
    }

    private void GiveValueChanged(string strGive)
    {
        _strGiveField = strGive;
    }

    private void OnClickUpdate()
    {
        if (string.IsNullOrEmpty(_strTotalField) || string.IsNullOrEmpty(_strGiveField))
            return;

        int total = System.Convert.ToInt32(_strTotalField);
        int give = System.Convert.ToInt32(_strGiveField);

        DataManager.Instance.GiftsData.Update_Total(_currentSelectGift, total);
        DataManager.Instance.GiftsData.Update_Give(_currentSelectGift, give);

        On();
    }
    #endregion
}
