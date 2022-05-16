using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Image ZooMatchImage;
    [SerializeField] private Image EndlessRunnerImage;
    [SerializeField] private Image ShootingGalleryImage;
    [SerializeField] private Image SettingsImage;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChargeGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ChangeImage(int idx) {
        switch (idx) {
            case 0:
                ZooMatchImage.transform.SetSiblingIndex(3);
                break;
            case 1:
                EndlessRunnerImage.transform.SetSiblingIndex(3);
                break;
            case 2:
                ShootingGalleryImage.transform.SetSiblingIndex(3);
                break;
            case 3:
                SettingsImage.transform.SetSiblingIndex(3);
                break;
            default:
                ZooMatchImage.transform.SetSiblingIndex(3);
                break;
        }
    }
}
