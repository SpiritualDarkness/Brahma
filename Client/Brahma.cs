using System.Reflection;
using AssetManagementBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Graphics2D.UI;

namespace Tech.SpritualDarkness.Brahma.Client
{
    public class Brahma : Game
    {
        private GraphicsDeviceManager _graphics;
        private Desktop _desktop;
        private AssetManager _globalAssetManager;
        private string _assetPath = "./Assets";
        private string _guiAssetPath = "/Gui";
        public Brahma()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            base.Initialize();
            _globalAssetManager = AssetManager.CreateFileAssetManager(_assetPath);

            string data = File.ReadAllText()
            Project.LoadFromXml("",_globalAssetManager);
        }

        protected override void LoadContent()
        {
            MyraEnvironment.Game = this;

            var grid = new Grid
            {
                RowSpacing = 8,
                ColumnSpacing = 8
            };

            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));

            var helloWorld = new Label
            {
                Id = "label",
                Text = "Hello, World!"
            };
            grid.Widgets.Add(helloWorld);

            // ComboBox
            var combo = new ComboBox();
            Grid.SetColumn(combo, 1);
            Grid.SetRow(combo, 0);

            combo.Items.Add(new ListItem("Red", Color.Red));
            combo.Items.Add(new ListItem("Green", Color.Green));
            combo.Items.Add(new ListItem("Blue", Color.Blue));
            grid.Widgets.Add(combo);

            // Button
            var button = new Button
            {
                Content = new Label
                {
                    Text = "Show"
                }
            };
            Grid.SetColumn(button, 0);
            Grid.SetRow(button, 1);

            button.Click += (s, a) =>
            {
                var messageBox = Dialog.CreateMessageBox("Message", "Some message!");
                messageBox.ShowModal(_desktop);
            };

            grid.Widgets.Add(button);

            // Spin button
            var spinButton = new SpinButton
            {
                Width = 100,
                Nullable = true
            };
            Grid.SetColumn(spinButton, 1);
            Grid.SetRow(spinButton, 1);

            grid.Widgets.Add(spinButton);

            // Add it to the desktop
            _desktop = new Desktop();
            _desktop.Root = grid;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _desktop.Render();

            base.Draw(gameTime);
        }
    }
}