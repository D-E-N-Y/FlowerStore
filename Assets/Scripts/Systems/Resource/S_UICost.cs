using TMPro;

[System.Serializable]
public struct SUICost
{
    public EResourceType resource;
    public TextMeshProUGUI ui_amount;

    public SUICost(EResourceType resource, TextMeshProUGUI ui_amount)
    {
        this.resource = resource;
        this.ui_amount = ui_amount;
    }
}