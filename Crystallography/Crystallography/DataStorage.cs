using System;
using System.IO;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Crystallography
{
	public static class DataStorage
	{
		static readonly string SAVE_FILE = "/Documents/crystallon.dat";
		static readonly string SAVE_TEMP = "/Documents/crystallon.tmp";
		
		static readonly int numPuzzles = 41;
		static readonly int numTimedModes = 4;
		static readonly int numTimedHighScores = 3;
		static readonly int numTimedHighCubes = 3;
		static readonly int numInfiniteHighScores = 1;
		static readonly int numInfiniteHighCubes = 1;
		static readonly int numInfiniteHighTimes = 1;
		
		static public Boolean[] puzzleComplete = new Boolean[numPuzzles];
		static public Boolean[] puzzleLocked = new Boolean[numPuzzles];
		static public Dictionary<Int32, List<Int32[]>> puzzleSolutionsFound = new Dictionary<Int32, List<Int32[]>>();
		static public Int32[] puzzleSolutionsCount = new Int32[numPuzzles];
		
		static public Int32[,,] timedScores = new Int32[numTimedModes, numTimedHighScores, 2];
		static public Int32[,,] timedCubes = new Int32[numTimedModes, numTimedHighCubes, 2];
		
		static public Int32[,] infiniteScores = new Int32[numInfiniteHighScores, 3];
		static public Int32[,] infiniteCubes = new Int32[numInfiniteHighCubes, 3];
		static public Int32[,] infiniteTimes = new Int32[numInfiniteHighTimes, 3];
		
		static public Int32[] options = new Int32[5];
		
#if METRICS
		static readonly string METRICS_FILE = "/Documents/metrics.dat";
		static readonly string METRICS_TEMP = "/Documents/metrics.tmp";
		static private List<string> metrics = new List<string>();
		static private LinkedList<Metric> m = new LinkedList<Metric>();
#endif
		
		public static void SavePuzzleScore(int pLevel, int pCubes, int pScore, bool pComplete) {
#if DEBUG
			Console.WriteLine("### SAVING SOLUTION DATA: Lv {0} Cu {1} Sc {2} Cmpl {3}", pLevel, pCubes, pScore, pComplete);
#endif
			Int32[] solution = new Int32[]{pCubes, pScore};
			var previousSolutions = puzzleSolutionsFound[pLevel];
			bool okToAdd = true;
			foreach( var ps in previousSolutions ) { // ---- Check if solution was found previously
				if ( ps[0] == solution[0] ) {
					if ( ps[1] == solution[1] ) {
						okToAdd = false;
						break;
					}
				}
			}
			if( okToAdd ) {
				puzzleSolutionsFound[pLevel].Add(solution); // ----------------------- Record it
				puzzleSolutionsCount[pLevel]++; // ----------------------------------- Increment the counter
				puzzleComplete[pLevel] = pComplete;
				if (pLevel+1 < puzzleComplete.Length) {
					puzzleLocked[pLevel+1] = false;
				}
				SaveData(); // ------------------------------------------------------- Externalize (only if we made a change)
			}
		}
		
		public static void SaveTimedScore(int pCubes, int pScore, int pWhichTimedMode) {
#if DEBUG
			Console.WriteLine("### SAVING TIMED SCORE DATA: Lv {0} Cu {1} Sc {2} Mode {3}", 999, pCubes, pScore, pWhichTimedMode);
#endif
			int score = pScore;
			int cubes = pCubes;
			int[] buffer = {0,0};
			for( int i=0; i<numTimedHighScores; i++) {
				if (timedScores[pWhichTimedMode,i,1] < score) {
					buffer[0] = timedScores[pWhichTimedMode, i, 0];
					buffer[1] = timedScores[pWhichTimedMode, i, 1];
					timedScores[pWhichTimedMode, i, 0] = cubes;
					timedScores[pWhichTimedMode, i, 1] = score;
					cubes = buffer[0];
					score = buffer[1];
				}
			}
			score = pScore;
			cubes = pCubes;
			for( int i=0; i<numTimedHighCubes; i++) {
				if( timedCubes[pWhichTimedMode, i, 0] < cubes ) {
					buffer[0] = timedCubes[pWhichTimedMode, i, 0];
					buffer[1] = timedCubes[pWhichTimedMode, i, 1];
					timedCubes[pWhichTimedMode, i, 0] = cubes;
					timedCubes[pWhichTimedMode, i, 1] = score;
					cubes = buffer[0];
					score = buffer[1];
				}
			}
			SaveData();
		}
		
		public static void SaveInfiniteScore(int pCubes, int pScore, float pTime) {
			int iTime = (int)Sce.PlayStation.Core.FMath.Floor(pTime);
#if DEBUG
			Console.WriteLine("### SAVING INF SCORE DATA: Lv {0} Cu {1} Sc {2} Time {3}", 999, pCubes, pScore, iTime);
#endif
			int[] buffer = {0,0,0};
			
			// BEST SCORES
			int score = pScore;
			int cubes = pCubes;
			int time = iTime;
			for( int i=0; i<numInfiniteHighScores; i++ ) {
				if (infiniteScores[i, 1] < score) {
					buffer[0] = infiniteScores[i, 0];
					buffer[1] = infiniteScores[i, 1];
					buffer[2] = infiniteScores[i, 2];
					infiniteScores[i, 0] = cubes;
					infiniteScores[i, 1] = score;
					infiniteScores[i, 2] = time;
					cubes = buffer[0];
					score = buffer[1];
					time  = buffer[2];
				}
			}
			// BEST CUBES
			score = pScore;
			cubes = pCubes;
			time = iTime;
			for( int i=0; i<numInfiniteHighCubes; i++ ) {
				if (infiniteCubes[i, 0] < cubes) {
					buffer[0] = infiniteCubes[i, 0];
					buffer[1] = infiniteCubes[i, 1];
					buffer[2] = infiniteCubes[i, 2];
					infiniteCubes[i, 0] = cubes;
					infiniteCubes[i, 1] = score;
					infiniteCubes[i, 2] = time;
					cubes = buffer[0];
					score = buffer[1];
					time  = buffer[2];
				}
			}
			// BEST TIMES
			score = pScore;
			cubes = pCubes;
			time = iTime;
			for( int i=0; i<numInfiniteHighTimes; i++ ) {
				if (infiniteTimes[i, 2] < time) {
					buffer[0] = infiniteTimes[i, 0];
					buffer[1] = infiniteTimes[i, 1];
					buffer[2] = infiniteTimes[i, 2];
					infiniteTimes[i, 0] = cubes;
					infiniteTimes[i, 1] = score;
					infiniteTimes[i, 2] = time;
					cubes = buffer[0];
					score = buffer[1];
					time  = buffer[2];
				}
			}
			SaveData();
		}
		
		public static void SaveOptions( Int32[] pOptions ) {
			options = pOptions;
		}
		
		
		public static void SaveData() {
#if DEBUG
			Console.WriteLine("==Save Data==");
#endif
			int numRecords = 0;
			for (int i = 0; i < puzzleSolutionsCount.Length ; i++) {
				numRecords += (2 * puzzleSolutionsCount[i] + 1); // 2 ints per number of found solutions + the counter itself
			}
			
			int bufferSize = sizeof(Int32) * numRecords;
			bufferSize += sizeof(Boolean) * numRecords;	// complete?
			bufferSize += sizeof(Boolean) * numRecords; // locked?
			bufferSize += sizeof(Int32) * 2 * numTimedHighScores * numTimedModes;
			bufferSize += sizeof(Int32) * 2 * numTimedHighCubes * numTimedModes;
			bufferSize += sizeof(Int32) * 3 * numInfiniteHighScores;
			bufferSize += sizeof(Int32) * 3 * numInfiniteHighCubes;
			bufferSize += sizeof(Int32) * 3 * numInfiniteHighTimes;
			bufferSize += sizeof(Int32) * options.Length; // options
			bufferSize += sizeof(Int32) * 1; // hash
			
			byte[] buffer = new byte [bufferSize];
			
			Int32 sum = 0;
			int count = 0;
			int bufferBase = 0;
			
			// PUZZLE MODE DATA
			
			// WHETHER OR NOT ALL PUZZLE SOLUTIONS HAVE BEEN FOUND YET
			for( int i=0; i < numPuzzles; ++i ) {
				Buffer.BlockCopy(puzzleComplete, sizeof(Boolean) * i, buffer, bufferBase + sizeof(Boolean) * count, sizeof(Boolean));
				count++;
				sum += (puzzleComplete[i] ? 1 : 0);
			}
			bufferBase += sizeof(Boolean) * count;
			
			// WHETHER OR NOT THE PUZZLE IS UNLOCKED ON LEVEL-SELECT SCREEN
			count = 0;
			for( int i=0; i < numPuzzles; ++i ) {
				Buffer.BlockCopy(puzzleLocked, sizeof(Boolean) * i, buffer, bufferBase + sizeof(Boolean) * count, sizeof(Boolean));
				count++;
				sum += (puzzleLocked[i] ? 1 : 0);
			}
			bufferBase += sizeof(Boolean) * count;
			
			// SOLUTION COUNTS FOR EACH LEVEL
			count = 0;
			for( int i=0; i< numPuzzles; ++i) {
				Buffer.BlockCopy(puzzleSolutionsCount, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
				count++;
				sum+=puzzleSolutionsCount[i];
			}
			bufferBase += sizeof(Int32) * count;
			
			// ACTUAL SOLUTION DATA
			count = 0;
			for (int key=0; key<puzzleSolutionsFound.Keys.Count; key++) {
//			foreach( int key in puzzleSolutionsFound.Keys) {
				foreach ( var pair in puzzleSolutionsFound[key] ) {
					for (int i=0; i<pair.Length; ++i) {
						Buffer.BlockCopy(pair, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
						count++;
						sum+=pair[i];
					}
				}
			}
			bufferBase += sizeof(Int32) * count;
			
			// TIMED MODE DATA
			count = 0;
			var row = 0;
			for( int mode=0; mode < numTimedModes; mode++ ) {
				for( int i=0; i < 2 * numTimedHighScores; ++i ) {
					Buffer.BlockCopy(timedScores, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
					count++;
					sum += timedScores[mode,row,0];
					i++;
					Buffer.BlockCopy(timedScores, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
					count++;
					sum += timedScores[mode,row,1];
					row++;
				}
				row = 0;
			}
			bufferBase += sizeof(Int32) * count;
			
			count = 0;
			row = 0;
			for( int mode=0; mode < numTimedModes; mode++ ) {
				for( int i=0; i < 2 * numTimedHighCubes; ++i ) {
					Buffer.BlockCopy(timedCubes, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
					count++;
					sum += timedCubes[mode,row,0];
					i++;
					Buffer.BlockCopy(timedCubes, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
					count++;
					sum += timedCubes[mode,row,1];
					row++;
				}
				row = 0;
			}
			bufferBase += sizeof(Int32) * count;
			
			// INFINITE MODE DATA
			count = 0;
			row = 0;
			for( int i=0; i < 3 * numInfiniteHighScores; ++i ) {
				Buffer.BlockCopy(infiniteScores, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
				count++;
				sum += infiniteScores[row,0];
				i++;
				Buffer.BlockCopy(infiniteScores, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
				count++;
				sum+=infiniteScores[row,1];
				i++;
				Buffer.BlockCopy(infiniteScores, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
				count++;
				sum+=infiniteScores[row,2];
				row++;
			}
			bufferBase += sizeof(Int32) * count;
			
			count = 0;
			row = 0;
			for( int i=0; i < 3 * numInfiniteHighCubes; ++i ) {
				Buffer.BlockCopy(infiniteCubes, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
				count++;
				sum += infiniteCubes[row,0];
				i++;
				Buffer.BlockCopy(infiniteCubes, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
				count++;
				sum+=infiniteCubes[row,1];
				i++;
				Buffer.BlockCopy(infiniteCubes, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
				count++;
				sum+=infiniteCubes[row,2];
				row++;
			}
			bufferBase += sizeof(Int32) * count;
			
			count = 0;
			row = 0;
			for( int i=0; i < 3 * numInfiniteHighTimes; ++i ) {
				Buffer.BlockCopy(infiniteTimes, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
				count++;
				sum += infiniteTimes[row,0];
				i++;
				Buffer.BlockCopy(infiniteTimes, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
				count++;
				sum+=infiniteTimes[row,1];
				i++;
				Buffer.BlockCopy(infiniteTimes, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
				count++;
				sum+=infiniteTimes[row,2];
				row++;
			}
			bufferBase += sizeof(Int32) * count;
			
			// OPTIONS
			count = 0;
			for( int i=0; i < options.Length; ++i ) {
				Buffer.BlockCopy(options, sizeof(Int32) * i, buffer, bufferBase + sizeof(Int32) * count, sizeof(Int32));
				count++;
				sum+=options[i];
			}
			bufferBase += sizeof(Int32) * count;
			
			Int32 hash = sum.GetHashCode();
			
#if DEBUG
			Console.WriteLine("sum={0}, hash={1}", sum, hash);
#endif
			
			Buffer.BlockCopy(BitConverter.GetBytes(hash), 0, buffer, bufferBase, sizeof(Int32));
			
			using ( FileStream hStream = File.Open(@SAVE_TEMP, FileMode.Create) ) {
				hStream.SetLength((int)bufferSize);
				hStream.Write(buffer, 0, (int)bufferSize );
				hStream.Close();
			}
			
			if (File.Exists(@SAVE_FILE)) {
				File.Delete(@SAVE_FILE);
			}
			
			File.Move(@SAVE_TEMP, @SAVE_FILE);
#if DEBUG
			Console.WriteLine("==Save Data Complete==");
#endif
		}
		
		
		public static bool LoadData() {
#if DEBUG
			Console.WriteLine("==Load Data==");
#endif
			if ( false == File.Exists(@SAVE_FILE) && true == File.Exists(@SAVE_TEMP) ) {
				File.Move(@SAVE_TEMP, @SAVE_FILE);
			}
			
			if ( true == File.Exists(@SAVE_FILE) ) {
			
				using ( FileStream hStream = File.Open(@SAVE_FILE, FileMode.Open) ) {
					if (hStream != null) {
						long size = hStream.Length;
						byte[] buffer = new byte[size];
						hStream.Read(buffer, 0, (int)size);
						
						Int32 sum=0;
						int count=0;
						int bufferBase = 0;
						// PUZZLE MODE DATA
						
						for( int i=0; i<numPuzzles; ++i ) {
							Buffer.BlockCopy(buffer, bufferBase + sizeof(Boolean) * count, puzzleComplete, sizeof(Boolean) * i, sizeof(Boolean) );
							count++;
							sum += ( puzzleComplete[i] ? 1 : 0);
						}
						bufferBase += sizeof(Boolean) * count;
						
						count = 0;
						for( int i=0; i<numPuzzles; ++i ) {
							Buffer.BlockCopy(buffer, bufferBase + sizeof(Boolean) * count, puzzleLocked, sizeof(Boolean) * i, sizeof(Boolean) );
							count++;
							sum += ( puzzleLocked[i] ? 1 : 0);
						}
						bufferBase += sizeof(Boolean) * count;
						
						// SOLUTION COUNTS FOR EACH LEVEL
						count = 0;
						for( int i=0; i<numPuzzles; ++i ) {
							Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, puzzleSolutionsCount, sizeof(Int32) * i, sizeof(Int32) );
							count++;
							sum += puzzleSolutionsCount[i];
						}
						bufferBase += sizeof(Int32) * count;
						
						// ACTUAL SOLUTION DATA
						count = 0;
						Int32[] solution;
						for (int key=0; key<puzzleSolutionsCount.Length; key++) { // ------ For each level
							if( puzzleSolutionsFound.ContainsKey(key) == false ) {
								puzzleSolutionsFound.Add(key, new List<Int32[]>() );
							}
							puzzleSolutionsFound[key].Clear();
							for (int psc=0; psc < puzzleSolutionsCount[key]; psc++) { // -------- For each solution already found
								solution = new Int32[]{0,0};
								for (int i=0; i < solution.Length; ++i) { // ------------------ Make a cube/score data pair
									Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, solution, sizeof(Int32) * i, sizeof(Int32) );
									count++;
									sum += solution[i];
								}
								puzzleSolutionsFound[key].Add(solution); // ----------------- Add the data pair to the level's list
							}
						}
						bufferBase += sizeof(Int32) * count;

						// TIMED MODE DATA
						count = 0;
						var row = 0;
						for( int mode=0; mode < numTimedModes; mode++ ) {
							for( int i=0; i<2*numTimedHighScores; ++i ) {
								Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, timedScores, sizeof(Int32) * i, sizeof(Int32) );
								count++;
								sum += timedScores[mode,row,0];
								i++;
								Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, timedScores, sizeof(Int32) * i, sizeof(Int32) );
								count++;
								sum += timedScores[mode,row,1];
								row++;
							}
							row = 0;
						}
						bufferBase += sizeof(Int32) * count;
						
						count = 0;
						row = 0;
						for( int mode=0; mode < numTimedModes; mode++ ) {
							for( int i=0; i<2*numTimedHighCubes; ++i ) {
								Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, timedCubes, sizeof(Int32) * i, sizeof(Int32) );
								count++;
								sum += timedCubes[mode,row,0];
								i++;
								Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, timedCubes, sizeof(Int32) * i, sizeof(Int32) );
								count++;
								sum += timedCubes[mode,row,1];
								row++;
							}
							row = 0;
						}
						bufferBase += sizeof(Int32) * count;
						
						// INFINITE MODE DATA
						count = 0;
						row = 0;
						for( int i=0; i<3 * numInfiniteHighScores; ++i ) {
							Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, infiniteScores, sizeof(Int32) * i, sizeof(Int32) );
							count++;
							sum += infiniteScores[row,0];
							i++;
							Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, infiniteScores, sizeof(Int32) * i, sizeof(Int32) );
							count++;
							sum += infiniteScores[row,1];
							i++;
							Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, infiniteScores, sizeof(Int32) * i, sizeof(Int32) );
							count++;
							sum += infiniteScores[row,2];
							row++;
						}
						bufferBase += sizeof(Int32) * count;
						
						count = 0;
						row = 0;
						for( int i=0; i<3 * numInfiniteHighCubes; ++i ) {
							Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, infiniteCubes, sizeof(Int32) * i, sizeof(Int32) );
							count++;
							sum += infiniteCubes[row,0];
							i++;
							Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, infiniteCubes, sizeof(Int32) * i, sizeof(Int32) );
							count++;
							sum += infiniteCubes[row,1];
							i++;
							Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, infiniteCubes, sizeof(Int32) * i, sizeof(Int32) );
							count++;
							sum += infiniteCubes[row,2];
							row++;
						}
						bufferBase += sizeof(Int32) * count;
						
						count = 0;
						row = 0;
						for( int i=0; i<3 * numInfiniteHighTimes; ++i ) {
							Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, infiniteTimes, sizeof(Int32) * i, sizeof(Int32) );
							count++;
							sum += infiniteTimes[row,0];
							i++;
							Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, infiniteTimes, sizeof(Int32) * i, sizeof(Int32) );
							count++;
							sum += infiniteTimes[row,1];
							i++;
							Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, infiniteTimes, sizeof(Int32) * i, sizeof(Int32) );
							count++;
							sum += infiniteTimes[row,2];
							row++;
						}
						bufferBase += sizeof(Int32) * count;
						
						// OPTIONS
						count = 0;
						for( int i=0; i<options.Length; ++i ) {
							Buffer.BlockCopy(buffer, bufferBase + sizeof(Int32) * count, options, sizeof(Int32) * i, sizeof(Int32) );
							count++;
							sum += options[i];
						}
						bufferBase += sizeof(Int32) * count;
						
						Int32 hash = BitConverter.ToInt32( buffer, bufferBase );
						
						hStream.Close();
						
						if(sum.GetHashCode() == hash) {
#if DEBUG
							Console.WriteLine("==Load Data OK==");
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
			Console.WriteLine("==Load Data FILE DOES NOT EXIST==");
#endif
			return false;
		}
		
		
		public static void ClearData() {
#if DEBUG
			Console.WriteLine("==Clear Data==");
#endif
			// PUZZLE MODE DATA
			puzzleSolutionsFound.Clear();
			for ( int i=0; i<numPuzzles; ++i) {
				puzzleComplete[i] = false;
				puzzleLocked[i] = true;
//				puzzleScores[i] = 0;
				puzzleSolutionsFound.Add(i, new List<Int32[]>() );
				puzzleSolutionsCount[i] = 0;
			}
			puzzleLocked[0] = false;
			// TIMED MODE DATA
			for( int mode=0; mode < numTimedModes; mode++) {
				for( int i=0; i<numTimedHighScores; i++ ) {
					timedScores[mode,i,0] = 10 + mode;
					timedScores[mode,i,1] = 11 + mode;
				}
				for( int i=0; i<numTimedHighCubes; i++ ) {
					timedCubes[mode,i,0] = 12;
					timedCubes[mode,i,1] = 13;
				}
			}
			// INFINITE MODE DATA
			for( int i=0; i < numInfiniteHighScores; i++ ) {
				infiniteScores[i,0] = 7;
				infiniteScores[i,1] = 8;
				infiniteScores[i,2] = 9;
			}
			for( int i=0; i < numInfiniteHighCubes; i++ ) {
				infiniteCubes[i,0] = 4;
				infiniteCubes[i,1] = 5;
				infiniteCubes[i,2] = 6;
			}
			for( int i=0; i < numInfiniteHighTimes; i++) {
				infiniteTimes[i,0] = 1;
				infiniteTimes[i,1] = 2;
				infiniteTimes[i,2] = 3;
			}
			// OPTIONS
			options[0] = 40;	// OPTIONS: MUSIC VOL
			options[1] = 70;	// OPTIONS: EFFECTS VOL
			options[2] = 70;	// OPTIONS: ORBIT DISTANCE
			options[3] = 700;	// OPTIONS: STICKYNESS
			options[4] = 60;	// INFINITE: TIMER DURATION
			
			SaveData();
		}
		
		
		public static void Init() {
			ClearData();
		}
		
		
#if METRICS
		
		/// <summary>
		/// Identifies a field or property for Snapshotting, with an explicit spread sheet position
		/// </summary>
		/// <param name='pName'>
		/// A name used to label the spreadsheet column
		/// </param>
		/// <param name='expr'>
		/// The variable. Use: () => [the variable]
		/// </param>
		/// <param name='pPosition'>
		/// The column number. Duplicates will be added IN FRONT.
		/// </param>
		public static void AddMetric<T>( string pName, Expression<Func<T>> expr, int pPosition) {
			AddMetric(pName, expr, MetricSort.POSITION, pPosition);
		}
		
		/// <summary>
		/// Identifies a field or property for Snapshotting
		/// </summary>
		/// <param name='pName'>
		/// A name used to label the spreadsheet column
		/// </param>
		/// <param name='expr'>
		/// The variable. Use: () => [the variable]
		/// </param>
		/// <param name='pSort'>
		/// defaults to MetricSort.LAST
		/// </param>
		public static void AddMetric<T>( string pName, Expression<Func<T>> expr, MetricSort pSort = MetricSort.LAST) {
			int position = ( pSort == MetricSort.FIRST ) ? -100 : 100;
			AddMetric(pName, expr, pSort, position);
		}
		
		/// <summary>
		/// Identifies a field or property for Snapshotting
		/// </summary>
		/// <param name='pName'>
		/// A name used to label the spreadsheet column
		/// </param>
		/// <param name='expr'>
		/// The variable. Use: () => [the variable]
		/// </param>
		/// <param name='pSort'>
		/// defaults to MetricSort.LAST
		/// </param>
		/// <param name='pPosition'>
		/// The column number. Duplicates will be added IN FRONT.
		/// </param>
		private static void AddMetric<T>( string pName, Expression<Func<T>> expr, MetricSort pSort = MetricSort.LAST, int pPosition = -1) {
			
			var body = ((MemberExpression)expr.Body);
			Metric entry = new Metric();
			entry.Name = pName;
			entry.Position = pPosition;
			entry.Value = body;
			if (m.Count == 0) {
				m.AddLast(entry);
				return;
			}
			
//			if (m.Count < 0){
//				m.AddLast(entry);
//				return;
//			}
			if (pPosition < m.First.Value.Position) {
				pSort = MetricSort.FIRST;
			} else if (m.Last.Value.Position > pPosition) {
				pSort = MetricSort.LAST;
			}
			
			switch(pSort) {
			case(MetricSort.FIRST):
				m.AddFirst(entry);
				break;
			case(MetricSort.POSITION):
				foreach( Metric metric in m ) {
					if (metric.Position >= pPosition) {
						m.AddBefore(m.Find(metric), entry);
						break;
					} else if (metric == m.Last.Value) {
						m.AddAfter(m.Find(metric), entry);
						break;
					}
				}
				break;
			case(MetricSort.LAST):
			default:
				m.AddLast(entry);
				break;
			}
			return;
		}
		
		/// <summary>
		/// Removes a variable from the list we want to Snapshot.
		/// </summary>
		/// <param name='pName'>
		/// Spread sheet column name.
		/// </param>
		public static void RemoveMetric( string pName ) {
			foreach( Metric metric in m ) {
				if (metric.Name == pName) {
					m.Remove(metric);
					return;
				}
			}
			Console.WriteLine("===METRICS: {0} was not a registered metric.===", pName);
		}
		
		
		public static void CollectMetrics() {
			string s = "";
			
			foreach(Metric entry in m) {
				// HANDLE FIELDS (MUST BE public)
				if (System.Reflection.MemberTypes.Field == entry.Value.Member.MemberType) {
					s += "" + ((System.Reflection.FieldInfo)entry.Value.Member).GetValue(((ConstantExpression)entry.Value.Expression).Value) + ',';
				}
				// HANDLE PROPERTIES (GET & SETS, AND STATICS -- GET MUST BE public)
				else if (System.Reflection.MemberTypes.Property == entry.Value.Member.MemberType) {
					// STATICS
					if ( true == ((System.Reflection.PropertyInfo)entry.Value.Member).GetGetMethod().IsStatic ) {
						s += "" + ((System.Reflection.PropertyInfo)entry.Value.Member).GetGetMethod().Invoke(null,null) + ',';
					} 
					// GET & SETS
					else {
						s += "" + ((System.Reflection.PropertyInfo)entry.Value.Member).GetValue(((ConstantExpression)entry.Value.Expression).Value,null) + ',';
					}
				}
			}
			s = s.Substring(0,s.Length-1);
			s += '\n';
			
			if (metrics.Count == 0) {
				SaveMetricNames();
			}
			
			metrics.Add(s);
			SaveMetrics();
		}
		
		
		private static void SaveMetricNames() {
			string s = "";
			foreach( Metric entry in m) {
				s+= entry.Name + ',';
			}
			if(s.Length > 0) {
				s = s.Substring( 0,s.Length-1 );
				s+='\n';
			}
			metrics.Add(s);
		}
		
		public static void SaveMetrics() {
			Console.WriteLine("===METRICS: save===");
			string s = "";
//			foreach( Metric entry in m) {
//				s+= entry.Name + ',';
//			}
//			if(s.Length > 0) {
//				s = s.Substring( 0,s.Length-1 );
//				s+='\n';
//			}
			
			foreach( string str in metrics ){
				s += str;
//				s += '\n';
			}
//			if (s.Length>0) {
//				s = s.Substring( 0,s.Length-1 );
//			}
			
			int bufferSize = sizeof(Char) * s.ToCharArray().Length;
			if (bufferSize == 0){
				return;
			}
			
			byte[] buffer = new byte[bufferSize];
			Buffer.BlockCopy(s.ToCharArray(), 0, buffer, 0, bufferSize);
			
			using ( FileStream hStream = File.Open(@METRICS_TEMP, FileMode.Create) ) {
				hStream.SetLength((int)bufferSize);
				hStream.Write(buffer, 0, (int)bufferSize );
				hStream.Close();
			}
			
			if (File.Exists(@METRICS_FILE)) {
				File.Delete(@METRICS_FILE);
			}
			
			File.Move(@METRICS_TEMP, @METRICS_FILE);
			Console.WriteLine("===METRICS: save complete===");
			LoadMetrics();
		}
		
		
		public static bool LoadMetrics() {
			Console.WriteLine("===METRICS: load===");
			if ( false == File.Exists(@METRICS_FILE) && true == File.Exists(@METRICS_TEMP) ) {
				File.Move(@METRICS_TEMP, @METRICS_FILE);
			}
			if ( true == File.Exists(@METRICS_FILE) ) {
				metrics.Clear();
				using ( FileStream hStream = File.Open (@METRICS_FILE, FileMode.Open) ) {
					if (hStream != null) {
						long size = hStream.Length;
						byte[] buffer = new byte[size];
						hStream.Read(buffer,0, (int)size);
						var numChars = size / sizeof(Char);
						Char[] data = new Char[numChars];
						Buffer.BlockCopy(buffer, 0, data, 0, (int)size);
						metrics.Add(new String(data));
						hStream.Close();
					}
//					metrics[0] = metrics[0].Substring(metrics[0].IndexOf('\n')+1);
				}
				Console.WriteLine("===METRICS: load OK===");
				return true;
			}
			Console.WriteLine("===METRICS: load FILE DOES NOT EXIST===");
			return false;
		}
		
		
		public static void ClearMetrics() {
			Console.WriteLine("===METRICS: clear===");
			metrics.Clear();
			SaveMetrics();
		}
		
		
		public static void PrintMetrics() {
//			LoadMetrics();
			Console.WriteLine("==== BEGIN METRICS DUMP ====");
//			string str = "";
//			foreach( Metric entry in m) {
//				str+= entry.Name + ',';
//			}
//			if(str.Length > 0) {
//				str = str.Substring( 0,str.Length-1 );
//			}
//			Console.WriteLine(str);
			foreach (string s in metrics) {
				Console.WriteLine(s);
			}
			Console.WriteLine("==== END METRICS DUMP ====");
		}
#endif
		
	}
	
	public class Metric {
		public string Name;
		public MemberExpression Value;
		public Type Type;
		public int Position;
	}
	
	public enum MetricSort {
		FIRST, LAST, POSITION
	}
	
}

