using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    [SerializeField]
    float force;
    [SerializeField]
    Transform checkAttack;
    [SerializeField]
    public float Damage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkAttack.position, 0.17f);
        for (int i = 0; i < colliders.Length; i++)
        {
            GameObject go = colliders[i].gameObject;
            if (go.name.Contains("Player"))
            {
                go.GetComponent<PlayerControl>().BeAttack((int)Damage);
                Destroy(gameObject);
            }

            if (go.name.Contains("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetBulletMove(Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * force;
    }
}
