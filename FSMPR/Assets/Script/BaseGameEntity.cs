using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 에이전트들이 상속받는 기반클래스
public abstract class BaseGameEntity : MonoBehaviour
{
    // 상속받는 오브젝트들의 고유 ID(0부터 시작해서 1씩 증가)
    private static int m_iNextValidID = 0;

    private int id;
    public int ID
    {
        get => id;
        set
        {
            id = value;
            m_iNextValidID++;
        }
    }

    private string entityName;      // 에이전트 이름
    private string personalColor;   // 에이전트 색상(출력용)

    /// <summary>
    /// Setup 메서드에서 고유번호, 이름, 색상을 설정한다.
    /// 파생클래스에서 base.Setup()으로 호출
    /// </summary>
    public virtual void Setup(string name)
    {
        ID = m_iNextValidID;

        entityName = name;

        int color = Random.Range(0, 1000000);
        personalColor = $"#{color.ToString("X6")}";

    }

    // GameController 클래스에서 모든 에이전트의 Updated() 를 호출해 에이전트를 구동한다.
    public abstract void Updated();

    /// <summary>
    /// Console View에 [이름 : "대사"] 출력
    /// </summary>
    public void PrintText(string text)
    {
        Debug.Log($"<color={personalColor}><b>{entityName}</b></color> : {text}");
    }
}
