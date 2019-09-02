using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class sctObjChange : MonoBehaviour {

    public string[] ObjStatus;
    public GameObject[] obj;
    int objStatus = 0;



    public Text text;

	void Start()
	{
		for(int i=0;i< ObjStatus.Length;i++)
		{
			if(objStatus == i)
			{
				//obj[i].SetActive(true);
				obj[i].transform.localScale = new Vector3(150f,150f,150f);
			}
			else
			{
				
				//obj[i].SetActive(false);
				obj[i].transform.localScale = Vector3.zero;
			}
		}
		text.text = ObjStatus[objStatus];
	}


    public void SetStatus()
    {
        objStatus += 1;

        if (objStatus > ObjStatus.Length - 1)
        {
            objStatus = 0;
        }


        for(int i=0;i< ObjStatus.Length;i++)
        {
            if(objStatus == i)
            {
                //obj[i].SetActive(true);
				obj[i].transform.localScale = new Vector3(150f,150f,150f);
            }
            else
            {
                //obj[i].SetActive(false);
				obj[i].transform.localScale = Vector3.zero;
            }
        }
        
        text.text = ObjStatus[objStatus];
    }
}
