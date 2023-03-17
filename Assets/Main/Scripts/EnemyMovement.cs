using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    [SerializeField]
    public float speed;
    [SerializeField]
    protected float maxDistance = 0.05F;
    bool facingLeft = true;
    SpriteRenderer spriteRenderer;

    bool isDead = false;
    void Start()
    {
        player = GameObject.Find("Player");
        spriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 distance = player.transform.position - transform.position;

        Vector3 target = playerPosition - distance.normalized * this.maxDistance; ;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, speed * Time.deltaTime);


        if(!facingLeft && playerPosition.x > transform.position.x)
        {
            Filp();
        } else if(facingLeft && playerPosition.x < transform.position.x)
        {
            Filp(); 
        }
    }

    void Filp()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
