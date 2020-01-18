
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPbarControl : MonoBehaviour
{
    [Header("HPbar文字")]
    public Text T_HPbar;
    [Header("受傷文字")]
    public Text H_HPbar;
    [Header("HPbar圖片")]
    public Image I_HPbar;
    private void Start()
    {
        T_HPbar = transform.GetChild(0).GetComponent<Text>();
        I_HPbar = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        H_HPbar= transform.GetChild(2).GetComponent<Text>();
        H_HPbar.gameObject.SetActive(false);
        
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
  /// <summary>
  /// 顯示傷害效果 傷害文字向上移動消失 爆擊效果
  /// </summary>
  /// <param name="damage">要顯示的傷害值</param>
  /// <returns></returns>
    public IEnumerator UpdateDamage(float damage) 
    {
        Vector3 posOriginal = H_HPbar.transform.position;//原始位置
        H_HPbar.gameObject.SetActive(true);
        if (damage > 150)
        {
            H_HPbar.fontSize = 50;
            H_HPbar.color = Color.red;
        }
        else
        {
            H_HPbar.fontSize = 30;
            H_HPbar.color = Color.white;
        }
            H_HPbar.text = "-" + damage;
        yield return new WaitForSeconds(0.01f);
        for (int i = 0; i < 10; i++)
        {
        H_HPbar.transform.position += new Vector3(0,0.1f,0);//傷害值向上移動
        yield return new WaitForSeconds(0.05f);//等待
        }
        H_HPbar.transform.position = posOriginal;//恢復為原始位置
        H_HPbar.text = "";
        H_HPbar.gameObject.SetActive(false);

    }

}
