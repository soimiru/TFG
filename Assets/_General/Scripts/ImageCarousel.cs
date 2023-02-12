using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCarousel : MonoBehaviour
{
    [SerializeField] private List<Sprite> images;
    Image myImage;
    int currentImage;

    // Start is called before the first frame update
    void Awake()
    {
        myImage = GetComponent<Image>();
    }

    private void Start()
    {
        currentImage = 0;
        myImage.sprite = images[currentImage];

        InvokeRepeating("NextImage", 5f, 5f);

    }

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
