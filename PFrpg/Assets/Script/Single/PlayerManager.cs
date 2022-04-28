using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static Player _Player;

    private void Start()
    {
        _Player = GameObject.Find("PlayerCharacter").GetComponent<Player>();
    }

}
