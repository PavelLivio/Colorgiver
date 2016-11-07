using UnityEngine;
using System.Collections;

public class ColorBarScript : MonoBehaviour {
    public Transform colorBarGreen;
    public Transform colorBarRed;
    public Transform colorBarBlue;
    public Transform colorBarBlack;
    float scaleUnit;
    //Transform origPosT;

    // Use this for initialization
    void Start () {
        scaleUnit = colorBarBlack.localScale.x;
    }

    public void SetColor(Color inColor)
    {
        Debug.Log("SetColor " + inColor);
        colorBarRed.localScale = new Vector3 (scaleUnit* inColor.r, colorBarRed.localScale.y, colorBarRed.localScale.z);
        //colorBarRed.position = new Vector3(origPosT.position + colorBarRed.right * transform.localScale.x * 0.5f * scaleUnit * inColor.r;

        colorBarBlue.localScale = new Vector3(scaleUnit* inColor.b, colorBarBlue.localScale.y, colorBarBlue.localScale.z);
        //colorBarBlue.position = colorBarBlue.right * transform.localScale.x * 0.5f * scaleUnit * inColor.b;

        colorBarGreen.localScale = new Vector3(scaleUnit * inColor.g, colorBarGreen.localScale.y, colorBarGreen.localScale.z);
        //colorBarGreen.position = colorBarGreen.right * transform.localScale.x * 0.5f * scaleUnit * inColor.g;
        
    }
   
}
