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
    #endregion

    #region 事件
    private void Start()
    {
        aim = GetComponent<Animator>();//先取得元件 再做設定
        agent = GetComponent<NavMeshAgent>();
        //將代理器的移動速度=敵人資料的速度
        agent.speed = data.Speed;
        Player = GameObject.Find("Player").transform;
        // agent.SetDestination(Player.position);
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
    public  void Hit() 
    {
    }
    public void Dead() 
    {
    }
    #endregion
}
