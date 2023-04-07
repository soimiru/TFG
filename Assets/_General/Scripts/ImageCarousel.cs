using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCarousel : MonoBehaviour
{
    [SerializeField] private List<Sprite> images;
    Image myImage;
    int currentImage;

    void Awake()
    {
        myImage = GetComponent<Image>();
    }

    private void Start()
    {
        currentImage = 0;
        myImage.sprite = images[currentImage];

        //Se llama al metodo NextImage() después de 5 segundos y cada 5 segundos
        InvokeRepeating("NextImage", 5f, 5f);
    }

    /// <summary>
    /// Cambia a la siguiente imagen dentro del array de imágenes, se invoca mediante las flechas del menú.
    /// Controla el primer y el último elemento del array para evitar excepciones.
    /// </summary>
    public void NextImage() {
        CancelInvoke();
        if (currentImage + 1 >= images.Count)
        {
            currentImage = 0;
        }
        else {
            currentImage += 1;
        }
        myImage.sprite = images[currentImage];
        InvokeRepeating("NextImage", 5f, 5f);

    }

    /// <summary>
    /// Cambia a la anterior imagen dentro del array de imágenes, se invoca mediante las flechas del menú.
    /// Controla el primer y el último elemento del array para evitar excepciones.
    /// </summary>
    public void PreviousImage() {
        CancelInvoke();
        if (currentImage - 1 < 0)
        {
            currentImage = images.Count-1;
        }
        else
        {
            currentImage -= 1;
        }
        myImage.sprite = images[currentImage];
        InvokeRepeating("NextImage", 5f, 5f);

    }
}
