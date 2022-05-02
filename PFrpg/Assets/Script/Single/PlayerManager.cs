using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ManagerClassbase<PlayerManager>
{
    private Player _Player;

    public Player player => _Player = _Player ??
        GameObject.Find("PlayerCharacter").GetComponent<Player>();
}
