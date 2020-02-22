
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
   
{/// <summary>
/// 玩家資料
/// </summary>
    public PlayerData data;
    public void LoadLevel()
    {
        data.hp = data.HP_Max;
        SceneManager.LoadSceneAsync("關卡1");
    }
   
 public void BuyHP ()
    {

        data.HP_Max += 500;
        data.hp = data.HP_Max;
    }

   

}
