using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.PyroParticles;

public class Abilities : MonoBehaviour
{
    
    //Fireball
    [Header("Ability 1")]
    public Image abiltyImg1;
    public float cooldown1 = 5;
    bool isCooldown = false;
    public KeyCode ability1;

    //Fireball Input
    Vector3 position;
    public Canvas ability1Canvas;
    public Image skillshot;
    public Transform player;

    public GameObject FireballPrefab;
 

    //Teleport
    [Header("Ability 3")]
    public Image abiltyImg2;
    public float cooldown2 = 2;
    bool isCooldown2 = false;
    public KeyCode ability2;

    //Teleport Input
    public Image targetCircle;
    public Image indicatorRangeCircle;
    public Canvas ability2Canvas;
    private Vector3 posUp;
    public float maxAbility2Range;

    //Shield
    [Header("Ability 2")]
    public Image abiltyImg3;
    public float cooldown3 = 5;
    bool isCooldown3 = false;
    public KeyCode ability3;

    //Poison
    [Header("Ability 4")]
    public Image abiltyImg4;
    public float cooldown4 = 5;
    bool isCooldown4 = false;
    public KeyCode ability4;

    //Poision input
    public Image targetCircle3;
    public Canvas ability3Canvas;
    private Vector3 posUp3;
    public float maxAbility3Range;


    // Start is called before the first frame update
    void Start()
    {
        abiltyImg1.fillAmount = 0;
        abiltyImg2.fillAmount = 0;
        abiltyImg3.fillAmount = 0;
        abiltyImg4.fillAmount = 0;

        skillshot.GetComponent<Image>().enabled = false;
        targetCircle.GetComponent<Image>().enabled = false;
        indicatorRangeCircle.GetComponent<Image>().enabled = false;
        targetCircle3.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();
        Ability2();
        Ability3();
        Ability4();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Fireball
        if (Physics.Raycast(ray,out hit,Mathf.Infinity)){
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        //Tp
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
            if (hit.collider.gameObject != this.gameObject){
                posUp = new Vector3(hit.point.x, 10f, hit.point.z);
                position = hit.point;
            }
        }
        //Poison
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
            if (hit.collider.gameObject != this.gameObject){
                posUp3 = new Vector3(hit.point.x, 10f, hit.point.z);
                position = hit.point;
            }
        }

        //Fireball input
        Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y-180,transRot.eulerAngles.z);
        ability1Canvas.transform.rotation = Quaternion.Lerp(transRot, ability1Canvas.transform.rotation, 0f);
    
        //TP
        var hitPosDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point,transform.position);
        distance = Mathf.Min(distance, maxAbility2Range);

        var newHitPos = transform.position + hitPosDir * distance;
        ability2Canvas.transform.position = (newHitPos);
    
        //Poision
        var hitPosDir3 = (hit.point - transform.position).normalized;
        float distance3 = Vector3.Distance(hit.point,transform.position);
        distance3 = Mathf.Min(distance3, maxAbility3Range);

        var newHitPos3 = transform.position + hitPosDir3 * distance3;
        ability3Canvas.transform.position = (newHitPos3);

    }

    void Ability1(){
        if (Input.GetKeyDown(ability1) && isCooldown == false)
        {
            skillshot.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = false;
            indicatorRangeCircle.GetComponent<Image>().enabled = false;
            targetCircle3.GetComponent<Image>().enabled = false;
        }

        if (skillshot.GetComponent<Image>().enabled == true && Input.GetMouseButton(0)){
            isCooldown = true;
            abiltyImg1.fillAmount = 1;

            FireballSpawn();
        }

        if (isCooldown){
            abiltyImg1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            skillshot.GetComponent<Image>().enabled = false;

            if (abiltyImg1.fillAmount <= 0){
                abiltyImg1.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

    void Ability2(){
        if (Input.GetKey(ability2) && isCooldown2 == false)
        {
            skillshot.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = true;
            indicatorRangeCircle.GetComponent<Image>().enabled = true;
            targetCircle3.GetComponent<Image>().enabled = false;
        }


        if (targetCircle.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0)){
            isCooldown2 = true;
            abiltyImg2.fillAmount = 1;
        }

        if (isCooldown2){
            abiltyImg2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            targetCircle.GetComponent<Image>().enabled = false;
            indicatorRangeCircle.GetComponent<Image>().enabled = false;

            if (abiltyImg2.fillAmount <= 0){
                abiltyImg2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }

    void Ability3(){
        if (Input.GetKey(ability3) && isCooldown3 == false)
        {
            isCooldown3 = true;
            abiltyImg3.fillAmount = 1;
        }

        if (isCooldown3){
            abiltyImg3.fillAmount -= 1 / cooldown3 * Time.deltaTime;

            if (abiltyImg3.fillAmount <= 0){
                abiltyImg3.fillAmount = 0;
                isCooldown3 = false;
            }
        }
    }

    void Ability4(){
        if (Input.GetKey(ability4) && isCooldown4 == false)
        {
            skillshot.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;
            indicatorRangeCircle.GetComponent<Image>().enabled = true;
            targetCircle3.GetComponent<Image>().enabled = true;
        }

        if (targetCircle3.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0)){
            isCooldown4 = true;
            abiltyImg4.fillAmount = 1;
        }

        if (isCooldown4){
            abiltyImg4.fillAmount -= 1 / cooldown4 * Time.deltaTime;
            targetCircle3.GetComponent<Image>().enabled = false;
            indicatorRangeCircle.GetComponent<Image>().enabled = false;

            //indicatorRangeCircle3.GetComponent<Image>().enabled = false;

            if (abiltyImg4.fillAmount <= 0){
                abiltyImg4.fillAmount = 0;
                isCooldown4 = false;
            }
        }
    }

    void FireballSpawn(){
        Vector3 SpellSpawnPoint = player.transform.position;

        SpellSpawnPoint.y +=1;

        var rotation = Quaternion.LookRotation(position - player.transform.position);

        GameObject clone = Instantiate(FireballPrefab,SpellSpawnPoint,rotation);
        clone.tag="fireball";
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
    }
}
