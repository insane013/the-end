using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject creditsWindow;
    [SerializeField] GameObject controlsWindow;
    [SerializeField] Toggle skipIntroToggle;
    [SerializeField] AudioSource audioSourse;
    [SerializeField] Button music_switcher;
    [SerializeField] TextMeshProUGUI music_switcher_text;
    [SerializeField] Sprite music_switcher_on;
    [SerializeField] string music_switcher_on_text;
    [SerializeField] Sprite music_switcher_off;
    [SerializeField] string music_switcher_off_text;
    [SerializeField] Slider music_volume;

    private void OnEnable()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        skipIntroToggle.isOn = DataHodler.skipMainIntro;
    }

    public void Play()
    {
        SceneManager.LoadScene("map");
    }

    public void Credits()
    {
        creditsWindow.SetActive(!creditsWindow.activeSelf);
    }

    public void Controls()
    {
        controlsWindow.SetActive(!controlsWindow.activeSelf);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void MusicSwitch()
    {
        if (audioSourse.mute)
        {
            audioSourse.mute = false;
            music_switcher.image.sprite = music_switcher_off;
            music_switcher_text.text = music_switcher_off_text;
        }
        else
        {
            audioSourse.mute = true;
            music_switcher.image.sprite = music_switcher_on;
            music_switcher_text.text = music_switcher_on_text;
        }
    }
    public void MusicVolume ()
    {
        audioSourse.volume = music_volume.value;
    }

    public void SkipIntro(Toggle tg)
    {
        DataHodler.skipMainIntro = tg.isOn;
    }

}
