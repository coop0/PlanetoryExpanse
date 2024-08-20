using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewDisplay : MonoBehaviour
{
    [SerializeField] private Sprite _defaultPreview;
    [SerializeField] private List<Sprite> _previews;
    [SerializeField] private Image _image;
    public void ShowPreview(int index)
    {
        if (_previews[index] == null)
        {
            print("null preview");
            return;
        }
        _image.sprite = _previews[index];
    }

    public void DefaultPreview()
    {
        _image.sprite = _defaultPreview;
    }
}
