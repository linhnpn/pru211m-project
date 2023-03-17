using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    GameObject message;

    bool IsOpen;
    float timeOpen = 2f;
    float time;
    float timeEndOpen;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        time = 0;
        IsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (IsOpen && time >= timeEndOpen)
        {
            IsOpen = false;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.name.Equals("Player"))
        {
            Animator animator = go.GetComponent<Animator>();
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Player_dead"))
            {
                Destroy(gameObject.GetComponent<Collider2D>());
                GiveEffect(gameObject.name);
            }
        }
    }

    void GiveEffect(string name)
    {
        switch (name)
        {
            case "Health":
                {
                    PlayerControl playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
                    playerControl.BeHealed((int)(playerControl.maxHp * 0.1));
                    Destroy(gameObject);
                    break;
                }
            case "Box":
                {
                    gameObject.GetComponent<Animator>().Play("Open_box");
                    GiveRandomBuff();
                    IsOpen = true;
                    timeEndOpen = time + timeOpen;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    void GiveRandomBuff()
    {
        int index = UnityEngine.Random.Range(0, 4);
        switch (index)
        {
            case 0:
                {
                    //Attack Damage + 2
                    GameObject.Find("Weapon 4").GetComponent<Shoot>().Damage += 2;
                    GiveMessage("Attack Damage +2");
                    break;
                }
            case 1:
                {
                    //Attack Speed + 0.5
                    float newTimeShoot = GameObject.Find("Weapon 4").GetComponent<Shoot>().timeShoot;
                    newTimeShoot = (float)(1 / ((1 / newTimeShoot) + 0.5));
                    GameObject.Find("Weapon 4").GetComponent<Shoot>().timeShoot = newTimeShoot;
                    GiveMessage("Attack Speed +0.5");
                    break;
                }
            case 2:
                {
                    //Max Hp +25
                    GameObject.Find("Player").GetComponent<PlayerControl>().IncreaseMaxHp(25);
                    GiveMessage("Max Hp +25");
                    break;
                }
            case 3:
                {
                    //Movement Speed + 0.5
                    GameObject.Find("Player").GetComponent<PlayerControl>().speed += 0.5f;
                    GiveMessage("Movement Speed + 0.5");
                    break;
                }
        }
    }

    void GiveMessage (string givenMessage)
    {
        GameObject pingMessage = Instantiate(message);
        pingMessage.name = "Message";
        pingMessage.GetComponent<TextMeshPro>().text = givenMessage;
        pingMessage.gameObject.SetActive(true);
    }
}
