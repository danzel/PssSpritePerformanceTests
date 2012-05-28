using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Sce.Pss.Core;
using Sce.Pss.Core.Environment;
using Sce.Pss.Core.Graphics;
using Sce.Pss.Core.Input;

namespace PssSpritePerformanceTests
{
	public class AppMain
	{
		public static ReusableStopWatch StopWatch = new ReusableStopWatch();
		
		private static GraphicsContext _graphics;
		private static Texture2D _texture;
		private static ISpriteBatch _spriteBatch;
		
		public static void Main (string[] args)
		{
			Initialize ();

			while (true) {
				SystemEvents.CheckEvents ();
				Update ();
				Render ();
			}
		}

		public static void Initialize ()
		{
			// Set up the graphics system
			_graphics = new GraphicsContext ();
			
			_texture = new Texture2D("/Application/32.png", false);
			
			//_spriteBatch = new MonoGameSpriteBatch(_graphics, 256);
			//_spriteBatch = new MonoGameSpriteBatchNoIndex(_graphics, 256);
			//_spriteBatch = new MonoGameSpriteBatchVector3(_graphics, 256);
			_spriteBatch = new MonoGameSpriteBatchUnpacked(_graphics, 256);
		}

		public static void Update ()
		{
			// Query gamepad for current state
			//var gamePadData = GamePad.GetData (0);
		}

		public static void Render ()
		{
			StopWatch.Start();
			// Clear the screen
			_graphics.SetClearColor (0.0f, 0.0f, 0.0f, 0.0f);
			_graphics.Clear ();
			
			StopWatch.TakeReading(); //0
			_spriteBatch.Begin(Matrix4.Identity);
			StopWatch.TakeReading(); //1
			
			//for (int i = 0; i < 100; i++)
				_spriteBatch.Draw(_texture, new Vector2(50, 50), Color.White);
			StopWatch.TakeReading(); //2
			
			_spriteBatch.End();
			StopWatch.TakeReading(); //3
			
			// Present the screen
			_graphics.SwapBuffers ();
			StopWatch.TakeReading(); //4
		}
	}
}