using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScene : SceneBase
{
    [SerializeField] Button         acceptBtn;
    [SerializeField] Text           rewardText;
    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);

        acceptBtn.onClick.AddListener(OnNext);
    }

    public override void Init()
    {

    }

    public override void On()
    {
        DefGiftsListTable giftTable = CSVTableManager.Instance.GetTable<DefGiftsListTable>();
        GIFTS reward = DataManager.Instance.GiftsData.WinningGift;
        var data = giftTable.GetData(reward);
        rewardText.text = data?.FullName ?? "";
    }

    protected override void OnNext()
    {
        _sceneMgr.FinishUser();   
    }
}
