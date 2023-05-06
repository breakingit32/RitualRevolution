using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private Text tooltipText;
    

    private void Start()
    {
        tooltipPanel.SetActive(false);
    }

    public void SetDescription(string description)
    {
        tooltipText.text = description;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipPanel.SetActive(true);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipPanel.SetActive(false);
    }
}
