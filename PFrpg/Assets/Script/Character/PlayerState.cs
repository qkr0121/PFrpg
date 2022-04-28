using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class Idle : State<Player>
    {
        public override void Enter(Player entity)
        {
            Debug.Log("Idle Enter");
        }

        public override void Execute(Player entity)
        {
            Debug.Log("Idle Execute");

            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                entity.stateMachine.ChangeState(entity.playerState[1]);
            }
        }

        public override void Exit(Player entity)
        {
            Debug.Log("Idle Exit");
        }
    }

    public class Run : State<Player>
    {
        public override void Enter(Player entity)
        {
            Debug.Log("Run Enter");
        }

        public override void Execute(Player entity)
        {
            Debug.Log("Run Execute");

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                entity.stateMachine.ChangeState(entity.playerState[0]);
            }
        }

        public override void Exit(Player entity)
        {
            Debug.Log("Run Exit");
        }
    }



}

