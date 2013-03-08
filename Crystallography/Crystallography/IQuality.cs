using System;
using System.Linq; 
using System.Xml; 
using System.Collections.Generic; 
using System.IO;

namespace Crystallography
{
	public interface IQuality {
		
		void Apply( ICrystallonEntity pEntity, int pVariant );
		
		int Match( ICrystallonEntity[] pEntities, bool pForScore );
	}
}