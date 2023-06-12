using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    private Vector2 movementInput;
    private Vector2 lookInput;
    private float speed = 5f;
    private Rigidbody2D rigBody2D;
    private Camera cam;
    private Vector2 lastDirection;
    public GameObject sprite;

    public void OnFire()
    {
        LayerMask mask = LayerMask.GetMask("Ennemy");
        ContactFilter2D filter2D = new ContactFilter2D().NoFilter();
        RaycastHit2D hit = Physics2D.BoxCast(new Vector2(this.transform.position.x + this.lastDirection.x, this.transform.position.y + this.lastDirection.y), Vector2.one, 0.0f, lastDirection, 1f, mask);
        sprite.transform.position = new Vector2(this.transform.position.x + this.lastDirection.x, this.transform.position.y + this.lastDirection.y);
        sprite.transform.localScale = Vector2.one;
        Debug.Log(hit.collider);
        Debug.Log(lastDirection);
    }

    /*public void OnLook(InputValue value) {
        Vector2 screenSpace = cam.WorldToScreenPoint(this.transform.position);
        lookInput = new Vector2(value.Get<Vector2>().x - screenSpace.x, value.Get<Vector2>().y - screenSpace.y);
        Debug.Log(lookInput);
    }*/

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();

        if(movementInput != Vector2.zero)
        {
            if(Mathf.Abs(movementInput.y) > Mathf.Abs(movementInput.x))
            {
                lastDirection.x = 0;
                if(movementInput.y < 0)
                {
                    lastDirection.y = -1;
                }
                else
                {
                    lastDirection.y = 1;
                }
            }
            else
            {
                lastDirection.y = 0;
                if (movementInput.x < 0)
                {
                    lastDirection.x = -1;
                }
                else
                {
                    lastDirection.x = 1;
                }
            }
        }
    }

    void Awake() {
        this.rigBody2D = this.GetComponent<Rigidbody2D>();
        this.cam = this.GetComponentInChildren<Camera>();
    }

    void FixedUpdate() {
        rigBody2D.MovePosition(this.transform.position + (new Vector3(movementInput.x, movementInput.y, 0f) * speed * Time.deltaTime));
    }
}