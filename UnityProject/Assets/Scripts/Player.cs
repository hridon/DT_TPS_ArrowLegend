
using UnityEngine;

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
    #endregion

    #region 事件
    private void Awake()
    {
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

        if (joy.Horizontal != 0 || joy.Vertical != 0)
        {
            PlayerAim.SetBool("Run", true);
        }//播放走路動畫
        else 
        {
            PlayerAim.SetBool("Run", false);
        }
        Vector3 PlayerPos = transform.position;//取得玩家座標
        Vector3 TargetPos = new Vector3(PlayerPos.x - joy.Horizontal, PlayerPos.y, PlayerPos.z - joy.Vertical);//目標座標= 新三維向量(玩家.X+搖桿水平,0,玩家.Y+搖桿垂直)
        Target.position = TargetPos;
        PlayerRig.transform.LookAt(Target);//面向目標
    }
  
    #endregion


}
