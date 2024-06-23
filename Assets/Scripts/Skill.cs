using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
   [Header("Base Skill")]
   public string skillName; //nombre de la habilidad
   public float animationDuration; //duracion de la habilidad
   public bool selfInflicted; // si es autoinfligida o no y si es daño, es falso
   public GameObject effectPrfb; //paara la animacion
   protected Fighter emitter; //quien emite
   protected Fighter receiver; //quien recibe
   

   /*------------Instancia el efecto de la habilidad-----------*/
   /*private void Animate()
   {

    var go = Instantiate (this.effectPrfb, this.receiver.transform.position, Quaternion.identity);
    Destroy(go, this.animationDuration);

   }*/
   public void Run()
   {
    if (this.selfInflicted) // si la aplicamos a nosotros mismos, es verdadera
    {
        this.receiver = this.emitter; //la recibe el emisor, el receptor es el emisor
    }
    //this.Animate(); //anima la habilidad
    this.OnRun(); 
   }
   public void SetEmitterAndReceiver(Fighter _emitter, Fighter _receiver)
   {
    this.emitter = _emitter;
    this.receiver = _receiver;
   }
   protected abstract void OnRun();

}
