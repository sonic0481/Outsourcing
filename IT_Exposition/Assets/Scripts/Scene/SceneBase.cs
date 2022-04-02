using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneBase : MonoBehaviour
{
    [SerializeField] SCENE      _myScene;
    protected SceneManager      _sceneMgr;
    public SCENE MYSCENE { get { return _myScene; } }

    public virtual void AwakeInit(SceneManager sceneManager)
    {
        _sceneMgr = sceneManager;
    }
    public abstract void Init();
    public abstract void On();
    protected virtual void OnNext() { _sceneMgr?.NextScene(); }
    protected virtual void OnPrev() { _sceneMgr?.PrevScene(); }    
}
