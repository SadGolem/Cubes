using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Interactable interactable;
    [SerializeField] private Renderer rendererObj;

/*    public Cube()
    {
        originalColor = rendererObj.material.color;
        interactable.isCanTouch = true;
    }*/
    private void Start()
    {
        interactable.isCanTouch = true;
    }
    public Color OriginalColor
    {
        get { return rendererObj.material.color; }
        set { rendererObj.material.color = value; }
    }

    public bool IsCanTouch
    {
        get { return interactable.isCanTouch; }
        set { interactable.isCanTouch = value; }
    }
}
