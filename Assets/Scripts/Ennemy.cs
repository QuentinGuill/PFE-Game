using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour, IEntity
{
    private Vector3 lastPlayerPosition;
    private Vector3 nextRandPosition;
    private HealthBar hb;
    private bool dead = false;
    private float speed = 5f;
    private Rigidbody2D rb2d;

    void Awake()
    {
        this.hb = new HealthBar(2);
        StartCoroutine(CheckForPlayer());
        StartCoroutine(ChangeDirection());
        this.rb2d = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (hb.isDead())
        {
            die();
        } else
        {
            move();
        }
    }

    private PlayerControler findPlayer()
    {
        PlayerControler foundPlayer = null;
        LayerMask mask = LayerMask.GetMask("Player");
        ContactFilter2D filter2D = new ContactFilter2D().NoFilter();
        RaycastHit2D hit = Physics2D.CircleCast(this.transform.position, 5f, Vector2.one, 0.0f, mask);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponentInParent<PlayerControler>() != null)
            {
                lastPlayerPosition = hit.collider.transform.position;
                foundPlayer = hit.collider.GetComponentInParent<PlayerControler>();
            }
        }

        return foundPlayer;
    }

    private void move()
    {
        if(lastPlayerPosition != Vector3.zero)
        {
            pathFindToPlayer();
        }
        else if (nextRandPosition != Vector3.zero)
        {
            pathFindToRandom();
        }
    }

    public void pathFindToPlayer()
    {
        if (((int)this.transform.position.x == (int)lastPlayerPosition.x) && ((int)this.transform.position.y == (int)lastPlayerPosition.y))
        {
            lastPlayerPosition = Vector3.zero;
        }
        else
        {
            moveToDestination(lastPlayerPosition);
        }
    }


    public void pathFindToRandom()
    {
        if (((int)this.transform.position.x == (int)nextRandPosition.x) && ((int)this.transform.position.y == (int)nextRandPosition.y))
        {
            nextRandPosition = Vector3.zero;
        }
        else
        {
            moveToDestination(nextRandPosition);
        }
    }

    public void moveToDestination(Vector3 destination)
    {
        Vector3 direction = this.transform.position - destination;
        Vector3 normalizedDirection = Vector3.Normalize(direction);
        rb2d.MovePosition(this.transform.position - (normalizedDirection * Time.fixedDeltaTime * speed));
    }

    private IEnumerator CheckForPlayer()
    {
        while (true)
        {
            PlayerControler playcont = findPlayer();
            if (playcont != null)
            {
                if (Vector2.Distance(this.transform.position, lastPlayerPosition) < 2f)
                {
                    Debug.Log("damaging");
                    playcont.takeDamage(1);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            if(findPlayer() == null)
            {
                nextRandPosition = new Vector3(this.transform.position.x + Random.Range(-5, 5), this.transform.position.y + Random.Range(-5, 5), 0f);
            }
            yield return new WaitForSeconds(5f);
        }
    }

    public void die()
    {
        dead = true;
    }

    public bool isDead()
    {
        return dead;
    }

    public void takeDamage(int damage)
    {
        hb.changeHp(damage);
    }
}
