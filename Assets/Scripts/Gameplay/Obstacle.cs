using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    }

    public Direction forceDirection;
    public float forceMagnitude = 10f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AddForceInDirection();
    }

    private void AddForceInDirection()
    {
        Vector2 force = Vector2.zero;

        switch (forceDirection)
        {
            case Direction.Up:
                force = Vector2.up;
                break;
            case Direction.Down:
                force = Vector2.down;
                break;
            case Direction.Left:
                force = Vector2.left;
                break;
            case Direction.Right:
                force = Vector2.right;
                break;
            case Direction.UpLeft:
                force = new Vector2(-1, 1).normalized;
                break;
            case Direction.UpRight:
                force = new Vector2(1, 1).normalized;
                break;
            case Direction.DownLeft:
                force = new Vector2(-1, -1).normalized;
                break;
            case Direction.DownRight:
                force = new Vector2(1, -1).normalized;
                break;
        }

        rb.AddForce(force * forceMagnitude, ForceMode2D.Impulse);
    }
}
