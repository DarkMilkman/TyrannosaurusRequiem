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

namespace finalGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //varibles for tileMap
        TileMap landmap = new TileMap();
        OverlayMap overLay = new OverlayMap();
        Collision collisionMap = new Collision();
      
        //varibles for input
        InputHandler handleInput = new InputHandler();
        AnimationCycle cycle = new AnimationCycle();

        //camera
        Camera camera = new Camera();

        //player varibles
        Animation animUp;
        Animation animDown;
        Animation animLeft;
        Animation animRight;

        //pokemon varibles
        Sprite squirtle;
        Sprite pichu;
        Sprite slowpoke;
        Sprite suicune;
        Sprite typhlosion;
        Sprite enemySprite;

        //random sprites
        Sprite ghost;
        int ghostLocX;
        int ghostLocY;
        int ghostDrawn;
        float ghostSize;
        Boolean ghostUp;
        SpriteEffects effect;
        

        //background sprite
        Sprite battleBackGround;
        Sprite textBoxBackground;

        //Main menu background
        Sprite mainMenuBackground;

        //health bar
        Sprite enemyHealth;
        Sprite yourHealth;
        Sprite HealthBarOutline;
        BattleStat enemyHealthCalc = new BattleStat();
        BattleStat yourHealthCalc = new BattleStat();
        BattleStat booleanAttackStat = new BattleStat();
        Texture2D healthBarTexture;
        int turnTimer;
        int enemyTurnTimer;
        int enemyPrevHealth;
        int playerPrevHealth;

        //random pokemon locations
        int pokemonLocation;
        int msGame;
        int loopTimes;

        //stream reader
        TextReader textReader = new TextReader();

        //fps counter
        Counter fpsCounter = new Counter();

        //pixMod
        Effect grayScale;
        EffectParameter circleRadius;
        EffectParameter circleCenter;
        EffectParameter screenSize;
        int GrayScaleRadius = 0;
        RenderTarget2D tempRender;

        //particle
        Damage damage;
        Damage enemyDamage;

        //boolean
        Boolean attackDamageParticle;
        Boolean enemyAttackDamageParticle;

        //transition
        Sprite transition;
        public static int transitionStart;
        int transitionEnd;

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
            //screensize
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 640;
            graphics.ApplyChanges();

            //tilemap
            landmap.Init(Content);
            overLay.Init(Content);
            collisionMap.Init(Content);

            //input
            handleInput.Init();

            //battle stats
            enemyHealthCalc.Init();
            yourHealthCalc.Init();

            //pokemon locations
            pokemonLocation = GenFunctions.RandomPokemonLocation();
            msGame = 0;
            loopTimes = 0;
            turnTimer = 0;

            //boolean
            attackDamageParticle = false;
            enemyAttackDamageParticle = false;

            //transition 
            transitionStart = 0;
            transitionEnd = 1;

            //ghost locations
            ghostLocX = 300;
            ghostLocY = 350;
            ghostDrawn = 0;
            ghostSize = 0.0f;
            ghostUp = false;

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

            //load player
            Texture2D animation = Content.Load<Texture2D>("sprites/trainer");
            animUp = new Animation(animation, new Vector2(0, 99), new Vector2(31, 31), 3, 120, new Vector2(15, 16));
            animDown = new Animation(animation, new Vector2(0, 0), new Vector2(31, 31), 3, 120, new Vector2(15, 16));
            animLeft = new Animation(animation, new Vector2(0, 33), new Vector2(31, 31), 3, 120, new Vector2(15, 16));
            animRight = new Animation(animation, new Vector2(0, 66), new Vector2(31, 31), 3, 120, new Vector2(15, 16));

            //pokemon Sprites
            Texture2D squirtleTexture = Content.Load<Texture2D>("sprites/squirtle");
            squirtle = new Sprite(squirtleTexture, new Rectangle(0, 0, squirtleTexture.Width, squirtleTexture.Height));
            Texture2D slowpokeTexture = Content.Load<Texture2D>("sprites/slowpoke");
            slowpoke = new Sprite(slowpokeTexture, new Rectangle(0, 0, slowpokeTexture.Width, slowpokeTexture.Height));
            Texture2D suicuneTexture = Content.Load<Texture2D>("sprites/suicune");
            suicune = new Sprite(suicuneTexture, new Rectangle(0, 0, suicuneTexture.Width, suicuneTexture.Height));
            Texture2D typhlosionTexture = Content.Load<Texture2D>("sprites/typhlosion");
            typhlosion = new Sprite(typhlosionTexture, new Rectangle(0, 0, typhlosionTexture.Width, typhlosionTexture.Height));
            Texture2D pichuTexture = Content.Load<Texture2D>("sprites/pichu");
            pichu = new Sprite(pichuTexture, new Rectangle(0, 0, pichuTexture.Width, pichuTexture.Height));

            //default slowpoke
            enemySprite = new Sprite(slowpokeTexture, new Rectangle(0, 0, slowpokeTexture.Width, slowpokeTexture.Height));

            //background
            Texture2D battleBackGroundTexture = Content.Load<Texture2D>("sprites/background");
            battleBackGround = new Sprite(battleBackGroundTexture, new Rectangle(0, 0, battleBackGroundTexture.Width, battleBackGroundTexture.Height));
            //textbox background
            Texture2D textBoxBackgroundTexture = Content.Load<Texture2D>("sprites/textbox");
            textBoxBackground = new Sprite(textBoxBackgroundTexture, new Rectangle(0, 0, textBoxBackgroundTexture.Width, textBoxBackgroundTexture.Height));

            //main menu background
            Texture2D mainMenuBackgroundTexture = Content.Load<Texture2D>("sprites/title");
            mainMenuBackground = new Sprite(mainMenuBackgroundTexture, new Rectangle(0, 0, mainMenuBackgroundTexture.Width, mainMenuBackgroundTexture.Height));

            //health bar
            healthBarTexture = Content.Load<Texture2D>("sprites/healthbar");
            enemyHealth = new Sprite(healthBarTexture, new Rectangle(0, 0, enemyHealthCalc.ReturnHealth(), healthBarTexture.Height));
            yourHealth = new Sprite(healthBarTexture, new Rectangle(0, 0, enemyHealthCalc.ReturnHealth(), healthBarTexture.Height));
            Texture2D healthBarOutlineTexture = Content.Load<Texture2D>("sprites/healthbaroutline");
            HealthBarOutline = new Sprite(healthBarOutlineTexture, new Rectangle(0, 0, healthBarOutlineTexture.Width, healthBarOutlineTexture.Height));

            //ghost
            Texture2D ghostTexture = Content.Load<Texture2D>("sprites/ghost");
            ghost = new Sprite(ghostTexture, new Rectangle(0, 0, ghostTexture.Width, ghostTexture.Height));

            //stream reader
            textReader.LoadContent(Content);

            //PixMod
            grayScale = Content.Load<Effect>("PixMod/gray");
            circleRadius = grayScale.Parameters["radius"];
            circleCenter = grayScale.Parameters["center"];
            screenSize = grayScale.Parameters["size"];
            screenSize.SetValue(new Vector2(640, 640));
            tempRender = new RenderTarget2D(GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            //particles
            Damage.LoadContent(Content);
            damage = new Damage();
            enemyDamage = new Damage();

            //transition
            Texture2D transitionTexture = Content.Load<Texture2D>("transition/transition");
            transition = new Sprite(transitionTexture, new Rectangle(0, 0, transitionTexture.Width, transitionTexture.Height));

            //music
            SoundEffect music = Content.Load<SoundEffect>("music/Dance");
            SoundEffectInstance musicInstance = music.CreateInstance();
            musicInstance.IsLooped = true;
            musicInstance.Play();
        }

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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //music
            if (handleInput._musicOn)
                SoundEffect.MasterVolume = 1.0f;
            else
                SoundEffect.MasterVolume = 0.0f;

            //particle attack 
            if (turnTimer >= 10)
            {
                attackDamageParticle = false;
                turnTimer = 0;
            }
            if (enemyTurnTimer >= 10)
            {
                enemyAttackDamageParticle = false;
                enemyTurnTimer = 0;
            }

            //player animation 
            if (handleInput._inGame)
            {
                cycle.Update(gameTime, animUp, handleInput);
                cycle.Update(gameTime, animDown, handleInput);
                cycle.Update(gameTime, animLeft, handleInput);
                cycle.Update(gameTime, animRight, handleInput);
                //camera
                camera.Update(gameTime, cycle._playerPosition);
            }

            //pokemonlocations
            if (handleInput._inGame)
                msGame += (gameTime.ElapsedGameTime.Milliseconds / 10);
            if ((msGame >= pokemonLocation) && (!ghostUp) && (!handleInput._inMenu) || (handleInput._inBattle))
            {
                if (loopTimes == 0)
                {
                    transitionStart = 0;
                    handleInput.initBattleBooleans();
                }
                loopTimes++;

                enemyPrevHealth = enemyHealthCalc.ReturnHealth();
                playerPrevHealth = yourHealthCalc.ReturnHealth();
                handleInput.InputInBattle();
                enemyHealthCalc.CalculatePlayerAttack(handleInput);
                enemyHealth = new Sprite(healthBarTexture, new Rectangle(0, 0, enemyHealthCalc.ReturnHealth(), healthBarTexture.Height));

                yourHealthCalc.CalculateEnemyAttack(handleInput);
                yourHealth = new Sprite(healthBarTexture, new Rectangle(0, 0, yourHealthCalc.ReturnHealth(), healthBarTexture.Height));

                if (enemyPrevHealth != enemyHealthCalc.ReturnHealth())
                    attackDamageParticle = true;
                if (playerPrevHealth != yourHealthCalc.ReturnHealth())
                    enemyAttackDamageParticle = true;

                handleInput._APressed = false;
            }

            //resetting the enemy pokemon's health and sprite
            if (yourHealthCalc.ReturnHealth() <= 0 || enemyHealthCalc.ReturnHealth() <= 0)
            {
                //pix mod radius
                if (yourHealthCalc.ReturnHealth() <= 0)
                {
                    if (GrayScaleRadius <= 0)
                        GrayScaleRadius = 0;
                    else
                        GrayScaleRadius -= 25;
                }
                if (enemyHealthCalc.ReturnHealth() <= 0)
                {
                    if (GrayScaleRadius >= 235)
                        GrayScaleRadius = 235;
                    else
                        GrayScaleRadius += 25;

                    //ghost
                    ghostUp = true;
                }

                //resetting all health
                enemyHealthCalc.Init();
                yourHealthCalc.Init();
                enemyHealth = new Sprite(healthBarTexture, new Rectangle(0, 0, enemyHealthCalc.ReturnHealth(), healthBarTexture.Height));
                yourHealth = new Sprite(healthBarTexture, new Rectangle(0, 0, yourHealthCalc.ReturnHealth(), healthBarTexture.Height));
                enemySprite = GenFunctions.returnSprite(slowpoke, pichu, typhlosion, suicune);

                //resetting booleans to the right state
                handleInput.resetBattleBooleans();
                msGame = 0;
                loopTimes = 0;

                //particle reset
                turnTimer = 10;
                attackDamageParticle = false;
                enemyAttackDamageParticle = false;


                //getting anoth pokemon location
                pokemonLocation = GenFunctions.RandomPokemonLocation();

                //resetting transition time
                transitionStart = 0;
            }

            if (handleInput._inMenu)
                handleInput.HandleInputInMenus();

            //update what the language list is
            textReader.Update(gameTime);

            fpsCounter.Update(gameTime);

            if (handleInput._justQuit)
                Exit();

            //pixmod circle location
            circleCenter.SetValue(new Vector2(159, 159));
            circleRadius.SetValue(GrayScaleRadius);

            //particles
            damage.Initialize(450, 290);
            damage.Update(gameTime);
            enemyDamage.Initialize(180, 550);
            enemyDamage.Update(gameTime);

            //ghost movement
            if (ghostUp)
            {
                ghostLocX += 0;
                ghostLocY -= 2;
                ghostSize += 0.01f;
            }

            //reset to get the ghost going againg
            if((ghostLocX >= 1200) || (ghostLocY <= -800))
            {
                ghostLocX = 280;
                ghostLocY = 320;
                ghostDrawn = 0;
                ghostUp = false;
                ghostSize = 0;
            }

            //restart level
            //if((handleInput._isPaused) && (handleInput._inMenu))
            //{
            //}

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            RenderTargetBinding[] activeTarget = GraphicsDevice.GetRenderTargets();
            GraphicsDevice.SetRenderTarget(tempRender);
            GraphicsDevice.Clear(Color.CadetBlue);
            //temp render          
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.CreateScale(camera._fZoomLevel));
            landmap.Draw(spriteBatch, camera._drawLocation);
            spriteBatch.End();

            //player
            spriteBatch.Begin();
            if (handleInput._isUp)
                cycle.Draw(spriteBatch, camera._drawLocation, animUp);
            if (handleInput._isDown)
                cycle.Draw(spriteBatch, camera._drawLocation, animDown);
            if (handleInput._isLeft)
                cycle.Draw(spriteBatch, camera._drawLocation, animLeft);
            if (handleInput._isRight)
                cycle.Draw(spriteBatch, camera._drawLocation, animRight);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.CreateScale(camera._fZoomLevel));
            overLay.Draw(spriteBatch, camera._drawLocation);
            spriteBatch.End();

            GraphicsDevice.SetRenderTargets(activeTarget);
            GraphicsDevice.Clear(Color.LightGray);

            //Battles
            if (handleInput._inBattle)
            {
                if (transitionStart <= transitionEnd)
                {
                    spriteBatch.Begin();
                    transition.Draw(spriteBatch, new Vector2(0, 0), 0.0f, 1.0f, SpriteEffects.None, 0.0f);
                    spriteBatch.End();
                    transitionStart++;
                }
                else
                {
                    spriteBatch.Begin();
                    //background
                    battleBackGround.Draw(spriteBatch, new Vector2(0, 100), 0.0f, 1.0f, SpriteEffects.None, 0.0f);
                    textBoxBackground.Draw(spriteBatch, new Vector2(0, 0), 0.0f, 1.0f, SpriteEffects.None, 0.0f);

                    //battle menu
                    textReader.Draw(spriteBatch, handleInput, enemyHealthCalc);

                    //you
                    HealthBarOutline.Draw(spriteBatch, new Vector2(68, 469), 0.0f, 1.0f, SpriteEffects.None, 0.0f);
                    yourHealth.Draw(spriteBatch, new Vector2(70, 470), 0.0f, 1.0f, SpriteEffects.None, 0.0f);
                    squirtle.Draw(spriteBatch, new Vector2(50, 500), Color.White, 0.0f, 1.0f, SpriteEffects.None, 0.0f);

                    //enemy
                    HealthBarOutline.Draw(spriteBatch, new Vector2(368, 119), 0.0f, 1.0f, SpriteEffects.None, 0.0f);
                    enemyHealth.Draw(spriteBatch, new Vector2(370, 120), 0.0f, 1.0f, SpriteEffects.None, 0.0f);
                    enemySprite.Draw(spriteBatch, new Vector2(355, 160), 0.0f, 1.0f, SpriteEffects.None, 0.0f);

                    spriteBatch.End();

                    if (attackDamageParticle)
                    {
                        damage.Draw(spriteBatch);
                        turnTimer++;
                    }

                    if (enemyAttackDamageParticle)
                    {
                        enemyDamage.Draw(spriteBatch);
                        enemyTurnTimer++;
                    }
                }
            }

            if (!handleInput._inBattle)
            {
                if (transitionStart <= transitionEnd)
                {
                    spriteBatch.Begin();
                    transition.Draw(spriteBatch, new Vector2(0, 0), 0.0f, 1.0f, SpriteEffects.None, 0.0f);
                    spriteBatch.End();
                    transitionStart++;
                }
                else
                {
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.CreateScale(camera._fZoomLevel));
                    grayScale.CurrentTechnique.Passes[0].Apply();
                    spriteBatch.Draw(tempRender, new Rectangle(0, 0, tempRender.Width, tempRender.Height), Color.White);
                    spriteBatch.End();

                    spriteBatch.Begin();
                    if (ghostDrawn == 0)
                    {
                        effect = GenFunctions.RandomSpritesEffects();
                        ghostDrawn++;
                    }
                    if(ghostUp)
                        ghost.Draw(spriteBatch, new Vector2(ghostLocX, ghostLocY), 0.0f, ghostSize, effect, 0.0f); 

                    textReader.Draw(spriteBatch, handleInput);

                    spriteBatch.End();
                }
            }

            //handle main menu input
            if ((handleInput._inMenu) && (!handleInput._isPaused) && (!handleInput._inGame) && (!handleInput._inBattle))
            {
                spriteBatch.Begin();
                mainMenuBackground.Draw(spriteBatch, new Vector2(0, 0), 0.0f, 1.0f, SpriteEffects.None, 0.0f);
                textReader.Draw(spriteBatch, handleInput);
                spriteBatch.End();
            }

            //fps
            fpsCounter.Draw(gameTime, spriteBatch, textReader._inGameFont);

            base.Draw(gameTime);
        }
    }
}
