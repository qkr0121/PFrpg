using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    // 제어를 위한 모든 에이전트 리스트


    // 모든 에이전트들의 Updated() 를 호출합니다.
    private void Update()
    {
        PlayerManager._Player.Updated();
    }

}
