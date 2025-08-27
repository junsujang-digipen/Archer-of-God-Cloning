using UnityEngine;

public class ArrowFactory : MonoBehaviour
{
    [SerializeField] GameObject _arrowPrefab;
    private static ArrowFactory instance = null;
    public static ArrowFactory Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CreateArrow(Vector3 position, Vector3 targetPosition)
    {
        GameObject arrow = Instantiate(_arrowPrefab);
        arrow.transform.position = position;
        Arrow compo = arrow.GetComponent<Arrow>();
        compo.targetPosition = targetPosition;
    }
}
