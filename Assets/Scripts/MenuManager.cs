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
        if (PlayerPrefs.GetInt("PlayerScore", 0) >= 200)
        {
            SceneManager.LoadScene("Pacman_level2");
        }
        else
        {
            Debug.Log("Pontuação insuficiente para acessar o nível 2.");
            // Exibir uma mensagem ao jogador informando que a pontuação mínima não foi atingida
        }
    }

    public void level3()
    {
        if (PlayerPrefs.GetInt("PlayerScore", 0) >= 500)
        {
            SceneManager.LoadScene("Pacman_level3");
        }
        else
        {
            Debug.Log("Pontuação insuficiente para acessar o nível 3.");
            // Exibir uma mensagem ao jogador informando que a pontuação mínima não foi atingida
        }
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
