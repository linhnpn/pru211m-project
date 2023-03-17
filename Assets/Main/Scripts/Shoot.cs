using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    GameObject bulletGameObject;
    [SerializeField]
    float speed;
    float time = 1f;
    [SerializeField]
    public float timeShoot;
    [SerializeField]
    public float Damage;

    Vector2 moving;

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale <= 0)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            time += Time.deltaTime;
            if(time >= timeShoot)
            {
                GameObject bullet = Instantiate(bulletGameObject);
                bullet.name = "Bullet";
                bullet.transform.position = transform.position;
                bullet.gameObject.SetActive(true);
                time = 0;
                FindObjectOfType<AudioManagement>().Play("Player_shoot");
            } 
        }
    }
}
