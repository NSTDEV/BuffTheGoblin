using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-------------------Clase que representa la categoria de habilidades que modifica la salud----------*/
public enum HealthModType
{
    STAT_BASED, FIXED, PORCENTAJE //tenemos tres tipos: basado en stats, daño o restauracion y basado en porcentaje
}
public class HealthModSkill : Skill
{
    [Header("Health Mod")]
    public float amount; // sirve para calcular los tres tipos
    public HealthModType modType;

    protected override void OnRun()
    {
        float amount = this.GetModification(); // obtenemos la modificacion que se hace
        this.receiver.ModifyHealth(amount); //modifica la salud del receptor
    }

    public float GetModification()
    {
        switch (this.modType)
        {
            case HealthModType.STAT_BASED:
               Stats emitterStats = this.emitter.GetCurrentStats();
               Stats receiverStats = this.receiver.GetCurrentStats();

               float rawDamage = (((2 * emitterStats.level)/5)+2) * this.amount * (emitterStats.attack / receiverStats.deffense);
               return (rawDamage / 50)+2;

            case HealthModType.FIXED:
                return this.amount;

            case HealthModType.PORCENTAJE:   
                Stats rStats = this.receiver.GetCurrentStats();

                return rStats.maxHealth * this.amount;
        }

        throw new System.InvalidOperationException("HealthModSkills::GetDamage.Unreachable!");
    }
}
