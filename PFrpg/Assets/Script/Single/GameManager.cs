using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _GameManager = null;

    private PlayerManager playerManager;

    private void Awake()
    {
        // 유일성 검사
        if (_GameManager != null)
            Destroy(this.gameObject);
        else
        {
            _GameManager = this;
            DontDestroyOnLoad(gameObject);
        }

        // Manager 등록
        playerManager = transform.GetComponentInChildren<PlayerManager>();

    }

    
}
