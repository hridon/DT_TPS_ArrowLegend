
using UnityEngine;
[CreateAssetMenu(fileName ="玩家資料",menuName ="DT/玩家資料")]
public class PlayerData : ScriptableObject
{
    #region 屬性
    [Header("目前血量"), Range(0, 999)]
    public float hp = 200;
    [Header("玩家最大血量"), Range(0, 9999)]
    public float HP_Max=200;
    [Header("攻擊力"), Range(0, 999)]
    public float attack = 10;
    //[Header("移動速度"), Range(0, 10)]
    //public float Speed = 1.5f;
    [Header("CD時間"), Range(0, 60)]
    public float CD = 3.5f;
    [Header("攻擊距離"), Range(0, 10)]
    public float AttackDistance;
    [Header("攻擊延遲")]
    public float AttackDelay;
    [Header("子彈力道")]
    public float Force;
    #endregion

    #region 事件
    #endregion

    #region 方法
    #endregion
}
