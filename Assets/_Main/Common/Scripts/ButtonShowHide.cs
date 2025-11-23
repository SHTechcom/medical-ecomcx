using UnityEngine;

public class ButtonShowHide : MonoBehaviour
{
    [SerializeField] private GameObject showhide;
    public void ShowHide() => showhide.SetActive(!showhide.activeSelf);
}
