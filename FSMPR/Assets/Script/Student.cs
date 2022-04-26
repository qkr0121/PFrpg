using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Student의 상태 열거형
public enum StudentStates { RestAndSleep = 0, StudyHard, TakeAExam, PlayAGame, HitTheBottle }

// 에이전트의 한 종류(ex.슬라임,드래곤)
public class Student : BaseGameEntity
{
    private int knowledge;              // 지식
    private int stress;                 // 스트레스
    private int fatigue;                // 피곤
    private int totalScore;             // 점수
    private Locations currentLocation;    // 현재 위치

    // Student가 가지고 있는 모든 상태, 현재 상태
    private State[] states;
    private State currentState;

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

    public override void Setup(string name)
    {
        // 기반 클래스의 Setup 메소드 호출
        base.Setup(name);

        // 생성되는 오브젝트 이름 설정
        gameObject.name = $"{ID:D2}_Student_{name}";

        // Student 가 가질 수 있는 상태 개수만큼 메모리 할당, 각 상태에 클래스 메모리 할당
        states = new State[5];
        states[(int)StudentStates.RestAndSleep] = new StudentOwnedStates.RestAndSleep();
        states[(int)StudentStates.StudyHard] = new StudentOwnedStates.StudyHard();

        // 현재 상태를 집에서 쉬는 "RestAndSleep" 상태로 설정
        // ChangeState() 메소드로 설정해야 첫 상태의 Enter()를 호출할 수 있다.
        ChangeState(StudentStates.RestAndSleep);

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

        if(currentState != null)
        {
            currentState.Execute(this);
        }
    }

    public void ChangeState(StudentStates newState)
    {
        // 새로 바꾸려는 상태가 비어있으면 상태를 바꾸지 않는다.
        if (states[(int)newState] == null) return;

        // 현재 재생중인 상태가 있으면 Exit() 메소드 호출
        if(currentState != null)
        {
            currentState.Exit(this);
        }

        // 새로운 상태로 변경하고, 새로 바뀐 상태의 Enter() 메소드 호출
        currentState = states[(int)newState];
        currentState.Enter(this);
    }
}
