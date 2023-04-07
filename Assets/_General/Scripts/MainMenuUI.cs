using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Image potionMatchImage;
    [SerializeField] private Image eternalJumpImage;
    [SerializeField] private Image luckyDuckyImage;
    [SerializeField] private Image settingsImage;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
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
}
