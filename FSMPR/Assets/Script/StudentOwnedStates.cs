using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudentOwnedStates
{
    public class RestAndSleep : State<Student>
    {
        public override void Enter(Student entity)
        {
            // 장소를 집으로 설정하고, 집에오면 스트레스가 0이 된다.
            entity.CurrentLocation = Locations.SweetHome;
            entity.Stress = 0;

            entity.PrintText("집에 들어온다. 행복한 우리집~ 집에 오니 스트레스가 사라졌다.");
            entity.PrintText("침대에 누워서 잠을 잔다.");
        }

        public override void Execute(Student entity)
        {
            entity.PrintText("zzZ~ zz~ zzzzZ~~");

            // 피로가 0이 아니면
            if(entity.Fatigue > 0)
            {
                // 피로 10씩 감소
                entity.Fatigue -= 10;
            }
            // 피로가 0이면
            else
            {
                // 도서관에 가서 공부를 하는 "StudyHard" 상태로 변경
                entity.ChangeState(StudentStates.StudyHard);
            }
        }

        public override void Exit(Student entity)
        {
            entity.PrintText("침대를 정리하고 집 밖으로 나간다.");
        }
    }

    public class StudyHard : State<Student>
    {
        public override void Enter(Student entity)
        {
            entity.CurrentLocation = Locations.Library;

            entity.PrintText("공부를 하기 위해 도서관에 왔다.");
        }

        public override void Execute(Student entity)
        {
            entity.PrintText("공부 공부 공부 공부...");

            // 지식, 스트레스, 피로가 1씩 증가
            entity.Knowledge++;
            entity.Stress++;
            entity.Fatigue++;

            // 지식이 3~10 사이가 되면
            if(entity.Knowledge >= 3 && entity.Knowledge <= 10 )
            {
                int isExit = Random.Range(0, 2);
                if(isExit == 1 || entity.Knowledge == 10)
                {
                    // 강의실에 가서 시험을 보는 TakeAExam" 상태로 변경
                    entity.ChangeState(StudentStates.TakeAExam);
                }
            }

            // 스트레스가 20 이상이 되면
            if(entity.Stress >= 20)
            {
                // PC방에 가서 게임을 하는 "PlayAGame" 상태롤 변경
                entity.ChangeState(StudentStates.PlayAGame);
            }

            // 피로가 50 이상이 되면
            if(entity.Fatigue >= 50)
            {
                // 집에 가서 쉬는 "RestAndSleep" 상태로 변경
                entity.ChangeState(StudentStates.RestAndSleep);
            }
        }

        public override void Exit(Student entity)
        {
            entity.PrintText("자리를 정리하고 도서관을 떠난다.");
        }
    }

    public class TakeAExam : State<Student>
    {
        public override void Enter(Student entity)
        {
            entity.CurrentLocation = Locations.LectrueRoom;

            entity.PrintText("강의실에 들어간다. 시험지를 받았다.");
        }

        public override void Execute(Student entity)
        {
            int examScore = 0;

            // 지식이 10이면 획득점수는 10
            if(entity.Knowledge == 10)
            {
                examScore = 10;
            }
            else
            {
                // 지식이 높을수록 높은 점수를 받을 확률이 높다.
                int randIndex = Random.Range(0, 10);
                examScore = randIndex < entity.Knowledge ? Random.Range(6, 11) : Random.Range(1, 6);
            }

            // 시험 직후 지식은 0 으로 초기화, 피로는 5 ~ 10증가
            entity.Knowledge = 0;
            entity.Fatigue += Random.Range(5, 11);

            // 시험 점수를 TotalScore에 추가, 결과 출력
            entity.TotalScore += examScore;
            entity.PrintText($"시험 성적({examScore}), 총점({entity.TotalScore})");

            // 100점이상이면 게임종료
            if(entity.TotalScore >= 100)
            {
                GameController.Stop(entity);
                return;
            }

            // 시험 점수에 따라 다음 행동 설정
            if(examScore <= 3)
            {
                // 술집에 가서 술을 마시는 "HitTheBottle" 상태로 변경
                entity.ChangeState(StudentStates.HitTheBottle);
            }
            else if(examScore <= 7)
            {
                // 도서관에 가서 공부를 하는 "StudyHard" 상태로 변경
                entity.ChangeState(StudentStates.StudyHard);
            }
            else
            {
                // PC방에 가서 게임을 하는 "PlayGame" 상태로 변경
                entity.ChangeState(StudentStates.PlayAGame);
            }
        }

        public override void Exit(Student entity)
        {
            entity.PrintText("강의실 문을 열고 밖으로 나온다.");
        }
    }

    public class PlayAGame : State<Student>
    {
        public override void Enter(Student entity)
        {
            entity.CurrentLocation = Locations.PCRoom;

            entity.PrintText("한시간만.. 딱 한시간만 놀아야지");
        }

        public override void Execute(Student entity)
        {
            entity.PrintText("건전하게?? 게임을 즐긴다..");

            int randState = Random.Range(0, 10);
            if(randState == 0 || randState == 9)
            {
                entity.Stress += 20;

                // 술집에 가서 술을 마시는 "HitTheBottle" 상태로 변경
                entity.ChangeState(StudentStates.HitTheBottle);
            }
            else
            {
                entity.Stress--;
                entity.Fatigue += 2;

                if(entity.Stress <= 0)
                {
                    // 도서관에 가서 공부를 하는 "StudyHard" 상태로 변경
                    entity.ChangeState(StudentStates.StudyHard);
                }
            }
        }

        public override void Exit(Student entity)
        {
            entity.PrintText("PC방에서 나온다.");
        }
    }

    public class HitTheBottle : State<Student>
    {
        public override void Enter(Student entity)
        {
            entity.CurrentLocation = Locations.Pub;

            entity.PrintText("술이나 한잔할까? 술집으로 들어간다.");
        }

        public override void Execute(Student entity)
        {
            entity.PrintText("술을 마신다.");

            entity.Stress -= 5;
            entity.Fatigue += 5;

            if(entity.Stress <=0 || entity.Fatigue >= 50)
            {
                // 집에 가서 쉬는 "RestAndSleep" 상태로 변경
                entity.ChangeState(StudentStates.RestAndSleep);
            }
        }

        public override void Exit(Student entity)
        {
            entity.PrintText("그만 마셔야지.. 술집에서 나온다.");
        }
    }

    public class VisitBathroom : State<Student>
    {
        public override void Enter(Student entity)
        {
            entity.PrintText("화장실에 들어간다.");
        }

        public override void Execute(Student entity)
        {
            entity.PrintText("볼일을 본다.");

            // 바로 직전상태로 되돌아간다.
            entity.RevertToPreviousState();
        }

        public override void Exit(Student entity)
        {
            entity.PrintText("손을 씻고 화장실에서 나간다.");
        }
    }

    public class StateGlobal : State<Student>
    {
        public override void Enter(Student entity)
        {
        }

        public override void Execute(Student entity)
        {
            if(entity.CurrentState == StudentStates.VisitBathroom)
            {
                return;
            }

            int bathroomState = Random.Range(0, 100);
            if(bathroomState < 10)
            {
                entity.ChangeState(StudentStates.VisitBathroom);
            }
        }

        public override void Exit(Student entity)
        {
        }
    }

}
