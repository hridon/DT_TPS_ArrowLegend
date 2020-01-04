using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region 屬性
    public EnemyData data;
    public NavMeshAgent agent;
   public Transform Player;
    public Animator aim;
    /// <summary>
    /// 計時器
    /// </summary>
    public float timer;
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
    public virtual void Move() 
    {
    }
    public virtual void Wait() 
    {
    }
    public virtual void Attack() 
    { 
    }
    public  void Hit() 
    {
    }
    public void Dead() 
    {
    }
    #endregion
}
