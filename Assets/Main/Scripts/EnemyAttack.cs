using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Transform checkAttack;
    [SerializeField]
    float checkAttackRadius = 0.35f;
    [SerializeField]
    float timeAttack = 1f;
    [SerializeField]
    public float damage;
    float time = 0;
    bool isAttack = true;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckBeAttacked();
    }

    void CheckBeAttacked()
    {
        Collider2D[] collider2s = Physics2D.OverlapCircleAll(checkAttack.position, checkAttackRadius);
        if(collider2s.Length == 0)
        {
            time = 0;
        }
        for (int i = 0; i < collider2s.Length; i++)
        {
            if (collider2s[i].gameObject.name.Contains("Player"))
            {
                time += Time.deltaTime;
                if(time > timeAttack)
                {
                    PlayerControl playerControl = collider2s[i].gameObject.GetComponent<PlayerControl>();
                    playerControl.BeAttack((int)damage);
                    time = 0;
                } 
                else
                {
                    isAttack = false;
                }
            }

        }

    }
}
