using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    int inventory = 0;
    public GameObject[] gameObjects;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if (inventory>0)
               inventory -= 1;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (inventory < gameObjects.Length)
                inventory += 1;
        }
        for(int i=0;i<gameObjects.Length;i++)
        {
            if(inventory == i)
            {
                gameObjects[i].SetActive(true);
            }
            else
            {
                gameObjects[i].SetActive(false);
            }
        }
    }
}
