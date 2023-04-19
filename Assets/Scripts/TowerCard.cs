using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour, /*IPointerEnterHandler, IPointerExitHandler,*/ IPointerClickHandler
{
    public Transform TowerPrefab;

    private Image objectImage;

    void Start()
    {
        objectImage = GetComponent<Image>(); // получаем компонент Image у текущего объекта
        Ceil.TowerToBuildChanged += ResetColor;
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    objectImage.color = Color.yellow; // изменяем цвет компонента Image на желтый при наведении мыши на объект
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    objectImage.color = Color.white; // изменяем цвет компонента Image на белый при уходе мыши с объекта
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        Ceil.ChangeTowerToBuild(TowerPrefab);
        objectImage.color = Color.yellow;
    }
    public void ResetColor()
    {
        objectImage.color = Color.white;
    }
}
