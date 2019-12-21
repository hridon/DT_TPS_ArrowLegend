
using UnityEngine;

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
    Animator Door;
    #endregion
    #region 事件
  
    private void Start()
    {
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
    #endregion
}
