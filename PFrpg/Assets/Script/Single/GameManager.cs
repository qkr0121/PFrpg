using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _GameManager;

    private void Awake()
    {
        base.gameObject.name = "GameManager";

        // 다른 GameManager 가 존재하면 삭제하고 기존 GameManager를 파괴되지 않도록 합니다.
        GameObject obj = GameObject.Find("GameManager");
        _GameManager = obj.GetComponent<GameManager>();
        if(_GameManager != this && _GameManager == null)
        {
            Object.Destroy(obj);
        }
        else
        {
            Object.DontDestroyOnLoad(base.gameObject);
        }
    }
}
