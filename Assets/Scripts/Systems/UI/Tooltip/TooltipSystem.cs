using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem current;

    [SerializeField] private UI_Tooltip ui_tooltip;

    public void Initialize()
    {
        current = this;
    }

    public void Show(string header, string text)
    {
        if (header == "" && text == "") return;

        ui_tooltip.SetText(header, text);
        ui_tooltip.gameObject.SetActive(true);
    }

    public void Hide()
    {
        ui_tooltip.gameObject.SetActive(false);
    }
}
