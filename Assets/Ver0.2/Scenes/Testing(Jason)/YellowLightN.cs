using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YellowLightN : ILight
{
    [SerializeField] private float AffectTime;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D m_SphereLight;
    [SerializeField] private TextMeshProUGUI m_DurationText;

    private float initIntensity;
    private float appearanceTimeCount;

    public bool activated = false;
    private GameObject m_Player;

    public override void Start()
    {
        //GetComponent<BoxCollider2D>().enabled = false;
        //initIntensity = m_SphereLight.intensity;
        appearanceTimeCount = 0;

        if(m_Player == null)
        {
            m_Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        if (activated)
        {
            appearanceTimeCount += Time.deltaTime;
            m_DurationText.text = (ApperanceTime - appearanceTimeCount).ToString("0.0");
            //m_SphereLight.intensity -= initIntensity / ApperanceTime * Time.deltaTime;
        }
        else
            m_DurationText.text = "";
    }

    public void ActivateLight()
    {
        activated = true;
        //GetComponent<BoxCollider2D>().enabled = true;

        // Set Alpha
        Color curColor = GetComponent<SpriteRenderer>().color;
        curColor.a = 0.5f;
        GetComponent<SpriteRenderer>().color = curColor;

        FreezeAll();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        // Affect Player / Enemy / Object(dead enemy)'s Gravity
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PreAffectedObjects.Add(collision.gameObject);
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PreAffectedObjects.Remove(collision.gameObject);
        }
    }

    private void FreezeAll()
    {
         foreach (GameObject obj in PreAffectedObjects)
         {
            if (obj.CompareTag("Enemy"))
            {
                obj.GetComponent<IEnemy>().freezeSelf();
            }

            if (obj.GetComponent<SpriteRenderer>())
            {
                obj.GetComponent<SpriteRenderer>().color = Color.yellow;
            }

            AffectedObjects.Add(obj);

        }
        //PreAffectedObjects.Clear();

        StartCoroutine("ResetFreeze");
    }

    IEnumerator ResetFreeze()
    {
        yield return new WaitForSeconds(AffectTime);
        foreach (GameObject obj in AffectedObjects)
        {
            if (obj.GetComponent<IEnemy>())
            {
                obj.GetComponent<IEnemy>().unfreezeSelf();
            }

            if (obj.GetComponent<SpriteRenderer>())
            {
                obj.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        AffectedObjects.Clear();
        RemoveLight();
    }

    public override void RemoveLight()
    {
        activated = false;
        gameObject.SetActive(false);
        appearanceTimeCount = 0;
        transform.position = m_Player.transform.position;
        transform.parent = m_Player.transform;
    }
}
