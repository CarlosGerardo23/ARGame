using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMovement : MonoBehaviour
{

  [SerializeField] Vector2 startPosition;
  [SerializeField] Vector2 lastPosition;
  [SerializeField] float limitPositionX;
  [SerializeField] float limitPositionY;
  [SerializeField] float swipeMinimumMovement;
  [SerializeField] float speed;

  public Vector2 direction;
  public bool pressed;


  // Update is called once per frame
  void Update()
  {
    CheckInput();
  }

  void CheckInput()
  {
#if UNITY_EDITOR
    CheckEditor();
#endif

#if UNITY_ANDROID
            CheckAndroid();
#endif
  }

  private void CheckEditor()
  {
    if (pressed)
    {
      lastPosition = Input.mousePosition;

      if (lastPosition != startPosition)
      {
        CheckDirection();
        Move();
      }
    }
    else if (Input.GetMouseButtonDown(0))
    {   
        pressed = true;

        startPosition = Input.mousePosition;   
    }

    if (Input.GetMouseButtonUp(0))
    {
      pressed = false;
      lastPosition = new Vector2(0, 0);
      direction = new Vector2(0, 0);
    }
    
  }

  private void CheckAndroid()
  {
    foreach (Touch touch in Input.touches)
    {
      if (touch.phase == TouchPhase.Began)
      {
        startPosition = touch.position;
        lastPosition = touch.position;
      }

      if (touch.phase == TouchPhase.Moved)
      {
        lastPosition = touch.position;
        if (lastPosition != startPosition)
        {
          CheckDirection();
          Move();
        }

      }
    }
  }

  float DistancePositions()
  {
    float result = Vector2.Distance(lastPosition, startPosition);
    return result;
  }
  private void CheckDirection()
  {
    if (MoveHorizontalTotal() > swipeMinimumMovement)
    {
      if (lastPosition.x - startPosition.x > 0)
        direction = Vector2.right;
      else if (lastPosition.x - startPosition.x < 0)
        direction = Vector2.left;
    }
    else if (MoveVertcalTotal() > swipeMinimumMovement)
    {
      if (lastPosition.y - startPosition.y > 0)
        direction = Vector2.up;
      else if (lastPosition.y - startPosition.y < 0)
        direction = Vector2.down;
    }



  }
  float MoveHorizontalTotal()
  {
    return Mathf.Abs(lastPosition.x - startPosition.x);
  }
  float MoveVertcalTotal()
  {
    return Mathf.Abs(lastPosition.y - startPosition.y);
  }
  private void Move()
  {
    if (direction == Vector2.right && transform.localPosition.x < limitPositionX)
      transform.Translate(new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime);
    if (direction == Vector2.left && transform.localPosition.x > -limitPositionX)
      transform.Translate(new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime);
    if (direction == Vector2.up && transform.localPosition.z < limitPositionY)
      transform.Translate(new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime);
    if (direction == Vector2.down && transform.localPosition.z > limitPositionY - 3)
      transform.Translate(new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime);


  }
}
