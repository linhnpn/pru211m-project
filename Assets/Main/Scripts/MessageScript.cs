using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageScript : MonoBehaviour
{
    public string message;
    float timeAppear = 2f;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        FindObjectOfType<AudioManagement>().Play("LevelUp");
        transform.position = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time < timeAppear)
        {
            Vector2 vector2 = GameObject.Find("Player").transform.position;
            transform.position = new Vector2(vector2.x, vector2.y + time);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
