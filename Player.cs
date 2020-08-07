using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public PlayerStats stats;

    Vector3 playerScale = new Vector3(3f, 3f, 3);

    public int[] ItemMoneyList = { 1100, 2600,2800,3200,3400,3500,450,300,400,900,1300,3600 };
    public List<string> ItemList;
    public Sprite[] ItemSprites;
    public List<string> ItemStrings;
    public int Money = 50000;

    public int UltDmg = 200;

    public int KillW = 0;
    public int KillB = 0;

    public int QStack = 0;

    public bool Isinvincibility = false;

    public int Kill = 0;

    public Text HPT;
    public Text EnergyT;
    public GameObject AttackEffect1;
    GameObject HPBar;
    GameObject EBar;
    GameObject rayPoint;
    public GameObject Target = null;

    GameObject EnemyUI;
    Animator animator;
    public GameObject Lightning;

    Skill QScript;
    Skill WScript;
    Skill EScript;
    Skill RScript;

    int jmpCnt = 2;

    public int HP = 2000;
    public int ShieldE = 1000;

    public float staticCharge = 0;
    //public int Dmg = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Background" || collision.gameObject.tag == "Floor")
            jmpCnt = 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Background" || collision.gameObject.tag == "Floor")
            jmpCnt = 2;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Return) && collision.gameObject.name == "portal")
        {
            print("AAAAAAAAAAAAAA");
            SceneManager.LoadScene(1);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Background")
            collision.collider.isTrigger = true;
    }

    public bool Critical()
    {
        return Random.Range(0, 100) <= stats.Critical ? true : false;        
    }
    void Start()
    {
        GameObject.Find("Yasuo_").transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<IkCCD2D>().enabled = false;
        transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<IkCCD2D>().enabled = true;

        if (SceneManager.GetActiveScene().buildIndex == 1)
            GameObject.Find("EnemyUI").SetActive(false);

        HPBar = GameObject.Find("HP_Bar");
        EBar = GameObject.Find("Energy_Bar");

        animator = transform.GetChild(0).GetComponent<Animator>();
        rayPoint = transform.GetChild(1).gameObject;

        QScript = GameObject.Find("CoolTime Q").GetComponent<Skill>();
        WScript = GameObject.Find("CoolTime W").GetComponent<Skill>();
        EScript = GameObject.Find("CoolTime E").GetComponent<Skill>();
        RScript = GameObject.Find("CoolTime R").GetComponent<Skill>();

        ItemList = new List<string>();
        stats = new PlayerStats(0,11, 10, 10, 0, 1.2f, 0);
    }

    void SetAS()
    {
        transform.GetChild(0).GetComponent<Animator>().SetFloat("AS", 2-stats.AttackSpeed);
    }
    public GameObject staticChargeImage;
    void ApplyItem(string name)
    {
        switch(name)
        {
            case "신속":
                stats.Speed += 0.2f;
                break;
            case "스태틱":
                if(stats.AttackSpeed - 0.7f >= 0.2f)
                    stats.AttackSpeed -=  0.7f;
                stats.Critical += 50;
                staticChargeImage.SetActive(true);
                SetAS();
                break;
            case "도미닉":
                stats.Dmg += 45;
                stats.ArmorPenetration += 20;
                break;
            case "스테락":
                stats.Dmg += 70;
                stats.Armor += 30;
                break;
            case "인피":
                stats.Dmg += 80;
                stats.Critical += 50;
                break;
            case "피바":
                stats.Dmg += 80;
                if(stats.Blood + 20 <= 100)
                    stats.Blood += 20;
                break;
            case "도란검":
                stats.Dmg += 8;
                if (stats.Blood + 3 <= 100)
                    stats.Blood += 3;
                break;
            case "단검":
                if (stats.AttackSpeed - 0.2f >= 0.2f)
                    stats.AttackSpeed -= 0.2f;                
                SetAS();
                break;
            case "장갑":
                stats.Critical += 5;
                break;
            case "흡낫":
                stats.Dmg += 15;
                if (stats.Blood + 10 <= 100)
                    stats.Blood += 10;
                break;
            case "BF":
                stats.Dmg += 40;
                break;
            case "라바돈":
                stats.AP += 120;
                stats.AP += (int)(stats.AP * 0.5f);
                break;
        }
    }

    public void DeleteItem(string name)
    {
        switch (name)
        {
            case "신속":
                stats.Speed -= 0.2f;
                break;
            case "스태틱":
                stats.Critical -= 50;                
                stats.AttackSpeed += 0.7f;
                staticChargeImage.SetActive(true);
                SetAS();
                break;
            case "도미닉":
                stats.Dmg -= 45;
                stats.ArmorPenetration -= 20;
                break;
            case "스테락":
                stats.Dmg -= 70;
                stats.Armor -= 30;
                break;
            case "인피":
                stats.Dmg -= 80;
                stats.Critical -= 50;
                break;
            case "피바":
                stats.Dmg -= 80;
                if (stats.Blood - 20 >= 0)
                    stats.Blood -= 20;
                break;
            case "도란검":
                stats.Dmg -= 8;
                stats.Blood -= 3;
                break;
            case "장갑":
                stats.Critical -= 5;
                break;
            case "단검":
                if(stats.AttackSpeed + 0.2f <= 1.2f)
                    stats.AttackSpeed += 0.2f;
                SetAS();
                break;
            case "흡낫":
                stats.Dmg -= 15;
                if (stats.Blood - 10 >= 0)
                    stats.Blood -= 10;
                break;
            case "BF":
                stats.Dmg -= 40;
                break;
            case "라바돈":
                stats.AP -= 120;
                break;
        }
    }

    public void AddItem(string name)
    {
        ItemList.Add(name);
        ApplyItem(name);
        DisplayItem();
    }

    void Display()
    {
        //DisplayItem();
        DisplayMoney();
        DisplayStats();
    }

    void DisplayStats()
    {
        GameObject.Find("AD").transform.GetChild(0).GetComponent<Text>().text = stats.Dmg.ToString();
        GameObject.Find("AS").transform.GetChild(0).GetComponent<Text>().text = stats.AttackSpeed.ToString();
        GameObject.Find("AP").transform.GetChild(0).GetComponent<Text>().text = stats.AP.ToString();
        GameObject.Find("AM").transform.GetChild(0).GetComponent<Text>().text = stats.Armor.ToString();
    }

    void DisplayMoney()
    {
        GameObject.Find("Money").GetComponent<Text>().text = "Money : " + Money.ToString();
    }

    public void DisplayItem()
    {
        for(int i=0;i<ItemList.Count;i++)
        {
            Image obj = GameObject.Find("Cell_" + i).GetComponent<Image>();
            obj.sprite = ItemSprites[ItemStrings.IndexOf(ItemList[i])];
            obj.color = Color.white;
        }
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jmpCnt > 0)// && GetComponent<Rigidbody2D>().velocity.y == 0
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            jmpCnt--;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x >= GameObject.Find("EndRight").transform.position.x)
                goto Skip2;
            transform.localScale = playerScale;
            animator.SetBool("IsWalk", true);
            transform.Translate(new Vector2(1, 0) * Time.deltaTime * stats.Speed);
            if (ShieldE <= 1000)
                ShieldE += 2;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x <= GameObject.Find("EndLeft").transform.position.x)
                goto Skip2;
            transform.localScale = playerScale - new Vector3(6, 0, 0);
            animator.SetBool("IsWalk", true);
            transform.Translate(new Vector2(-1, 0) * Time.deltaTime * stats.Speed);
            if (ShieldE <= 1000)
                ShieldE += 2;
        }
        if (ItemList.Contains("스태틱"))
            staticCharge += 0.1f;
    Skip2:
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("IsWalk", false);
        }
    }
    float width1hp = 0.584f;

    public bool a = false;

    public void DisplayTargetInfo()
    {
        GameObject.Find("EnemyHP").GetComponent<Text>().text =
            Target.GetComponent<MonsterInfo>().hp + "/" + Target.GetComponent<MonsterInfo>().maxHp;
        GameObject.Find("EHP_Bar").GetComponentInChildren<RectTransform>().sizeDelta =
            new Vector2(
                900 / Target.GetComponent<MonsterInfo>().maxHp * Target.GetComponent<MonsterInfo>().hp,
                GameObject.Find("EHP_Bar").GetComponentInChildren<RectTransform>().sizeDelta.y
                );
    }

    public void DisplayTarget(GameObject target)
    {
        if (target != Target && a)
        {
            print(Target.name);
            if (Target.name.Contains("Monster_Bird"))
            {
                for (int i = 0; i <= 3; i++)
                    Target.transform.GetChild(0).GetChild(i).GetComponent<SpriteMeshInstance>().color = Color.white;
            }
            else
            {
                Target.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        Target = target;
        if (target.name.Contains("Monster_Bird"))
        {
            for (int i = 0; i <= 3; i++)
                target.transform.GetChild(0).GetChild(i).GetComponent<SpriteMeshInstance>().color = Color.red;
        }
        else
            target.GetComponent<SpriteRenderer>().color = Color.red;
        GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("Name").GetComponent<Text>().text = Target.GetComponent<MonsterInfo>().Name;
        GameObject.Find("DmgText").GetComponent<Text>().text = Target.GetComponent<MonsterInfo>().Dmg + "";
        if (Target.name.Contains("Monster_Bird"))
        {
            GameObject.Find("ProfileImage").GetComponentInChildren<RectTransform>().sizeDelta =
            Target.GetComponent<MonsterInfo>().profileImage.rect.size / 5f;
        }
        else
            GameObject.Find("ProfileImage").GetComponentInChildren<RectTransform>().sizeDelta =
            Target.GetComponent<MonsterInfo>().profileImage.rect.size / 1.5f;
        GameObject.Find("ProfileImage").GetComponent<Image>().sprite = Target.GetComponent<MonsterInfo>().profileImage;

        DisplayTargetInfo();
        a = true;
    }

    IEnumerator ShieldC()
    {
        yield return new WaitForSeconds(1);
        Color c = transform.GetChild(2).GetComponent<SpriteRenderer>().color;
        c.a = 116;
        transform.GetChild(2).GetComponent<SpriteRenderer>().color = c;
        yield return new WaitForSeconds(0.8f);
        c.a = 255;
        transform.GetChild(2).GetComponent<SpriteRenderer>().color = c;
        yield return new WaitForSeconds(0.8f);
        c.a = 116;
        transform.GetChild(2).GetComponent<SpriteRenderer>().color = c;
        yield return new WaitForSeconds(0.8f);
        transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        Isinvincibility = false;
        ss = true;
    }
    bool ss = true;
    public void Shield()
    {
        if (ss)
        {
            ss = false;
            StartCoroutine(ShieldC());
        }
    }

    void ShakeCamera(bool Shake, float ShakePower)
    {
        if (Shake)
        {
            if (ShakePower > 0)
                ShakePower -= 5.0f * Time.deltaTime;
        }
        else
        {
            Shake = false;
            ShakePower = 0;
        }
    }
    int LKill = 0;
    void Update()
    {
        if (HP <= 0)
            return;
        Display();
        if (ShieldE > 1000) ShieldE = 1000;

        EnergyT.text = ShieldE + "/1000";
        EBar.GetComponentInChildren<RectTransform>().sizeDelta =
        new Vector2(1.168f * ShieldE,
                    EBar.GetComponentInChildren<RectTransform>().sizeDelta.y
                    );

        HPBar.GetComponentInChildren<RectTransform>().sizeDelta =
            new Vector2(width1hp * HP,
                        HPBar.GetComponentInChildren<RectTransform>().sizeDelta.y
                        );

        HPT.text = HP + "/2000";


        RaycastHit2D hit = Physics2D.Raycast(rayPoint.transform.position, new Vector2(0, -10), 20);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Background")
            {
                hit.collider.isTrigger = false;
            }
        }
        if (!transform.GetChild(0).GetComponent<Animator>().GetBool("Ult") &&
            !transform.GetChild(0).GetComponent<Animator>().GetBool("3QS"))
            Movement();

        if(ItemList.Contains("스태틱"))
        {
            if (staticCharge >= 100)
            {
                staticCharge = 0;
                Instantiate(Lightning, Target.transform.position,Quaternion.identity);
            }
            staticChargeImage.transform.GetChild(0).GetComponent<Text>().text = ((int)staticCharge).ToString();
        }

        if (Input.GetKey(KeyCode.LeftControl) && CanControlAttack)
        {
            StopCoroutine("Timer");
            CanControlAttack = false;
            StartCoroutine("Timer");
            GameObject CEffect = Instantiate(AttackEffect1, new Vector3(transform.position.x + 0.8f, transform.position.y, 0), Quaternion.identity);
            CEffect.transform.parent = gameObject.transform;
            animator.SetBool("LeftControl", true);

            transform.GetChild(0).GetChild(1).GetComponent<NormalAttack>().NormalEffect();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("LeftControl", false);
        }
        if (Input.GetKey(KeyCode.Q) && QScript.canUseSkill)
        {
            if (!EAction)
            {
                if (QStack == 2)
                {
                    QStack = 0;
                    GameObject.Find("QS").GetComponent<Text>().text = QStack + "";
                    animator.SetBool("3QS", true);
                }
                else
                    animator.SetBool("QSkill", true);
            }
            else
            {
                animator.SetBool("EQS", true);
                transform.GetChild(0).GetChild(3).GetComponent<EQSkill>().EEffect();
            }
            GameObject.Find("CoolTime Q").SendMessage("UseSkill");
        }

        if (Input.GetKey(KeyCode.E) && EScript.canUseSkill)
        {
            EAction = true;
            for (int i = 0; i < 7; i++)
                GameObject.Find("잔상").transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
            int move = transform.localScale.x < 0 ? -1 : 1;
            if (move == -1)
            {
                if (transform.position.x - 15 < GameObject.Find("EndLeft").transform.position.x)
                {
                    GameObject.Find("잔상").transform.position = new Vector3(
                        GameObject.Find("EndLeft").transform.position.x,
                        transform.position.y, 0);
                }
                else
                    GameObject.Find("잔상").transform.position = new Vector3(
                        transform.position.x - 15,
                        transform.position.y, 0);
            }
            else
            {
                if (transform.position.x + 15 > GameObject.Find("EndRight").transform.position.x)
                {
                    GameObject.Find("잔상").transform.position = new Vector3(
                        GameObject.Find("EndRight").transform.position.x,
                        transform.position.y, 0);
                }
                else
                    GameObject.Find("잔상").transform.position = new Vector3(
                        transform.position.x + 15,
                        transform.position.y, 0);
            }
        }

        if (Input.GetKeyUp(KeyCode.E) && EScript.canUseSkill)
        {
            EAction = true;
            StartCoroutine(ESKillM());
            transform.GetChild(4).GetComponent<ParticleSystem>().Play();
            transform.GetChild(0).GetChild(2).GetComponent<ESkillEffect>().EEffect();
            GameObject.Find("CoolTime E").SendMessage("UseSkill");
            transform.position = GameObject.Find("잔상").transform.position;
            for (int i = 0; i < 7; i++)            
                GameObject.Find("잔상").transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;            
        }

        if (Input.GetKeyDown(KeyCode.R) && RScript.canUseSkill && IsThereAirboned())
        {
            RScript.UseSkill();
            GameObject[] gameObjects = GetAirbonedMonsters();
            float avX = 0;
            int cnt = 0;
            float sum = 0;
            foreach (GameObject item in gameObjects)
            {
                if (item.name.Contains("Wolf"))
                {
                    item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
                    item.GetComponent<Wolf>().CantMove = true;
                }
                else if (item.name.Contains("Bird"))
                {
                    item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
                    item.GetComponent<Bird>().CantMove = true;
                }
                sum += item.transform.position.x;
                cnt++;
            }
            avX = sum / cnt;

            transform.position = new Vector3(avX, transform.position.y, transform.position.z);
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

            transform.GetChild(0).GetComponent<UltEffect>().avX = avX;
            transform.GetChild(0).GetComponent<UltEffect>().Monsters = gameObjects;
            transform.GetChild(0).GetComponent<Animator>().SetBool("Ult", true);
        }
        if (Input.GetKey(KeyCode.W) && WScript.canUseSkill && Application.loadedLevelName == "Fight")
        {
            if (!Target)
                return;
            WScript.UseSkill();
            Target.GetComponent<MonsterInfo>().hp -= Target.GetComponent<MonsterInfo>().maxHp / 100 * 20;
            HP += (int)(Target.GetComponent<MonsterInfo>().maxHp * 0.4f + stats.AP);
            
        }
        if (HP > 2000) HP = 2000;
    }

    bool IsThereAirboned()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Monsters"))
        {
            if (item.name.Contains("Wolf"))
            {
                if (item.GetComponent<Wolf>().IsAirboned)
                    return true;
            }
            if (item.name.Contains("Bird"))
            {
                if (item.GetComponent<Bird>().IsAirboned)
                    return true;
            }
        }
        return false;
    }

    GameObject[] GetAirbonedMonsters()
    {
        List<GameObject> list = new List<GameObject>();
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Monsters"))
        {
            if (item.name.Contains("Wolf"))
            {
                if (item.GetComponent<Wolf>().IsAirboned)
                    list.Add(item);
            }
            if (item.name.Contains("Bird"))
            {
                if (item.GetComponent<Bird>().IsAirboned)
                    list.Add(item);
            }
        }
        return list.ToArray();
    }

    bool EAction = false;
    IEnumerator ESKillM()
    {
        yield return new WaitForSeconds(0.5f);
        EAction = false;
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(stats.AttackSpeed);
        CanControlAttack = true;
    }
    bool CanControlAttack = true;
}
