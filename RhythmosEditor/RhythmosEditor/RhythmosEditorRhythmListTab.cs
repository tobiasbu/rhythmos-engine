//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Xml;

namespace RhythmosEditor
{
	internal class RhythmosEditorRhythmListTab	{
	
		[SerializeField]
		private List<Rhythm> _rhythmList;
		
		[SerializeField]
		private List<NoteLayout> _noteList;
		
		[SerializeField]
		private UndoRhythmManager _undoManager;
		
		[SerializeField]
		private RhythmosEditorTimeline _timeline;
		
		[SerializeField]
		Rhythm m_rhythm;
		
		
		private GUIStyle m_label;
		private Texture2D m_pixelSelect;
		private Texture2D g_arrowNext;
		private Texture2D g_arrowBack;
		
		
		private int _selected = -1;
		private int _lastselected = -1;
		float m_totalsize = 0;
		bool _mouseChanged = false;
		bool _mouseChanged2 = false;
		float vSbarValue = 0;
		float vSbarValueNl = 0;
		Vector2 _mpos = new Vector2(-99, -99);
		Vector2 _mpos2 = new Vector2(-99, -99);
		
		
		public RhythmosEditorRhythmListTab() {
			
			if ( _timeline == null)
				_timeline = new RhythmosEditorTimeline();
			
			if (_rhythmList == null)
				_rhythmList = new List<Rhythm>();
				
			if (_undoManager == null)
				_undoManager = new UndoRhythmManager();
			else
				_undoManager.Clear();
			
			Load ();
			
		}
		
		public void Load() {
			
			if (m_pixelSelect == null) {
				
				Color select = Color.white;
				
				m_pixelSelect = TextureUtility.CreateTexture(1,1,select);
				m_pixelSelect.hideFlags = HideFlags.HideAndDontSave;
				//m_label = EditorStyles.label;
				
				
			}
			
			if (m_label == null) {
				m_label = new GUIStyle();
				m_label.normal.background = m_pixelSelect;
				m_label.normal.textColor = Color.white;
				m_label.contentOffset = new Vector2(2,1);
			} 
			
			if (g_arrowNext == null)  {
				g_arrowNext =  TextureUtility.CreateFromBase64("iVBORw0KGgoAAAANSUhEUgAAAAwAAAAKCAYAAACALL/6AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAN1wAADdcBQiibeAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAABqSURBVBiVldGxDcJAFAPQB1UaNkjHBkyQBTIMU7AEM0VkCDJB6vwUuQJIDt1ZcmPZ1pe/iHBEPHDZ6X8CL4xof3RNhiMCb9w+A1HAGX1EOCWhBAvu50LzF6pP6jJFT1wxJfOw1dfOWvu4FV/9ohvJGuUdAAAAAElFTkSuQmCC");
				g_arrowNext.hideFlags = HideFlags.HideAndDontSave;
			}
			
			if (g_arrowBack == null) {
				g_arrowBack =  TextureUtility.CreateFromBase64("iVBORw0KGgoAAAANSUhEUgAAAAwAAAAKCAYAAACALL/6AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAN1wAADdcBQiibeAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAABrSURBVBiVndGxDQIxDIXhL1fRsAC6KbIHW1x9azAQi8AAlGxAi3xNdEIRKAmWXmP/T36yRYRP4YhL3d/nFTzjjlvTgIwnopgOX1XgM14FbsmKdycck9EajZSKQUop44oTHlh+bvjrrL2P2wBx3qpQLdAy9gAAAABJRU5ErkJggg==");
				g_arrowBack.hideFlags = HideFlags.HideAndDontSave;
			}
			
		}
		
		public XmlElement CreateXMLElement(ref XmlDocument xmlDoc) {
			
			XmlElement elmRhythmList = xmlDoc.CreateElement("RhythmList");
			
			foreach (Rhythm rtm in _rhythmList) {
				
				XmlElement elmRhythmEntry = xmlDoc.CreateElement("RhythmEntry");
				XmlElement elmRhythmSequence = xmlDoc.CreateElement("RhythmSequence");
				
				XmlNode node0 = XMLUtility.CreateNodeByName(ref xmlDoc,"name",rtm.Name);
				XmlNode node1 = XMLUtility.CreateNodeByName(ref xmlDoc,"bpm",rtm.BPM.ToString());
				
				elmRhythmEntry.AppendChild(node0);
				elmRhythmEntry.AppendChild(node1);
				
				foreach (Note note in rtm.NoteList()) {
					
					XmlElement elmNote = xmlDoc.CreateElement("Note");
					XmlNode nodeNote1 = XMLUtility.CreateNodeByName(ref xmlDoc,"rest",note.isRest.ToString());
					XmlNode nodeNote2 = XMLUtility.CreateNodeByName(ref xmlDoc,"duration",note.duration.ToString());
					XmlNode nodeNote3 = XMLUtility.CreateNodeByName(ref xmlDoc,"layoutIndex",note.layoutIndex.ToString());
					
					elmNote.AppendChild(nodeNote1);
					elmNote.AppendChild(nodeNote2);
					elmNote.AppendChild(nodeNote3);
					
					elmRhythmSequence.AppendChild(elmNote);
				}
				
				
				elmRhythmEntry.AppendChild(elmRhythmSequence);
				
				elmRhythmList.AppendChild(elmRhythmEntry);
				
			}
			
			return elmRhythmList;
			
		}
		
		public void LoadXMLElement(ref XmlDocument xmlDoc) {
			
			XmlNodeList RhythmEntry = xmlDoc.GetElementsByTagName("RhythmEntry");
			
			_rhythmList.Clear();
			
			foreach (XmlNode node in RhythmEntry) {
				
				Dictionary<string,string> subDictionary = new Dictionary<string,string>();
				XmlNodeList rhythmContent = node.ChildNodes;
				Rhythm rtm = new Rhythm();
				
				foreach (XmlNode rhythmItens in rhythmContent) {
					
					if (rhythmItens.Name == "name" || rhythmItens.Name == "bpm") {
						
						subDictionary.Add(rhythmItens.Name,rhythmItens.InnerText);
						
					} else if (rhythmItens.Name == "RhythmSequence") {
						
						// select note sequence
						XmlNodeList sequence = rhythmItens.SelectNodes("./Note");
						
						//Debug.Log(sequence.Count.ToString());
						
						foreach (XmlNode nodeNote in sequence) {
							
							// select note sequence itens
							XmlNodeList childSequence = nodeNote.ChildNodes;
							Dictionary<string,string> dictionarySequence = new Dictionary<string,string>();
							
							foreach (XmlNode nodeNoteItens in childSequence) {
								
								if (nodeNoteItens.Name == "rest" || nodeNoteItens.Name == "duration" || nodeNoteItens.Name == "layoutIndex") {
									
									dictionarySequence.Add(nodeNoteItens.Name,nodeNoteItens.InnerText);
									
								} 
							}
							
							
							//if (dictionarySequence.Count == 3) {
							
							Note nt = new Note();
							
							foreach (KeyValuePair<string,string> entry in dictionarySequence) {
								
								if (entry.Key == "duration")
									nt.duration = float.Parse(entry.Value);
								else if (entry.Key == "rest")
									nt.isRest =  bool.Parse(entry.Value);
								else if (entry.Key == "layoutIndex") 
									nt.layoutIndex = int.Parse(entry.Value);
							}	
							
							rtm.AppendNote(nt);
							
						}
						
					}
					
				}
				
				
				
				
				foreach (KeyValuePair<string,string> entry in subDictionary) {
					
					if (entry.Key == "name")
						rtm.Name = entry.Value;
					else if (entry.Key == "bpm")
						rtm.BPM = float.Parse(entry.Value);
					
					
				}
				
				//else if (entry.Key == "id")
				//nt.m_id = int.Parse(entry.Value);
				
				
				
				
				AddRhythm(rtm);
				
				
			}
			
		}
		
		//Public functions
		public void AddRhythm(Rhythm rhythm)
		{
			
			_rhythmList.Add(rhythm);
			
		}
		
		public void AddRhythm(string Name)
		{
			
			Rhythm newRhythm = new Rhythm(Name,80);
			newRhythm.BPM = 80;
			
			
			_rhythmList.Add(newRhythm);
			
		}
		public void RemoveRhythm(int index)
		{
			_rhythmList.RemoveAt(index);
		}
		
		public void LoadList(List<Rhythm> ListToLoad)
		{
			_rhythmList = ListToLoad;
		}
		public void Clear()
		{
			_rhythmList.Clear();
		}
		public void RenameRhythm(string name, int index) {
			
			Rhythm back = _rhythmList[index];
			back.Name = name;
			_rhythmList[index].Name = name;
			//_rhythmList.RemoveAt(index);
			//_rhythmList.Insert(index,name);
			
		}
		
		public void ClearRedoUndo() {
			_undoManager.Clear();
		}
		
		public void DeleteAt(int index) {

			if (_rhythmList.Count != 0) {
				
				_rhythmList.RemoveAt(index);
				
				
			} else {
				
				_selected = -1;
				
			}
		}
		
		public void SetNoteList(List<NoteLayout> list) {
			//_timeline.SetNoteList(list);
			_timeline.SetNoteList(list);
			_noteList = list;
		}
		
		public void SetMetroSound(AudioClip sound) {
			_timeline.SetMetronomeAudioClip(sound);
		}
		
		public int GetSelectedItem() {
			
			return _selected;
			
		}
		
		public bool SelectionChanged() {
			
			if (_lastselected != _selected)
				return true;
			else
				return false;
			
		}
		
		public void Update() {
			
			_timeline.Playing();
			
		}
		
		public void DrawHeader(Rect Area, ref Texture2D undo, ref Texture2D redo) {
		
			GUILayout.BeginArea(new Rect(Area.x,Area.y,Area.width,Area.height),"");
			

			GUILayout.BeginHorizontal();

			if (_undoManager.UndoCount == 0)
				GUI.enabled = false;
			else
				GUI.enabled = true;

			if (GUILayout.Button(new GUIContent(undo,"Undo"),GUILayout.Width(24))) {
				PerformUndo();
			}
			
			if (_undoManager.RedoCount == 0)
				GUI.enabled = false;
			else
				GUI.enabled = true;
			
			if (GUILayout.Button(new GUIContent(redo,"Redo"),GUILayout.Width(24))) {
				PerformRedo();
			}
			
			GUI.enabled = true;

			if (GUILayout.Button("Add New Rhythm",GUILayout.Width(120))) {
				
				AddRhythm("New Rhythm " + (_rhythmList.Count+1));

					vSbarValue = _rhythmList.Count;

				_selected = _rhythmList.Count-1;
				m_rhythm = _rhythmList[_selected];
				
				_timeline.SetRhythm(m_rhythm);
				_timeline.SetSelectedNote(-1);
				_undoManager.RecordRhythm(m_rhythm,"Add New Rhythm",_selected,_timeline.GetSelectedNote(),true);
				
			}
			
			if (_selected != -1) {
				GUI.enabled = true;
			} else {
				GUI.enabled = false;
			}
			
			if (GUILayout.Button("Delete Rhythm",GUILayout.Width(120))) {
				
				if (_selected != -1) {
					
					int value = _selected;
					_undoManager.RecordRhythm(m_rhythm,"Remove Rhythm",value,_timeline.GetSelectedNote(),true);
					DeleteAt(value);
					
					
					
					if (_timeline.IsPlaying())
						_timeline.Stop();
					
					if (_selected >= _rhythmList.Count)
						_selected = _rhythmList.Count-1;
					
					if (_rhythmList.Count == 0) {
						_selected = -1;
						m_rhythm = null;
					} else {
						m_rhythm = _rhythmList[_selected];
						_timeline.SetRhythm(m_rhythm);
						_timeline.SetSelectedNote(-1);
					}
					
					
					
				}
				
			}
			
			GUI.enabled = true;
			
			GUILayout.EndHorizontal();
			
			GUILayout.EndArea();
		
		}
		
		void PerformRedo() {

			UndoRhythm redo = _undoManager.Redo();
				
				if (redo.undoAction != "none") {
					
				if (redo.undoAction == "Add New Rhythm") {
						
					_undoManager.RecordRhythm(redo.rhythm,redo.undoAction,redo.index,redo.lastSelectedNote,false);
					
					if (_rhythmList.Count == 0)
						_rhythmList.Add(new Rhythm(redo.rhythm));
					else
						_rhythmList.Insert(redo.index,new Rhythm(redo.rhythm));
						
					m_rhythm = _rhythmList[redo.index];
					_timeline.SetRhythm(m_rhythm);
					_timeline.SetSelectedNote(redo.lastSelectedNote);
					
					_selected = redo.index;
						
						
				} else if (redo.undoAction == "Remove Rhythm") {
						
					if (_rhythmList.Count != 0) {
						_undoManager.RecordRhythm(redo.rhythm,redo.undoAction,redo.index,redo.lastSelectedNote,false);
						_rhythmList.RemoveAt(redo.index);
						_selected = _rhythmList.Count-1;
						m_rhythm = _rhythmList[_rhythmList.Count-1];
						_timeline.SetRhythm(m_rhythm);
						_timeline.SetSelectedNote(-1);
					}
					
						
				} else {
					
					_undoManager.RecordRhythm(redo.rhythm,redo.undoAction,redo.index,redo.lastSelectedNote,false);
					_rhythmList[redo.index] = redo.rhythm;
					m_rhythm = _rhythmList[redo.index];
					_timeline.SetRhythm(m_rhythm);
					_timeline.SetSelectedNote(redo.lastSelectedNote);
					_selected = redo.index;
					
				}
					
					
					
					_undoManager.RemoveLastRedo();
					
				}
				
			
		
		}
		
		void PerformUndo() {
			
			UndoRhythm undo = _undoManager.Undo();
			
			if (undo.undoAction != "none") {
				
				if (undo.undoAction == "Add New Rhythm") {
					
					//_undoManager.RecordRhythmRedo(undo.rhythm,undo.undoAction,undo.index,_timeline.GetSelectedNote());
					
					_undoManager.RecordRhythmRedo(undo.rhythm,undo.undoAction,undo.index,undo.lastSelectedNote);
					
					if (_rhythmList.Count != 0) {
						if (_rhythmList.Count == 1) {
							_rhythmList.Clear();
							m_rhythm = null;
							_selected = -1;
						} else {
							_rhythmList.RemoveAt(undo.index);
							m_rhythm = _rhythmList[_rhythmList.Count-1];
							_selected = _rhythmList.Count-1;
						}
						
					} 

					_timeline.SetRhythm(m_rhythm);
					_timeline.SetSelectedNote(-1);
					
				} else if (undo.undoAction == "Remove Rhythm") {
					
					if (_rhythmList.Count == 0) {
						_rhythmList.Add(new Rhythm(undo.rhythm));
						m_rhythm = _rhythmList[0];
					} else {
						_rhythmList.Insert(undo.index, new Rhythm(undo.rhythm));
						m_rhythm = _rhythmList[undo.index];
					}
						
					
					_undoManager.RecordRhythmRedo(undo.rhythm,undo.undoAction,undo.index,undo.lastSelectedNote);
					_timeline.SetRhythm(m_rhythm);
					_timeline.SetSelectedNote(undo.lastSelectedNote);
					_selected = undo.index;
					
				} else {
					
					
					_rhythmList[undo.index] = undo.rhythm;
					m_rhythm = _rhythmList[undo.index];
					_undoManager.RecordRhythmRedo(undo.rhythm,undo.undoAction,undo.index,undo.lastSelectedNote);
					_timeline.SetRhythm(m_rhythm);
					_timeline.SetSelectedNote(undo.lastSelectedNote);
					_selected = undo.index;
					
				}
				
				
				_undoManager.RemoveLastUndo();
				
			}
			
		}
		
		public void DrawListBox(Rect Area, float ItemHeight, Color BackgroundColor) {
			
			Load();
			
			float _y = 0;
			string _s = "";
			float totalsize = 0;
			Rect listBox = new Rect(0, 0, Area.width, Area.height-16);
			
			GUI.color = BackgroundColor;
			
			GUILayout.BeginArea(Area, "");
			
			GUI.Box(listBox, "");
			
			GUI.BeginGroup(listBox, "");
			
			GUI.color = Color.white;
			
			totalsize = (ItemHeight * _rhythmList.Count);
			_y = -(ItemHeight *vSbarValue);// entryList.Count*((Area.height-16-50)/totalsize);
			
			//iinit = //(int)(Mathf.Lerp(0,entryList.Count,vSbarValue));
			//iinit+(int)(Mathf.Ceil(((Area.height-16-50)/totalsize)*entryList.Count));

			//Loop through to draw the entries and check for selection.
			for(int i = 0;/*(int)Mathf.Ceil(vSbarValue)*/ i < _rhythmList.Count; i++) {
				//Get the list entry's name
				_s = _rhythmList[i].Name;
				//Get the selection's area.
				Rect _entryBox;
				if (totalsize > Area.height) {
					_entryBox = new Rect(1, _y+1, Area.width-15-1, ItemHeight-2);
				} else {
					_entryBox = new Rect(1, _y+1, Area.width-2, ItemHeight-1);
				}

				if(_entryBox.Contains(_mpos) && _mouseChanged)	{
					_lastselected = _selected;
					_selected = i;	
					
					if (_timeline.IsPlaying())
						_timeline.Stop();
					
					m_rhythm = _rhythmList[i];
					_timeline.SetRhythm(m_rhythm);
					_timeline.SetSelectedNote(-1);
					_mouseChanged = false;
				} 
				//Draw a box if it's selected
				if(_selected == i) {

					
					GUI.contentColor = Color.white;
					GUI.backgroundColor = new Color(62f/255f,125f/255f,231f/255f);
					GUI.Label(_entryBox, _s,m_label);
				} else {
					GUI.Label(_entryBox, _s,EditorStyles.label);
					GUI.color = Color.white;
				}
				
				_y += ItemHeight;
			}
			
			GUI.backgroundColor = Color.white;
			GUI.color = Color.white;
			
			if (totalsize >= Area.height-16) {
				float viewableRatio = ((Area.height-16)/totalsize);
				vSbarValue = GUI.VerticalScrollbar(new Rect(Area.width-15, 0, 30, Area.height-16), vSbarValue,_rhythmList.Count*viewableRatio,0,_rhythmList.Count);
			} else {
				vSbarValue = 0;
			}
			
			
			if (Event.current != null) {
				if (Event.current.type == EventType.MouseDown && Event.current.button == 0) {
					
					_mpos = Event.current.mousePosition;
					_mouseChanged = true;
					
				} else if(Event.current.type == EventType.ScrollWheel) {
					
					if (listBox.Contains(Event.current.mousePosition)) {
						if (Event.current.delta.y > 0) {
							
							vSbarValue += 1;
							if (vSbarValue >= _rhythmList.Count)
								vSbarValue = _rhythmList.Count;
							
						} else if (Event.current.delta.y < 0) {
							
							vSbarValue -= 1;
							if (vSbarValue <= 0)
								vSbarValue = 0;
							
						}
					}
					
				}
			}
			
			
			GUI.EndGroup();
			
			GUILayout.EndArea();
			
			GUI.color = Color.white;

		}
		
		public void DrawRhythmSettings(Rect ritArea) {
			
			Load();
			
			float boxy = 0;
			float boxw = 0;
			float totalsizebox = 0;
			float ylast = 0;
			
			if(GetSelectedItem() != -1) {
				
				
				GUILayout.BeginArea(ritArea, "");
				
				
				GUILayout.Label("Rhythm Settings:", EditorStyles.boldLabel);
				
				GUILayout.Label("Rhythm ID: " + GetSelectedItem(), EditorStyles.label);
				
				EditorGUI.BeginChangeCheck();
				//GUILayout.Label("Name", EditorStyles.label,GUILayout.Width(50));
				string str = EditorGUILayout.TextField("Name",m_rhythm.Name,GUILayout.Width(ritArea.width));
				
				if (EditorGUI.EndChangeCheck()) {
					
					_undoManager.RecordRhythm(m_rhythm,"Set Name",_selected,_timeline.GetSelectedNote(),true);
					m_rhythm.Name = str;
					//RenameRhythm(m_rhythm.Name,_selected);
					//Undo.RecordObject(m_rhythm, "Rename Rhythm");
					//EditorUtility.SetDirty(m_rhythm);
					
				}
				
				
				
				GUILayout.Label("Tempo:", EditorStyles.boldLabel);
				

				
				if (_timeline.IsPlaying())
					GUI.enabled = false;
				else 
					GUI.enabled = true;
				
				EditorGUI.BeginChangeCheck();
				float t = EditorGUILayout.FloatField("BPM",m_rhythm.BPM,GUILayout.Width(ritArea.width));
				if (EditorGUI.EndChangeCheck()) {

					if (t < 0.1f)
						t = 0.1f;
					
					_undoManager.RecordRhythm(m_rhythm,"Set BPM",_selected,_timeline.GetSelectedNote(),true);
					
					m_rhythm.BPM = t;
					//RenameEntry(m_rhythm.Name,_selected);
					//Undo.RecordObject(m_rhythm, "Rename Rhythm");
					//EditorUtility.SetDirty(m_rhythm);
					
				}
				
				GUI.enabled = true;
				
				GUILayout.Label("Track Editor:", EditorStyles.boldLabel);
				
				
				_timeline.Draw(ritArea.width,40);
				
				//GUILayout.BeginArea(new Rect(ritArea.x, GUILayoutUtility.GetLastRect().height,ritArea.width,ritArea.height), "");
				//
				
				if (_timeline.IsPlaying())
					GUI.enabled = false;
				else
					GUI.enabled = true;
				
				
				if (_timeline.GetSelectedNote() >= 0 && m_rhythm.NoteCount > 0) {
					
					GUILayout.BeginHorizontal();
					
					if (GUILayout.Button("Add Note",GUILayout.Width(100))) {
						_undoManager.RecordRhythm(m_rhythm,"Add Rhythm Note",_selected,_timeline.GetSelectedNote(),true);
						if (_noteList.Count == 0) {
							m_rhythm.AppendNote(0,0.5f,true);
						} else {
							m_rhythm.AppendNote(0,0.5f,false);
						}
						_timeline.SetSelectedNote(m_rhythm.NoteCount-1);	
					}
					
					if (GUILayout.Button("Insert Note",GUILayout.Width(100))) {
						
						bool pause = false;
						
						if (_noteList.Count == 0)
							pause = true;
						
						Note note = new Note(0.5f,pause,0);
						
						_undoManager.RecordRhythm(m_rhythm,"Insert Note",_selected,_timeline.GetSelectedNote(),true);
						m_rhythm.InsertNoteAt(_timeline.GetSelectedNote(),note);
						_timeline.SetSelectedNote(_timeline.GetSelectedNote());	
					}
					
					if (GUILayout.Button("Duplicate",GUILayout.Width(100))) {
						_undoManager.RecordRhythm(m_rhythm,"Duplicate Note",_selected,_timeline.GetSelectedNote(),true);
						m_rhythm.InsertNoteAt(_timeline.GetSelectedNote(),m_rhythm.NoteList()[_timeline.GetSelectedNote()]);
						_timeline.SetSelectedNote(_timeline.GetSelectedNote()+1);	
					}
					
					if (GUILayout.Button("Remove",GUILayout.Width(100))) {
					
						int selDel = _timeline.GetSelectedNote();
						_undoManager.RecordRhythm(m_rhythm,"Remove Note",_selected,selDel,true);						
						m_rhythm.RemoveNote(selDel);
						
						if (selDel >= m_rhythm.NoteCount-1)
							selDel = m_rhythm.NoteCount-1;
						
						_timeline.SetSelectedNote(selDel);
						
					}
					
					if (_timeline.GetSelectedNote() != 0)
						GUI.enabled = true;
					else
						GUI.enabled = false;
					
					GUIContent content = new GUIContent(g_arrowBack,"Move note to left");
					if (GUILayout.Button(content,GUILayout.Width(24),GUILayout.Height(18))) {
						_undoManager.RecordRhythm(m_rhythm,"Move note Left",_selected,_timeline.GetSelectedNote(),true);	
						int index = _timeline.GetSelectedNote();
						m_rhythm.SwapNote(index,index-1);
						_timeline.SetSelectedNote(index-1);
						
						
					}
					
					if (_timeline.GetSelectedNote() < m_rhythm.NoteCount-1)
						GUI.enabled = true;
					else
						GUI.enabled = false;
					
					GUIContent content2 = new GUIContent(g_arrowNext,"Move note to right");
					if (GUILayout.Button( content2,GUILayout.Width(24),GUILayout.Height(18))) {
						_undoManager.RecordRhythm(m_rhythm,"Move note Right",_selected,_timeline.GetSelectedNote(),true);	
						int index = _timeline.GetSelectedNote();
						m_rhythm.SwapNote(index,index+1);
						_timeline.SetSelectedNote(index+1);					
					}
					
					GUI.enabled = true;
					
					GUILayout.EndHorizontal();
					
				} else {
					
					if (GUILayout.Button("Add Note",GUILayout.Width(100))) {
						_undoManager.RecordRhythm(m_rhythm,"Add Rhythm Note",_selected,_timeline.GetSelectedNote(),true);
						if (_noteList.Count == 0) {
							m_rhythm.AppendNote(0,0.5f,true);
						} else {
							m_rhythm.AppendNote(0,0.5f,false);
						}
						_timeline.SetSelectedNote(m_rhythm.NoteCount-1);	
					}
					
					
				}
				
				GUI.enabled = true;
				
				if (_timeline.GetSelectedNote() >= 0 && m_rhythm.NoteCount > 0) {
					
					float h = ritArea.height-GUILayoutUtility.GetLastRect().y-ritArea.y;
					
					Rect baseRect = new Rect (GUILayoutUtility.GetLastRect().x+5,GUILayoutUtility.GetLastRect().y+24+5,ritArea.width-1,ritArea.height-GUILayoutUtility.GetLastRect().y-ritArea.y-25);
					
					TextureUtility.DrawBox(new Rect(GUILayoutUtility.GetLastRect().x,GUILayoutUtility.GetLastRect().y+24,ritArea.width-1,h),Color.black,ref m_pixelSelect,1);
					
					GUI.BeginGroup(baseRect);
					//GUI.BeginArea(new Rect(0,hy,100,100),"");
					
					
					
					//GUILayout.Label("Selected note at position " + _timeline.GetSelectedNote().ToString() + ".", EditorStyles.label);
					//GUILayout.Space(10);
					
					GUI.Label( new Rect(0,0,100,16),"Note Settings:", EditorStyles.boldLabel);
					GUI.Label( new Rect(baseRect.width-80,0,100,16),"Index: " + _timeline.GetSelectedNote().ToString(), EditorStyles.label);
					
					
					bool checkerNote = true;
					bool oldOption = m_rhythm.NoteList()[_timeline.GetSelectedNote()].isRest;
					
					if (oldOption)
						checkerNote = false;
					
					
					checkerNote = GUI.Toggle( new Rect(0,24,100,16),checkerNote,"Note");
					checkerNote = GUI.Toggle( new Rect(100,24,100,16),!checkerNote,"Rest");
					
					
					if (checkerNote != oldOption) {
						
						bool opt = true;
						
						if (oldOption == false)
							opt = true;
						else if (oldOption == true)
							opt = false;
						
						_undoManager.RecordRhythm(m_rhythm,"Toggle Note-Rest",_selected,_timeline.GetSelectedNote(),true);
						Note nt = m_rhythm.NoteList()[_timeline.GetSelectedNote()];
						nt.isRest = opt;
						m_rhythm.ReplaceNote(_timeline.GetSelectedNote(),nt);
					}
					
					GUI.Label( new Rect(0,(24*2),100,16),"Value:", EditorStyles.boldLabel);
					
					string[] strDur2 = {"8", "4", "2", "1","1/2","1/4","1/8","1/16","1/32", "1/64", "1/128", "1/256"};
					//string[] strDur = {"8", "4", "2", "1"};
					
					
					float dur = m_rhythm.NoteList()[_timeline.GetSelectedNote()].duration;
					int selDur = DurationToIndex(dur);
					int oldSelect = selDur;
					
					
					selDur = GUI.SelectionGrid(new Rect(0,(24*3),342,18*3),selDur,strDur2,6);
					//selDur0 = GUI.Toolbar(new Rect(0,(24*3),300,18),selDur0,strDur);
					//selDur = GUI.Toolbar(new Rect(0,(24*3)+18-1,300,18),selDur,strDur2);
					
					if (oldSelect != selDur) {
						_undoManager.RecordRhythm(m_rhythm,"Set note Duration",_selected,_timeline.GetSelectedNote(),true);
						dur = IndexToDuration(selDur);
						Note nt = m_rhythm.NoteList()[_timeline.GetSelectedNote()];
						nt.duration = dur;
						m_rhythm.ReplaceNote(_timeline.GetSelectedNote(),nt);
						
					}
					
					if (m_rhythm.NoteList()[_timeline.GetSelectedNote()].isRest)
						GUI.enabled = false;
					else
						GUI.enabled = true;
					
					ylast = (24*3+18*3)+5;
					
					
					
					GUI.Label( new Rect(0,ylast,100,16),"Layout:", EditorStyles.boldLabel);
					
					
					boxw = baseRect.width*0.5f;
					ylast += 24;
					
					Rect notelistBox = new Rect(0,ylast,boxw,baseRect.height-ylast-5);
					boxy = -(18*vSbarValueNl);
					totalsizebox = 18*_noteList.Count;
					m_totalsize = totalsizebox;
					
					/*if (totalsizebox >= notelistBox.height)
					vSbarValueNl = 0;*/
					
					
					
					
					GUI.Box( notelistBox,"");
					
					GUI.BeginGroup(new Rect(0,ylast+1,boxw,baseRect.height-ylast-7));
					
					
					int sel = _timeline.GetSelectedNote();
					
					for (int i = 0; i < _noteList.Count; i++) {
						
						Rect labelRect = new Rect(1,boxy,boxw-2,18);
						
						if(labelRect.Contains(_mpos2) && _mouseChanged2)	{
							_undoManager.RecordRhythm(m_rhythm,"Set note layout",_selected,_timeline.GetSelectedNote(),true);
							Note nt = m_rhythm.NoteList()[_timeline.GetSelectedNote()];
							nt.layoutIndex = i;
							m_rhythm.ReplaceNote(_timeline.GetSelectedNote(),nt);
							
							_mouseChanged2 = false;
							
						} 
						
						
						if (i == m_rhythm.NoteList()[sel].layoutIndex) {
							GUI.contentColor = Color.white;
							GUI.backgroundColor = new Color(62f/255f,125f/255f,231f/255f);
							GUI.Label(labelRect,"",m_label);
							GUI.Label( new Rect(5+20,boxy+1,boxw-25-1,16),_noteList[i].Name, EditorStyles.whiteLabel);
							GUI.contentColor = Color.white;
						} else {
							GUI.Label( new Rect(5+20,boxy+1,boxw-25-1,16),_noteList[i].Name, EditorStyles.label);
						}
						
						Color noteColor = _noteList[i].Color;
						noteColor.a = 1f;
						GUI.color = noteColor;
						GUI.Box(new Rect(5+1+1,boxy+1,16,16),"");
						GUI.DrawTexture( new Rect(5+1+2,boxy+2,14,14),m_pixelSelect);
						GUI.color = Color.white;
						
						boxy += 18;
						
					}
					
					GUI.backgroundColor = Color.white;
					
					
					
					if (totalsizebox >= notelistBox.height) {
						float viewableRatio = ((notelistBox.height-2)/totalsizebox);
						vSbarValueNl = GUI.VerticalScrollbar(new Rect(notelistBox.width-15, 0, 30, notelistBox.height+1), vSbarValueNl,((float)_noteList.Count)*viewableRatio,0,(float)_noteList.Count);
					} else {
						//vSbarValueNl = 0;
					}
					
					GUI.enabled = true;
					
					if (Event.current != null) {
						if (Event.current.type == EventType.MouseDown && Event.current.button == 0) {
							
							_mpos2 = Event.current.mousePosition;
							_mouseChanged2 = true;
							
							
						} else if(Event.current.type == EventType.ScrollWheel) {
							
							if (notelistBox.Contains(Event.current.mousePosition) && totalsizebox >= notelistBox.height) {
								if (Event.current.delta.y > 0) {
									
									vSbarValueNl += 1f;
									if (vSbarValueNl >= (float)_noteList.Count-1)
										vSbarValueNl = (float)_noteList.Count-1;
									
								} else if (Event.current.delta.y < 0) {
									
									vSbarValueNl -= 1;
									if (vSbarValueNl <= 0)
										vSbarValueNl = 0;
									
								}
							}
							
						} 
						
						if (Event.current.type == EventType.KeyDown) {
							if ( Event.current.control && Event.current.keyCode == KeyCode.Z) {
						
								PerformUndo();
							}
						}
					}
					
					
					
					GUI.EndGroup();
					GUI.EndGroup();
					
				}

				
				GUILayout.EndArea();

				
			}

			
		}
		

		
		int DurationToIndex(float duration) {
			
			if (duration == 8f)
				return 0;
			else if (duration == 4f)
				return 1;
			else if (duration == 2f)
				return 2;
			else if (duration == 1f)
				return 3;
			else if (duration == 0.5f)
				return 4;
			else if (duration == 0.25f)
				return 5;
			else if (duration == 0.125f)
				return 6;
			else if (duration == 1f/16f) // 0.0625f
				return 7;
			else if (duration == 1f/32f) //0.03125f
				return 8;
			else if (duration == 1f/64f) //0.015625f
				return 9;
			else if (duration == 1f/128f) // 0.0078125f 
				return 10;
			else if (duration == 1f/256f) // 0.00390625
				return 11;
			
			return -1;
			
		}
		
		float IndexToDuration(int index) {
			
			if (index == 0)
				return 8f;
			else if (index == 1)
				return 4f;
			else if (index == 2)
				return 2f;
			else if (index == 3)
				return 1f;
			else if (index == 4)
				return 0.5f;
			else if (index == 5)
				return 0.25f;
			else if (index == 6)
				return (1f/8f);
			else if (index == 7)
				return (1f/16f);
			else if (index == 8)
				return (1f/32f);
			else if (index == 9)
				return (1f/64f);
			else if (index == 10)
				return (1f/128f);
			else if (index == 11)
				return (1f/256f);
			
			return -1;
			
		}
		
		
	}
	
}

