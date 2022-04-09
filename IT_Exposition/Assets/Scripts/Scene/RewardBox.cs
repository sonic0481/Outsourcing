using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RewardBox : MonoBehaviour
{
    public enum RewardState
    {
        BASE, FLIP, TURNED, ROCK
    }

    [SerializeField] Image          beforeCard;
    [SerializeField] Image          afterCard;
    [SerializeField] RectTransform  rc_beforeCard;
    [SerializeField] RectTransform  rc_afterCard;

    [SerializeField] Text           reward_Text;

    [SerializeField] Button         giftBtn;

    System.Action                   flipCallback;
    System.Action                   completeCallback;
    System.Action<int>              clickCallback;
    int                             index;

    DefGiftsListTable               giftTable;

    public void Init(int _index, System.Action c_callback, System.Action f_callback, System.Action<int> click_callback)
    {
        giftTable = CSVTableManager.Instance.GetTable<DefGiftsListTable>();

        index = _index;

        completeCallback = c_callback;
        flipCallback = f_callback;
        clickCallback = click_callback;

        giftBtn.onClick.AddListener(() => {            
            clickCallback?.Invoke(index);
            SetState(RewardState.FLIP);
        });        
    }

    public void ResetBox()
    {
        SetState(RewardState.BASE);
    }

    public void SetState(RewardState state)
    {
        switch (state)
        {
            case RewardState.BASE:
                rc_beforeCard.localScale = Vector3.one;
                rc_afterCard.localScale = new Vector3(0, 1, 1);
                giftBtn.interactable = true;
                break;
            case RewardState.FLIP:
                flipCallback?.Invoke();
                rc_beforeCard.DOScaleX(0f, 0.5f).OnComplete(() => {
                    rc_afterCard.DOScaleX(1f, 0.5f).OnComplete(() => {
                        SetState(RewardState.TURNED);
                    });
                });
                break;
            case RewardState.TURNED:
                rc_beforeCard.localScale = new Vector3(0, 1, 1);
                rc_afterCard.localScale = Vector3.one;

                completeCallback?.Invoke();
                break;
            case RewardState.ROCK:
                giftBtn.interactable = false;
                break;
        }
    }

    public void SetReward(GIFTS gift)
    {
        var data = giftTable.GetData(gift);
        reward_Text.text = data.FullName;
    }
}
