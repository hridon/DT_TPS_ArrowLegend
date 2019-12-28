
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    #region 屬性
    /// <summary>
    /// 玩家位置
    /// </summary>
    Transform PlayerTrm;
   [Header("跟隨速度"),Range(1,10f)]
    public float FallowSpeed;
   [Header("限制範圍")]
    public float Top,Bottom;
    #endregion
    #region 事件
    private void Start()
    {
        PlayerTrm = GameObject.Find("Player").transform;
    }
    #endregion
   /// <summary>
   /// 延遲更新 
   /// </summary>
    private void LateUpdate()
    {
        Track();
    }
    #region 方法
    /// <summary>
    /// 攝影機追蹤玩家
    /// </summary>
    void Track() 
    {
        Vector3 PlayerPos = PlayerTrm.position;
        Vector3 CameraPos = transform.position;
        PlayerPos.x = 0;//固定X軸
        PlayerPos.y = 37;//固定Y軸
        PlayerPos.z += 33;//固定Z軸
        PlayerPos.z=Mathf.Clamp(PlayerPos.z, Top, Bottom);
        transform.position = Vector3.Lerp(CameraPos, PlayerPos, 0.5f * FallowSpeed);
        //變形.座標=Vector3.差值(攝影機,玩家,百分比)
       
    }
    #endregion
}
