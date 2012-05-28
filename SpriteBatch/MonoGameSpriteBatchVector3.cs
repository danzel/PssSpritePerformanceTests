using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sce.Pss.Core;
using Sce.Pss.Core.Graphics;

namespace PssSpritePerformanceTests
{
	public class MonoGameSpriteBatchVector3 : ISpriteBatch
	{
		public readonly int Size;
		GraphicsContext _context;
		
		ushort[] _indexArray;
		VertexPositionColorTexture[] _vertexArray;
		VertexBuffer _vertexBuffer;
		
		Matrix4 _matrix;
		Texture2D _texture;

		int _index;
		
		ShaderProgram _shader;
		
		public MonoGameSpriteBatchVector3 (GraphicsContext context, int size)
		{
			_context = context;
			Size = size;
			
			_indexArray = new ushort[6 * size];
			for (int i = 0; i < size; i++)
			{
				_indexArray[i * 6 + 0] = (ushort)(i * 4);
				_indexArray[i * 6 + 1] = (ushort)(i * 4 + 1);
				_indexArray[i * 6 + 2] = (ushort)(i * 4 + 2);
				_indexArray[i * 6 + 3] = (ushort)(i * 4 + 1);
				_indexArray[i * 6 + 4] = (ushort)(i * 4 + 3);
				_indexArray[i * 6 + 5] = (ushort)(i * 4 + 2);
			}
			
			_vertexArray = new VertexPositionColorTexture[4 * size];
			_vertexBuffer = new VertexBuffer(4 * size, 6 * size, VertexFormat.Float3, VertexFormat.UByte4N, VertexFormat.Float2);
			_vertexBuffer.SetIndices(_indexArray, 0, 0, 6 * size);
			
			
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
			
			_vertexArray[_index++] = new VertexPositionColorTexture(new Vector3(position.X, position.Y, 0), color, Vector2.Zero);
			_vertexArray[_index++] = new VertexPositionColorTexture(new Vector3(position.X + texture.Width, position.Y, 0), color, Vector2.UnitX);
			_vertexArray[_index++] = new VertexPositionColorTexture(new Vector3(position.X, position.Y + texture.Height, 0), color, Vector2.UnitY);
			_vertexArray[_index++] = new VertexPositionColorTexture(new Vector3(position.X + texture.Width, position.Y + texture.Height, 0), color, Vector2.One);
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

			_context.DrawArrays(DrawMode.Triangles, 0, _index / 2 * 3);
		}
		#endregion
	}
}

