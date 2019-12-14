
using UnityEngine;

public class LearnArray : MonoBehaviour
{

    //陣列 :類型[]
    public int[] enemyID;
    public string[] enemyName;
    public GameObject[] enemyObj;

    //宣告陣列方式
    public int[] array1;// 定義數量為0
    public int[] array2 = new int[10] ; //定義數量為3
    public int[] array3 = { 100, 200, 300 };//定義資料為 100, 200, 300

    private void Awake() //在Start 前執行一次
    {
        //存取陣列
        array2[0] = array3[1];
        print("取得第二筆資料" + array3[1]);
        //設定陣列
        array3[2] = 999;
        //陣列長度(數量)
        print("陣列2數量為"+array2.Length);
        //enemyName[1] = 999.ToString();
        //透過標籤尋找物件名稱傳回陣列
        enemyObj = GameObject.FindGameObjectsWithTag("Target");

        
    }
   
}
