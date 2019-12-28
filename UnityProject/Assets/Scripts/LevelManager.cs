
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
    Animator Door;
    public string stageName;
    /// <summary>
    /// 取得載入場景資訊
    /// </summary>
    AsyncOperation ao;
    #endregion
    #region 事件

    private void Start()
    {
        Cross = GameObject.Find("轉場畫面").GetComponent<Image>();
           Door = GameObject.Find("門").GetComponent<Animator>();
       
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
    /// 載入關卡
    /// </summary>
    IEnumerator LoadLevel() 
    {//取得載入場景資訊=要載入場景的資訊
        ao= SceneManager.LoadSceneAsync(stageName, LoadSceneMode.Single);
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
    #endregion
}
