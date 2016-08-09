using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AnimationTest : MonoBehaviour {

	public float m_fTime;
	public int m_iAnim;
	public int m_iAnimOffset;
	public Image m_imgTest;
	public string m_strTexture;

	public Slider m_slAnimationSpeeed;

	private float FIX_INTERVAL = 0.25f;
	private float m_fAnimationSpeedBase;
	public float m_fAnimationSpeed;

	public Dropdown m_ddFilename;

	public string[] m_strFilenameArr;

	// Use this for initialization

	public void changeDir(){
		m_iAnimOffset += 7;
		m_iAnimOffset %= 8;
	}

	public void changeNextAnimation(){
		m_fTime += FIX_INTERVAL;
		m_slAnimationSpeeed.value = 0.0f;
	}
	public void changeAnimationSpeed( float _fValue ){
		m_fAnimationSpeed = m_fAnimationSpeedBase * (_fValue / m_slAnimationSpeeed.maxValue);
	}

	void Start () {

		string readPath = string.Format ("{0}/test/texture", Application.persistentDataPath);
		EditDirectory.CopyAndReplace (Application.streamingAssetsPath, readPath);

		m_strFilenameArr= System.IO.Directory.GetFiles (readPath, "*.png");

		m_ddFilename.ClearOptions ();

		List<string> filelist = new List<string> ();
		foreach (string file in m_strFilenameArr) {

			filelist.Add ( System.IO.Path.GetFileName( file));
		}
		m_ddFilename.AddOptions (filelist);
		m_ddFilename.value = (m_strFilenameArr.Length - 1);

		// 正面
		m_iAnimOffset = 7;
		m_fAnimationSpeedBase = 2.0f;
		m_fAnimationSpeed = m_fAnimationSpeedBase * (m_slAnimationSpeeed.value / m_slAnimationSpeeed.maxValue);
	}

	public void changeTexture( int _iIndex ){
		m_strTexture = m_ddFilename.captionText.text;
		m_strTexture = string.Format ("test/texture/{0}", m_strTexture);
		m_fTime += FIX_INTERVAL;
	}


	void Update () {
		m_fTime += Time.deltaTime * m_fAnimationSpeed;

		if (FIX_INTERVAL < m_fTime) {
			m_iAnim += 1;
			if (9 <= m_iAnim) {
				m_iAnim = 1;
			}
			m_iAnimOffset %= 8;

			m_imgTest.sprite = SpriteManager.Instance.SpriteCreate ( m_strTexture, new Rect (64.0f*m_iAnim, 98.0f*m_iAnimOffset, 64.0f, 98.0f), new Vector2(0.0f,0.0f));
			m_fTime -= FIX_INTERVAL;
		}


	}
}
