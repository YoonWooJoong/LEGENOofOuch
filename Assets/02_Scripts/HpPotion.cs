using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class HpPotion : MonoBehaviour
{
    [SerializeField] float heal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            BaseCharacter bc = collision.GetComponent<BaseCharacter>();
            bc.ChangeHealth(heal);
            Destroy(gameObject);
        }
    }
}
