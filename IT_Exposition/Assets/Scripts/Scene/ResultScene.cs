using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScene : SceneBase
{
    [SerializeField] Button         acceptBtn;
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

    }

    protected override void OnNext()
    {
        _sceneMgr.FinishUser();   
    }
}
