using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{[Header("子彈傷害")]
    public float damage;
    /// <summary>
    /// 是否為玩家持有
    /// </summary>
    public bool isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        //damage = GameObject.Find("Dragon").GetComponent<DragonControl>().data.attack;
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider hit)
    {
        if (!isPlayer)
        {
            if (hit.tag == "Player")
            {
                hit.GetComponent<Player>().Hurt(damage);
                Destroy(gameObject, 0.1f);
                if (hit.GetComponent<Player>()._PlayerData.hp <= 0)
                {
                    GameObject.Find("Dragon").GetComponent<DragonControl>().data.CanAttack = false;
                }
            }
        }
        else
        {
            if (hit.GetComponent<Collider>().tag == "Enemy")
            {
                hit.GetComponent<Enemy>().Hit(damage);
                Destroy(gameObject, 0.1f);
            }

        }
    }
}
