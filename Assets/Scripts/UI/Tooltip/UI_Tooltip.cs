using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UI_Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ui_header;
    [SerializeField] private TextMeshProUGUI ui_text;

    [SerializeField] private LayoutElement layoutElement;

    [SerializeField, Range(1, 500)] private int characterWrapLimits;

    [SerializeField] private RectTransform rectTransform;

    public void SetText(string header, string text)
    {
        ui_header.text = header;
        ui_text.text = text;

        int headerLength = ui_header.text.Length;
        int textLength = ui_text.text.Length;

        layoutElement.enabled =
            headerLength > layoutElement.preferredWidth ||
            textLength > layoutElement.preferredWidth;

        Vector2 _position = Input.mousePosition;

        float pivotX = _position.x / Screen.width;
        float pivotY = _position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = _position;
    }
}
