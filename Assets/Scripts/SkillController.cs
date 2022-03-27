using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.Enums;

namespace Assets.Scripts
{
    class SkillController: MonoBehaviour
    {

        [SerializeField] SkillScriptableObject skillScriptableObject;
        
        SkillStatus status;
        
        KeyCode skillKey;
        
        int _unlockLevel;
        
        float skillUseTimeStep;
        float damage;
        float range;
        float speed;

        Sprite _sprite;
        
        [SerializeField] GameObject _effectObjectInstance;
        [SerializeField] GameObject _skillObjectInstance;
        string _skillName;
        
        float waitTime = 0f;

        List<Bullet> spawnedBullets= new List<Bullet>();
        private void Awake()
        {
            InitSkillFromScriptableObject();
        }
        private void Update()
        {
            if (status == SkillStatus.Charging)
            {
                waitTime += Time.deltaTime;
            }

            if(waitTime>skillUseTimeStep)
            {
                status = SkillStatus.Ready;
            }

            if (status == SkillStatus.Fired)
            {
                status = SkillStatus.Charging;
            }
            MoveBullets();
        }
        
        public void InitSkillFromScriptableObject() 
        {
            _unlockLevel = skillScriptableObject.unlockLevel;
            _sprite = skillScriptableObject.skillSprite;
            _skillName = skillScriptableObject.skillName;

            skillUseTimeStep = skillScriptableObject.skillUseTimeStep;
            damage = skillScriptableObject.damage;
            range = skillScriptableObject.range;
            speed = skillScriptableObject.speed;
            skillKey = skillScriptableObject.skillKey;
            status = SkillStatus.Ready;
        }

        public void SpawnBullet()
        {
            if(_unlockLevel>CharacterController.Instance.GetLevel() && status != SkillStatus.Ready )
            {
                return;
            }
            else
            {
                GameObject bulletObject=Instantiate(_skillObjectInstance,CharacterController.Instance.transform.position,CharacterController.Instance.transform.rotation);
                bulletObject.transform.GetComponent<SpriteRenderer>().sprite = _sprite;
                status = SkillStatus.Fired;
                Bullet bullet = new Bullet(bulletObject,bulletObject.transform.position,CharacterController.Instance.GetDirection());

                spawnedBullets.Add(bullet);
            }
        }

        public void MoveBullets()
        {
            for (int i = 0; i < spawnedBullets.Count; i++)
            {
                GameObject bulletObject = spawnedBullets[i].GetBulletObjet();
                bulletObject.transform.position += speed * Time.deltaTime * spawnedBullets[i].GetMovingDireciton();
                if (Vector2.Distance(bulletObject.transform.position, spawnedBullets[i].GetStartPoint()) > range)
                {
                    DestroyBullet(bulletObject.transform.position, bulletObject);
                }
            }
        }

        public void DestroyBullet(Vector3 bulletHitPoint,GameObject destroyedBullet)
        {
            if (_effectObjectInstance != null)
            {
                Instantiate(_effectObjectInstance,bulletHitPoint,Quaternion.identity);
            }
            Bullet bullet = spawnedBullets.FirstOrDefault(b => destroyedBullet == b.GetBulletObjet());
            spawnedBullets.Remove(bullet);
            Destroy(destroyedBullet);
        }

        public void SetSkillKey(KeyCode key)
        {
            skillKey = key;
        }
        public KeyCode GetSkillKey()
        {
            return skillKey;
        }
    }
}
