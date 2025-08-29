using UnityEngine;

public class AfterEffect : MonoBehaviour
{
    public float Damage = 0f;
    [SerializeField] float Duration = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.identity;
        Destroy(gameObject, Duration);
    }
}
