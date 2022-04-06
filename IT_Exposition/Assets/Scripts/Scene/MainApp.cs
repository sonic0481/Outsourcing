using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainApp : MonoBehaviour
{
    [SerializeField] SceneManager sceneMgr;

    private void Awake()
    {
        CSVTableManager.Instance.InitTable(() => {
            DataManager.Instance.InitializeData();
            sceneMgr.InitScene();            
        });
    }
}
