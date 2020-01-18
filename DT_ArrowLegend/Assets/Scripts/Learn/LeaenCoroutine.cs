using System.Collections;//引用系統.集合 API
using UnityEngine;

public class LeaenCoroutine : MonoBehaviour
{//傳回類型 IEnumerator
   IEnumerator DelayEffect() 
    {
        print("開始協程");
        yield return new WaitForSeconds(1f);//傳回新的等待秒數
        print("1秒後");
        yield return new WaitForSeconds(3f);//傳回新的等待秒數
        print("3秒後");
    }


    private void Start()
    {//啟動協程
        StartCoroutine(DelayEffect());
    }
}
