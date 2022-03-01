using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GazeAt : MonoBehaviour
{
    Material oriMat;
    public Material gazeMat;

    public bool isGaze;

    // Start is called before the first frame update
    void Start()
    {
        oriMat = GetComponent<Renderer>().material;

        isGaze = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGazeAt(bool gazeAt)
    {
        isGaze = gazeAt;
    }
}
