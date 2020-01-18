using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{[Header("子彈傷害")]
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = GameObject.Find("Dragon").GetComponent<DragonControl>().data.attack;
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag=="Player")
        {
            hit.GetComponent<Player>().Hurt(damage);
            if (hit.GetComponent<Player>()._PlayerData.hp <= 0)
            {
                GameObject.Find("Dragon").GetComponent<DragonControl>().data.CanAttack = false;
            }
        }

    }
}
