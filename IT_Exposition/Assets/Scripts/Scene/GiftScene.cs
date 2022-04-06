using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftScene : SceneBase
{
    [SerializeField] Transform      rewardContent;

    List<RewardBox>                 rewardList = new List<RewardBox>();

    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);
    }

    public override void Init()
    {
        int     rewardCount = rewardContent.childCount;

        for(int i = 0; i < rewardCount; ++i)
        {
            RewardBox reward = rewardContent.GetChild(i).GetComponent<RewardBox>();

            if (null == reward)
                continue;

            reward.Init(NextScene, FlipUpdate);
            reward.SetReward("AAAAAAAAA" + i.ToString());
        }
    }


    public override void On()
    {
        for (int i = 0; i < rewardList.Count; ++i)
        {
            rewardList[i].SetState(RewardBox.RewardState.BASE);
        }
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
}
