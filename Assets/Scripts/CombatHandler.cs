using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatHandler : MonoBehaviour {
    public GameObject perms;
    public GameObject enemyBlade; 
    public string objectName;
    public GameObject panel;
    public Slider speedBar;
    public GameObject[] slots;
    public Sprite[] availableSprites;
    public GameObject winner;
    private TextMeshProUGUI winText;
    private Image[] slotStates;
    private int[] playerSlots;
    public int numOfEnemySlots;
    private int[] enemySlots;
    private int slot=0;
    private TextMeshProUGUI textmeshPro;
    private GameObject nameWikiGen;
    private GetWiki title;
    private Transform bladeTran;
    private Vector3 origin;
    private float offset;
    private PlayerStatus status;
    private HealthVariables enemyStatus;

    // Use this for initialization
    void Start () {
        winText = winner.GetComponent<TextMeshProUGUI>();
        slotStates= new Image[slots.Length];
        playerSlots = new int[slots.Length];
        enemySlots = new int[numOfEnemySlots];
        enemyStatus = GetComponent<HealthVariables>();
        for(int i=0; i < 3; i++)
        {
            slotStates[i] = slots[i].GetComponent<Image>();
        }
        status = perms.GetComponent<PlayerStatus>();
        panel.SetActive(true);

        textmeshPro = GetComponent<TextMeshProUGUI>();
        nameWikiGen = GameObject.Find(objectName);
        title = nameWikiGen.GetComponent<GetWiki>();
        bladeTran = enemyBlade.GetComponent<Transform>();
        origin = bladeTran.position;
        StartCoroutine(Countdown());
    }
	
	// Update is called once per frame
	void Update () {
        textmeshPro.SetText(title.title);
    }

    private IEnumerator Countdown()
    {
        float duration = 5; // 3 seconds you can change this to
                             //to whatever you want
        float totalTime = 0;
        EnemyChooseMoves();
        while (totalTime <= duration)
        {
            inputHandle();
            speedBar.value = totalTime / duration;
            totalTime += Time.deltaTime;
            var integer = (int)totalTime; /* choose how to quantize this */
                                          /* convert integer to string and assign to text */
                                          // Debug.Log("Index:" + (1/(float)numOfEnemySlots));
            int iter = (int)(Mathf.Floor(speedBar.value * 10) % Mathf.Floor((1 / (float)numOfEnemySlots) * 10));
           // Debug.Log("Index1:" + iter);

            if (iter == 0) {
                int stage = (int)(Mathf.Floor(speedBar.value * 10) / Mathf.Floor((1 / (float)(numOfEnemySlots)) * 10));
                if (stage == numOfEnemySlots) {
                    stage--;
                }
                Debug.Log("Stage" + (enemySlots[stage]-1));
                Debug.Log("Test" + enemySlots[0] + ", " + enemySlots[1] + ", " + enemySlots[2]);
                Debug.Log("Index2: "+ ((enemySlots[stage]-1)*90));

                bladeTran.transform.rotation = Quaternion.Euler(0, 0, (enemySlots[stage] - 1) * 90);

            }
            yield return null;
        }

        yield return new WaitForSeconds(.5f);
        EvaluateOutcome();
    }


    void inputHandle() {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            slotStates[slot].sprite = availableSprites[0];
            playerSlots[slot] = 1;
            slot++;
            if (slot > 2)
            {
                StartCoroutine(Attack());
            }

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            slotStates[slot].sprite = availableSprites[1];
            playerSlots[slot] = 2;
            slot++;
            if (slot > 2)
            {
                StartCoroutine(Attack());
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            slotStates[slot].sprite = availableSprites[2];
            playerSlots[slot] = 3;
            slot++;
            if (slot > 2)
            {
                StartCoroutine(Attack());
            }
        }
    }
    IEnumerator Attack() {
        slot = 0;
        yield return new WaitForSeconds(.5f);
        EvaluateOutcome();
        for (int i = 0; i < 3; i++) {
            playerSlots[i] = 0;
            slotStates[i].sprite = availableSprites[3];
        }

    }

    void EnemyChooseMoves() {

        for (int i = 0; i < numOfEnemySlots; i++) {
            enemySlots[i] = Mathf.RoundToInt(Random.RandomRange(1,4));
            Debug.Log(enemySlots[i]);
        }
    }

    void EvaluateOutcome() {

        for (int i=0; i<numOfEnemySlots; i++)
        {
            if(enemySlots[i] != playerSlots[i] && playerSlots[i] !=0)
            {
                switch (i) {
                    case 1:
                        if (enemyStatus.leftArmAttached == true)
                        {
                            enemyStatus.leftArmAttached = false;
                        }
                        else {
                            enemyStatus.leftLegAttached = false;
                        }
                        break;
                    case 2:
                        if(enemyStatus.leftArmAttached == false && enemyStatus.rightArmAttached == true)
                        {
                            enemyStatus.headAttached = false;
                        }
                        break;
                    case 3:
                        if (enemyStatus.rightArmAttached == true)
                        {
                            enemyStatus.rightArmAttached = false;
                        }
                        else
                        {
                            enemyStatus.rightLegAttached = false;
                        }
                        break;
                }
            }
            FetchWinner();
        }
    }

    int FetchWinner() {
        int playerLeft = 0;
        int enemyLeft = 0;
        int result = 0;
        //Enemy
        if (enemyStatus.headAttached)
        {
            enemyLeft++;
        }
        if (enemyStatus.leftArmAttached)
        {
            enemyLeft++;
        }
        if (enemyStatus.rightArmAttached)
        {
            enemyLeft++;
        }
        if (enemyStatus.leftLegAttached) {
            enemyLeft++;
        }
        if (enemyStatus.rightLegAttached)
        {
            enemyLeft++;
        }
        //Player
        if (status.headAttached)
        {
            playerLeft++;
        }
        if (status.leftArmAttached)
        {
            playerLeft++;
        }
        if (status.rightArmAttached)
        {
            playerLeft++;
        }
        if (status.leftLegAttached)
        {
            playerLeft++;
        }
        if (status.rightLegAttached)
        {
            playerLeft++;
        }
        //Result
        if (playerLeft > enemyLeft) {
            result = 1;
            winText.text = "Winner!";
        }
        Debug.Log(result);

        return result;
    }
}

