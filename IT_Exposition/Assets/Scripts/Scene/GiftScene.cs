using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftScene : SceneBase
{
    [SerializeField] Transform      rewardContent;

    List<RewardBox>                 rewardList = new List<RewardBox>();
    int winCount;
    GIFTS rewardGift;

    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);

        rewardList.Clear();

        for(int i = 0; i < rewardContent.childCount; ++i)
        {
            RewardBox rewardBox = rewardContent.GetChild(i).GetComponent<RewardBox>();
            
            if(null != rewardBox)
            {
                rewardBox.Init(i, NextScene, FlipUpdate, ClickUpdate);
                rewardList.Add(rewardBox);
            }
        }
    }

    public override void Init()
    {
        int     rewardCount = (int)GIFTS.END;

        for(int i = 0; i < rewardList.Count; ++i)
        {
            if (null == rewardList[i])
                continue;

            rewardList[i].ResetBox();
        }
    }


    public override void On()
    {
        winCount = 0;
        QuestionData questionData = DataManager.Instance.QuestionData;
        GiftsData giftData = DataManager.Instance.GiftsData;

        for (int i = 0; i < rewardList.Count; ++i)
        {
            rewardList[i].SetState(RewardBox.RewardState.BASE);
        }

        for(int i = 0; i < (int)QUESTION.Q_END; ++i)
        {
            DefQuestionTable.Data data = questionData.QuestionList[i];
            ANSWER selectAnswer = questionData.GetAnswer((QUESTION)i);

            if(data.Answer == selectAnswer)
            {
                winCount++;
            }
        }

        var wins = giftData.GetGiftWins(winCount);
        SetReward(wins);
    }    

    protected override void OnNext()
    {
        base.OnNext();
    }

    private void NextScene()
    {
        Invoke("OnNext", 2f);
    }

    private void FlipUpdate()
    {
        for (int i = 0; i < rewardList.Count; ++i)
        {
            rewardList[i].SetState(RewardBox.RewardState.ROCK);
        }
    }

    private void ClickUpdate(int index)
    {
        List<GIFTS> giftList = new List<GIFTS>();

        for (GIFTS g = GIFTS.START; g < GIFTS.END; ++g)
            giftList.Add(g);        

        for(int i = 0; i < rewardList.Count; ++i)
        {
            if (i == index) 
            {
                rewardList[i].SetReward(rewardGift);
                giftList.Remove(rewardGift);
            }
            else
            {
                if(giftList.Count > 0)
                {
                    rewardList[i].SetReward(giftList[0]);
                    giftList.RemoveAt(0);
                }
                else
                {
                    rewardList[i].SetReward(GIFTS.MEGASTUDY);
                }
            }
        }
    }

    private void SetReward(int[] wins)
    {
        int total = 0;
        int checkPoint = 0;
        int roulletPoint = 0;
        GIFTS reward = GIFTS.START;

        for(int i = 0; i < wins.Length; ++i)
        {
            total += wins[i];
        }

        roulletPoint = Random.Range(0, total);

        for(int i = 0; i < wins.Length; ++i)
        {
            int check = wins[i];

            if(check > 0)
            {
                GIFTS key = (GIFTS)i;
                checkPoint += check;

                if(checkPoint > roulletPoint)
                {
                    reward = key;
                    break;
                }
            }
        }

        GiftsData giftData = DataManager.Instance.GiftsData;

        rewardGift = reward;
        giftData.WinningGift = reward;
        giftData.Add_Give(reward);
    }
}
