using UnityEngine;

public class Move : CharacterAction
{
    [SerializeField] float _defaultMoveSpeed;
    float _moveSpeed;
    public float MoveSpeed { set { _moveSpeed = value; } }
    float direction; // -1: left, 1: right
    public float Direction { set { direction = (value < 0f) ? -1f : (value > 0f) ? 1f : 0f; } }
    void Awake()
    {
        _moveSpeed = _defaultMoveSpeed;
    }
    public override void Do()
    {
        Vector3 dist = new Vector3(direction * _moveSpeed * GameTime.DeltaTime, 0, 0);
        transform.position += dist;
    }
}
