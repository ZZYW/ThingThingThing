using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class sctFace : MonoBehaviour {

    public string[] FaceStatus;
    public Texture[] obj;
    int faceStatus = 0;



    public Text text;

    public void SetStatus()
    {
        faceStatus += 1;

        if (faceStatus > FaceStatus.Length - 1)
        {
            faceStatus = 0;
        }
        Renderer test = this.gameObject.GetComponent<Renderer>();
        test.material.mainTexture = obj[faceStatus];
        text.text = FaceStatus[faceStatus];
    }
}
