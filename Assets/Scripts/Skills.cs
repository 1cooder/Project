using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class SkillsController: MonoBehaviour
    {
        int unlockLevel;
        float waitDuration;
        float damage;
        string name;
        float range;
        Sprite sprite;
        GameObject effectObject;

        public void SetSkill(int unlockLevel, float waitDuration,float damage,float range,string name,Sprite sprite,GameObject effectObject) 
        {
            this.unlockLevel = unlockLevel;
            this.waitDuration = waitDuration;
            this.damage = damage;
            this.range = range;
            this.name = name;
            this.sprite = sprite;
            this.effectObject = effectObject;
        }

        public void UseSkill()
        {

        }
    }
}
