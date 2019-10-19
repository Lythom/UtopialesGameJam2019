using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string targetSceneName;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
