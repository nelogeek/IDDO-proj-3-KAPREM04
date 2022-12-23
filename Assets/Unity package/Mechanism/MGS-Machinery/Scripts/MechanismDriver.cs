/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MechanismDriver.cs
 *  Description  :  Define driver for test mechanism quickly.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machinery
{
    [AddComponentMenu("MGS/Machinery/MechanismDriver")]
    [RequireComponent(typeof(Mechanism))]
    public class MechanismDriver : MonoBehaviour
    {
        #region Field and Property
        public float velocity = 1;
        public DriveType type = DriveType.Ignore;
        public KeyCode positive = KeyCode.P;
        public KeyCode negative = KeyCode.N;

        [HideInInspector] public Transform startPosition;
        [HideInInspector] public Mechanism mechanism;

        #endregion
        public enum Direction
        {
            Positive,
            Negative
        }

        public Direction direction;

        #region Protected Method
        protected virtual void Awake()
        {
            Initialize();
        }

        protected virtual void Update()
        {
            DriveMechanismSelf();
        }

        protected virtual void Initialize()
        {
            mechanism = GetComponent<Mechanism>();
            startPosition = this.transform;
        }

        void DriveMechanismSelf()
        {
            if (Input.GetKey(positive))
            {
                mechanism.Drive(velocity, type);
            }
            else if (Input.GetKey(negative))
            {
                mechanism.Drive(-velocity, type);
            }
        }

        public void DriveMechanism()
        {
            if (direction == Direction.Negative)
                mechanism.Drive(-velocity, type);

            if (direction == Direction.Positive)
                mechanism.Drive(velocity, type);
        }
        #endregion
    }
}