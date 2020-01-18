using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFar : Enemy
{[Header("BulletOBj")]
    public GameObject Bullet;
    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(CreateBullet());
    }
    IEnumerator CreateBullet()  
    {
        yield return new WaitForSeconds(data.AttackDelay);
        Vector3 pos = new Vector3(data.attackoffset.x, data.attackoffset.y, data.attackoffset.z);

        Instantiate(Bullet, transform.forward*data.attackoffset.z+pos, Quaternion.identity);
    }
}
