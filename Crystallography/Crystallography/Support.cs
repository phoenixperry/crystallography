/* SCE CONFIDENTIAL
 * PlayStation(R)Suite SDK 0.98.2
 * Copyright (C) 2013 Sony Computer Entertainment Inc.
 * All Rights Reserved.
 */

using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Audio;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Imaging;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Crystallography
{
	public static class Support
	{
		public static TextureFilterMode DefaultTextureFilterMode = TextureFilterMode.Linear;
		public static Dictionary<string, Texture2D> TextureCache = new Dictionary<string, Texture2D>();
		public static Dictionary<string, TextureInfo> TextureInfoCache = new Dictionary<string, TextureInfo>();
		
		/// <summary>
		/// Returns the index of the first slot in the array that equals <c>null</c>.
		/// </summary>
		/// <param name='pArray'>
		/// <see cref="System.Array"/>
		/// </param>
		public static int FindFreeSlot( object[] pArray ) {
			return System.Array.FindIndex(pArray, (obj) => obj == null);
		}
		
		public static Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile SpriteFromFile(string filename)
		{
			if (TextureCache.ContainsKey(filename) == false)
			{
				TextureCache[filename] = new Texture2D(filename, false);
				TextureInfoCache[filename] = new TextureInfo(TextureCache[filename], new Vector2i(1, 1));
			}
			
			var tex = TextureCache[filename];
			var info = TextureInfoCache[filename];
			var result = new Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile() { TextureInfo = info, };

			result.Quad.S = new Vector2(info.Texture.Width, info.Texture.Height);

			// DEBUG: testing for current assets
			result.Scale = new Vector2(1.0f);

			tex.SetFilter(DefaultTextureFilterMode);

			return result;
		}

		public static Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile UnicolorSprite(string name, byte r, byte g, byte b, byte a)
		{
			uint color = (uint)(a << 24 | b << 16 | g << 8 | r);

			if (TextureCache.ContainsKey(name) == false)
			{
				TextureCache[name] = GraphicsContextAlpha.CreateTextureUnicolor(color);
				TextureInfoCache[name] = new TextureInfo(TextureCache[name], new Vector2i(1, 1));
			}

			var tex = TextureCache[name];
			var info = TextureInfoCache[name];
			var result = new Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile() { TextureInfo = info };

			result.Quad.S = new Vector2(info.Texture.Width, info.Texture.Height);

			tex.SetFilter(DefaultTextureFilterMode);

			return result;
		}

		public static Sce.PlayStation.HighLevel.GameEngine2D.SpriteUV SpriteUVFromFile(string filename)
		{
			if (TextureCache.ContainsKey(filename) == false)
			{
				TextureCache[filename] = new Texture2D(filename, false);
				TextureInfoCache[filename] = new TextureInfo(TextureCache[filename]);
			}

			var tex = TextureCache[filename];
			var info = TextureInfoCache[filename];
			var result = new Sce.PlayStation.HighLevel.GameEngine2D.SpriteUV() { TextureInfo = info };

			result.Quad.S = new Vector2(info.Texture.Width, info.Texture.Height);

			tex.SetFilter(DefaultTextureFilterMode);

			return result;
		}

		public static Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile TiledSpriteFromFile(string filename, int x, int y)
		{
			if (TextureCache.ContainsKey(filename) == false)
			{
				TextureCache[filename] = new Texture2D(filename, false);
				TextureInfoCache[filename] = new TextureInfo(TextureCache[filename], new Vector2i(x, y));
			}
			
			var tex = TextureCache[filename];
			var info = TextureInfoCache[filename];
			var result = new Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile() { TextureInfo = info };

			result.TileIndex2D = new Vector2i(0, 0);

			result.Quad.S = new Vector2(info.Texture.Width / x, info.Texture.Height / y);

			tex.SetFilter(DefaultTextureFilterMode);

			return result;
		}

		public static Sce.PlayStation.HighLevel.GameEngine2D.SpriteList TiledSpriteListFromFile(string filename, int x, int y)
		{
			if (TextureCache.ContainsKey(filename) == false)
			{
				TextureCache[filename] = new Texture2D(filename, false);
				TextureInfoCache[filename] = new TextureInfo(TextureCache[filename], new Vector2i(x, y));
			}

			var tex = TextureCache[filename];
			var info = TextureInfoCache[filename];
			var result = new Sce.PlayStation.HighLevel.GameEngine2D.SpriteList(info);

			tex.SetFilter(DefaultTextureFilterMode);
			
			return result;
		}

		public static int IncrementTile(Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile sprite, int steps, int min, int max, bool looping)
		{
			int x = sprite.TextureInfo.NumTiles.X;
			int y = sprite.TextureInfo.NumTiles.Y;

			int current = sprite.TileIndex2D.X + sprite.TileIndex2D.Y * x;

			if (looping)
			{
				current -= min;
				current += steps;
				current %= max - min;
				current += min;
			}
			else
			{
				current += steps;
				current = System.Math.Min(current, max - 1);
			}

			sprite.TileIndex2D = new Vector2i(current % x, current / x);

			return current;
		}

		public static void SetTile(Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile sprite, int n)
		{
			int x = sprite.TextureInfo.NumTiles.X;
			int y = sprite.TextureInfo.NumTiles.Y;
			sprite.TileIndex2D = new Vector2i(n % x, n / x);
		}

		public static int GetTileIndex2D(Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile sprite)
		{
			if (sprite.TextureInfo.NumTiles.X <= 1 && sprite.TextureInfo.NumTiles.Y <= 1)
			{
				return 0;
			}

			return sprite.TileIndex2D.Y * sprite.TextureInfo.NumTiles.X + sprite.TileIndex2D.X;
		}

		public class AnimationAction : Sce.PlayStation.HighLevel.GameEngine2D.ActionBase
		{
			int animation_start;
			int animation_end;
			Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile attached_sprite;
			float counter;
			float frame_time;
			float speed;
			bool looping;

			public AnimationAction(Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile sprite, float seconds)
				: this(sprite, 0, sprite.TextureInfo.NumTiles.X * sprite.TextureInfo.NumTiles.Y, seconds)
			{
			}

			public AnimationAction(Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile sprite, int a, int b, float seconds, bool looping = false)
			{
				this.looping = looping;
				speed = 1.0f;

				attached_sprite = sprite;

				int min = System.Math.Min(a, b);
				int max = System.Math.Max(a, b);
				int frames = System.Math.Max(1, max - min);	

				frame_time = seconds / (float)frames;
				animation_start = min;
				animation_end = max;

				Reset();
			}

			public override void Run()
			{
				base.Run();
				Reset();
			}

			public override void Update(float dt)
			{
				dt *= speed;
				
				base.Update(dt);

				counter += dt;

				int frames = 0;
				while (frame_time > 0.0f && counter > frame_time)
				{
					counter -= frame_time;
					frames += 1;
				}

				int tile_index = IncrementTile(attached_sprite, frames, animation_start, animation_end, looping);

				if (!looping && tile_index == animation_end - 1)
				{
					Stop();
				}
			}

			public void SetSpeed(float speed)
			{
				this.speed = speed;
			}

			public void Reset()
			{
				counter = 0.0f;
				SetTile(attached_sprite, animation_start);
			}
		}

		public class SoundSystem
		{
			public static SoundSystem Instance = new SoundSystem("/Application/assets/audio/sfx/");

			public string AssetsPrefix;
			public Dictionary<string, SoundPlayer> SoundDatabase;

			public SoundSystem(string assets_prefix)
			{
				AssetsPrefix = assets_prefix;
				SoundDatabase = new Dictionary<string,SoundPlayer>();
			}

			public void CheckCache(string name)
			{
				if (SoundDatabase.ContainsKey(name)){
					return;
				}

				using (var sound = new Sound(AssetsPrefix + name) )
				{
					var player = sound.CreatePlayer();
					SoundDatabase[name] = player;
				}
			}

			public void Play(string name)
			{
				CheckCache(name);

				// replace any playing instance
				SoundDatabase[name].Stop();
				SoundDatabase[name].Play();
				SoundDatabase[name].Volume = 0.7f;
			}

			public void Stop(string name)
			{
				CheckCache(name);
				SoundDatabase[name].Stop();
			}

			public void PlayNoClobber(string name)
			{
				CheckCache(name);

				if (SoundDatabase[name].Status == SoundStatus.Playing){
					return;
				}

				SoundDatabase[name].Play();
				SoundDatabase[name].Volume = 0.7f;
			}
		}

		public class MusicSystem
		{
			public static MusicSystem Instance = new MusicSystem("/Application/assets/audio/bgm/");

			public string AssetsPrefix;
			public Dictionary<string, BgmPlayer> MusicDatabase;

			public MusicSystem(string assets_prefix)
			{
				AssetsPrefix = assets_prefix;
				MusicDatabase = new Dictionary<string, BgmPlayer>();
			}

			public void StopAll()
			{
				foreach (KeyValuePair<string, BgmPlayer> kv in MusicDatabase)
				{
					kv.Value.Stop();
					kv.Value.Dispose();
				}

				MusicDatabase.Clear();
			}

			public void Play(string name)
			{
				StopAll();

				using (var music = new Bgm(AssetsPrefix + name) )
				{
					var player = music.CreatePlayer();
					MusicDatabase[name] = player;
	
					MusicDatabase[name].Play();
					MusicDatabase[name].Loop = true;
					MusicDatabase[name].Volume = 0.4f;
				}
			}

			public void Stop(string name)
			{
				StopAll();
			}

			public void PlayNoClobber(string name)
			{
				if (MusicDatabase.ContainsKey(name))
				{
					if (MusicDatabase[name].Status == BgmStatus.Playing)
					{
						return;
					}
				}

				Play(name);
			}
		}

		public class AdjustableDelayAction
			: Sce.PlayStation.HighLevel.GameEngine2D.DelayTime
		{
			public float Speed { get; set; }

			public AdjustableDelayAction()
			{
				Speed = 1.0f;
			}

			public override void Update(float dt)
			{
				base.Update(dt * Speed);
			}
		}

		public class ParticleEffectsManager
			: Sce.PlayStation.HighLevel.GameEngine2D.Node
		{
			protected static ParticleEffectsManager _instance;
			
			public static ParticleEffectsManager Instance {
				get {
					if (_instance == null) {
						_instance = new ParticleEffectsManager();
					}
					return _instance;
				}
				protected set {
					_instance = value;
				}
			}
			
//			public static ParticleEffectsManager Instance = new ParticleEffectsManager();
			
			public class Particle
			{
				public SpriteTileCrystallonEntity parent;
				public int type;
				public float variant;
				public Vector2 position;
				public Vector2 offset;
				public Vector4 color;
				public Vector2 velocity;
				public float time;
				public float lifetime;
				public Vector2 size;
				public Vector2 size_delta;
			};

			public struct VertexData
			{
				public Vector2 position;
				public Vector2 uv;
				public Vector4 color;
			};
			
			public enum ParticleType {
				QUALITY = 0,
				SCORE = 1
			}

			public List<Particle> QualityParticles;
			public List<Particle> ScoreParticles;
//			public int ActiveParticles;
			public int ActiveQualityParticles;
			public int ActiveScoreParticles;
			public VertexData[] VertexDataArray;
			public ShaderProgram ShaderProgram;
			public Texture2D ParticleDotTexture;
			public Texture2D ParticleScoreIconTexture;
			public Vector2 gravity;

			ImmediateModeQuads< VertexData > imm_quads;
			int max_particles { get { return 768; } }
			int max_qualityParticles { get {return 668; } }
			int max_scoreParticles { get { return 100; } }

			public ParticleEffectsManager()
			{
				QualityParticles = new List<Particle>();
				ScoreParticles = new List<Particle>();
				ActiveQualityParticles = 0;
				ActiveScoreParticles = 0;
				gravity = new Vector2(0.0f, -800.0f);
				
				for (int i = 0; i < max_qualityParticles; ++i){
					QualityParticles.Add(new Particle());
				}
				for (int i=0; i < max_scoreParticles; i++){
					ScoreParticles.Add (new Particle());
				}

				ShaderProgram = new ShaderProgram("/Application/shaders/pfx.cgx");
				ParticleDotTexture = new Texture2D("/Application/assets/images/particles.png", false);
				ParticleScoreIconTexture = new Texture2D("/Application/assets/images/icons/icons.png", false);
				Sce.PlayStation.HighLevel.GameEngine2D.Scheduler.Instance.Schedule(this, Tick, 0.0f, false);

				AdHocDraw += this.DrawParticles;

				imm_quads = new ImmediateModeQuads< VertexData >( Director.Instance.GL, (uint)max_particles, VertexFormat.Float2, VertexFormat.Float2, VertexFormat.Float4 );
			}

			public void Tick(float dt)
			{
				float fullness = (float)ActiveQualityParticles / (float)QualityParticles.Count;
				float life_speed = fullness;
				
				// QUALITY PARTICLES
				for (int i = 0; i < ActiveQualityParticles; ++i)
				{
					Particle p = QualityParticles[i];
					p.offset += dt * p.velocity;
					p.position = p.parent.getNode().LocalToWorld(p.parent.getNode().Pivot/2) + p.offset;
					p.time += dt;
					p.time += dt * fullness;
					p.size += p.size_delta;
					p.color.W -= dt/p.lifetime;
				}

				for (int i = 0; i < ActiveQualityParticles; ++i)
				{
					Particle p = QualityParticles[i];

					if (p.size.X <= 0.0f || p.size.Y <= 0.0f)
					{
						p.time = p.lifetime;
					}

					if (p.time < p.lifetime)
					{
						continue;
					}

					QualityParticles[i] = QualityParticles[ActiveQualityParticles - 1];
					QualityParticles[ActiveQualityParticles - 1] = p;
					ActiveQualityParticles--;
					i--;
				}
				
				// SCORE PARTICLES
				for (int i = 0; i < ActiveScoreParticles; ++i)
				{
					Particle p = ScoreParticles[i];
					p.velocity += dt * gravity;
					p.offset += dt * p.velocity;
					p.position = p.parent.getNode().LocalToWorld(p.parent.getNode().Pivot/2) + p.offset;
					p.time += dt;
					p.time += dt * fullness;
//					p.size += p.size_delta;
					p.color.W -= dt/p.lifetime;
				}

				for (int i = 0; i < ActiveScoreParticles; ++i)
				{
					Particle p = ScoreParticles[i];

					if (p.size.X <= 0.0f || p.size.Y <= 0.0f)
					{
						p.time = p.lifetime;
					}

					if (p.time < p.lifetime)
					{
						continue;
					}

					ScoreParticles[i] = ScoreParticles[ActiveScoreParticles - 1];
					ScoreParticles[ActiveScoreParticles - 1] = p;
					ActiveScoreParticles--;
					i--;
				}
			}

			public void DrawParticles()
			{
				float x1, x2, y1, y2;
				Matrix4 transform = Director.Instance.GL.GetMVP();
				
				Director.Instance.GL.ModelMatrix.Push();
				Director.Instance.GL.ModelMatrix.SetIdentity();

				ShaderProgram.SetUniformValue(ShaderProgram.FindUniform("MVP"), ref transform);

				ShaderProgram.SetAttributeBinding(0, "iPosition");
				ShaderProgram.SetAttributeBinding(1, "iUV");
				ShaderProgram.SetAttributeBinding(2, "iColor");

				Director.Instance.GL.Context.SetShaderProgram(ShaderProgram);
				
				Common.Assert( ActiveQualityParticles <= imm_quads.MaxQuads );

				imm_quads.ImmBeginQuads( (uint)ActiveQualityParticles );
				
				for (int i = 0; i < ActiveQualityParticles; ++i)
				{
					Particle p = QualityParticles[i];
					y1 = 0.0f;
					y2 = 1.0f;
					x1 = p.variant/2.0f;
					x2 = (p.variant+1)/2.0f;
					Director.Instance.GL.Context.SetTexture(0, ParticleDotTexture);
					imm_quads.ImmAddQuad( 
					new VertexData() { position = p.position + new Vector2(0, 0), uv = new Vector2(x1, 1.0f-y1), color = p.color },
					new VertexData() { position = p.position + new Vector2(p.size.X, 0), uv = new Vector2(x2, 1.0f-y1), color = p.color },
					new VertexData() { position = p.position + new Vector2(0, p.size.Y), uv = new Vector2(x1, 1.0f-y2), color = p.color },
					new VertexData() { position = p.position + new Vector2(p.size.X, p.size.Y), uv = new Vector2(x2, 1.0f-y2), color = p.color } );
				}

				imm_quads.ImmEndQuads();

				Director.Instance.GL.Context.SetShaderProgram(null);
				Director.Instance.GL.Context.SetVertexBuffer(0, null);
				Director.Instance.GL.ModelMatrix.Pop();
				
				Director.Instance.GL.ModelMatrix.Push();
				Director.Instance.GL.ModelMatrix.SetIdentity();

				ShaderProgram.SetUniformValue(ShaderProgram.FindUniform("MVP"), ref transform);

				ShaderProgram.SetAttributeBinding(0, "iPosition");
				ShaderProgram.SetAttributeBinding(1, "iUV");
				ShaderProgram.SetAttributeBinding(2, "iColor");

				Director.Instance.GL.Context.SetShaderProgram(ShaderProgram);
				
				Common.Assert( ActiveQualityParticles <= imm_quads.MaxQuads );

				imm_quads.ImmBeginQuads( (uint)ActiveScoreParticles );
				
				for (int i = 0; i < ActiveScoreParticles; ++i)
				{
					Particle p = ScoreParticles[i];
					y1 = (float)System.Math.Floor(p.variant/4.0f);
					y2 = 1.0f+y1;
					x1 = (p.variant-y1*4.0f)%4.0f;
					x2 = 1.0f+x1;
					Director.Instance.GL.Context.SetTexture(0, ParticleScoreIconTexture);
					imm_quads.ImmAddQuad( 
					new VertexData() { position = p.position + new Vector2(0, 0), uv = new Vector2(x1/4.0f, 1.0f-y1/2.0f), color = p.color },
					new VertexData() { position = p.position + new Vector2(p.size.X, 0), uv = new Vector2(x2/4.0f, 1.0f-y1/2.0f), color = p.color },
					new VertexData() { position = p.position + new Vector2(0, p.size.Y), uv = new Vector2(x1/4.0f, 1.0f-y2/2.0f), color = p.color },
					new VertexData() { position = p.position + new Vector2(p.size.X, p.size.Y), uv = new Vector2(x2/4.0f, 1.0f-y2/2.0f), color = p.color } );
					
				}

				imm_quads.ImmEndQuads();

				Director.Instance.GL.Context.SetShaderProgram(null);
				Director.Instance.GL.Context.SetVertexBuffer(0, null);
				Director.Instance.GL.ModelMatrix.Pop();
			}

//			public void AddParticlesBurst(int count, Vector2 position, Vector2 velocity, Vector4 color, float jitter = 0.0f, float scale_multiplier = 1.0f)
//			{
//				for (int i = 0; i < count; ++i)
//				{
//					Vector2 p = position + Game.Instance.Random.NextVector2() * jitter;
//					Vector2 v = velocity + Game.Instance.Random.NextVector2() * jitter;
//					AddParticle(p, v, color, scale_multiplier);
//				}
//			}
//
//			public void AddParticlesCone(int count, Vector2 position, Vector2 velocity, Vector4 color, float spread, float scale_multiplier = 1.0f)
//			{
//				for (int i = 0; i < count; ++i)
//				{
//					Vector2 p = position + velocity * Game.Instance.Random.NextFloat();
//					Vector2 v = velocity + velocity.Rotate(Game.Instance.Random.NextSignedFloat() * spread + spread * 0.5f);
//					AddParticle(p, v, color, scale_multiplier);
//				}
//			}

			public void AddParticle( int pVariant, SpriteTileCrystallonEntity pParent, Vector4 color, float scale_multiplier)
			{
				if (ActiveQualityParticles >= QualityParticles.Count)
				{
#if DEBUG
					System.Console.WriteLine("Hit Max Particle Count(" + QualityParticles.Count.ToString() + "! Particle not created.");
#endif
					return;
				}

				Particle p = QualityParticles[ActiveQualityParticles];
				p.variant = pVariant;
				p.parent = pParent;
				p.offset = new Vector2( GameScene.Random.NextSignedFloat() * pParent.Width*0.25f + 15.0f,
				                       GameScene.Random.NextSignedFloat() * pParent.Height*0.35f + 15.0f);
				p.position = pParent.getNode().LocalToWorld(new Vector2(pParent.Width/2.0f, pParent.Height/2.0f)) + p.offset;
//				p.velocity = GameScene.Random.NextVector2();
				p.color = color;
				p.time = 0.0f;
				p.lifetime = 0.3f;
				p.size = Vector2.One * scale_multiplier;
				p.velocity = Vector2.Zero;
				p.size_delta = new Vector2(1.5f);
				p.type = (int)ParticleType.QUALITY;
//				p.gravity = 0.75f;
				ActiveQualityParticles++;
			}
			
			public void AddScoreParticle( string pName, SpriteTileCrystallonEntity pParent, Vector4 pColor) {
				if (ActiveScoreParticles >= ScoreParticles.Count) {
#if DEBUG
					System.Console.WriteLine("Hit Max Particle Count(" + ScoreParticles.Count.ToString() + "! Particle not created.");
#endif
					return;		
				}
				if( pName == null) { 
					return; 
				}
				
				Particle p = ScoreParticles[ActiveScoreParticles];
				p.variant = (float)EnumHelper.FromString<Icons>(pName);
				p.parent = pParent;
				
				p.position = pParent.getNode().LocalToWorld(pParent.getNode().Pivot) + p.offset;
				p.color = pColor;
				p.time = 0.0f;
				p.lifetime = 2.0f;
				p.size = new Vector2(64.0f, 64.0f);
				p.offset = new Vector2( p.size.X * 0.5f, pParent.Height * 0.5f);
//				p.size_delta = Vector2.Zero;
				p.velocity = -0.3f * p.lifetime * gravity + Vector2.UnitX * GameScene.Random.NextSign()*(50.0f + 50.0f * GameScene.Random.NextFloat());
				p.type = (int)ParticleType.SCORE;
				ActiveScoreParticles++;
			}
			
			public void Destroy() {
				imm_quads.Dispose();
				Sce.PlayStation.HighLevel.GameEngine2D.Scheduler.Instance.Unschedule(this, Tick);
				AdHocDraw -= this.DrawParticles;
				ParticleScoreIconTexture.Dispose();
				ParticleDotTexture.Dispose();
				foreach (Particle p in QualityParticles) {
					p.parent = null;
//					p.position = null;
//					p.offset = null;
//					p.color = null;
//					p.velocity = null;
//					p.size = null;
//					p.size_delta = null;
				}
				QualityParticles.Clear();
				QualityParticles = null;
				foreach (Particle p in ScoreParticles) {
					p.parent = null;
//					p.position = null;
//					p.offset = null;
//					p.color = null;
//					p.velocity = null;
//					p.size = null;
//					p.size_delta = null;
				}
				ScoreParticles.Clear();
				ScoreParticles = null;
//				gravity = null;
				Instance = null;
			}
				
				
//			public void AddParticleWater(Vector2 position, Vector2 velocity, Vector4 color, float scale_multiplier)
//			{
//				if (ActiveParticles >= Particles.Count)
//				{
//					return;
//				}
//				
//				Particle p = Particles[ActiveParticles];
//				p.position = position;
//				p.velocity = velocity;
//				p.friction = Vector2.One;
//				p.color = color;
//				p.time = 0.0f;
//				p.lifetime = 1.0f;
//				p.size = Vector2.One * 8.0f * scale_multiplier;
//				p.size_delta = new Vector2(-0.1f);
//				p.gravity = -0.025f;
//				ActiveParticles++;
//			}
		}

		public class TextureTileMapManager
		{
			public struct VertexData
			{
				public Vector2 position;
				public Vector2 uv;
			};

			public VertexBuffer VertexBuffer;
			public ShaderProgram ShaderProgram;
			public VertexData[] VertexDataArray;
			public ushort[] IndexDataArray;

			public class Entry
			{
				public int TilesX;
				public int TilesY;
				public int TileWidth;
				public int TileHeight;
				public List<List<byte>> Data;
			};

			public Dictionary<string, Entry> TileData;

			public static int ScaleDivisor = 4;

			public TextureTileMapManager()
			{
				VertexBuffer = new VertexBuffer(4, 4, VertexFormat.Float2, VertexFormat.Float2);
				ShaderProgram = new ShaderProgram("SirAwesomealot/offscreen.cgx");
				VertexDataArray = new VertexData[4];
				IndexDataArray = new ushort[4] { 0, 1, 2, 3 };
				
				TileData = new Dictionary<string, Entry>();
			}

			public void Add(string name, Texture2D texture, int tiles_x, int tiles_y)
			{
				int tile_width = (int)FMath.Round((float)texture.Width / (float)tiles_x);
				int tile_height = (int)FMath.Round((float)texture.Height / (float)tiles_y);
				tile_height /= ScaleDivisor;
				tile_width /= ScaleDivisor;
				ColorBuffer color_buffer = new ColorBuffer(tile_width, tile_height, PixelFormat.Rgba);
				FrameBuffer frame_buffer = new FrameBuffer();
				frame_buffer.SetColorTarget(color_buffer);

				FrameBuffer old_frame_buffer = Director.Instance.GL.Context.GetFrameBuffer();
				Director.Instance.GL.Context.SetFrameBuffer(frame_buffer);

				ShaderProgram.SetAttributeBinding(0, "iPosition");
				ShaderProgram.SetAttributeBinding(1, "iUV");

				texture.SetWrap(TextureWrapMode.ClampToEdge);
				texture.SetFilter(TextureFilterMode.Linear);

				Director.Instance.GL.Context.SetTexture(0, texture);
				Director.Instance.GL.Context.SetVertexBuffer(0, VertexBuffer);
				Director.Instance.GL.Context.SetShaderProgram(ShaderProgram);
				
				ImageRect old_scissor = Director.Instance.GL.Context.GetScissor();
				ImageRect old_viewport = Director.Instance.GL.Context.GetViewport();
				
				Director.Instance.GL.Context.SetScissor(0, 0, tile_width, tile_height);
				Director.Instance.GL.Context.SetViewport(0, 0, tile_width, tile_height);
				
				Entry entry = new Entry();
				entry.TilesX = tiles_x;
				entry.TilesY = tiles_y;
				entry.TileWidth = tile_width;
				entry.TileHeight = tile_height;
				entry.Data = new List<List<byte>>();
				for (int i = 0; i < tiles_y * tiles_x; ++i)
				{
					entry.Data.Add(new List<byte>());
				}

				Vector2 uv_step = new Vector2(1.0f, 1.0f) / new Vector2(tiles_x, tiles_y);
				for (int y = 0; y < tiles_y; y++)
				{
					for (int x = 0; x < tiles_x; x++)
					{
						float uv_x0 = uv_step.X * (x + 0);
						float uv_x1 = uv_step.X * (x + 1);
						float uv_y0 = uv_step.Y * (tiles_y - 1 - y + 0);
						float uv_y1 = uv_step.Y * (tiles_y - 1 - y + 1);

						VertexDataArray[0] = new VertexData() { position = new Vector2(-1.0f, -1.0f), uv = new Vector2(uv_x0, uv_y1) };
						VertexDataArray[1] = new VertexData() { position = new Vector2(-1.0f, +1.0f), uv = new Vector2(uv_x0, uv_y0) };
						VertexDataArray[2] = new VertexData() { position = new Vector2(+1.0f, +1.0f), uv = new Vector2(uv_x1, uv_y0) };
						VertexDataArray[3] = new VertexData() { position = new Vector2(+1.0f, -1.0f), uv = new Vector2(uv_x1, uv_y1) };
						VertexBuffer.SetIndices(IndexDataArray, 0, 0, 4);
						VertexBuffer.SetVertices(VertexDataArray, 0, 0, 4);

						Director.Instance.GL.Context.SetVertexBuffer(0, VertexBuffer);
						Director.Instance.GL.Context.SetClearColor(0.0f, 0.0f, 0.0f, 0.0f);
						Director.Instance.GL.Context.Clear();
						Director.Instance.GL.Context.DrawArrays(DrawMode.TriangleFan, 0, 4);
						
						byte[] data = new byte[tile_width * tile_height * 4];
						Director.Instance.GL.Context.ReadPixels(data, PixelFormat.Rgba, 0, 0, tile_width, tile_height);
						
						List<byte> output = entry.Data[tiles_x * y + x];
						for (int i = 0; i < tile_width * tile_height * 4; ++i)
						{
							output.Add(data[i]);
						}
					}
				}

				Director.Instance.GL.Context.SetVertexBuffer(0, null);
				Director.Instance.GL.Context.SetShaderProgram(null);
				Director.Instance.GL.Context.SetFrameBuffer(old_frame_buffer);
				Director.Instance.GL.Context.SetScissor(old_scissor);
				Director.Instance.GL.Context.SetViewport(old_viewport);

				TileData[name] = entry;
			}

			public Texture2D MakeTexture(string name, int tile)
			{
				Entry entry = TileData[name];
				List<byte> data = entry.Data[tile];

				Texture2D texture = new Texture2D(entry.TileWidth, entry.TileHeight, false, PixelFormat.Rgba);
				texture.SetPixels(0, data.ToArray());
				return texture;
			}
		}

		public static Vector4 Color(byte r, byte g, byte b, byte a = 255)
		{
			return new Sce.PlayStation.Core.Vector4(
				(float)r / 255.0f,
				(float)g / 255.0f,
				(float)b / 255.0f,
				(float)a / 255.0f
			);
		}

		public static float SafeLength(this Vector2 self)
		{
			float length_squared = self.LengthSquared();

			if (length_squared < 0.00001f)
			{
				return 0.0f;
			}

			return (float)System.Math.Sqrt(length_squared);
		}
	}

}



