using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillCast : MonoBehaviour
{
    [Header("Mana Settings")]
    public float totalMana = 100f;
    public float manaRegenSpeed = 2f;
    private Image manaBar;
    [Header("Cooldown Icons")]
    public Image[] CooldownIcon;
    [Header("Out Of Mana Icons")]
    public Image[] OutOfManaIcons;
    [Header("Cooldown Times")]
    public float cooldownTime1 = 1f;
    public float cooldownTime2 = 1f;
    public float cooldownTime3 = 1f;
    public float cooldownTime4 = 1f;
    public float cooldownTime5 = 1f;
    public float cooldownTime6 = 1f;
    [Header("Mana Amounts")]
    public float Skill1ManaAmount = 20f;
    public float Skill2ManaAmount = 20f;
    public float Skill3ManaAmount = 20f;
    public float Skill4ManaAmount = 20f;
    public float Skill5ManaAmount = 20f;
    public float Skill6ManaAmount = 20f;
    [Header("Required level")]
    public int skill1 = 2 ;
    public int skill2 = 3;
    public int skill3 = 4;
    public int skill4 = 5;
    public int skill5 = 6;
    public int skill6 = 7;

    private bool faded = false;

    private int[] fadeImages = new int[] { 0, 0, 0, 0, 0, 0, };
    [HideInInspector]public List<float> CooldownTimesList = new List<float>();
    private List<float> ManaAmountList = new List<float>();
    private List<int> LevelList = new List<int>();
    private Animator anim;

    private bool canAttack = true;

    private PlayerOnClick playerOnClick;

    private LevelManager levelManager;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerOnClick = GetComponent<PlayerOnClick>();
        manaBar = GameObject.Find("ManaOrb").GetComponent<Image>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Start()
    {
        AddList();
    }

    void AddList()
    {
        //CooldownListAdd
        CooldownTimesList.Add(cooldownTime1);
        CooldownTimesList.Add(cooldownTime2);
        CooldownTimesList.Add(cooldownTime3);
        CooldownTimesList.Add(cooldownTime4);
        CooldownTimesList.Add(cooldownTime5);
        CooldownTimesList.Add(cooldownTime6);
        //ManaAmountListAdd
        ManaAmountList.Add(Skill1ManaAmount);
        ManaAmountList.Add(Skill2ManaAmount);
        ManaAmountList.Add(Skill3ManaAmount);
        ManaAmountList.Add(Skill4ManaAmount);
        ManaAmountList.Add(Skill5ManaAmount);
        ManaAmountList.Add(Skill6ManaAmount);
        //Level
        LevelList.Add(skill1);
        LevelList.Add(skill2);
        LevelList.Add(skill3);
        LevelList.Add(skill4);
        LevelList.Add(skill5);
        LevelList.Add(skill6);
    }

    void Update()
    {
        if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Great Sword Idle"))
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
        if (anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Great Sword Idle"))
        {
            TurnThePlayer();
        }
        if (totalMana < 100f)
        {
            totalMana += Time.deltaTime * manaRegenSpeed;
            manaBar.fillAmount = totalMana / 100f;
        }
        CheckMana();
        CheckToFade();
        CheckInput();
        CheckLevel();


    }

    void CheckInput()
    {
        if (anim.GetInteger("Attack") == 0)
        {
            playerOnClick.FinishedMovement = false;
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Great Sword Idle"))
            {
                playerOnClick.FinishedMovement = true;
            }
        }
        //skill Input
        if (Input.GetKeyDown(KeyCode.Alpha1) && totalMana >= Skill1ManaAmount && levelManager.GetLevel >= skill1)
        {
            playerOnClick.TargetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[0] != 1 && canAttack)
            {
                totalMana -= Skill1ManaAmount;
                fadeImages[0] = 1;
                anim.SetInteger("Attack", 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && totalMana >= Skill2ManaAmount && levelManager.GetLevel >= skill2)
        {
            playerOnClick.TargetPosition = transform.position;
            if (/*playerOnClick.FinishedMovement &&*/ fadeImages[1] != 1)
            {
                totalMana -= Skill2ManaAmount;
                fadeImages[1] = 1;
                anim.SetInteger("Attack", 2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && totalMana >= Skill3ManaAmount && levelManager.GetLevel >= skill3)
        {
            playerOnClick.TargetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[2] != 1)
            {
                totalMana -= Skill3ManaAmount;
                fadeImages[2] = 1;
                anim.SetInteger("Attack", 3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && totalMana >= Skill4ManaAmount && levelManager.GetLevel >= skill4)
        {
            playerOnClick.TargetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[3] != 1)
            {
                totalMana -= Skill4ManaAmount;
                fadeImages[3] = 1;
                anim.SetInteger("Attack", 4);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && totalMana >= Skill5ManaAmount && levelManager.GetLevel >= skill5)
        {
            playerOnClick.TargetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[4] != 1)
            {
                totalMana -= Skill5ManaAmount;
                fadeImages[4] = 1;
                anim.SetInteger("Attack", 5);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && totalMana >= Skill6ManaAmount && levelManager.GetLevel >= skill6)
        {
            playerOnClick.TargetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[5] != 1)
            {
                totalMana -= Skill6ManaAmount;
                fadeImages[5] = 1;
                anim.SetInteger("Attack", 6);
            }
        }
        else
        {
            anim.SetInteger("Attack", 0);
        }
    }

    void CheckToFade()
    {
        for (int i = 0; i < CooldownIcon.Length; i++)
        {
            if (fadeImages[i] == 1)
            {
                if (FadeAndWait(CooldownIcon[i], CooldownTimesList[i]))
                {
                    fadeImages[i] = 0;
                }
            }
        }
    }

    void CheckMana()
    {
        for (int i = 0; i < OutOfManaIcons.Length; i++)
        {
            if (levelManager.GetLevel >= LevelList[i])
            {
                if (totalMana < ManaAmountList[i])
                {
                    OutOfManaIcons[i].gameObject.SetActive(true);
                }
                else
                {
                    OutOfManaIcons[i].gameObject.SetActive(false);
                }
            }
            
        }
    }
    void CheckLevel()
    {
        for (int i = 0; i < OutOfManaIcons.Length; i++)
        {
            if (levelManager.GetLevel < LevelList[i])
            {
                OutOfManaIcons[i].gameObject.SetActive(true);
            }
        }
    }
    bool FadeAndWait(Image fadeImage, float fadeTime)
    {
        faded = false;
        if (fadeImage == null)
        {
            return faded;
        }
        if (!fadeImage.gameObject.activeInHierarchy)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.fillAmount = 1f;
        }

        fadeImage.fillAmount -= fadeTime * Time.deltaTime;

        if (fadeImage.fillAmount <= 0f)
        {
            fadeImage.gameObject.SetActive(false);
            faded = true;
        }
        return faded;
    }
    void TurnThePlayer()
    {
        Vector3 targetPos = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit))
        {
            targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position),
            playerOnClick.turnSpeed * Time.deltaTime);
    }
}
