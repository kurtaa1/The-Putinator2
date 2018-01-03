using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace The_Putinator
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D HillaryAnsikte;
        Texture2D CruzAnsikte;
        Texture2D SandersAnsikte;
        Texture2D TrumpAnsikte;
        Texture2D kroppMan;
        Texture2D kroppKvinna;
        public static Texture2D flagga;
        Texture2D vitaHuset;
        public static Texture2D explosion;
        public static Texture2D bomb;

        Song nationalsång;
        Song stridsång;
        Song introsång;

        public static SoundEffect explosionSFX;

        Vector2 karaktärstorlek = new Vector2(300,300);
        Vector2 text1Position;
        Vector2 textFortsPosition = new Vector2 (800,800);
        Vector2 flaggaPosition = new Vector2(0);

        static karaktär Trump;
        static karaktär Cruz;
        static karaktär Hillary;
        static karaktär Sanders;
        karaktär[] karaktärLista = new karaktär[4];
        karaktär vinnaren;
        static karaktär spelare1;
        static karaktär spelare2;
    
        SpriteFont font;
        Rectangle musRektangel; 

        string text1;
        string textForts = " ";

        public static int borderXMax;
        public static int borderXmin;
        public static int borderYmax;
        public static int borderYmin;
        int gameState = 1;


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
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            borderXMax = graphics.GraphicsDevice.Viewport.Width - 200;
            borderYmax = 840;

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

            text1Position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 3, 150);
            font = Content.Load<SpriteFont>("Trajan Pro");
            nationalsång = Content.Load<Song>("NationalSång");
            stridsång = Content.Load<Song>("StridMusik2");
            introsång = Content.Load<Song>("Intro2");
            explosionSFX = Content.Load<SoundEffect>("boom");
            flagga = Content.Load<Texture2D>("Flagga2");
            vitaHuset = Content.Load<Texture2D>("WhiteHouse");
            bomb = Content.Load<Texture2D>("Bomb");
            explosion = Content.Load<Texture2D>("Explosion");
            kroppMan = Content.Load<Texture2D>("Gladiator_Man_Klar");
            kroppKvinna = Content.Load<Texture2D>("Gladiator_Kvinna_Klar");

            CruzAnsikte = Content.Load<Texture2D>("Cruz");
            Cruz = new karaktär("Cruz", karaktärstorlek, CruzAnsikte, kroppMan, 0, 0);
            karaktärLista[0] = Cruz;

            HillaryAnsikte = Content.Load<Texture2D>("Hillary");
            Hillary = new karaktär("Clinton", karaktärstorlek, HillaryAnsikte, kroppKvinna, 0, 0);
            karaktärLista[1] = Hillary;

            TrumpAnsikte = Content.Load<Texture2D>("Trump");
            Trump = new karaktär("Trump", karaktärstorlek, TrumpAnsikte, kroppMan, 0, 0);
            karaktärLista[2] = Trump;

            SandersAnsikte = Content.Load<Texture2D>("Sanders");
            Sanders = new karaktär("Sanders", karaktärstorlek, SandersAnsikte, kroppMan, 0, 0);
            karaktärLista[3] = Sanders;          
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState tangentbord = Keyboard.GetState();
            MouseState mus = Mouse.GetState();
            musRektangel = new Rectangle(mus.X*10/7, mus.Y*10/7, 10, 10);
            
            if (gameState == 1 || gameState == 2)
            {               
                for (int i = 0; i < 4; i++)
                {
                    karaktärLista[i].posX = graphics.GraphicsDevice.Viewport.Width / 10 + 400 * i;
                    karaktärLista[i].posY = 400;
                }
                text1 = "Player " + gameState.ToString() + ": Choose your candidate";
            }

            Match.väljKaraktär(karaktärLista, musRektangel, mus, karaktärstorlek, gameState);
            spelare1 = Match.spelare1;
            spelare2 = Match.spelare2;
            textForts = Match.textForts;

            if (gameState == 3)
            {
                spelare1.storlek = new Vector2(200,200);
                spelare2.storlek = new Vector2(200,200);
                spelare1.kontroller(spelare1,tangentbord,1,spelare2);
                spelare2.kontroller(spelare2, tangentbord,2,spelare1);
                if (spelare1.liv <= 0)
                {
                    text1 = spelare2.karaktärNamn + " Vann";
                    vinnaren = spelare2;
                    gameState = 4;
                }
                if (spelare2.liv <= 0)
                {
                    text1 = spelare1.karaktärNamn + "Vann";
                    vinnaren = spelare1;
                    gameState = 4;
                }                      
            }
            //tillåter dig att gå vidare
            if (spelare1 !=null && tangentbord.IsKeyDown(Keys.Enter) && gameState == 1)
            {
                gameState = 2;
                Match.textForts = " ";
            }
            if (spelare2 != null && tangentbord.IsKeyDown(Keys.Enter) && gameState == 2){
                spelare1.posX = 20;
                spelare1.posY = 850;
                spelare2.posX = 1700;
                spelare2.posY = 850;
                gameState = 3;
            }

            if (gameState == 4)
            {
                vinnaren.posX = 600;
                vinnaren.posY = 400;
                vinnaren.storlek = new Vector2(450, 450);
                Match.textForts = "Press enter to play again";
                if (tangentbord.IsKeyDown(Keys.Enter))
                {
                    spelare1.liv = 100;
                    spelare2.liv = 100;
                    Match.spelare1 = null;
                    Match.spelare2 = null;
                    vinnaren = null;
                    Match.textForts = " ";
                    gameState = 1;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            if (gameState == 1 || gameState == 2)
            {
                spriteBatch.Draw(flagga, flaggaPosition, Color.White);
                spriteBatch.DrawString(font, text1, text1Position, Color.Tan);
                Trump.Draw(spriteBatch);
                Sanders.Draw(spriteBatch);
                Hillary.Draw(spriteBatch);
                Cruz.Draw(spriteBatch);
                spriteBatch.DrawString(font, textForts, textFortsPosition, Color.Tan);
            }
            if (gameState == 3)
            {              
                spriteBatch.Draw(vitaHuset, flaggaPosition, Color.White);
                spelare1.Draw(spriteBatch);
                spelare2.Draw(spriteBatch);
                spriteBatch.DrawString(font, spelare1.karaktärNamn + " : " + spelare1.liv.ToString(), new Vector2(100, 100), Color.Black);
                spriteBatch.DrawString(font, spelare2.karaktärNamn + " : " + spelare2.liv.ToString(), new Vector2(1000, 100), Color.Black);
                if (spelare1.drawBomb == true)
                {
                    spelare1.bomb.skapaBomb(spriteBatch);
                }
                if (spelare2.drawBomb == true)
                {
                    spelare2.bomb.skapaBomb(spriteBatch);
                }
                if (spelare1.explodera == true)
                {
                    spelare1.bomb.Explode(spriteBatch);
                }
                if (spelare2.explodera == true)
                {
                    spelare2.bomb.Explode(spriteBatch);
                }
            }

            if (gameState == 4)
            {
                spriteBatch.Draw(flagga, flaggaPosition, Color.White);
                vinnaren.Draw(spriteBatch);
                spriteBatch.DrawString(font, text1, text1Position, Color.Tan);
                spriteBatch.DrawString(font, textForts, textFortsPosition, Color.Tan);
            }
            spriteBatch.Draw(flagga, musRektangel, Color.Black);
            //spriteBatch.DrawString(font, gameState.ToString(), text1Position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
