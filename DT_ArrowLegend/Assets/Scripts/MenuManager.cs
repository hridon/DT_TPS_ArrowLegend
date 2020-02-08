
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{public void LoadLevel() 
    {
        SceneManager.LoadSceneAsync("關卡1");
    }
   
}
