
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{/// <summary>
/// 廣告用遊戲ID,Google or Ios
/// </summary>
    string GameID = "3436905";
   /// <summary>
   /// 是否為測試模式
   /// </summary>
    bool testmode=true;
    string PlacementReBorn = "revival";//廣告類型 名稱(例:復活)
    private void Start()
    {
        Advertisement.Initialize(GameID, testmode);//廣告物件初始化(ID,測試模式)
    }
}
