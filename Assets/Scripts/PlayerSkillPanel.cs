using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillPanel : MonoBehaviour
{
    public GameObject [] skillButtons; //lista de botones
    public Text[] skillButtonLabels; //lsita de etiquetas de esos botones

/*------------------ Desactivamos los Botones------------*/
    void Awake()
    {
        this.Hide();

        foreach (var btn in this.skillButtons)
        {
            btn.SetActive(false);
        }
    }

/*-----------Funcion para activar individualmente los botones---------*/
    public void ConfigureButtons (int index, string skillName)
    {
        this.skillButtons[index].SetActive(true);
        this.skillButtonLabels[index].text = skillName; //le asignamos el nombre de la habilidad
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
