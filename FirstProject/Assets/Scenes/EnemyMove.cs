using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public GameObject player;
    public float close = 5.0f;
    public float speed = 2.0f;


    // Update is called once per frame
    void Update()
    {
        Vector3 playerDir = player.transform.position - transform.position;
        float playerDist = playerDir.magnitude;
        playerDir.Normalize();

        if (playerDist <= close)
        {
            GetComponent<Rigidbody2D>().velocity = playerDir * speed;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            playerDir = Vector3.zero;
        }
        GetComponent<Animator>().SetFloat("xInput", playerDir.x);
        GetComponent<Animator>().SetFloat("yInput", playerDir.y);
    }
}
