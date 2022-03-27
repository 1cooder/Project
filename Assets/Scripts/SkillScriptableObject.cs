using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Skills/SkillScriptableObject")]
    class SkillScriptableObject :ScriptableObject
    {
        int unlockLevel;
        float waitDuration;
        float damage;
        string skillName;
        float range;
        Sprite skillSprite;

    }
}
