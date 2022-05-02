using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerClassbase<T> : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get 
        { 
            // ���ϼ��� ���� �̹� ���� �Ǿ����� Ȯ��
            if(instance == null)
            {
                var obj = FindObjectOfType<ManagerClassbase<T>>();
                if(obj != null)
                {
                    instance = obj.GetComponent<T>();
                }
                else
                {
                    var newobj = new GameObject().AddComponent<ManagerClassbase<T>>();
                    instance = newobj.GetComponent<T>();
                }
            }
            return instance; 
        }
    }

}
