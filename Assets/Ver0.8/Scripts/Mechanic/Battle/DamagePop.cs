using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePop : MonoBehaviour
{
    [SerializeField] private GameObject DamageText;

    public void PopDamage(float damage)
    {
        GameObject dmgText = Instantiate(DamageText, transform.position, Quaternion.identity);
        dmgText.transform.SetParent(transform, true);
        dmgText.transform.Find("DamageText").GetComponent<TextMeshProUGUI>().text = "-" + damage.ToString();
    }


}
