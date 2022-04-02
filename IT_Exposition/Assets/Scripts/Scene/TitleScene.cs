using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : SceneBase
{
    [SerializeField] Button         _startBtn;

    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);

        _startBtn.onClick.AddListener(OnClickStart);
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
