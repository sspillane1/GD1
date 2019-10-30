using UnityEngine;

public class HealthVariables : MonoBehaviour
{
    public GameObject head;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject leftLeg;
    public GameObject rightLeg;
    public bool headAttached;
    public bool leftArmAttached;
    public bool rightArmAttached;
    public bool leftLegAttached;
    public bool rightLegAttached;

    public GameObject perms;
    private PlayerStatus status;
    public bool isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        status=perms.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {

        head.SetActive(headAttached);
        leftArm.SetActive(leftArmAttached);
        rightArm.SetActive(rightArmAttached);
        leftLeg.SetActive(leftLegAttached);
        rightLeg.SetActive(rightLegAttached);

        if (isPlayer)
        {
            head.SetActive(status.headAttached);
            leftArm.SetActive(status.leftArmAttached);
            rightArm.SetActive(status.rightArmAttached);
            leftLeg.SetActive(status.leftLegAttached);
            rightLeg.SetActive(status.rightLegAttached);
        }
    }
}
