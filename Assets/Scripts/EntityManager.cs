using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public List<GameObject> entities;
    public PlayerControler pc;

    void Awake()
    {
        List<Vector3> poses = new List<Vector3>();
        Vector3 pos = pc.GetComponentInParent<Transform>().position;
        poses.Add(pos + new Vector3(Random.Range(0, 10), Random.Range(0, 10), 0f));
        poses.Add(pos + new Vector3(Random.Range(0, 10), Random.Range(0, 10), 0f));
        poses.Add(pos + new Vector3(Random.Range(0,10), Random.Range(0, 10), 0f));

        foreach (Vector3 p in poses)
        {
            if (p != pos)
            {
                entities.Add((GameObject)Instantiate(Resources.Load("Boxes/Pot"), p, Quaternion.identity));
            }
        }
    }

    void FixedUpdate()
    {
        foreach(GameObject e in entities)
        {
            if(e.GetComponent<IEntity>().isDead())
            {
                Destroy(e);
                entities.Remove(e);
            }
        }
    }
}
