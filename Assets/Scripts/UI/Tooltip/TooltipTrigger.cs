using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string header, text;

    public void SetText(string header, string text)
    {
        this.header = header;
        this.text = text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.current.Show(header, text);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.current.Hide();
    }
}