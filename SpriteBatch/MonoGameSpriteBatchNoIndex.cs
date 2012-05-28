using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sce.Pss.Core;
using Sce.Pss.Core.Graphics;

namespace PssSpritePerformanceTests
{
	public class MonoGameSpriteBatchNoIndex : ISpriteBatch
	{
		public readonly int Size;
		GraphicsContext _context;
		
		VertexPosition2ColorTexture[] _vertexArray;
		VertexBuffer _vertexBuffer;
		
		Matrix4 _matrix;
		Texture2D _texture;

		int _index;
		
		ShaderProgram _shader;
		
		public MonoGameSpriteBatchNoIndex(GraphicsContext context, int size)
		{
			_context = context;
			Size = size;
			
			_vertexArray = new VertexPosition2ColorTexture[4 * size];
			_vertexBuffer = new VertexBuffer(4 * size, VertexFormat.Float2, VertexFormat.UByte4N, VertexFormat.Float2);
			
			_shader = new ShaderProgram("/Application/shaders/Texture.cgx");
		}

		#region ISpriteBatch implementation
		public void Begin (Matrix4 matrix)
		{
			_matrix = matrix;
			_index = 0;
		}

		public void Draw (Texture2D texture, Vector2 position, Color color)
		{
			_texture = texture; //Hack sorta
			
			_vertexArray[_index++] = new VertexPosition2ColorTexture(position, color, Vector2.Zero);
			_vertexArray[_index++] = new VertexPosition2ColorTexture(position + new Vector2(texture.Width, 0), color, Vector2.UnitX);
			_vertexArray[_index++] = new VertexPosition2ColorTexture(position + new Vector2(0, texture.Height), color, Vector2.UnitY);

			_vertexArray[_index++] = new VertexPosition2ColorTexture(position + new Vector2(texture.Width, 0), color, Vector2.UnitX);
			_vertexArray[_index++] = new VertexPosition2ColorTexture(position + new Vector2(texture.Width, texture.Height), color, Vector2.One);
			_vertexArray[_index++] = new VertexPosition2ColorTexture(position + new Vector2(0, texture.Height), color, Vector2.UnitY);
		}

		public void End ()
		{
			Matrix4 projection = Matrix4.Ortho(0, Screen.Width, Screen.Height, 0, 0, 1);
			Matrix4 transform = _matrix * projection;
			
			_vertexBuffer.SetVertices(_vertexArray, 0, 0, _index);
			_shader.SetUniformValue(0, ref transform);
			
			_context.SetShaderProgram(_shader);
			_context.SetVertexBuffer(0, _vertexBuffer);
			_context.SetTexture(0, _texture);

			_context.Enable(EnableMode.Blend);
			_context.SetBlendFunc(BlendFuncMode.Add, BlendFuncFactor.SrcAlpha, BlendFuncFactor.OneMinusSrcAlpha);

			_context.DrawArrays(DrawMode.Triangles, 0, _index);
		}
		#endregion
	}
}

