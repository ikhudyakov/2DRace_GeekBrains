using JoostenProductions;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Views
{
    public class InputAcceleration : BaseInputView
    {
        public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
        {
            base.Init(leftMove, rightMove, speed);
            UpdateManager.SubscribeToUpdate(Move);
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
        }

        private void Move()
        {
            Vector3 direction = Vector3.zero;
            direction.x = Input.acceleration.y;
            direction.z = Input.acceleration.x;

            if (direction.sqrMagnitude > 1)
                direction.Normalize();

            OnRightMove(direction.z / 20 * _speed);
        }
    }
}