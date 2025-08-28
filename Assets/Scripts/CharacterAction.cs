using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    bool _isExitable = false;
    public bool IsExitable
    {
        get { return _isExitable; }
        set { _isExitable = value; }
    }
    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void Exit() { }
}
