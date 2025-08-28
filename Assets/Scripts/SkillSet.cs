using System.Collections.Generic;
using UnityEngine;

public class SkillSet : MonoBehaviour
{
    [SerializeField] Skill[] skills;
    List<SkillPlayer> _skillPlayer;
    public List<SkillPlayer> SkillPlayers { get { return _skillPlayer; } }
    int _currSkillIdx = 0;
    public int CurrSkillIdx { get { return _currSkillIdx; } set { _currSkillIdx = value; } }
    public SkillPlayer CurrentSkillPlayer { get { return _skillPlayer[_currSkillIdx]; } }
    void Start()
    {
        _skillPlayer = new List<SkillPlayer>();
        foreach (var skill in skills)
        {
            SkillPlayer sp = gameObject.AddComponent<SkillPlayer>(); // 안되면 오브젝트 생성해서 집어넣기
            sp.Skill = skill;
            _skillPlayer.Add(sp);
        }
    }
    
    void Update()
    {
        foreach (var sp in _skillPlayer)
        {
            sp.CoolDown();
        }
    }
}
