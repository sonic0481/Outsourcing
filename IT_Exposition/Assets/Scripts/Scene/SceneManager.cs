using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] SceneBase[] _scenes = new SceneBase[(int)SCENE.END];

    AudioSource clickSound;

    private SCENE _currentScene;

    void Awake()
    {
        clickSound = GetComponent<AudioSource>();

        for (int i = 0; i < _scenes.Length; ++i)
            _scenes[i]?.AwakeInit(this);
    }    

    public void InitScene()
    {
        for (int i = 0; i < _scenes.Length; ++i)
            _scenes[i]?.Init();

        _currentScene = SCENE.TITLE;
        SetScene(_currentScene);
    }

    private void SetScene(SCENE scene)
    {
        _currentScene = scene;

        for(int i = 0; i < _scenes.Length; ++i)
        {
            if (null == _scenes[i])
                continue;

            if(scene == (SCENE)i)
            {
                if(false == _scenes[i]?.gameObject.activeSelf)
                {
                    _scenes[i].gameObject.SetActive(true);
                    _scenes[i].On();
                }                
            }
            else
            {
                if (_scenes[i].gameObject.activeSelf)
                    _scenes[i].gameObject.SetActive(false);
            }
        }
    }

    public void NextScene()
    {
        _currentScene++;

        if (SCENE.MANAGE <= _currentScene)
            _currentScene = SCENE.TITLE;

        SetScene(_currentScene);
    }

    public void PrevScene()
    {
        if (SCENE.TITLE >= _currentScene)
        {
            _currentScene = SCENE.TITLE;
            return;
        }
        _currentScene--;        

        SetScene(_currentScene);
    }

    public void ForceScene(SCENE scene)
    {
        _currentScene = scene;      
        SetScene(_currentScene);
    }    

    private bool IsCurrentScene(SCENE scene)
    {
        return _currentScene == scene;
    }

    public void FinishUser()
    {
        InitScene();
    }    

    public void OnClickSound()
    {
        clickSound.Play();
    }
}
