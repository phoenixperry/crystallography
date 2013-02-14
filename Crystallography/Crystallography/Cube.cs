using System;

namespace Crystallography
{
	public class Cube
	{
			private string _card1; 
			private string _card2; 
			private string _card3; 
			private int size; 
		
		public Cube ()
		{
			
		}
		public void SetSize(){
			size++; 
		}
		public  void card1(string s){
				_card1 =s; 
			}
			public  void card2(string s){
				_card2 =s;
			}
			public  void card3 (string s){
				_card3 =s; 
			}
				public bool testCube(){
				if((_card1 != _card2 && _card1 != _card3 && _card2 != _card3) || 
			   (_card1 == _card2 && _card1== _card3)){
					Console.WriteLine("it's a cube"); 
					return true;
		
				}
				else{
					Console.WriteLine("not a cube"); 
					return false;} 
					
			}
	
	}
}

