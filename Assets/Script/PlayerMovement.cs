using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool wasJustClicked;
    bool canMove;
    Vector2 playerSize;
    Rigidbody2D rb;
    Vector2 startingPosition;
    public Transform BoundaryHolder;
    Boundary playerBoundary;
    
    struct Boundary
    {
        public float Up, Down, Left, Right; 
        public Boundary(float up, float down, float left, float right)
        {
           Up= up; Down = down; Left= left; Right= right;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerSize = gameObject.GetComponent<SpriteRenderer>().bounds.extents;
        rb =GetComponent<Rigidbody2D>();
        startingPosition = rb.position;
        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                      BoundaryHolder.GetChild(1).position.y,
                                      BoundaryHolder.GetChild(2).position.x,
                                      BoundaryHolder.GetChild(3).position.x
            );
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            wasJustClicked = true;
            if (wasJustClicked)
            {
                
                wasJustClicked = false;
               
                if ((mousePos.x >= transform.position.x && mousePos.x < transform.position.x + playerSize.x ||
                mousePos.x <= transform.position.x && mousePos.x > transform.position.x - playerSize.x) &&
                (mousePos.y >= transform.position.y && mousePos.y < transform.position.y + playerSize.y ||
                mousePos.y <= transform.position.y && mousePos.y > transform.position.y - playerSize.y)) // 마우스 안전 장치 범위 
                {
                    Debug.Log("x 좌표:"+ mousePos.x);
                    Debug.Log("라켓 X좌표:"+transform.position.x);
                    canMove = true;
                    Debug.Log(canMove);
                }
                else
                {
                    Debug.Log("x 좌표:" + mousePos.x);
                    Debug.Log("라켓 X좌표:" + transform.position.x);
                  
                    Debug.Log(canMove);
                }
                
            }

            else             
            {
                canMove = false;

            }
            
            if (canMove)
            {
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left, playerBoundary.Right),
                                                      Mathf.Clamp(mousePos.y,playerBoundary.Down,playerBoundary.Up));
                rb.MovePosition(clampedMousePos);
            }
        }
    }
    public void ResetPosition()
    {
        rb.position = startingPosition;
    }
}
