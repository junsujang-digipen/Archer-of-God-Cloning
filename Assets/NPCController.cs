using System;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] Character target;
    ConstrictMovableRange constrictMovableRange;
    SkillSet _skillSet;
    int skillCount;
    int _currentState = 0;// 0: skill0, 1: skill1, 2: skill2, ..., skillCount: Idle
    float _stateInterval = 2f;
    float _stateTimer = 0f;
    float _moveTo;
    void Start()
    {
        constrictMovableRange = target.GetComponent<ConstrictMovableRange>();
        _skillSet = target.GetComponent<SkillSet>();
        skillCount = _skillSet.SkillPlayers.Count;
        _moveTo = transform.position.x;
    }
    void Update()
    {
        if (_stateTimer <= 0f) // 공격 또는 스킬
        {
            _stateInterval = UnityEngine.Random.Range(1.5f, 3f);
            _stateTimer = _stateInterval;
            _currentState = 0;
            for (; _currentState < skillCount; ++_currentState)
            {
                if (_skillSet.SkillPlayers[_currentState].IsAble == true) break;
            }
            if (_currentState != skillCount)
            {
                PlaySkill(_currentState);
                ShotSkill(GlobalVariables.PlayerCharacter.transform.position);
            }
            _moveTo = UnityEngine.Random.Range(constrictMovableRange.MinXPosition, constrictMovableRange.MaxXPosition);
        }
        else // 목표 지점 이동
        {
            _stateTimer -= GameTime.DeltaTime;
            float dist = _moveTo - target.transform.position.x;
            if (Math.Abs(dist) > 0.1f) target.Move(dist);
            else
            {
                target.StopMove();
            }
        }
    }
    void PlaySkill(int skillNumber)
    {
        target.Skill(skillNumber);
    }
    void ShotSkill(Vector3 targetPosition)
    {
        target.CurrentSkillPlayer.SetTargetPosition(targetPosition);
        target.CurrentSkillPlayer.Shot();
    }
}
