using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    // Start is called before the first frame update
    float time;
    float timeDropped = 0.5f;
    void Start()
    {
        if(gameObject.name == "Box")
        {
            gameObject.GetComponent<Animator>().Play("Unopen_box");
        }
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time < timeDropped)
        {
            Vector2 vector2 = transform.position;
            transform.position = new Vector2(vector2.x, vector2.y - time * 0.1f);
        }
    }
}
