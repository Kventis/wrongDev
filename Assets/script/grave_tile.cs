using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class grave_tile : Tile {

	[SerializeField]
	private Sprite[] graveTiles;

	[SerializeField]
	private Sprite preview;

	private static Dictionary<string, int> graveTilesMap =new Dictionary<string, int>();

	private string coordinate;

	#if UNITY_EDITOR
	[MenuItem("Assets/Create/Tiles/GraveTile")]
	public static void CreateGraveTile(){
		string path = EditorUtility.SaveFilePanelInProject ("Save GraveTile", "New GraveTile", "asset", "Save graveTile", "Assets");
		if (path == "")
			return;

		AssetDatabase.CreateAsset (ScriptableObject.CreateInstance<grave_tile> (), path);

			
	}
	#endif


	public override void  RefreshTile(Vector3Int location, ITilemap tilemap){
		for (int y = 1; y >= -1; y--) {
			for (int x = -1; x <= 1; x++) {
				if (x == 0 && y == 0)
					continue;
				//GetTileData( new Vector3Int (location.x + x, location.y + y, location.z),tilemap);
				tilemap.RefreshTile (new Vector3Int (location.x + x, location.y + y, location.z));

			}

		}
		//GetTileData (location, tileMap, tileData);
	}

	public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData) {
		if (graveTilesMap.Count == 0) {
			tileData.sprite = graveTiles [5];
			fillMap ();
		}
		coordinate = string.Empty;
		/*for (int y = 1; y >= -1; y--) {
			for (int x = -1; x <= 1; x++) {
				if (x == y == 0)
					continue;
				coordinate +=	getLiteral (tileMap, new Vector3Int (location.x + x, location.y + y, location.z));

			}
		
		}*/
		coordinate +=	getLiteral (tilemap, new Vector3Int (location.x - 1, location.y, location.z));
		coordinate +=	getLiteral (tilemap, new Vector3Int (location.x, location.y + 1, location.z));
		coordinate +=	getLiteral (tilemap, new Vector3Int (location.x + 1, location.y, location.z));
		coordinate +=	getLiteral (tilemap, new Vector3Int (location.x, location.y - 1, location.z));
		int tileNum = 4;
		graveTilesMap.TryGetValue (coordinate, out tileNum);
		tileData.sprite = graveTiles [tileNum];
		tileData.colliderType = ColliderType.Sprite;
	
	}

	public string getLiteral(ITilemap tileMap, Vector3Int position){

		if(tileMap.GetTile(position) == this) return "F";
		return "E";
	}


	public void fillMap(){

		graveTilesMap.Add ("EEFF",0);
		graveTilesMap.Add ("FEFF",1);
		graveTilesMap.Add ("FEEF",2);
		graveTilesMap.Add ("EFFF",3);
		graveTilesMap.Add ("FFFF",4);
		graveTilesMap.Add ("FFEF",5);
		//graveTilesMap.Add ("FEFF",6);
		//graveTilesMap.Add ("",7);
		graveTilesMap.Add ("FFFE",8);
		//graveTilesMap.Add ("",9);
		//graveTilesMap.Add ("",10);
		graveTilesMap.Add ("EFFE",11);
		graveTilesMap.Add ("FFEE",12);
		graveTilesMap.Add ("EEFE",13);
		graveTilesMap.Add ("FEFE",14);
		graveTilesMap.Add ("FEEE",15);
		graveTilesMap.Add ("EFEE", 8);

	}

}
