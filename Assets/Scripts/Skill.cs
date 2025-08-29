using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skill")]
public class Skill : ScriptableObject
{
    public string SkillName = "Skill";
    public GameObject ArrowPrefab;
    public float ArrowSpeed = 10f;
    public int GenCount = 1;
    public float GenInterval = 0.1f;
    public float Cooldown = 5f;
    public bool IsStraight = false;
    public bool IsJump = false;
}
