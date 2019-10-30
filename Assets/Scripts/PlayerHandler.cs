using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour {


    public GameObject perms;
    public GameObject panel;
    public GameObject enemy;
    public Slider speedBar;
    public GameObject enemyBlade;
    private TextMeshProUGUI textmeshPro;
    private Transform bladeTran;
    private PlayerStatus status;
    private HealthVariables enemyStatus;
    private int lastKey;
    private Transform enemyBladeTran;
    
    // Use this for initialization
    void Start()
    {
        enemyBladeTran = enemyBlade.GetComponent<Transform>();
        textmeshPro = GetComponent<TextMeshProUGUI>();
        lastKey = 0;
        status = perms.GetComponent<PlayerStatus>();
        enemyStatus = enemy.GetComponent<HealthVariables>();
        panel.SetActive(true);
        StartCoroutine(Countdown());    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator WaitForKeyDown()
    {
        do
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                lastKey = 1;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                lastKey = 2;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                lastKey = 3;
            }
            yield return null;
        } while (lastKey==0);
    }

    private IEnumerator Countdown()
    {
        float duration = 5-status.level;
        lastKey = 0;
        float totalTime = 0;
        while (totalTime <= duration)
        {
            speedBar.value = totalTime / duration;
            totalTime += Time.deltaTime;
            var integer = (int)totalTime; 
                                          
                yield return null;
        }
        yield return StartCoroutine(WaitForKeyDown());
        StartCoroutine(Countdown());

    }
}
