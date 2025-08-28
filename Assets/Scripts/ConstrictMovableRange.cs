using System;
using UnityEngine;

public class ConstrictMovableRange : MonoBehaviour
{
    public MovableRange movableRange;
    private Character character;
    float minXPosition;
    float maxXPosition;
    void Start()
    {
        character = GetComponent<Character>();
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        minXPosition = movableRange.MinXPosition + sprite.bounds.extents.x;
        maxXPosition = movableRange.MaxXPosition - sprite.bounds.extents.x;
    }
    void Update()
    {
        Vector3 pos = character.transform.position;
        pos.x = Math.Min(pos.x, maxXPosition);
        pos.x = Math.Max(pos.x, minXPosition);
        character.transform.position = pos;
    }
}
