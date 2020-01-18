
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
    
    public LevelManager m_level;
   [Header("玩家資料")]
    public PlayerData _PlayerData;
    [Header("HPcontrol")]
    public HPbarControl _HPControl;

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
        _HPControl = transform.Find("血條系統").GetComponent<HPbarControl>();//1變形.尋找(子物件)
       
    }
    private void Update()
    {
        //print("水平位置"+joy.Horizontal);
        //print("垂直位置" + joy.Vertical);
            //StartCoroutine( Attack());
        
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Die();
        //}
        
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
        if (_PlayerData.hp > 0)
        {
        PlayerRig.AddForce(new Vector3( -joy.Horizontal * Speed,0, -joy.Vertical * Speed),ForceMode.Impulse);
            Vector3 PlayerPos = transform.position;//取得玩家座標
            Vector3 TargetPos = new Vector3(PlayerPos.x - joy.Horizontal, PlayerPos.y, PlayerPos.z - joy.Vertical);
            //目標座標= 新三維向量(玩家.X+搖桿水平,0(會吃土)改為玩家Y軸高度(可避免吃土),玩家.Y+搖桿垂直)
            Target.position = TargetPos;
            PlayerRig.transform.LookAt(Target);//面向目標
        }

        if (joy.Horizontal != 0 || joy.Vertical != 0)//搖桿的水平與垂直不等於0播放走路動畫
        {
            PlayerAim.SetBool("Run", true);
        }
        else //反之停止播放走路動畫
        {
            PlayerAim.SetBool("Run", false);
        }
       
       // PlayerAim.SetBool("Run", joy.Horizontal != 0 || joy.Vertical != 0);利用邏輯運算值(比較運算值)為布林值的特性
    }
    #region 玩家狀態方法
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(_PlayerData.AttackDelay);
        PlayerAim.SetTrigger("Attack");
        
       
    }
    /// <summary>
    /// 玩家受傷(傷害值) 顯示傷害值 更新血條 呼叫死亡
    /// </summary>
    /// <param name="damage"></param>
    public void Hurt(float damage)
    {
        int _Random = Random.Range(0, 100);
        float Critical = damage * Random.Range(2, 5);
        if (_Random<30)
        {
            _PlayerData.hp -= Critical;
            StartCoroutine(_HPControl.UpdateDamage(Critical));
        }
        else
        {
        _PlayerData.hp -= damage;
        StartCoroutine(_HPControl.UpdateDamage(damage));
        }
        _PlayerData.hp = Mathf.Clamp(_PlayerData.hp, 0, 999);
        _HPControl.UpdateHPbar(_PlayerData.HP_Max, _PlayerData.hp);
        print(_PlayerData.hp);
        if (_PlayerData.hp==0) Die();
       


    }
    /// <summary>
    /// 玩家死亡方法
    /// </summary>
    public void Die()
    {
        // PlayerAim.SetBool("Die", _PlayerData.hp == 0); 寫法1
        if (PlayerAim.GetBool("Die")) return;//如果角色已死亡跳出此方法
        PlayerAim.SetBool("Die", true);//寫法2
        //this.enabled = false;是否啟動該腳本(this此類別)
        StartCoroutine(m_level.countdownReborn());
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
