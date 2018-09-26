using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DynamicLights
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D background;
        private Texture2D backgroundNormals;

        private RenderTarget2D colorMapRenderTarget;
        private RenderTarget2D normalMapRenderTarget;
        private RenderTarget2D shadowMapRenderTarget;

        private Color ambientLight = new Color(.1f, .1f, .1f, 1f);

        private Effect lightEffect;
        private Effect lightCombinedEffect;

        private EffectParameter lightEffectParameterScreenWidth;
        private EffectParameter lightEffectParameterScreenHeight;
        private EffectParameter lightEffectParameterNormalMap;

        private EffectTechnique lightCombinedEffectTechique;
        private EffectParameter lightCombinedEffectParamAmbient;
        private EffectParameter lightCombinedEffectParamLightAmbient;
        private EffectParameter lightCombinedEffectParamAmbientColor;
        private EffectParameter lightCombinedEffectParamColorMap;
        private EffectParameter lightCombinedEffectParamNormalMap;
        private EffectParameter lightCombinedEffectParamShadowMap;


        //Dynamic variables
        private List<Light> lights = new List<Light>();
        private float specularStrenght = 1.0f;

        private EffectTechnique lightEffectTechniquePointLight;

        private EffectParameter lightEffectParameterStrength;
        private EffectParameter lightEffectParameterPosition;
        private EffectParameter lightEffectParameterConeDirection;
        private EffectParameter lightEffectParameterLightColor;
        private EffectParameter lightEffectParameterLightDecay;

        public VertexPositionColorTexture[] Vertices;
        public VertexBuffer VertexBuffer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 600;
            graphics.ApplyChanges();


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            background = Content.Load<Texture2D>("BGW1");
            backgroundNormals = Content.Load<Texture2D>("BackgroundWall0");

            PresentationParameters pp = GraphicsDevice.PresentationParameters;
            int Width = pp.BackBufferWidth;
            int Height = pp.BackBufferHeight;
            SurfaceFormat format = pp.BackBufferFormat;

            colorMapRenderTarget = new RenderTarget2D(GraphicsDevice, Width, Height);
            Vertices = new VertexPositionColorTexture[4];
            Vertices[0] = new VertexPositionColorTexture(new Vector3(-1, 1, 0), Color.White, new Vector2(0, 0));

            Vertices[1] = new VertexPositionColorTexture(new Vector3(1, 1, 0), Color.White, new Vector2(1, 0));

            Vertices[2] = new VertexPositionColorTexture(new Vector3(-1, -1, 0), Color.White, new Vector2(0, 1));

            Vertices[3] = new VertexPositionColorTexture(new Vector3(1, -1, 0), Color.White, new Vector2(1, 1));

            VertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColorTexture), Vertices.Length, BufferUsage.None);

            VertexBuffer.SetData(Vertices);


            normalMapRenderTarget = new RenderTarget2D(GraphicsDevice, Width, Height);
            shadowMapRenderTarget = new RenderTarget2D(GraphicsDevice, Width, Height, false, format, pp.DepthStencilFormat, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);

            lightEffect = Content.Load<Effect>("Multitarget");
            lightCombinedEffect = Content.Load<Effect>("DeferredCombined");

            lightEffectParameterNormalMap = lightEffect.Parameters["NormalMap"];
            lightEffectParameterScreenHeight = lightEffect.Parameters["screenHeight"];
            lightEffectParameterScreenWidth = lightEffect.Parameters["screenWidth"];

            lightEffectTechniquePointLight = lightEffect.Techniques["DeferredPointLight"];
            lightEffectParameterConeDirection = lightEffect.Parameters["ConeDirection"];
            lightEffectParameterLightColor = lightEffect.Parameters["lightColor"];
            lightEffectParameterLightDecay = lightEffect.Parameters["lightDecay"];
            lightEffectParameterPosition = lightEffect.Parameters["lightPosition"];
            lightEffectParameterStrength = lightEffect.Parameters["lightStrength"];

            lightCombinedEffectTechique = lightCombinedEffect.Techniques["DeferredCombined2"];
            lightCombinedEffectParamAmbient = lightCombinedEffect.Parameters["ambient"];
            lightCombinedEffectParamLightAmbient = lightCombinedEffect.Parameters["lightAmbient"];
            lightCombinedEffectParamAmbientColor = lightCombinedEffect.Parameters["ambientColor"];
            lightCombinedEffectParamColorMap = lightCombinedEffect.Parameters["ColorMap"];
            lightCombinedEffectParamNormalMap = lightCombinedEffect.Parameters["NormalMap"];
            lightCombinedEffectParamShadowMap = lightCombinedEffect.Parameters["ShadingMap"];




            lights.Add(new PointLight()
            {
                IsEnabled = true,
                Color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                Power = .7f,
                LightDecay = 200,
                Position = new Vector3(500, 400, 80)
            });
                

            




        }



        private void DrawColorMap()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            spriteBatch.End();
        }

        private void DrawNormalMap()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundNormals, Vector2.Zero, Color.White);

            spriteBatch.End();

            //Deactive the rander targets to resolve them
            GraphicsDevice.SetRenderTarget(null);
        }


        private void DrawCombinedMap()
        {
            lightCombinedEffect.CurrentTechnique = lightCombinedEffectTechique;
            lightCombinedEffectParamAmbient.SetValue(1f);
            lightCombinedEffectParamLightAmbient.SetValue(4);
            lightCombinedEffectParamAmbientColor.SetValue(ambientLight.ToVector4());
            lightCombinedEffectParamColorMap.SetValue(colorMapRenderTarget);
            lightCombinedEffectParamNormalMap.SetValue(normalMapRenderTarget);
            lightCombinedEffectParamShadowMap.SetValue(shadowMapRenderTarget);
            lightCombinedEffect.CurrentTechnique.Passes[0].Apply();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, lightCombinedEffect);

            spriteBatch.Draw(colorMapRenderTarget, Vector2.Zero, Color.White);

            spriteBatch.End();

        }

        private Texture2D GenerateShadowMap()
        {
            GraphicsDevice.SetRenderTarget(shadowMapRenderTarget);
            GraphicsDevice.Clear(Color.Transparent);

            foreach (var light in lights)
            {
                if (!light.IsEnabled) continue;
                GraphicsDevice.SetVertexBuffer(VertexBuffer);

                //Draw all lightScources
                lightEffectParameterStrength.SetValue(light.ActualPower);
                lightEffectParameterPosition.SetValue(light.Position);
                lightEffectParameterLightColor.SetValue(light.Color);
                lightEffectParameterLightDecay.SetValue(light.LightDecay);

                lightEffect.Parameters["specularStrength"].SetValue(specularStrenght);

                if (light.LightType == LightType.Point)
                {
                    lightEffect.CurrentTechnique = lightEffectTechniquePointLight;
                }

                lightEffectParameterScreenWidth.SetValue(GraphicsDevice.Viewport.Width);
                lightEffectParameterScreenHeight.SetValue(GraphicsDevice.Viewport.Height);
                lightEffect.Parameters["ambientColor"].SetValue(ambientLight.ToVector4());
                lightEffectParameterNormalMap.SetValue(normalMapRenderTarget);
                lightEffect.Parameters["ColorMap"].SetValue(colorMapRenderTarget);
                lightEffect.CurrentTechnique.Passes[0].Apply();

                //Add Belding (Black background)
                GraphicsDevice.BlendState = BlendBlack;

                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, Vertices, 0, 2);
                 
            }

            //Nollst�ll RenderTargets
            GraphicsDevice.SetRenderTarget(null);

            return shadowMapRenderTarget;

        }
        public static BlendState BlendBlack = new BlendState()
        {
            ColorBlendFunction = BlendFunction.Add,
            ColorSourceBlend = Blend.One,
            ColorDestinationBlend = Blend.One,

            AlphaBlendFunction = BlendFunction.Add,
            AlphaSourceBlend = Blend.One,
            AlphaDestinationBlend  = Blend.One

        };


                /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Defines gamePad and keyboard
            GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboard = Keyboard.GetState();

            //Press Escape or the back button to close down the game
            if (gamePad.Buttons.Back == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            //PRess F to go Full-screen Mode 
            if (keyboard.IsKeyDown(Keys.F))
            {

            }


            MouseState mouse = Mouse.GetState();

            var mousePos = new Vector3(mouse.X, mouse.Y, 1);

            lights[0].Position = mousePos;

           

            // TODO: Add your update logic here


          
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
           

            // TODO: Add your drawing code here

            GraphicsDevice.SetRenderTarget(colorMapRenderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            DrawColorMap();

            //Clear all render targets
            GraphicsDevice.SetRenderTarget(null);


            //Set the render targets
            GraphicsDevice.SetRenderTarget(normalMapRenderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            DrawNormalMap();

            //Clear all render targets
            GraphicsDevice.SetRenderTarget(null);
             GenerateShadowMap();
            GraphicsDevice.Clear(Color.Black);
            DrawCombinedMap();


            base.Draw(gameTime);
        }
    }
}
