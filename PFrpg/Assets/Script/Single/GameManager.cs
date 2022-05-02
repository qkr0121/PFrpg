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

        // �ٸ� GameManager �� �����ϸ� �����ϰ� ���� GameManager�� �ı����� �ʵ��� �մϴ�.
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
