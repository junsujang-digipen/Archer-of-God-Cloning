using UnityEngine;

public class Attack : CharacterAction
{
    [SerializeField] GameObject _targetSide;
    SearchTarget _searchTarget;
    [SerializeField] float _attackDelay;
    float _attackDelayTimer;
    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] Aiming _aiming;
    [SerializeField] GameObject _bow;
    Vector3 direction;
    void Start()
    {
        _searchTarget = _targetSide.GetComponent<SearchTarget>();
        ResetTimer();
    }
    void Update()
    {
        // 활 회전 설정
        direction = ComputeDirection(_searchTarget.GetTarget().transform.position, Physics2D.gravity.y);
        _aiming.Velocity = direction;
    }
    public override void Do()
    {
        if (_attackDelayTimer <= 0f)
        {
            ResetTimer();
            Debug.Log("Attack!");
            // 화살 생성
            GameObject arrow = Instantiate(_arrowPrefab);
            // 화살 초기 속도 설정
            Rigidbody2D _rigidbody2D = arrow.GetComponent<Rigidbody2D>();
            _rigidbody2D.linearVelocity = direction;
            
            arrow.transform.position = _bow.transform.position;
        }
        else
        {
            _attackDelayTimer -= GameTime.DeltaTime;
        }
    }
    public void ResetTimer()
    {
        _attackDelayTimer = _attackDelay;
    }
    Vector3 ComputeDirection(Vector3 targetPosition, float gravity)
    {
        Vector3 direction = targetPosition - transform.position;
        float distance = direction.magnitude;
        float t = 1.0f + distance*0.05f; // 1초 + 거리 기반 시간 추가
        direction.x /= t;
        direction.y -= 0.5f * gravity * t * t;
        direction.y /= t;
        return direction;
    }
}
