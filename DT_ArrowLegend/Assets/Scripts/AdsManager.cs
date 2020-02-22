
using UnityEngine;
using UnityEngine.Advertisements;
//C#僅能繼承一個類別 ,但可以實作多個介面(裝備)
public class AdsManager : MonoBehaviour,IUnityAdsListener
{/// <summary>
/// 廣告用遊戲ID,Google or Ios
/// </summary>
    string GameID = "3436905";
   /// <summary>
   /// 是否為測試模式
   /// </summary>
    bool testmode=false;
    string PlacementReBorn = "revival";//廣告類型 名稱(例:復活)
    public LevelManager LM;
    public Player m_Player;
    public static bool Lookad;
    private void Start()
    {
        LM = GameObject.FindObjectOfType<LevelManager>();
        m_Player=GameObject.FindObjectOfType<Player>();
        Advertisement.AddListener(this); //廣告監聽
        Advertisement.Initialize(GameID, testmode);//廣告物件初始化(ID,測試模式)
    }
    /// <summary>
    /// 顯示廣告
    /// </summary>
    public void ShowAD() 
    {
        if (Advertisement.IsReady(PlacementReBorn))// 如果廣告準備完成
        {
            Lookad = true;//有看廣告

            Advertisement.Show(PlacementReBorn);//廣告 顯示(廣告ID)
        }
    
    
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidError(string message)
    {
       ;
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId== PlacementReBorn)//如果廣告ID=復活廣告ID
        {
            switch (showResult)//switch 判斷式
            {
                case ShowResult.Failed://廣告失敗 狀況1
                    print("失敗");
                    break;

                case ShowResult.Skipped://廣告跳過 狀況2
                    print("跳過");
                    Lookad = false;
                    break;

                case ShowResult.Finished://廣告完成 狀況3
                    
                    print("復活");
                    
                    LM.HidePanelReBorn();
                    m_Player.Reborn();
                   // GameObject.Find("Player").GetComponent<Player>().Reborn();
                    Lookad = false;
                    break;
               
                //default://預設
                //    break;
            }

        }
    }
}
