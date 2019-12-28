
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    #region 屬性
    [Header("移動速度"),Range(1,10)]
    public float Speed;
    /// <summary>
    /// 虛擬搖桿
    /// </summary>
    Joystick joy;
    /// <summary>
  /// 玩家剛體
  /// </summary>
    Rigidbody PlayerRig;
   /// <summary>
   /// 玩家動畫控制器
   /// </summary>
    Animator PlayerAim;
    /// <summary>
    /// 面向目標物件
    /// </summary>
     Transform Target;
    [Header("玩家血量"),Range(0,9999)]
    public float HP;
    public LevelManager m_level;
    
    #endregion

    #region 事件
    private void Awake()
    {
        //m_level = GameObject.Find("GameMannger").GetComponent<LevelManager>();
        m_level = FindObjectOfType<LevelManager>();//透過類型取得元件
           joy = GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
        PlayerRig = GetComponent<Rigidbody>();//取得Rigidbody(泛形類別)元件
        PlayerAim= GetComponent<Animator>();
       // Target = GameObject.FindGameObjectWithTag("Target").transform;
        Target = GameObject.Find("目標").transform;//簡寫
    }
    private void Update()
    {
        //print("水平位置"+joy.Horizontal);
        //print("垂直位置" + joy.Vertical);
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine( Attack());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Die();
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region 方法
   /// <summary>
   /// 玩家移動方法
   /// </summary>
    public void Move() 
    { 
        PlayerRig.AddForce(new Vector3( -joy.Horizontal * Speed,0, -joy.Vertical * Speed),ForceMode.Impulse);

        if (joy.Horizontal != 0 || joy.Vertical != 0)//搖桿的水平與垂直不等於0播放走路動畫
        {
            PlayerAim.SetBool("Run", true);
        }
        else //反之停止播放走路動畫
        {
            PlayerAim.SetBool("Run", false);
        }
        Vector3 PlayerPos = transform.position;//取得玩家座標
        Vector3 TargetPos = new Vector3(PlayerPos.x - joy.Horizontal, PlayerPos.y, PlayerPos.z - joy.Vertical);
        //目標座標= 新三維向量(玩家.X+搖桿水平,0(會吃土)改為玩家Y軸高度(可避免吃土),玩家.Y+搖桿垂直)
        Target.position = TargetPos;
        PlayerRig.transform.LookAt(Target);//面向目標
       // PlayerAim.SetBool("Run", joy.Horizontal != 0 || joy.Vertical != 0);利用邏輯運算值(比較運算值)為布林值的特性
    }
    #region 玩家狀態方法
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAim.SetTrigger("Attack");
       
    }
    public void Die()
    {
        PlayerAim.SetBool("Die", HP <= 0);
    }
    public void Hurt(float damage) 
    {
    }
    #endregion

    #region 玩家進入下一關
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.GetComponent<Collider>().tag=="傳送區域")
        {
            m_level.StartCoroutine("LoadLevel");
        }
    }
    #endregion
    #endregion


}
