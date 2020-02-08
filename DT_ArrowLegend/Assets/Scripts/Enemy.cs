using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region 屬性
    public EnemyData data;
    protected NavMeshAgent agent;//protected 僅允許子類別使用(不顯示於面板上)
    protected Transform Player;
    protected Animator aim;
    /// <summary>
    /// 計時器
    /// </summary>
    protected float timer;
     HPbarControl _HPControl;
    float hp;//個別血量
    public LevelManager m_level;
    public Player _player;
    #endregion

    #region 事件
    private void Start()
    {
        m_level = FindObjectOfType<LevelManager>();//透過類型取得元件
        aim = GetComponent<Animator>();//先取得元件 再做設定
        agent = GetComponent<NavMeshAgent>();
        //將代理器的移動速度=敵人資料的速度
        agent.speed = data.Speed;
        Player = GameObject.Find("Player").transform;
        _player = GameObject.Find("Player").GetComponent<Player>();
        // agent.SetDestination(Player.position);
        _HPControl = transform.Find("血條系統").GetComponent<HPbarControl>();
        hp = data.HP_Max;
        _HPControl.UpdateHPbar(data.HP_Max,hp);
        data.CanAttack = true;
    }
    private void Update()
    {
        Move();
    }
    #endregion

    #region 方法

    //virtual關鍵字可用來修改方法、屬性、索引子或事件宣告，並可在衍生類別中受到覆寫。 例如，繼承這個方法的任何類別均可將其覆寫
    protected virtual void Move()
    {
        agent.SetDestination(Player.position);//追蹤至玩家位置
        Vector3 posTarget = Player.position;
        posTarget.y= transform.position.y;
        transform.LookAt(posTarget);
        if (agent.remainingDistance <= data.StopDistance)//如果距離<=指定距離則...
        {
            Wait();
        }
        else
        {
            agent.isStopped = false;//迭代器是否停止
            aim.SetBool("Run", true);//開起跑步動畫
        }
    }
    protected virtual void Wait()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        aim.SetBool("Run", false);//關閉跑步動畫
        if (timer <= data.CD)//如果計時器<=冷卻時間
        {
            timer += Time.deltaTime;//先累加
                                    // print("時間" + timer);
        }
        else
        {
            Attack();
        }
    }
    protected virtual void Attack()
    {
        if (data.CanAttack)
        {
            timer = 0;//計時歸零
            aim.SetTrigger("Attack");
          
        }
    }
    public  void Hit(float damage) 
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, 999);
        _HPControl.UpdateHPbar(data.HP_Max, hp);
        StartCoroutine(_HPControl.UpdateDamage(damage));
        if (hp == 0) Dead();
    }
   /// <summary>
   /// 怪物死亡方法
   /// </summary>
    public void Dead()
    {
       // if (aim.GetBool("Die")) return;
        aim.SetBool("Die", true);
       // m_level.TargetCount -= 1;
        _player.enemies.Clear();
        Destroy(gameObject, 0.8f);
    //this.enabled = false;// 是否啟動該腳本
   
    }
    #endregion
}
