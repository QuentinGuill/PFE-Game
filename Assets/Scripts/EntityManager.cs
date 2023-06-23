using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityManager : MonoBehaviour
{
    public List<GameObject> entities;
    public PlayerControler pc;

    void Awake()
    {
        this.entities = new List<GameObject>();
        StartCoroutine(generateEnnemies());
    }

    void FixedUpdate()
    {
        foreach(GameObject e in entities)
        {
            if(e.GetComponent<IEntity>().isDead())
            {
                entities.Remove(e);
                Destroy(e);
            }
        }
        if (pc.isDead())
        {
            SceneManager.LoadScene("DeathScreen");
        }
    }

    private IEnumerator generateEnnemies()
    {
        while(true)
        {
            if(entities.Count < 20)
            {
                for (int i = 0; i < 4; i++)
                {
                    float angle = Random.Range(0f, 1f) * Mathf.PI * 2f;
                    Vector3 pos = new Vector3(pc.transform.position.x + Mathf.Cos(angle) * 15f, pc.transform.position.y + Mathf.Sin(angle) * 15f, 0f);
                    entities.Add((GameObject)Instantiate(Resources.Load("Boxes/Pot"), pos, Quaternion.identity));
                }
                for (int i = 0; i < 3; i++)
                {
                    float angle = Random.Range(0f, 1f) * Mathf.PI * 2f;
                    Vector3 pos = new Vector3(pc.transform.position.x + Mathf.Cos(angle) * 15f, pc.transform.position.y + Mathf.Sin(angle) * 15f, 0f);
                    entities.Add((GameObject)Instantiate(Resources.Load("Ennemy/Ennemy"), pos, Quaternion.identity));
                }
            }
            
            yield return new WaitForSeconds(20f);
        }
    }
}
