using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainApp : MonoBehaviour
{
    [SerializeField] SceneManager sceneMgr;
    

    void Start()
    {
        sceneMgr.InitScene();
    }    
}
