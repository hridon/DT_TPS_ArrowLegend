
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    #region 屬性
    [Header("是否顯示選技能")]
    public  bool ShowRandomSkill;
    [Header("是否自動開門")]
    public bool AutoOpen;
    [Header("延遲時間"),Range(0f,10f)]
    public float DelaySpeed;
    public GameObject m_randomSkill;
    [Header("轉場介面物件")]
    public Image Cross;
    [Header("復活介面物件")]
    public CanvasGroup PanelReBorn;
    [Header("復活倒數文字")]
    public Text TextReBorn;
    Animator Door;
    public string stageName;
    /// <summary>
    /// 取得載入場景資訊
    /// </summary>
    AsyncOperation ao;
    [Header("目標是否存在")]
    public bool haveTarget;
    public int TargetCount;
    [Header("是否為魔王關")]
    public bool isBossStage;
    #endregion
    #region 事件

    private void Start()
    {
        Cross = GameObject.Find("轉場畫面").GetComponent<Image>();
           Door = GameObject.Find("門").GetComponent<Animator>();
        PanelReBorn = GameObject.Find("復活介面").GetComponent<CanvasGroup>();
            TextReBorn= GameObject.Find("等待秒數Text").GetComponent<Text>();
        PanelReBorn.alpha= 0;
        TargetCount = 1;
        if (AutoOpen)
        {
            if (m_randomSkill.active==false)
            {
            Invoke("OpenDoor", DelaySpeed);

            }
        }
        if (ShowRandomSkill)
        {
            C_ShowRandom();
        }

    }

   
    #endregion
    #region 方法
    /// <summary>
    /// 可以顯示選技能介面
    /// </summary>
    public void C_ShowRandom() 
    {
        m_randomSkill.SetActive(true);
    }
    /// <summary>
    /// 關閉顯示選技能介面
    /// </summary>
    public void N_ShowRandom() 
    {
        ShowRandomSkill = false;
        m_randomSkill.SetActive(false);
    }
   /// <summary>
   /// 把門打開
   /// </summary>
    void OpenDoor() 
    {
        Door.SetTrigger("OPen");
    }
   /// <summary>
   /// 顯示結算畫面
   /// </summary>
    public void ShowClearInfo() 
    {
    }

    /// <summary>
    /// 載入關卡
    /// </summary>
    IEnumerator LoadLevel() 
    {//取得載入場景資訊=要載入場景的資訊
       
        int sceneIndex =SceneManager.GetActiveScene().buildIndex;//取得當前場景在buildsetting的索引值
        //ao = SceneManager.LoadSceneAsync(stageName, LoadSceneMode.Single);
        ao = SceneManager.LoadSceneAsync(++sceneIndex);
        ao.allowSceneActivation = false;//是否允許載入場景
        //當場景尚未載入完成時
        while (!ao.isDone)
        {//轉場畫面:由透明至全白
            print(ao.progress);
            Cross.color = new Color(1, 1, 1, ao.progress);//轉場畫面.顏色=新顏色(R,G,B,透明度)//ao.progress載入進度值(max=0.9)
            print(ao.progress);
            yield return new WaitForSeconds(0.01f);
            if (ao.progress>=0.9f) ao.allowSceneActivation = true;
            //允許載入場景
            //敘述只有一行可省略大括弧
        }
        //
        //print(ao.progress);
        //yield return new WaitForSeconds(0.01f);
        //print(ao.progress);
    }
    /// <summary>
    /// 復活倒數計時方法
    /// </summary>
    /// <returns></returns>
    public IEnumerator countdownReborn()
    {
        PanelReBorn.alpha = 1;
        PanelReBorn.interactable = true;
        PanelReBorn.blocksRaycasts = true;
        for (int i = 10; i >= 0; i--)
        {
            if (i < 10)
            {
                TextReBorn.text = "0" + i;
            }
            else { TextReBorn.text = i.ToString(); }
            yield return new WaitForSeconds(1.5f);
        }
        PanelReBorn.transform.GetChild(2).GetComponent<Button>().interactable = false;
        //yield return new WaitForSeconds(2f);
        //HidePanelReBorn();
        //if (AdsManager.Lookad==false)
        //{
        //    yield return new WaitForSeconds(3f);
        //    BackToMenu();
        //} 
       
    }
    /// <summary>
    /// 隱藏復活畫面
    /// </summary>
    public void HidePanelReBorn()
    {
        PanelReBorn.alpha = 0;
        PanelReBorn.interactable = false;
        PanelReBorn.blocksRaycasts = false;
        StopCoroutine(countdownReborn());
    }
    /// <summary>
    /// 回首頁
    /// </summary>
    public void BackToMenu() 
    {
        SceneManager.LoadScene("選單畫面");
    }

    /// <summary>
    /// 過關:場景上沒有怪物時前往下一關
    /// </summary>
    public void PassLevel() 
    {
        OpenDoor();
    }
    #endregion
}
