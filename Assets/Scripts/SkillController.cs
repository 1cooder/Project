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
        [SerializeField] 
        SkillScriptableObject _skillScriptableObject;
        
        private SkillStatus _status;
        
        private KeyCode _skillKey;
        
        private int _unlockLevel;
       
        private float _skillUseTimeStep;
        private float _damage;
        private float _range;
        private float _speed;
        
        private Sprite _sprite;
        
        [SerializeField] 
        GameObject _effectObjectInstance;
        [SerializeField] 
        GameObject _skillObjectInstance;
        
        private string _skillName;
        
        private float _waitTime = 0f;

        List<Bullet> spawnedBullets= new List<Bullet>();
        private void Awake()
        {
            InitSkillFromScriptableObject();
        }
        private void Update()
        {
            if (_status == SkillStatus.Charging)
            {
                _waitTime += Time.deltaTime;
            }

            if(_waitTime>_skillUseTimeStep)
            {
                _status = SkillStatus.Ready;
            }

            if (_status == SkillStatus.Fired)
            {
                _status = SkillStatus.Charging;
            }
            MoveBullets();
        }
        
        public void InitSkillFromScriptableObject() 
        {
            _unlockLevel = _skillScriptableObject.unlockLevel;
            _sprite = _skillScriptableObject.skillSprite;
            _skillName = _skillScriptableObject.skillName;

            _skillUseTimeStep = _skillScriptableObject.skillUseTimeStep;
            _damage = _skillScriptableObject.damage;
            _range = _skillScriptableObject.range;
            _speed = _skillScriptableObject.speed;
            _skillKey = _skillScriptableObject.skillKey;
            _status = SkillStatus.Ready;
        }

        public void SpawnBullet()
        {
            if(_unlockLevel>PlayerController.Instance.GetLevel() && _status != SkillStatus.Ready )
            {
                return;
            }
            else
            {
                GameObject bulletObject=Instantiate(_skillObjectInstance,PlayerController.Instance.GetSkillSpawnPosition(),PlayerController.Instance.transform.rotation);
                bulletObject.transform.GetComponent<SpriteRenderer>().sprite = _sprite;
                _status = SkillStatus.Fired;
                Bullet bullet = new Bullet(bulletObject,bulletObject.transform.position,PlayerController.Instance.GetDirection());

                spawnedBullets.Add(bullet);
            }
        }

        public void MoveBullets()
        {
            for (int i = 0; i < spawnedBullets.Count; i++)
            {
                GameObject bulletObject = spawnedBullets[i].GetBulletObjet();
                bulletObject.transform.position += _speed * Time.deltaTime * spawnedBullets[i].GetMovingDireciton();
                if (Vector2.Distance(bulletObject.transform.position, spawnedBullets[i].GetStartPoint()) > _range)
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
            _skillKey = key;
        }
        public KeyCode GetSkillKey()
        {
            return _skillKey;
        }
    }
}
