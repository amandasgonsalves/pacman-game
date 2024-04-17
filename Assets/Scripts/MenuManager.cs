using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public void level1()
    {
        StartCoroutine(LoadSceneAfterDelay("Pacman_level1", 0f));
    }

    public void level2()
    {
        StartCoroutine(LoadSceneAfterDelay("Pacman_level2", 0f));
    }

    public void level3()
    {
 
        SceneManager.LoadScene("Pacman_level3");

    }

    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
