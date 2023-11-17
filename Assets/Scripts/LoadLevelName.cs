using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelName : MonoBehaviour
{
    public string sceneName; // Vari�vel para armazenar o nome da cena a ser carregada

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
