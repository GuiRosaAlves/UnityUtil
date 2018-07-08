using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownBehaviour : MonoBehaviour
{
    public Rigidbody2D rb2D { get; private set; }

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 dir, float speed)
    {
        transform.position += (Vector3)(dir * speed * Time.deltaTime);
    }

    public void RotateToPoint(Vector2 point)
    {
        transform.rotation = Quaternion.Euler(0, 0, AngleBetween2D(point, transform.position));
    }

    public static float AngleBetween2D(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}