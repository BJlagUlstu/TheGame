using UnityEngine;

public class SelectingObject : MonoBehaviour
{
    public void SetVisible(bool isVisible) 
    {
        gameObject.SetActive(isVisible);
    }
}