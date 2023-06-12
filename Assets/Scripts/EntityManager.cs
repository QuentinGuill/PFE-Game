using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    private List<Entity> entities;

    void Awake()
    {
        
    }

    void FixedUpdate()
    {
        foreach(Entity e in entities)
        {
            if(e.isDead())
            {

            }
        }
    }
}
