
using UnityEngine;

public class ItemManager : MonoBehaviour
{[Header("通關")]
    public bool Pass;
    [Header("玩家")]
    public Transform Player;
   //Header("道具音效")]
 // public AudioClip clip;
  //public AudioSource aud;
    private void Start()
    {
        Player = GameObject.Find("Player").transform;
        HandleCollison();
    }
    void HandleCollison()
    {
        Physics.IgnoreLayerCollision(10, 8);//忽略碰撞(圖層1,圖層2)
        Physics.IgnoreLayerCollision(10, 9);
      //Physics.IgnoreLayerCollision(10, 10);
    }
    public void GoToPlayer()
    {
        if (Pass)
        {
            transform.position = Vector3.Lerp(transform.position, Player.position, 0.3f * 10 * Time.deltaTime);
           //nvoke("gnoreCollision, 0.1f);
            if (Vector3.Distance(transform.position, Player.position)<2)//如果金幣與玩家距離<指定距離
            {
                Destroy(gameObject, 0.01f);
            }
        }
    }
    public void IgnoreCollision()
    {
        gameObject.GetComponent<Collider>().isTrigger = true;
    }

    private void Update()
    {
        GoToPlayer();
    }
}
