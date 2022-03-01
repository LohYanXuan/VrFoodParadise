using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GazeAt : MonoBehaviour
{
    private Renderer myRenderer;
    Material oriMat;
    public Material gazeMat;

    public bool isGaze;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        oriMat = myRenderer.material;

        isGaze = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGaze)
        {
            myRenderer.material = gazeMat;
        }
        else
        {
            myRenderer.material = oriMat;
        }
    }

    public void SetGazeAt(bool gazeAt)
    {
        isGaze = gazeAt;
    }
}
