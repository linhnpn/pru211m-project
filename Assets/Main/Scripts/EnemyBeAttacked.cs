using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeAttacked : MonoBehaviour
{

    [SerializeField]
    public float MaxHealth;

    [SerializeField]
    GameObject[] items;

    float CurrentHealth;
    GameObject HealthBar;
    Animator animator;

    bool IsAttacked;
    float timeHit = 0.1f;
    float time;
    float timeEndHit;

    bool IsDead;
    float timeDead = 0.5f;
    float time2;
    float timeEndDead;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        HealthBar = transform.Find("HealthBarCanvas").gameObject.transform.Find("HealthBar").gameObject;
        animator = GetComponent<Animator>();
        animator.enabled = true;
        IsAttacked = false;
        IsDead = false;
        time = 0;
        time2 = 0;
    }

    // Update is called once per frame
    void Update()
    {    
        time += Time.deltaTime;
        if (IsAttacked && time >= timeEndHit && !IsDead)
        {
            IsAttacked=false;
            animator.Play("Run");
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
                Destroy(gameObject);
            }
        }
    }

    public void BeAttacked(float damage)
    {
        if (!IsDead)
        {
            CurrentHealth -= damage;
        }
        else
        {
            return;
        }
        if (CurrentHealth <= 0)
        {
            IsDead = true;
            animator.Play("Dead");
            FindObjectOfType<AudioManagement>().Play("Hit1");
            DropItemOnDead();
            if (gameObject.name.Contains("Enemy 3"))
            {
                GameObject.Find("Main Camera").GetComponent<SpamEnemy>().IncreaseHard();
                GameObject.Find("Player").GetComponent<PlayerControl>().bossEnemyCount++;
            }
            else
            {
                GameObject.Find("Player").GetComponent<PlayerControl>().normalEnemyCount++;
            }
            Destroy(HealthBar);
            Destroy(gameObject.GetComponent<EnemyMovement>());
            Destroy(gameObject.GetComponent<EnemyAttack>());
            timeEndDead = time2 + timeDead;
            Vector2 vector2 = gameObject.transform.position;
            gameObject.transform.position = new Vector2(vector2.x, vector2.y + 1f);
        }
        else
        {
            animator.Play("Hit");
            FindObjectOfType<AudioManagement>().Play("Hit1");
            Vector2 vector2 = HealthBar.GetComponent<RectTransform>().sizeDelta;
            HealthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(CurrentHealth / MaxHealth, vector2.y);
            Vector2 vector21 = HealthBar.GetComponent<RectTransform>().position;
            HealthBar.GetComponent<RectTransform>().position = new Vector2(vector21.x - (damage / (MaxHealth * 2)), vector21.y);
            IsAttacked=true;
            timeEndHit = time + timeHit;
        }
    }

    void DropItemOnDead()
    {
        if(!gameObject.name.Contains("Enemy 3"))
        {
            DropItemWithRate("Health", 0.1f);
        }
        else
        {
            DropItemWithRate("Box", 1f);
        }
    }

    void DropItemWithRate(string itemName, float rate)
    {
        float rs = UnityEngine.Random.Range(0, 100) + 1;
        if (rs <= rate * 100)
        {
            GameObject droppedItem = Instantiate(Array.Find(items, i => i.name == itemName));
            droppedItem.name = itemName;
            Vector2 vector2 = gameObject.transform.position;
            droppedItem.transform.position = new Vector2(vector2.x, vector2.y + 2);
            droppedItem.gameObject.SetActive(true);
            FindObjectOfType<AudioManagement>().Play("Win");
        }
    }
}
