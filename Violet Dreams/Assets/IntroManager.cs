using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public Text startingText;

    public int targetScene;

    public float timeToAppear;

    public float sceneTime;

    float sceneTimer;

    // Start is called before the first frame update
    void Start()
    {
        sceneTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        sceneTimer += Time.deltaTime;

        if (sceneTimer <= timeToAppear)
        {
            Color xd = startingText.color;
            xd.a = sceneTimer / timeToAppear;
            startingText.color = xd;
        }

        if(Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene(targetScene);

        if (sceneTimer > sceneTime)
            SceneManager.LoadScene(targetScene);
        
    }
}
