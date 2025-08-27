using UnityEngine;

public class SearchTarget : MonoBehaviour
{
    public GameObject GetTarget()
    {
        Character[] characters = GetComponentsInChildren<Character>();
        if(characters.Length > 1)
        {
            foreach (Character character in characters)
            {
                if(character.tag != "Character")
                {
                    return character.gameObject;
                }
            }
        }
        return characters[0].gameObject;
    }

}
