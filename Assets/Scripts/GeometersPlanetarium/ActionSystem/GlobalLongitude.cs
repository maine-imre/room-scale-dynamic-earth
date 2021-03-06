﻿using IMRE.EmbodiedUserInput;
using IMRE.Math;
using Unity.Mathematics;
using UnityEngine;

namespace IMRE.HandWaver.Space
{
    public class GlobalLongitude : EmbodiedAction
    {
            /// <summary>
    		/// degrees from north.
    		/// </summary>
    		public float desiredAngle = 0f;
    		public float angleTol = 15f;
        /// <summary>
        /// Cooldown is used to limit how many toggles take place within how much time.
        /// Without this, the 
        /// </summary>
        public float cooldown = 2f;
        
        /// <summary>
        /// Start time is initialized to 0 to get through the first check of placing a pin on the earth.
        /// Otherwise it is set at the time when a finger places a pin on the Earth's surface.
        /// It is then used with cooldown to compare with the current time and limit pin placement
        /// </summary>
        private float startTime = 0f;
        public override void checkClassifier(EmbodiedClassifier classifier)
        {
            if (Operations.Angle(classifier.direction, new float3(0,1,0)) - desiredAngle < angleTol && Time.time > startTime + cooldown)
            {
                    startTime = Time.time;
                    RSDESManager.ins.GlobalLatitude = !RSDESManager.ins.GlobalLatitude;
                    classifier.shouldFinish = true;
            }
        }
    
        public override void endAction(EmbodiedClassifier classifier)
        {
            throw new System.NotImplementedException();
        }
    }
}
