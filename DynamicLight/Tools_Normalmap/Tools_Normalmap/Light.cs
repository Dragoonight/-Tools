using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Graphical and framework use
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace DynamicLights
{
    //Uses pointLight Enum
    public enum LightType
    {
        Point
    }

    //
    public abstract class Light
    {
        protected float initialPower;

        public Vector3 Position { get; set; }
        public Vector4 Color;

        public float ActualPower { get; set; }

        public float Power
        {
            get
            {
                return initialPower;
            }
            set
            {
                initialPower = value;
                ActualPower = initialPower;
            }
        }
        public int LightDecay { get; set; }
        public LightType LightType { get; private set; }
        public bool IsEnabled { get; set; }

        public Vector3 Direction { get; set; }
        protected Light (LightType lightType)
        {
            this.LightType = lightType;
        }



        public void EnableLight(bool enabled, float timeToEnable)
        {
            //Om ljuset skall vara tänt
            IsEnabled = enabled;
        }

        //Uppdaterar ljuset
        public virtual void Update(GameTime gameTime)
        {
            if (!IsEnabled) return;
        }

        protected void CopyBaseFields(Light light)
        {
            light.Color = this.Color;
            light.IsEnabled = this.IsEnabled;
            light.LightDecay = this.LightDecay;
            light.LightType = this.LightType;
            light.Position = this.Position;
            light.Power = this.Power;
        }


        public abstract Light DeepCopy();
    }

    

}
