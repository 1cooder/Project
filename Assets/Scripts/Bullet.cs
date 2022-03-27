using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Bullet
    {
        private GameObject bulletObject;
        private Vector2 startPoint;
        private Vector2 movingDirection;
        public Bullet(GameObject bulletObject, Vector2 startPoint,Vector2 movingDirection)
        {
            this.bulletObject = bulletObject;
            this.startPoint = startPoint;
            this.movingDirection = movingDirection;
        }

        public Vector2 GetStartPoint()
        {
            return startPoint;
        }
        public GameObject GetBulletObjet()
        {
            return bulletObject;
        }
        public Vector3 GetMovingDireciton() 
        {
            return movingDirection;
        }
    }
}
