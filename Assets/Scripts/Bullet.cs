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

        public Bullet(GameObject bulletObject, Vector2 startPoint)
        {
            this.bulletObject = bulletObject;
            this.startPoint = startPoint;
        }

        public Vector2 GetStartPoint()
        {
            return startPoint;
        }
        public GameObject GetBulletObjet()
        {
            return bulletObject;
        }
    }
}
