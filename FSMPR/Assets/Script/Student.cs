using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Student의 상태 열거형
public enum StudentStates { RestAndSleep = 0, StudyHard, TakeAExam, PlayAGame, HitTheBottle, VisitBathroom, Global }

// 에이전트의 한 종류(ex.슬라임,드래곤)
public class Student : BaseGameEntity
{
    private int knowledge;              // 지식
    private int stress;                 // 스트레스
    private int fatigue;                // 피곤
    private int totalScore;             // 점수
    private Locations currentLocation;    // 현재 위치

    // Student가 가지고 있는 모든 상태, 현재 상태
    private State<Student>[] states;
    /// <summary>
    /// 상태를 관리하는 것은 StateMachine에게 위임하기 때문에
    /// Student 본인이 가질 수 있는 상태를 제외하고, 상태와 관련된 변수, 메소드를 모두 삭제
    /// </summary>
    //private State<Student> currentState;
    private StateMachine<Student> stateMachine;

    public int Knowledge
    {
        get => knowledge;
        set => knowledge = Mathf.Max(0, value);
    }

    public int Stress
    {
        get => stress;
        set => stress = Mathf.Max(0, value);
    }

    public int Fatigue
    {
        get => fatigue;
        set => fatigue = Mathf.Max(0, value);
    }

    public int TotalScore
    {
        get => totalScore;
        set => totalScore = Mathf.Clamp(value, 0, 100);
    }

    public Locations CurrentLocation
    {
        get => currentLocation;
        set => currentLocation = value;
    }

    public StudentStates CurrentState { get; private set; } // 현재상태

    public override void Setup(string name)
    {
        // 기반 클래스의 Setup 메소드 호출
        base.Setup(name);

        // 생성되는 오브젝트 이름 설정
        gameObject.name = $"{ID:D2}_Student_{name}";

        // Student 가 가질 수 있는 상태 개수만큼 메모리 할당, 각 상태에 클래스 메모리 할당
        states = new State<Student>[7];
        states[(int)StudentStates.RestAndSleep] = new StudentOwnedStates.RestAndSleep();
        states[(int)StudentStates.StudyHard] = new StudentOwnedStates.StudyHard();
        states[(int)StudentStates.TakeAExam] = new StudentOwnedStates.TakeAExam();
        states[(int)StudentStates.PlayAGame] = new StudentOwnedStates.PlayAGame();
        states[(int)StudentStates.HitTheBottle] = new StudentOwnedStates.HitTheBottle();
        states[(int)StudentStates.VisitBathroom] = new StudentOwnedStates.VisitBathroom();
        states[(int)StudentStates.Global] = new StudentOwnedStates.StateGlobal();

        // 현재 상태를 집에서 쉬는 "RestAndSleep" 상태로 설정
        // ChangeState() 메소드로 설정해야 첫 상태의 Enter()를 호출할 수 있다.
        //ChangeState(StudentStates.RestAndSleep);

        // 상태를 관리하는 StateMachine에 메모리를 할당하고, 첫 상태를 설정
        stateMachine = new StateMachine<Student>();
        stateMachine.Setup(this, states[(int)StudentStates.RestAndSleep]);

        // 전역 상태 설정
        stateMachine.SetGlobalState(states[(int)StudentStates.Global]);

        knowledge = 0;
        stress = 0;
        fatigue = 0;
        totalScore = 0;
        currentLocation = Locations.SweetHome;

        //PrintText("Hello Real World");
    }

    public override void Updated()
    {
        //PrintText("대기중입니다...");

        //if(currentState != null)
        //{
        //    currentState.Execute(this);
        //}

        stateMachine.Execute();
    }

    public void ChangeState(StudentStates newState)
    {
        // 새로 바꾸려는 상태가 비어있으면 상태를 바꾸지 않는다.
        //if (states[(int)newState] == null) return;

        // 현재 재생중인 상태가 있으면 Exit() 메소드 호출
        //if(currentState != null)
        //{
        //    currentState.Exit(this);
        //}

        // 새로운 상태로 변경하고, 새로 바뀐 상태의 Enter() 메소드 호출
        //currentState = states[(int)newState];
        //currentState.Enter(this);

        CurrentState = newState;

        stateMachine.ChangeState(states[(int)newState]);
    }

    public void RevertToPreviousState()
    {
        stateMachine.RevertToPreviousState();
    }
}
