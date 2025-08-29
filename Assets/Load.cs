using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    [SerializeField] string _sceneName;

    void Start()
    {
        SceneManager.LoadScene(_sceneName,LoadSceneMode.Single);
    }
}
