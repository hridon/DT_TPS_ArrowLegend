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
        Vector3 pos = new Vector3(transform.position.x-data.attackoffset.x, data.attackoffset.y, data.attackoffset.z);
        if (data.CanAttack)
        {
        GameObject Tempbullet=Instantiate(Bullet, pos, Quaternion.identity);
        Tempbullet.GetComponent<Rigidbody>().AddForce(transform.forward * data.AttackSpeed);
        }
       // Tempbullet.AddComponent<BulletControl>(); 給物件添加指定元件
    }
}
