using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePop : MonoBehaviour
{
    [SerializeField] private GameObject DamageText;

    public void PopDamage(int damage)
    {
        GameObject dmgText = Instantiate(DamageText, transform.position, Quaternion.identity);
        dmgText.transform.parent = transform;
        dmgText.transform.Find("DamageText").GetComponent<TextMeshProUGUI>().text = "-" + damage.ToString();
    }


}
