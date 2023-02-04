using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    float speed = 5.0f;

    Vector2Int direction = Vector2Int.right;
    Vector2Int oppositeDirection = Vector2Int.left;
    List<Transform> tail = new List<Transform>();

    void Start()
    {
        InvokeRepeating("Move", 0.3f, 0.2f);
    }

    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 targetDirection = transform.position - worldPosition;
        Vector2Int tmpDirection = Vector2Int.RoundToInt((targetDirection).normalized);
        if (tmpDirection != oppositeDirection)
        {
            direction = tmpDirection;
        }
        else if (tmpDirection == Vector2Int.down || tmpDirection == Vector2Int.up)
        {
            direction = Vector2Int.right;
        }
        else if (tmpDirection == Vector2Int.left || tmpDirection == Vector2Int.right)
        {
            direction = Vector2Int.down;
        }
        else { 
            direction = Vector2Int.down;
        }
        oppositeDirection = -direction;
        
    }

    void Move()
    {
        transform.position -= new Vector3(direction.x,direction.y,0);

    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Food"))
    //    {
    //        GameObject food = other.gameObject;
    //        food.SetActive(false);
    //
    //        Transform newTail = Instantiate(food.transform, position, Quaternion.identity);
    //        tail.Add(newTail);
    //    }
    //}
}