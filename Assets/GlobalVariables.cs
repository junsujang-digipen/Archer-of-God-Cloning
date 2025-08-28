using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static Vector3 CenterPosition;
    void Awake()
    {
        CenterPosition = transform.position;
    }
}
