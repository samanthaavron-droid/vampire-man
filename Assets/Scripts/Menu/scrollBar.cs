using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class scrollBar : MonoBehaviour
{
    public float speed;
    public float margin;

    private TextMeshProUGUI _textPanel;
    private RectTransform _rect1;
    private RectTransform _rect2;
    private float _parentWidth;
    private float _width;
    private bool _isSetup = false;
    public void SetupScroll()
    {
        _rect1 = GetComponent<RectTransform>();
        _textPanel = GetComponent<TextMeshProUGUI>();
        _parentWidth = GetComponentInParent<RectTransform>().rect.width;

        _textPanel.ForceMeshUpdate();
        _width = _textPanel.preferredWidth;

        if (_width > _parentWidth && _rect2 == null)
        {
            GameObject clone = Instantiate(gameObject, transform.parent);
            Destroy(clone.GetComponent<scrollBar>());
            _rect2 = clone.GetComponent<RectTransform>();
            _rect2.anchoredPosition = new Vector2(_rect1.anchoredPosition.x + _width + margin, _rect1.anchoredPosition.y);
        }
        _isSetup = true;
    }

    void Update()
    {
        if (_isSetup == false || _textPanel == null || _width <= _parentWidth || _rect2 == null) return;

        _rect1.anchoredPosition += Vector2.left * speed * Time.unscaledDeltaTime;
        _rect2.anchoredPosition += Vector2.left * speed * Time.unscaledDeltaTime;

        if (_rect1.anchoredPosition.x < -_width)
            _rect1.anchoredPosition = new Vector2(_rect2.anchoredPosition.x + _width + margin, _rect1.anchoredPosition.y);

        if (_rect2.anchoredPosition.x < -_width)
            _rect2.anchoredPosition = new Vector2(_rect1.anchoredPosition.x + _width + margin, _rect2.anchoredPosition.y);
    }
}   
