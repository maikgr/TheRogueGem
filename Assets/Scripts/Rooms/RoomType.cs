using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomType {
	List<RoomTemplate> templates = new List<RoomTemplate> ();

	public RoomType () {
	}

	public void addTemplate(RoomTemplate template) {
		templates.Add(template);
	}

	public RoomTemplate getTemplate() {
		int ranIdx = Random.Range (0, templates.Count-1);
		return templates [ranIdx];
	}

}
