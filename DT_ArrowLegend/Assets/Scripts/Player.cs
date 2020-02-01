
using UnityEngine;
using System.Collections;
using System.Collections.Generic;//引用 系統 集合 一般
using System.Linq;//查詢語法 LinQ(Qurery)

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
    public float timer;
    Transform Friepoint;
   [Header("玩家發射的武器")]
    public GameObject Axe;
    /// <summary>
    /// 場景上的敵人
    /// </summary>
   // private Enemy[] enemies;//ˇ一般陣列 不可動態修改
    public List<Enemy> enemies;//清單 
   /// <summary>
   /// 敵人距離清單
   /// </summary>
   public List<float> enemiesDis;
    /// <summary>
    /// 子彈可生成數量
    /// </summary>
    float AttackCount;
    /// <summary>
    /// 子彈最大可生成數量
    /// </summary>
    float AttackCountMax;
    
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
        _HPControl = transform.Find("血條系統").GetComponent<HPbarControl>();//1變形.尋找(子物件)// _PlayerData.hp = _PlayerData.HP_Max;
        Friepoint = transform.Find("武器生成位置");
        
    }
    private void Start()
    {
       


    }
    private void Update()
    {
        //print("水平位置"+joy.Horizontal);
        //print("垂直位置" + joy.Vertical);
       
        
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
            if (_PlayerData.hp > 0 )
            {
                Sight();
            }
            PlayerAim.SetBool("Run", false);
           
        }

       
       
       // PlayerAim.SetBool("Run", joy.Horizontal != 0 || joy.Vertical != 0);利用邏輯運算值(比較運算值)為布林值的特性
    }
    #region 玩家狀態方法
   /// <summary>
   /// 瞄準目標
   /// </summary>
    private void Sight()
    {
       

        if (joy.Horizontal == 0 || joy.Vertical == 0)//搖桿的水平與垂直等於0播放攻擊動畫
        {
            if (timer < _PlayerData.CD)
            {
                timer += Time.deltaTime;
            }
            else
            {
          
               // yield return new WaitForSeconds(2f)
                enemies.Clear();//先清除清單
                enemies = FindObjectsOfType<Enemy>().ToList();//加入清單
                if (enemies.Count == 0) return;
                print(m_level.TargetCount);
                //陣列數量 length 
                //清單數量 count
                enemiesDis.Clear();
                for (int i = 0; i < enemies.Count; i++)
                {
                  float Dis=  Vector3.Distance(transform.position, enemies[i].transform.position);
                    enemiesDis.Add(Dis);
                }
                //最短距離
                float min = enemiesDis.Min();
                //最短距離編號
                int index = enemiesDis.IndexOf(min);
                Vector3 enemiesTarget = enemies[index].transform.position;
                enemiesTarget.y = transform.position.y;
                transform.LookAt(enemiesTarget);
                AttackCount = 1;
                //播放攻擊與生成子彈
                Invoke("Attack", _PlayerData.AttackDelay);
            }
        }
        else
        {
            PlayerAim.SetBool("Run", true);
        }
       
    }
    private void Attack()
    {
        
        if (AttackCount==1)
        {
            PlayerAim.SetTrigger("Attack");
            GameObject Bullet = Instantiate(Axe, Friepoint.position, Friepoint.rotation);
            Bullet.AddComponent<BulletControl>();
            Bullet.GetComponent<Rigidbody>().AddForce(transform.forward * _PlayerData.Force);
            Bullet.GetComponent<BulletControl>().damage = _PlayerData.attack;
            Bullet.GetComponent<BulletControl>().isPlayer = true;
        }
       
        timer = 0;
        
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
        _HPControl.UpdateHPbar(_PlayerData.HP_Max, _PlayerData.hp);//更新血條
       
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
        StopCoroutine(m_level.countdownReborn());
    }
/// <summary>
/// 玩家復活方法
/// </summary>
    public void Reborn()
    {
        m_level.HidePanelReBorn();
        StopCoroutine(m_level.countdownReborn());
        _PlayerData.hp = _PlayerData.HP_Max;
        _HPControl.UpdateHPbar(_PlayerData.HP_Max, _PlayerData.hp);
        PlayerAim.SetBool("Die", false);
        //this.enabled = true;
       
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
