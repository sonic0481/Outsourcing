using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : SceneBase
{
    [SerializeField] Button         _startBtn;
    [SerializeField] Button         _manageBtn;

    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);

        _startBtn.onClick.AddListener(OnClickStart);
        _manageBtn.onClick.AddListener(() => {
            _sceneMgr.ForceScene(SCENE.MANAGE);
        });
    }
    public override void Init()
    {
        
    }

    public override void On()
    {
            
    }    

    private void OnClickStart()
    {
        OnNext();
    }
}
