using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    // Start is called before the first frame update
    private const int RUN = 1;
    private const int DEAD = 2;

    int status = 0;
    float timeApear = 2f;
    float checkTime = 0;
    bool isRunning = false;
    float timeShoot = 5f;

    float point = Mathf.Sqrt(2F);
    List<Vector2> pointPosition;
    Animator animator;

    [SerializeField]
    public GameObject bullet;

    [SerializeField]
    public float Damage;
    void Start()
    {
        animator = GetComponent<Animator>();
        pointPosition = new List<Vector2>();
        pointPosition.Add(new Vector2(point, 0));
        pointPosition.Add(new Vector2(point, point));
        pointPosition.Add(new Vector2(0, point));
        pointPosition.Add(new Vector2(-point, point));
        pointPosition.Add(new Vector2(-point, 0));
        pointPosition.Add(new Vector2(-point, -point));
        pointPosition.Add(new Vector2(0, -point));
        pointPosition.Add(new Vector2(point, -point));
    }

    // Update is called once per frame
    void Update()
    {
        checkTime += Time.deltaTime;

        if (!isRunning && checkTime >= timeApear)
        {
            animator.SetInteger("status", RUN);
            isRunning = true;

            foreach (Vector2 point in pointPosition)
            {
                GameObject gameObject = Instantiate(bullet);
                gameObject.GetComponent<EnemyBulletScript>().Damage = Damage;
                gameObject.transform.position = transform.position;
                gameObject.GetComponent<EnemyBulletScript>().SetBulletMove(point);
            }
            checkTime = 0;
        }

        if (isRunning && checkTime >= timeShoot)
        {
            foreach(Vector2 point in pointPosition)
            {
                GameObject gameObject = Instantiate(bullet);
                gameObject.GetComponent<EnemyBulletScript>().Damage = Damage;
                gameObject.transform.position = transform.position;
                gameObject.GetComponent<EnemyBulletScript>().SetBulletMove(point);
            }
            checkTime = 0;
        }




    }
}
