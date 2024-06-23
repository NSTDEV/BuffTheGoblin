using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighter : Fighter
{
    [Header("UI")] //referencia al panel de habilidades
    public PlayerSkillPanel skillPanel; //referencia al panel de habilidades
    void Awake()
    {
        this.stats = new Stats(21, 60, 50, 45, 20 );
    }
    public override void InitTurn()
    {
        this.skillPanel.Show(); //muestra el panel
        for (int i = 0 ; i < this.skills.Length; i++)
        {
            this.skillPanel.ConfigureButtons(i, this.skills[i].skillName); //configuramos los botones con el nombre de skill
        }
    }

    ///<summary>
    ///</summary>
    ///<param name = "index" ></param>
    public void ExecuteSkill (int index)
    {
        this.skillPanel.Hide();
        Skill skill = this.skills[index];

        skill.SetEmitterAndReceiver
        (
            this, this.combatManager.GetOpposingFighter()
        );
        this.combatManager.OnFighterSkill(skill);

    }
}
