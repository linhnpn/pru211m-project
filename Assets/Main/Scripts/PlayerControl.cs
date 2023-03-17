using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    Animator animator;
    [SerializeField]
    UIDocument uIDocument;
    [SerializeField]
    public float maxHp;
    float currentHp;
    GameObject RotePoint;
    Rigidbody2D rigidbody;
    Vector2 moving;
    bool facingLeft = false;
    Vector3 mousePos;

    float timeHit = 0.1f;
    float time;
    float timeEndHit;
    bool IsAttacked;
    Color originalColor;


    bool IsDead;
    float timeDead = 0.5f;
    float time2;
    float timeEndDead;

    public int normalEnemyCount;
    public int bossEnemyCount;
    // Start is called before the first frame update
    void Start()
    {
        RotePoint = transform.Find("RotePoint").gameObject;
        rigidbody = GetComponent<Rigidbody2D>();
        currentHp = maxHp;
        IsAttacked = false;
        time = 0;
        IsDead = false;
        time2 = 0;
        originalColor = gameObject.GetComponent<Renderer>().material.color;
        normalEnemyCount = 0;
        bossEnemyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //if (Time.timeScale <= 0)
        //{
        //    return;
        //}
        if (!IsDead && Time.timeScale > 0)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = new Vector2(horizontal, vertical);

            if (mousePos.x < transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (mousePos.x > transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }

            time += Time.deltaTime;
            if (IsAttacked && time >= timeEndHit)
            {
                IsAttacked = false;
                gameObject.GetComponent<Renderer>().material.color = originalColor;
            }

            FindObjectOfType<GameController>().UpdateScore(normalEnemyCount, bossEnemyCount);
        }
        

        time2 += Time.deltaTime;
        if (IsDead)
        {
            if (time2 < timeEndDead)
            {
                Vector2 vector2 = gameObject.transform.position;
                gameObject.transform.position = new Vector2(vector2.x, vector2.y - 0.015f);
            }
            else
            {

                FindObjectOfType<GameController>().SetLoseScreen(normalEnemyCount, bossEnemyCount);
            }

        }
    }

    private void FixedUpdate()
    {
        if (!IsDead)
        {
            MoveObject(moving);
        }
    }

    void MoveObject(Vector2 direction)
    {
        rigidbody.MovePosition(rigidbody.position + direction * speed * Time.deltaTime);
        animator.SetFloat("Speed", direction != Vector2.zero ? speed : 0);
    }

    public void BeAttack(int damage)
    {
        if (!IsDead)
        {
            GameController gameController = uIDocument.GetComponent<GameController>();
            currentHp -= damage;
            gameController.UpdateHealth(currentHp, maxHp);
            FindObjectOfType<AudioManagement>().Play("Hit1");
            if (currentHp <= 0)
            {
                gameObject.GetComponent<Renderer>().material.color = originalColor;
                animator.Play("Player_dead");
                Destroy(RotePoint);
                IsDead = true;
                timeEndDead = time2 + timeDead;
                Vector2 vector2 = gameObject.transform.position;
                gameObject.transform.position = new Vector2(vector2.x, vector2.y + 1f);
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255, 135);
                IsAttacked = true;
                timeEndHit = time + timeHit;
            }
        }
    }

    public void BeHealed(int heal)
    {
        if (!IsDead)
        {
            GameController gameController = uIDocument.GetComponent<GameController>();
            currentHp += heal;
            if (currentHp > maxHp)
            {
                currentHp = maxHp;
            }
            FindObjectOfType<AudioManagement>().Play("LevelUp");
            gameController.UpdateHealth(currentHp, maxHp);
            gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255, 135);
            IsAttacked = true;
            timeEndHit = time + timeHit;
        }
    }

    public void IncreaseMaxHp(int hp)
    {
        if (!IsDead)
        {
            GameController gameController = uIDocument.GetComponent<GameController>();
            currentHp += hp;
            maxHp += hp;
            gameController.UpdateHealth(currentHp, maxHp);
            gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255, 135);
            IsAttacked = true;
            timeEndHit = time + timeHit;
        }
    }
}
