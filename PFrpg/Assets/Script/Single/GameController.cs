using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    // ��� ���� ��� ������Ʈ ����Ʈ


    // ��� ������Ʈ���� Updated() �� ȣ���մϴ�.
    private void Update()
    {
        PlayerManager._Player.Updated();
    }

}
