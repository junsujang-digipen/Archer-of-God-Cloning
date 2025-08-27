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
        minXPosition = movableRange.MinXPosition + character.transform.localScale.x/2;
        maxXPosition = movableRange.MaxXPosition - character.transform.localScale.x/2;
    }
    void Update()
    {
        Vector3 pos = character.transform.position;
        pos.x = Math.Min(pos.x, maxXPosition);
        pos.x = Math.Max(pos.x, minXPosition);
        character.transform.position = pos;
    }
}
