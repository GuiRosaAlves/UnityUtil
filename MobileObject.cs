using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MobileObject : MonoBehaviour
{
    public enum GameType { _3D, Platform2D, TopDown2D }
    public delegate void MovementHandler(Vector3 dir, float speed);

    public GameType gameType;
    private MovementHandler[] _behaviourList;
    public MovementHandler Move { get { return _behaviourList[(int)gameType]; } }
    public Rigidbody2D Rb2D { get; private set; }
    private Vector3 _dirModifier;

    private void Awake()
    {
        _dirModifier = new Vector3();
        Rb2D = GetComponent<Rigidbody2D>();

        _behaviourList = new MovementHandler[3];
        _behaviourList[(int)GameType._3D] = Move3D;
        _behaviourList[(int)GameType.Platform2D] = MovePlatform2D;
        _behaviourList[(int)GameType.TopDown2D] = MoveTopDown2D;
    }

    private void Move3D(Vector3 dir, float speed)
    {
        _dirModifier.x = 0;
        _dirModifier.z = 0;
        transform.position += (_dirModifier * speed * Time.deltaTime);
    }
    private void MovePlatform2D(Vector3 dir, float speed)
    {
        _dirModifier.x = dir.x;
        transform.position += (_dirModifier * speed * Time.deltaTime);
    }
    private void MoveTopDown2D(Vector3 dir, float speed)
    {
        _dirModifier.x = dir.x;
        _dirModifier.y = dir.y;
        transform.position += (_dirModifier * speed * Time.deltaTime);
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