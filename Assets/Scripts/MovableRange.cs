using UnityEngine;

public class MovableRange : MonoBehaviour
{
    public float MinXPosition;
    public float MaxXPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) Debug.Log("No SpriteRenderer for MovableRange");
        Bounds bounds = sr.bounds;
        MinXPosition = bounds.min.x;
        MaxXPosition = bounds.max.x;
    }
}
