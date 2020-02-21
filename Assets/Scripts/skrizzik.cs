using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkrizzikFloorplan{
	public List<TriangleCell> tunnels = new List<TriangleCell>();
	static int width = 100;
	static int height = 100;
	static int depth = 100;
	public int max = 49;
	TriangleCell[,,] triangleCells = new TriangleCell[width,height,depth];
	
	public TriangleCell AddCellAt(int x, int y, int z){
		TriangleCell cell = null;
		if(Mathf.Abs(x) < width/2 && Mathf.Abs(y) < height/2 && z >= 0 && z<depth){
			cell = new TriangleCell(x,y,z);
			triangleCells[x+(width/2), y+(height/2), z] = cell;
			tunnels.Add(cell);
		}
		return cell;
	}

	public TriangleCell GetCellAt(int x, int y, int z){
		if(Mathf.Abs(x) < width/2 && Mathf.Abs(y) < height/2 && z >= 0 && z<depth){
			return triangleCells[x+(width/2), y+(height/2), z];
		}else{
			return null;
		}
	}
}

public class TriangleCell {
	public bool pointsUp = true;
	public bool isMajorHub = true;
	public int x,y,z;
	public bool[] basicOpenings = new bool[3];
	public AdvancedOpening[] advancedOpenings = new AdvancedOpening[3];

	public TriangleCell(){}

	public TriangleCell(int a, int b, int c){
		x = a;
		y = b;
		z = c;
		for(int i=0;i<3;i++){
			basicOpenings[i] = false;
			advancedOpenings[i] = new AdvancedOpening();
		}
		bool isEvenFloor = (z % 2 == 0);
		bool isEvenRow = (y % 2 == 0);
		bool isEvenColumn = (x % 2 == 0);

		pointsUp = (isEvenColumn == isEvenRow);
		if(isEvenFloor){
			pointsUp = !pointsUp;
		}
		isMajorHub = (pointsUp == isEvenFloor);
	}

	public bool IsEmpty(){
		bool empty = true;
		for(int i=0;i<basicOpenings.Length;i++){
			if (basicOpenings[i]){
				return false;
			}
		}
		if(isMajorHub){
			for(int i=0;i<advancedOpenings.Length;i++){
				if (advancedOpenings[i].type != 0){
					return false;
				}
			}
		}
		return true;
	}
}

public class AdvancedOpening {
	public byte type = 0;
	public AdvancedOpening(){
		type = 0;
	}
}

