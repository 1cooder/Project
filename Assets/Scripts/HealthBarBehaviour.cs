using System;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarBehaviour : MonoBehaviour
{
    //added this enum because for different healt bar type. Every character can has different bar
    public enum CharType
    {
        Player,
        Enemy,
        Boss,
        None
    }

    public CharType _charTypeID = CharType.None;

    [SerializeField]
    public Slider Slider;
    public Color LowColor;
    public Color HighColor;
    public Vector3 Offset;


    private void Start()
    {
        SetHealthBarTransform();
    }

    public void SetHealthBar(float health, float maxHealth)
    {
        if (_charTypeID != CharType.None)
        {
            if(_charTypeID == CharType.Enemy)
            {
                //Slider.gameObject.SetActive(health < maxHealth); //In non damage, bar will be passive
            }

            Slider.maxValue = maxHealth;
            Slider.value = health;

            //color normalized
            // Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(LowColor, HighColor, Slider.normalizedValue);
            Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.green, Slider.normalizedValue);
        }
    }
    
    public void SetHealthBarTransform()
    {
        switch (_charTypeID)
        {
            case CharType.Player:
                break;
            case CharType.Enemy:
                //Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
                break;
            case CharType.Boss:
                break;
            case CharType.None:
                break;
            default:
                Debug.Log("enumda hata var");
                break;
        }
    }
}
