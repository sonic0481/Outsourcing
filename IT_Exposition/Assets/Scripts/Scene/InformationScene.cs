using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationScene : SceneBase
{
    [SerializeField] Toggle[]   _eventToggles = new Toggle[(int)EVENT.END];
    [SerializeField] Button     _previousBtn;
    [SerializeField] Button     _nextBtn;


    public override void AwakeInit(SceneManager sceneManager)
    {
        base.AwakeInit(sceneManager);

        _previousBtn.onClick.AddListener(OnPrev);
        _nextBtn.onClick.AddListener(OnNext);
    }

    public override void Init()
    {
        for(int i = 0; i < _eventToggles.Length; ++i)
        {
            _eventToggles[i].isOn = false;
        }        
    }

    public override void On()
    {
        for (int i = 0; i < _eventToggles.Length; ++i)
        {
            _eventToggles[i].isOn = false;
        }
    }

    protected override void OnNext()
    {
        EVENT selectEvent = EVENT.END;

        for(int i = 0; i < _eventToggles.Length; ++i)
        {
            if(_eventToggles[i].isOn)
            {
                selectEvent = (EVENT)i;
                break;
            }
        }

        if (EVENT.END == selectEvent)
            return;

        DataManager.Instance.QuestionData.SelectEvent = selectEvent;
        DataManager.Instance.QuestionData.SetQuestion(true);

        _sceneMgr?.NextScene();
    }
}
