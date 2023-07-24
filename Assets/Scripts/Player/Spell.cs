using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : PlayerSkillDamage
{
    public GameObject Explosion;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        if (enemyLayer == (1 << LayerMask.NameToLayer("Enemy")))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            transform.rotation = Quaternion.LookRotation(player.transform.forward);
        }
        else if (enemyLayer == (1 << LayerMask.NameToLayer("Player")))
        {
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");
            transform.rotation = Quaternion.LookRotation(boss.transform.forward);
        }
    }

    // Update is called once per frame
    internal override void Update()
    {
        base.Update();
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        if (colided)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
