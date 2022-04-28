using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player �� State
    public enum State { Idle, Run }

    // Player�� ��� ����
    private State<Player>[] _PlayerState;
    public State<Player>[] playerState => _PlayerState;

    // StateMachine ������Ʈ
    private StateMachine<Player> _StateMachine;
    public StateMachine<Player> stateMachine => _StateMachine;

    private void Start()
    {
        _PlayerState=  new State<Player>[2];
        _PlayerState[(int)State.Idle] = new PlayerState.Idle();
        _PlayerState[(int)State.Run] = new PlayerState.Run();

        _StateMachine = new StateMachine<Player>();
        _StateMachine.SetUp(this, _PlayerState[(int)State.Idle]);
    }

    // GameController ���� ������ ����
    public void Updated()
    {
        _StateMachine.Execute();
    }
}
