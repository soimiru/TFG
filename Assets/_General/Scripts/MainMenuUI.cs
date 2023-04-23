using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Image potionMatchImage;
    [SerializeField] private Image eternalJumpImage;
    [SerializeField] private Image luckyDuckyImage;
    [SerializeField] private Image settingsImage;

    public Slider musicSlider, sfxSlider;

    public Sprite soundON, soundOFF, musicON, musicOFF;
    public Image musicBtn, sfxBtn;

    private string soundText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        SoundManager.Instance.PlayMusic("MainTheme");

        //Recuperar los valores del Audiomixer y ponerlos en el slider
        SoundManager.Instance.audioMixer.GetFloat("MusicVolume", out var valueM);
        musicSlider.value = Mathf.Pow(10, valueM / 20);

        SoundManager.Instance.audioMixer.GetFloat("SFXVolume", out var valueS);
        sfxSlider.value = Mathf.Pow(10, valueS / 20);
        soundText = "Jump";
    }

    /// <summary>
    /// Cambia las escenas.
    /// </summary>
    /// <param name="scene">String que contiene el nombre de la escena.</param>
    public void ChargeGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// Modifica el orden de los paneles de la interfaz para visualizar el que se haya seleccionado.
    /// </summary>
    /// <param name="idx">Índice que sirve para saber que panel se ha seleccionado.</param>
    public void ChangeImage(int idx) {
        switch (idx) {
            case 0:
                potionMatchImage.transform.SetSiblingIndex(3);
                break;
            case 1:
                eternalJumpImage.transform.SetSiblingIndex(3);
                break;
            case 2:
                luckyDuckyImage.transform.SetSiblingIndex(3);
                break;
            case 3:
                settingsImage.transform.SetSiblingIndex(3);
                break;
            default:
                potionMatchImage.transform.SetSiblingIndex(3);
                break;
        }
    }

    public void ToggleMusic() {
        if (SoundManager.Instance.ToggleMusic())
        {
            musicBtn.sprite = musicOFF;
        }
        else { 
            musicBtn.sprite = musicON;
        }
    }
    public void ToggleSFX()
    {
        if (SoundManager.Instance.ToggleSFX())
        {
            sfxBtn.sprite = soundOFF;
        }
        else
        {
            sfxBtn.sprite = soundON;
        }
        SoundManager.Instance.PlaySFX("Jump");
    }

    public void MusicVolume() {
        SoundManager.Instance.MusicVolume(musicSlider.value);
    }

    public void SFXVolume()
    {
        SoundManager.Instance.SFXVolume(sfxSlider.value);
        SoundManager.Instance.PlaySFX(soundText);
    }

    public void GoToURL(string url)
    {
        Application.OpenURL(url);
    }

}
