using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sctPlayerControl : MonoBehaviour {

    public string[] CharStatus;
    int charStatus = 0;


    public Animator[] animator;

    public Text text;

    public void SetStatus()
    {
        charStatus += 1;

        if (charStatus > CharStatus.Length -1)
        {
            charStatus = 0;
        }
        text.text = CharStatus[charStatus];

		for(int i=0;i<animator.Length;i++)
		{
			//if(animator[i].isActiveAndEnabled)
			{
        	animator[i].SetInteger("Status", charStatus);
			}
		}
    }
}
