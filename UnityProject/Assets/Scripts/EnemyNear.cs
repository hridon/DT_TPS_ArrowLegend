using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNear : Enemy
{/// <summary>
/// 複寫繼承父類別的方法(例:移動)
/// </summary>
    public override void Move()
    {
        aim.SetBool("Run",true);//開起跑步動畫
        agent.SetDestination(Player.position);//追蹤至玩家位置
    }

}
