using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//[添加元件(類型(任何元件類型))-套用腳本時執行]
//[RequireComponent(typeof(AudioSource))]
public class RanDomSkill : MonoBehaviour
{
    #region 屬性
    [Header("圖片隨機")]
    public Sprite[] RandomSprite;
    [Header("技能圖片")]
    public Sprite[] SkillSprite;
/// <summary>
/// 要修改的圖片
/// </summary>
    Image imgSkill;
    [Header("間隔S"),Range(0f,1f)]
    public float Speed;
    [Header("迴轉次數")]
    public int count = 3;
   public AudioClip[] clip;
    AudioSource aud;
    /// <summary>
    /// 選技能次數
    /// </summary>
     int CountChoose;
    /// <summary>
    /// 最大選技能次數
    /// </summary>
    int maxCount = 2;
    /// <summary>
    /// 是否可以重選
    /// </summary>
    static bool canChangSkill;
    public Button ChangeSkill;
    public Button Btn;
    public GameObject ObjSkill;
    Text SkillText;
    public string[] SkillName1= { "2連射" ,"添加弓箭","前後", "左右", "血量增加", "攻擊增加", "攻速增加", "爆擊增加" };
  /// <summary>
  /// 隨機值
  /// </summary>
    int RandomIndex;
    #endregion
    #region 事件
    private void Start()
    {
        imgSkill = GetComponent<Image>();
           aud = GetComponent<AudioSource>();
        SkillText = transform.GetComponentInChildren<Text>();
        Btn= GetComponent<Button>();
        ObjSkill = GameObject.Find("選擇技能介面");
        StartCoroutine(StartRandom());//啟動協程

        Btn.onClick.AddListener(ChooseSkill);//按鈕.點擊事件.增加聆聽者(方法)

    }
    private void Update()
    {
        
        if (CountChoose < maxCount)
        {
            canChangSkill = true;
            ChangeSkill.interactable = true;
        }
        else 
        {
            canChangSkill = false;
            ChangeSkill.interactable = false;
        }
    }
    #endregion
    #region 方法
    /// <summary>
    /// 開始隨機技能
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartRandom()
    {
        Btn.interactable = false;//在一開始按鈕是不能被按下
        //imgSkill.sprite = RandomSprite[0];//技能.圖片=隨機圖片的索引值
        //yield return new WaitForSeconds(0.1f);
        //imgSkill.sprite = RandomSprite[1];
        //yield return new WaitForSeconds(0.1f);
        //imgSkill.sprite = RandomSprite[2];
        //yield return new WaitForSeconds(0.1f);
        for (int c = 0; c < count; c++)
        {
        for (int i = 0; i < RandomSprite.Length; i++)
        {
            imgSkill.sprite = RandomSprite[i];
                aud.PlayOneShot(clip[0], 0.1f);//播放音效(來源,音量)
            yield return new WaitForSeconds(Speed);
        }
          
        }
        RandomIndex = Random.Range(0, SkillSprite.Length);
        imgSkill.sprite = SkillSprite[RandomIndex];//隨機顯示技能
        SkillText.text = SkillName1[RandomIndex];
        aud.PlayOneShot(clip[1], 0.5f);
        Btn.interactable = true;//技能按鈕是可以被按下
    }
    /// <summary>
    /// 再啟動選技能協程
    /// </summary>
    public void ReRandomSkill()
    {
        if (canChangSkill)
        {
         CountChoose++;
        StartCoroutine(StartRandom());
        }
      
    }
    public void ChooseSkill() 
    {
        gameObject.AddComponent<Outline>();
        gameObject.GetComponent<Outline>().effectColor = new Color(256, 246, 0, 255);//將Outline顏色指定(new Color(R, G, B, A))
        gameObject.GetComponent<Outline>().effectDistance = new Vector2(10, 10);//將Outline距離指定(new Vector2(x,y))
        //ObjSkill.SetActive(false);
        print("以選擇的技能"+ SkillName1[RandomIndex]);
    }
    public void ChangSkill()
    { Destroy(gameObject.GetComponent<Outline>()); 
    }
    #endregion
}
