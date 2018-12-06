using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour {


    Image image;
    public bool fadeMode = false;
    public float fadeTimeCount = 1.5f;
    public float remainTimeCount = 0.0f;
    public GameObject AnnounceUI;


    // Use this for initialization
    void Start () {
        image = AnnounceUI.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //FadeManager.Instance.LoadScene("Main", 2.0f);
        }

        if (fadeMode == true)
        {
            remainTimeCount += Time.deltaTime;
            if (remainTimeCount > 1.5f)
            {
                fadeMode = false;
            }
        }
        else
        {
            remainTimeCount -= Time.deltaTime;
            if (remainTimeCount < 0.0f)
            {
                fadeMode = true;
            }
        }
        float alpha = remainTimeCount / fadeTimeCount;
        image.color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }
}
