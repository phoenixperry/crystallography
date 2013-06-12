using System;
using System.IO;

namespace Crystallography
{
	public static class DataStorage
	{
		static readonly string SAVE_FILE = "/Documents/crystallon.dat";
		static readonly string TEMP_FILE = "/Documents/crystallon.tmp";
		
		static readonly int numPuzzles = 41;
		static readonly int numTimedHighScores = 3;
		static readonly int numInfiniteHighScores = 3;
		
		static public Int32[] puzzleScores = new Int32[numPuzzles];
		static public Int32[] timedScores = new Int32[numTimedHighScores];
		static public Int32[] infiniteScores = new Int32[numInfiniteHighScores];
		
		public static void SavePuzzleScore(int pLevel, int pScore) {
			puzzleScores[pLevel] = pScore;
			SaveData();
		}
		
		public static void SaveTimedScore(int pScore) {
			int score = pScore;
			int scorebuffer;
			for( int i=0; i<numTimedHighScores; i++) {
				if (timedScores[i] < pScore) {
					scorebuffer = timedScores[i];
					timedScores[i] = score;
					score = scorebuffer;
				}
			}
			SaveData();
		}
		
		public static void SaveInfiniteScore(int pScore) {
			int score = pScore;
			int scorebuffer;
			for( int i=0; i<numInfiniteHighScores; i++) {
				if (infiniteScores[i] < pScore) {
					scorebuffer = infiniteScores[i];
					infiniteScores[i] = score;
					score = scorebuffer;
				}
			}
			SaveData();
		}
		
		public static void SaveData() {
#if DEBUG
			Console.WriteLine("==Save Data==");
#endif
			int bufferSize = sizeof(Int32) * numPuzzles;
			bufferSize += sizeof(Int32) * numTimedHighScores;
			bufferSize += sizeof(Int32) * numInfiniteHighScores;
			bufferSize += sizeof(Int32) * 1; // hash
			
			byte[] buffer = new byte [bufferSize];
			
			Int32 sum = 0;
			int count = 0;
			
			// PUZZLE MODE DATA
			for( int i=0; i < numPuzzles; ++i ) {
				Buffer.BlockCopy(puzzleScores, sizeof(Int32) * i, buffer, sizeof(Int32) * count, sizeof(Int32));
				count++;
				sum+=puzzleScores[i];
			}
			// TIMED MODE DATA
			for( int i=0; i < numTimedHighScores; ++i ) {
				Buffer.BlockCopy(timedScores, sizeof(Int32) * i, buffer, sizeof(Int32) * count, sizeof(Int32));
				count++;
				sum+=timedScores[i];
			}
			// INFINITE MODE DATA
			for( int i=0; i < numInfiniteHighScores; ++i ) {
				Buffer.BlockCopy(infiniteScores, sizeof(Int32) * i, buffer, sizeof(Int32) * count, sizeof(Int32));
				count++;
				sum+=infiniteScores[i];
			}
			
			Int32 hash = sum.GetHashCode();
			
#if DEBUG
			Console.WriteLine("sum={0}, hash={1}", sum, hash);
#endif
			
			Buffer.BlockCopy(BitConverter.GetBytes(hash), 0, buffer, count * sizeof(Int32), sizeof(Int32));
			
			using ( FileStream hStream = File.Open(@TEMP_FILE, FileMode.Create) ) {
				hStream.SetLength((int)bufferSize);
				hStream.Write(buffer, 0, (int)bufferSize );
				hStream.Close();
			}
			
			if (File.Exists(@SAVE_FILE)) {
				File.Delete(@SAVE_FILE);
			}
			
			File.Move(@TEMP_FILE, @SAVE_FILE);
#if DEBUG
			Console.WriteLine("==Save Data Complete==");
#endif
		}
		
		
		public static bool LoadData() {
#if DEBUG
			Console.WriteLine("==Load Data==");
#endif
			if ( false == File.Exists(@SAVE_FILE) && true == File.Exists(@TEMP_FILE) ) {
				File.Move(@TEMP_FILE, @SAVE_FILE);
			}
			
			if ( true == File.Exists(@SAVE_FILE) ) {
			
				using ( FileStream hStream = File.Open(@SAVE_FILE, FileMode.Open) ) {
					if (hStream != null) {
						long size = hStream.Length;
						byte[] buffer = new byte[size];
						hStream.Read(buffer, 0, (int)size);
						
						Int32 sum=0;
						int count=0;
						// PUZZLE MODE DATA
						for( int i=0; i<numPuzzles; ++i ) {
							Buffer.BlockCopy(buffer, sizeof(Int32) * count, puzzleScores, sizeof(Int32) * i, sizeof(Int32) );
							count++;
							sum += puzzleScores[i];
						}
						// TIMED MODE DATA
						for( int i=0; i<numTimedHighScores; ++i ) {
							Buffer.BlockCopy(buffer, sizeof(Int32) * count, timedScores, sizeof(Int32) * i, sizeof(Int32) );
							count++;
							sum += timedScores[i];
						}
						// INFINITE MODE DATA
						for( int i=0; i<numInfiniteHighScores; ++i ) {
							Buffer.BlockCopy(buffer, sizeof(Int32) * count, infiniteScores, sizeof(Int32) * i, sizeof(Int32) );
							count++;
							sum += infiniteScores[i];
						}
						
						Int32 hash = BitConverter.ToInt32( buffer, count * sizeof(Int32) );
						
						hStream.Close();
						
						if(sum.GetHashCode() == hash) {
#if DEBUG
							Console.WriteLine("==Load Data OKAY==");
#endif
							return true;
						} else {
#if DEBUG
							Console.WriteLine("==Load Data CORRUPTED==");
#endif
							return false;
						}
					}
				}
			}
#if DEBUG
			Console.WriteLine("==Load Data DATA DOES NOT EXIST==");
#endif
			return false;
		}
		
		
		public static void ClearData() {
#if DEBUG
			Console.WriteLine("==Clear Data==");
#endif
			// PUZZLE MODE DATA
			for ( int i=0; i<numPuzzles; ++i) {
				puzzleScores[i] = 0;
			}
			// TIMED MODE DATA
			for( int i=0; i<numTimedHighScores; ++i ) {
				timedScores[i] = 0;
			}
			// INFINITE MODE DATA
			for( int i=0; i < numInfiniteHighScores; ++i ) {
				infiniteScores[i] = 0;
			}
			
			SaveData();
		}
		
		
		public static void Init() {
			ClearData();
		}
	}
}

