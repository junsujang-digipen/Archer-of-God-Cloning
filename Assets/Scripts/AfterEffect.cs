using UnityEngine;

public class AfterEffect : MonoBehaviour
{
    public float Damage = 0f;
    [SerializeField] float Duration = 1f;
    void Start()
    {
        transform.rotation = Quaternion.identity;
        Destroy(gameObject, Duration);
    }
}
