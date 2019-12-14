
using UnityEngine;

public class LearnFor : MonoBehaviour
{   [Header("LearnArray腳本")]
    public LearnArray _learnArray;
    // Start is called before the first frame update
    void Start()
    {
        _learnArray = Camera.main.GetComponent<LearnArray>();
        int count = 5;
        //while 當布林值為True 一直執行{} 敘述
        while (count>0)
        {
            print("while迴圈" + count);
            count--;
        }
        //for
        //(初始值,條件,迭代器)
        for (int i = 5; i>0&&i<6; i--)
        {
            print("for迴圈" + i);
        }
        for (int i = 0; i < _learnArray.enemyObj.Length; i++)
        {
            print(_learnArray.enemyObj[i].name);
        }
    }

}
