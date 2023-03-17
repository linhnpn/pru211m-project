using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;
    public Vector3 offset;
   

    void Update()
    {
        //transform.position = player.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManagement>().Play("NormalBattle");
    }

    private void LateUpdate()
    {
        if (!player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player_dead"))
        {
            Vector3 vector3 = transform.position;
            vector3.x = player.gameObject.transform.position.x;
            transform.position = player.gameObject.transform.position;
        }
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
