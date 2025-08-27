using UnityEngine;

public class GameTime : MonoBehaviour
{
    public static float TimeScale = 1f;
    public static float DeltaTime = 0f;
    void Update()
    {
        DeltaTime = Time.deltaTime * TimeScale;
    }
}
