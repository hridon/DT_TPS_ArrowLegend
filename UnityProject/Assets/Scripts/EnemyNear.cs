using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNear : Enemy
{/// <summary>
///override 複寫繼承父類別的方法(例:移動)
/// </summary>
    public override void Move()
    {
        
        agent.SetDestination(Player.position);//追蹤至玩家位置
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
    public override void Wait()
    {//base.Wait() //使用父類別方法內容
        
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            aim.SetBool("Run", false);//關閉跑步動畫
        if (timer<=data.CD)//如果計時器<=冷卻時間
        {
            timer += Time.deltaTime;//先累加
           // print("時間" + timer);
        }
        else 
        {
            Attack();
        }
        
    }
    public override void Attack()
    {
        if (data.CanAttack)
        {
        timer = 0;//計時歸零
        aim.SetTrigger("Attack");
        StartCoroutine(DelayAttack());
        }
    }
    //ctrl+MO快速折疊
    //ctrl+ML快速展開
   /// <summary>
   /// 延遲傷害協程
   /// </summary>
   /// <returns></returns>
    IEnumerator DelayAttack() 
    {
        yield return new WaitForSeconds(data.AttackDelay);
        RaycastHit _hit;//射線碰撞資訊.存放碰到的內容

        if (Physics.SphereCast(transform.position + new Vector3(0, 1, 0),data.AttackDistance/2,transform.forward,out _hit , data.AttackDistance))
        {
            _hit.collider.GetComponent<Player>().Hurt(data.attack);
            if (_hit.collider.GetComponent<Player>()._PlayerData.hp <= 0)
            {
                data.CanAttack = false;
            }
           
            //print(_hit.collider.gameObject+"受到"+ data.attack+"傷害");//碰撞到的物件名稱
            //print("玩家血量" + _hit.collider.GetComponent<Player>().HP);
        } 
    }
    private void OnDrawGizmos()
    {//前方 transform.forward
     //右方 transform.right
     //上方 transform.up
       //在場景上繪製射線
        Gizmos.color = Color.red;//指定射線顏色
        Gizmos.DrawRay(transform.position+new Vector3(0,1,0),transform.forward*data.AttackDistance);//圖示.繪製射線(中心點,方向)
    }

}
