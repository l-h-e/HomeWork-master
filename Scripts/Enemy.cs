using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float   health;      // Enemy's HP
    private float damageRate;
    public GameObject Explosion;


    void Start() {
        // Temp
        health     = 100;
        damageRate = 100;
    }

    void Update() {
    }

    public void GetDamage() {
        health -= damageRate * Time.deltaTime;
        if(health < 0)
            this.Death();
    }

    private void Death() {

        //gameObject.SendMessage("KilledEnemy");
        GameObject buildedExplosion=  Instantiate(Explosion,transform.position,Quaternion.identity);
        Destroy(buildedExplosion,2);
        GetComponentInParent<CheckPoint>().KilledEnemy();

        Destroy(gameObject);

    }
}
