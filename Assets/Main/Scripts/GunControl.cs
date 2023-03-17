using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    private Vector3 mousePos;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject weapon;

    bool facingLeft = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0)
        {
            return;
        }
        facingLeft = player.GetComponent<SpriteRenderer>().flipX;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;
        weapon.GetComponent<SpriteRenderer>().flipY = facingLeft;

        float rot2 = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        if (facingLeft)
        {
            if (rot2 < 90 && rot2 > 0)
            {
                rot2 = 90;
            }
            else if (rot2 > -90 && rot2 < 0)
            {
                rot2 = -90;
            }
        }
        else
        {
            if (rot2 > 90 && rot2 > 0)
            {
                rot2 = 90;
            }
            else if (rot2 < -90 && rot2 < 0)
            {
                rot2 = -90;
            }
        }


        transform.rotation = Quaternion.Euler(0, 0, rot2);

    }
}
