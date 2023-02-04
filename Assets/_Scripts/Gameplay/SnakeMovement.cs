using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    float speed = 0.1f;

    Vector2Int direction = Vector2Int.right;
    Vector2Int oppositeDirection = Vector2Int.left;
    List<Transform> tail = new();
    private Master master;
    

    void Start()
    {
        master = GameObject.FindGameObjectsWithTag("Master")[0].GetComponent<Master>();
        InvokeRepeating("Move", 0.3f, 0.1f);
    }

    void Update()
    {
        ChangeDirection();
        

    }


    void ChangeDirection()
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
        // diagonally direction
        else
        {
            direction = Vector2Int.down;
        }
        oppositeDirection = -direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Resources resource;
        if (collision.GetComponent<Resources>())
        {
            resource = collision.GetComponent<Resources>();
            if (resource.CurrentResourceType == Resources.ResourseType.Rock)
            {
                Destroy(gameObject);
            }
            else if (resource.CurrentResourceType == Resources.ResourseType.Nitrogen)
            {
                Destroy(collision.gameObject);
                master.stats.nitrogen += 10;
            }
            else if (resource.CurrentResourceType == Resources.ResourseType.Water)
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
                master.stats.water += 10;
            }
        }

    }

    void Move()
    {
        transform.position -= new Vector3(direction.x,direction.y,0) * speed;
        GameObject go = Instantiate(master.newRoot, transform.position, Quaternion.identity);
        go.AddComponent<BoxCollider2D>().isTrigger = true;
        go.AddComponent<Resources>().CurrentResourceType = Resources.ResourseType.SpawingPoint;
        go.transform.parent = master.transform;
        master.roots.Add(go);


    }
}