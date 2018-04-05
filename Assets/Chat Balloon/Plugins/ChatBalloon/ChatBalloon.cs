using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatBalloon : MonoBehaviour {
	[SerializeField] private Canvas canvas;
	[SerializeField] private Text text;  // Text gameobject
//	[SerializeField] private Text textForDisplay;
	[SerializeField] private Image box;

	public AudioSource sound;

	[HideInInspector][System.NonSerialized] public float minWidth = 20f;
	[HideInInspector][System.NonSerialized] public float maxWidth = 300f;

	[HideInInspector][System.NonSerialized] public float minHeight = 66f;
	[HideInInspector][System.NonSerialized] public float maxHeight = 10000f;

	[HideInInspector][System.NonSerialized] public float widthBorder = 50f;
	[HideInInspector][System.NonSerialized] public float heightBorder = 50f;

	//SetText Cache Variable
	private Vector2 size = Vector2.zero;
	private bool overFlow;
    [HideInInspector]
    float showTime = 1f;

	//this is for testing, in real game the input should be passing through function directly.
	public InputField inputText;
	Color textColor;

	public void Awake(){
		reset ();
		textColor = text.color; //store original color
		text.color = new Color(0.0f,0.0f,0.0f,0.0f);//set text as transparent
		Hide();
	}

	public void SetBox(Sprite _boxSprite){
		#region method
		box.sprite = _boxSprite;
		#endregion
	}


	public void SetText(string _text){
		#region method

		StopCoroutine ("incrementString");
		resetTextSize ();

		//get inputfield content and fill the bubble text content
		text.text = _text.Replace("\\n","\n");
		size = Vector2.zero;
		overFlow = false;

		text.resizeTextForBestFit = false;
		text.horizontalOverflow = HorizontalWrapMode.Wrap;
		text.verticalOverflow = VerticalWrapMode.Truncate;
	
		//Debug.Log("before everything:   Text Box Size:\n" + text.rectTransform.sizeDelta);
		//Adjust Width of size
		if(text.preferredWidth + widthBorder <= minWidth ){
			size.x = minWidth;
		}else if(text.preferredWidth + widthBorder > maxWidth){
			size.x = maxWidth;
		}else{
			size.x = text.preferredWidth + widthBorder;
		}

		//Debug.Log ("current width of the text rectTransform -> " + text.rectTransform.sizeDelta.x + ", preferred height -> " + text.preferredHeight);

		//Adjust height
		if(text.preferredHeight + heightBorder <= minHeight ){
			size.y = minHeight;
		}else{
			size.y = text.preferredHeight + heightBorder;
		}


		StartCoroutine ("AnimateWidth", size);
//		box.rectTransform.sizeDelta = size;

		size.x -= widthBorder;
		size.y -= heightBorder;

		//adjust text bound box
		text.rectTransform.sizeDelta = size;
		 
		text.text = "";
		text.color = textColor;

		StartCoroutine ("incrementString", _text.Replace("\\n","\n"));

		#endregion
	}

	IEnumerator AnimateWidth(Vector2 targetSize) {
		float widthNow = box.rectTransform.sizeDelta.x;
		float heightNow = box.rectTransform.sizeDelta.y;

		float targetWidth = targetSize.x ;
		float targetHeight = targetSize.y ;

		while (Mathf.Abs (widthNow - targetWidth) > 2 || Mathf.Abs(heightNow - targetHeight) > 2 ){
			widthNow = box.rectTransform.sizeDelta.x;
			heightNow = box.rectTransform.sizeDelta.y;

			Vector2 tempSize = new Vector2(widthNow + (targetWidth-widthNow) / 3.0f , heightNow + (targetHeight-heightNow)/3.0f);
			box.rectTransform.sizeDelta = tempSize;
			yield return null;
		}
	}

	IEnumerator incrementString(string completeString){
		int substringIndex = 0;
		if (completeString != "") {
			sound.Play ();
		}

		while(substringIndex < completeString.Length+1){
			string currentString = completeString.Substring (0, substringIndex);

			if ( currentString!="" && char.IsWhiteSpace (currentString[currentString.Length - 1])) {
				sound.Play ();
			}
			text.text = currentString;
			substringIndex++;
			if (substringIndex == completeString.Length + 1) {
                //Debug.Log("Hide");
				Invoke ("Hide", showTime);
			}
			
			yield return null;
		}

	}

	public void reset(){
		StopCoroutine ("AnimateWidth");
		StopCoroutine ("incrementString");
		resetSizes ();
		text.text = "";
	}

	public void resetSizes(){
		resetTextSize ();
		resetBoxSize ();
	}

	public void resetTextSize(){
		text.rectTransform.sizeDelta = new Vector2 (maxWidth-widthBorder, minHeight-heightBorder);

	}

	public void resetBoxSize(){
		box.rectTransform.sizeDelta = new Vector2 (minWidth, minHeight);

	}

    public void SetTextAndActive(string s, float showTime){
        this.showTime = showTime;
		SetText (s);
		SetActive (true);
	}

	public void SetActive(bool status){
		#region method
		CancelInvoke();
		canvas.enabled = status;
		#endregion
	}
	public void Hide(){
        #region method
		SetActive(false);
		text.text = "";
		resetSizes ();
		#endregion
	}

	public void SetRenderLayer(string layerName){
		#region method
		canvas.sortingLayerName = layerName;
		#endregion
	}
	public void SetRenderLayer(int layerIndex){
		#region method
		canvas.sortingLayerID = layerIndex;
		#endregion
	}
	public void SetRenderOrder(int order){
		#region method
		canvas.sortingOrder = order;
		#endregion
	}
}