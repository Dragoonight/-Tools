using System;
using System.Collections.Generic;
using System.Linq;
//Graphical and framework use
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace DynamicLights
{
    public class PointLight : Light
    {
        public PointLight() : base(LightType.Point)
        {

        }

        public override Light DeepCopy()
        {
            var newLight = new PointLight();
            CopyBaseFields(newLight);

            return newLight;
        }
    }
}
