using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public GameObject player;
    Health playerHealth;
    public int health = 10;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.health -= 1;
            player.GetComponent<Health>().slider.value = playerHealth.health;
        } else if (collision.gameObject.tag == "Damage")
        {
            Destroy(collision.gameObject);
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
