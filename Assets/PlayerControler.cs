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

    public void OnFire()
    {
        Debug.Log("Jump!");
    }

    public void OnLook(InputValue value) {
        Vector2 screenSpace = cam.WorldToScreenPoint(this.transform.position);
        lookInput = new Vector2(value.Get<Vector2>().x - screenSpace.x, value.Get<Vector2>().y - screenSpace.y);
        Debug.Log(lookInput);
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void Awake() {
        this.rigBody2D = this.GetComponent<Rigidbody2D>();
        this.cam = this.GetComponentInChildren<Camera>();
    }

    void FixedUpdate() {
        rigBody2D.MovePosition(this.transform.position + (new Vector3(movementInput.x, movementInput.y, 0f) * speed * Time.deltaTime));
    }
}