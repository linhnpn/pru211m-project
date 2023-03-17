using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousePos;
    Rigidbody2D rb;
    [SerializeField]
    public float force;
    [SerializeField]
    Transform checkAttack;
    float Damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        Damage = GameObject.Find("Weapon 4").GetComponent<Shoot>().Damage;
    }

    // Update is called once per frame
    void Update()
    {
        Damage = GameObject.Find("Weapon 4").GetComponent<Shoot>().Damage;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkAttack.position, 0.17f);
        for (int i = 0; i < colliders.Length; i++)
        {
            GameObject go = colliders[i].gameObject;
            if (go.name.Contains("Enemy"))
            {
                Animator animator = go.GetComponent<Animator>();
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
                {
                    go.GetComponent<EnemyBeAttacked>().BeAttacked(Damage);
                    Destroy(gameObject);
                }
            }

            if (go.name.Contains("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }


}
