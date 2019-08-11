using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float hp;
    public float hpMax;
    public float Damage;
    public float GCD;

    public GameObject player;
    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        GCD = 1.5f;
        hpMax = 100.0f;
        hp = hpMax;
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (hp <= 0.0f) {
            Destroy(gameObject);
        }



        //GLOBAL COOLDOWN TIMER
        if (GCD > 0.0f)
        {
            GCD -= Time.deltaTime;
        }
        else
        {
            GCD = 0.0f;
        }

        if (CheckDistanceToPlayer() < 3.0f)
        {
            if (GCD == 0.0f) {
                DealDamage(Damage);
                GCD = 1.5f;
                Debug.Log("in Range");
            }

            if (pc.attacking == true)
            {
                TakeDamage(pc.damage);
            }
        } else
        {
            Debug.Log("out of range");
        }


    }

    public void TakeDamage(float dmgTaken)
    {
        if(hp > 0)
        {
            hp -= dmgTaken;
        } else {
            Destroy(gameObject);
        }
        
    }

    private void DealDamage(float dmgDealt)
    {
        pc.TakeDamage(dmgDealt);
    }

    private float CheckDistanceToPlayer() {

        return Vector2.Distance(transform.position, player.transform.position);

    }
}
