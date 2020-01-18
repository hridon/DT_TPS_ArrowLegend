
using UnityEngine;
//建立素材選項(檔案名稱,選項名稱)
[CreateAssetMenu(fileName ="敵人資料",menuName = "DT/EnemyData")]
public class EnemyData : ScriptableObject //腳本化物件 將資料儲存於專案中
{
    #region 屬性
   [Header("血量"),Range(0,999)]
    public float hp = 100;
    [Header("最大血量"), Range(0, 9999)]
    public float HP_Max = 100;
    [Header("攻擊力"), Range(0, 999)]
    public float attack = 10;
    [Header("移動速度"), Range(0, 10)]
    public float Speed = 1.5f;
    [Header("CD時間"), Range(0, 60)]
    public float CD = 3.5f;
    [Header("停止距離")]
    public float StopDistance;
    [Header("攻擊距離")]
    public float AttackDistance;
    [Header("子彈位置")]
    public Vector3 attackoffset;
    [Header("攻擊延遲")]
    public float AttackDelay;
    [Header("攻擊速度")]
    public float AttackSpeed;
    [Header("是否可以攻擊")]
    public bool CanAttack=true;
    #endregion

    #region 事件
    #endregion

    #region 方法
    #endregion
}
