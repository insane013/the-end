using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_menu : PauseMenu
{
    [SerializeField] GameObject options;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("main_menu", LoadSceneMode.Single);
        print("Loading");
    }

    public void LoadGame()
    {

    }

    public void SaveGame()
    {

    }

    public void OpenOptions()
    {
        options.SetActive(!options.activeSelf);
    }
}
