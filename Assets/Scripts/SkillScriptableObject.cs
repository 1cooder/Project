using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.Enums;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Skills/SkillScriptableObject")]
    class SkillScriptableObject :ScriptableObject
    {
        public int unlockLevel;

        public float skillUseTimeStep;
        public float damage;
        public float range;
        public float speed;

        public Sprite skillSprite;
        
        public KeyCode skillKey;
        
        public string skillName;

    }
}
