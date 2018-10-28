using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChatBalloon : MonoBehaviour {
	[SerializeField] Canvas canvas;
	[SerializeField] Text text;
	[SerializeField] Image box;

	public float minWidth = 20f;
	public float maxWidth = 300f;

	public float minHeight = 66f;
	public float maxHeight = 10000f;

	public float widthBorder = 50f;
	public float heightBorder = 50f;
	Color textColor = Color.black;
	Color bubbleColor = Color.white;

	//SetText Cache Variable
	Vector2 size = Vector2.zero;
	float showTime = 1f;

	public void SetTextAndActive (string s, float showTime) {
		this.showTime = showTime;
		SetText (s);
		SetActive (true);
	}

	public void SetTextAndActive (string s) {
		SetTextAndActive (s, 2);
	}

	void Awake () {
		Reset ();
		box.color = bubbleColor;
		text.color = new Color (0.0f, 0.0f, 0.0f, 0.0f); //set text as transparent
		Hide ();
	}

	void OnDisable () {
		StopAllCoroutines ();
	}

	void SetBox (Sprite _boxSprite) {
		box.sprite = _boxSprite;
	}

	void SetText (string _text) {

		Reset ();
		text.text = _text.Replace ("\\n", "\n");
		size = Vector2.zero;

		//config Text
		text.resizeTextForBestFit = false;
		text.horizontalOverflow = HorizontalWrapMode.Wrap;
		text.verticalOverflow = VerticalWrapMode.Truncate;

		//Debug.Log("before everything:   Text Box Size:\n" + text.rectTransform.sizeDelta);
		//Adjust Width of size
		if (text.preferredWidth + widthBorder <= minWidth) {
			size.x = minWidth;
		} else if (text.preferredWidth + widthBorder > maxWidth) {
			size.x = maxWidth;
		} else {
			size.x = text.preferredWidth + widthBorder;
		}

		//Adjust height
		if (text.preferredHeight + heightBorder <= minHeight) {
			size.y = minHeight;
		} else {
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

		StartCoroutine ("IncrementString", _text.Replace ("\\n", "\n"));

	}

	IEnumerator AnimateWidth (Vector2 targetSize) {
		float widthNow = box.rectTransform.sizeDelta.x;
		float heightNow = box.rectTransform.sizeDelta.y;

		float targetWidth = targetSize.x;
		float targetHeight = targetSize.y;

		while (Mathf.Abs (widthNow - targetWidth) > 2 || Mathf.Abs (heightNow - targetHeight) > 2) {
			widthNow = box.rectTransform.sizeDelta.x;
			heightNow = box.rectTransform.sizeDelta.y;

			Vector2 tempSize = new Vector2 (widthNow + (targetWidth - widthNow) / 3.0f, heightNow + (targetHeight - heightNow) / 3.0f);
			box.rectTransform.sizeDelta = tempSize;
			yield return null;
		}
	}

	IEnumerator IncrementString (string completeString) {
		int substringIndex = 0;

		while (substringIndex < completeString.Length + 1) {
			string currentString = completeString.Substring (0, substringIndex);
			text.text = currentString;
			substringIndex++;
			if (substringIndex == completeString.Length + 1) {
				Invoke ("Hide", showTime);
			}

			yield return null;
		}

	}

	void Update () {
		if (Time.frameCount % 4 == 0) {
			transform.LookAt (Camera.main.transform);
		}
	}

	void Reset () {
		StopAllCoroutines ();
		resetSizes ();
		text.text = "";
	}

	void resetSizes () {
		resetTextSize ();
		resetBoxSize ();
	}

	void resetTextSize () {
		text.rectTransform.sizeDelta = new Vector2 (maxWidth - widthBorder, minHeight - heightBorder);
	}

	void resetBoxSize () {
		box.rectTransform.sizeDelta = new Vector2 (minWidth, minHeight);
	}

	void SetActive (bool status) {
		CancelInvoke ();
		canvas.enabled = status;
	}

	void Hide () {
		SetActive (false);
		text.text = "";
		resetSizes ();
	}

	void SetRenderLayer (string layerName) {
		canvas.sortingLayerName = layerName;
	}

	void SetRenderLayer (int layerIndex) {
		canvas.sortingLayerID = layerIndex;
	}

	void SetRenderOrder (int order) {
		canvas.sortingOrder = order;
	}
}