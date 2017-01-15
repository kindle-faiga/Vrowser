using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
	[SerializeField]
	private Sprite[] tutorialImages;
	private Image mainImage;
	private int count = 0;
	private float easeTime = 0.1f;
	private float waitRange = 5.0f;
	private Vector3 defaultSize;

	void Start()
	{
		mainImage = GetComponent<Image>();

		mainImage.sprite = tutorialImages[0];

		defaultSize = transform.localScale;

		transform.localScale = Vector3.zero;

		StartCoroutine(WaitForUpdate());
	}

	void Update()
	{

	}

	public void updateImage()
	{
		mainImage.sprite = tutorialImages[count];
	}

	private IEnumerator WaitForUpdate()
	{
		yield return new WaitForSeconds(waitRange * 0.5f);

		iTween.ScaleTo(gameObject, defaultSize, easeTime);

		yield return new WaitForSeconds(waitRange);

		iTween.ScaleTo(gameObject, Vector3.zero, easeTime);

		yield return new WaitForSeconds(easeTime);

		++count;

		if (count < tutorialImages.Length)
		{
			updateImage();

			StartCoroutine(WaitForUpdate());
		}
	}
}
