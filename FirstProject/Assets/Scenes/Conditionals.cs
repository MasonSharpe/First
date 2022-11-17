using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditionals : MonoBehaviour
{
    int level = 3;
    public int xp = 10;
    public int levelUpXP = 15;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (xp >= levelUpXP)
        {
            level++;
            xp -= levelUpXP;
            levelUpXP += 10;
            Debug.Log("Level Up! You are now level: " + level + "!");
        }
            
    }
}
