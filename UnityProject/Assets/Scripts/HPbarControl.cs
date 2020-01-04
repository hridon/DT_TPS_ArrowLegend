
using UnityEngine;
using UnityEngine.UI;

public class HPbarControl : MonoBehaviour
{
    [Header("HPbar文字")]
    public Text T_HPbar;
    [Header("HPbar圖片")]
    public Image I_HPbar;
    private void Start()
    {
        T_HPbar = transform.GetChild(0).GetComponent<Text>();
        I_HPbar = transform.GetChild(1).GetChild(0).GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        AngleControl();
    }
    void AngleControl() 
    {//變型元件.歐拉角度=新三維向量().世界座標
        //transform.eulerAngles.世界座標
        //transform.localEulerAngles.區域座標
        transform.eulerAngles = new Vector3(0,0,0);
        
    }
    /// <summary>
    /// 更新血條方法 需宣告法血條圖片,血條文字(最大血量,目前血量)
    /// </summary>
    /// <param name="hpmax">最大血量</param>
    /// <param name="hpcurrent">目前血量</param>
    public void UpdateHPbar(float hpmax,float hpcurrent) 
    {
        I_HPbar.fillAmount = hpcurrent / hpmax;//圖片.填滿數值=目前血量/最大血量
        T_HPbar.text = hpcurrent.ToString();//文字.文字內容=目前.轉字串()
    }
}
