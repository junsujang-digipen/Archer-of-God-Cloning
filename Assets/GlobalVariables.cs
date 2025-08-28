using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static Vector3 CenterPosition;
    public static Character PlayerCharacter;
    void Awake()
    {
        CenterPosition = transform.position;
        PlayerCharacter = FindAnyObjectByType<Controller>().Target;
    }
}
