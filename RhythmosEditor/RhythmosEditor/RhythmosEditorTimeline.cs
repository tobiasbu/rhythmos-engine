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
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace RhythmosEditor
{
	internal class RhythmosEditorTimeline
	{
		
		Rhythm m_rhythm;
		List<NoteLayout> _noteList;
		
		private Texture2D m_timeline;
		private Texture2D m_timelineBG;
		private Texture2D m_timelineSep;
		private Texture2D m_timelinePixel;
		
		private Texture2D g_playbutton;
		private Texture2D g_loopbutton;
		private Texture2D g_stopbutton;
		private Texture2D g_muteOnbutton;
		private Texture2D g_muteOffbutton;
		private Texture2D g_metrobutton;
		private Texture2D g_timeline;
		private Texture2D g_toinit;
		private Texture2D g_toend;
		
		private GUIStyle m_button;
		
		Vector2 m_mpos = new Vector2(-99,-99);
		bool m_mouseChanged = false;
		int m_selected = -1;
		float m_zoomFactor = 0.25f;
		float hSbarValue = 0;
		
		
		int m_indexNote = 0;
		double m_timerPlay = 0;
		double m_timerPlayAmount = 0;
		double m_timerLastDelta = 0;
		
		double m_timerMetro = 0;
		double m_timerMetroColor = 0;
		
		float m_timerNoteC = 0;
		
		bool m_mute = false;
		bool m_playMetronome = false;
		bool m_playing = false;
		bool m_loop = false;
		
		AudioClip m_metro;
		
		
		
		public RhythmosEditorTimeline () {
			
			Load ();
			
		}
		
		
		
		
		public void Load() {
			
			
			if (m_timeline == null) {
				m_timeline = TextureUtility.CreateSpecialDegrade(1,24,Color.white);
				m_timeline.hideFlags = HideFlags.HideAndDontSave;
			}
			
			if (m_timelineBG == null) {
				m_timelineBG = TextureUtility.CreateTextureGradient(1,24,new Color(0.8f,0.8f,0.8f),new Color(0.6f,0.6f,0.6f));

				m_timelineBG.hideFlags = HideFlags.HideAndDontSave;
			}
			
			if (m_timelineSep == null) {
				m_timelineSep = TextureUtility.CreateTextureGradient(1,8,new Color(0.95f,0.95f,0.95f),new Color(0.35f,0.35f,0.35f));
				m_timelineSep.hideFlags = HideFlags.HideAndDontSave;
			}
			
			if (m_timelinePixel == null) {
				m_timelinePixel = TextureUtility.CreateTexture(1,1,new Color(1f,1f,1f));
				m_timelinePixel.hideFlags = HideFlags.HideAndDontSave;
			}
			
			
			//m_som0 = AssetDatabase.LoadMainAssetAtPath("Assets/Resources/Sounds/note0.ogg") as AudioClip;
			//m_som1 = AssetDatabase.LoadMainAssetAtPath("Assets/Resources/Sounds/note1.ogg") as AudioClip;
			//m_metro = AssetDatabase.LoadMainAssetAtPath("Assets/Resources/Sounds/METRO.wav") as AudioClip;
			
			if (g_playbutton == null) {
				g_playbutton = TextureUtility.CreateFromBase64("iVBORw0KGgoAAAANSUhEUgAAAAwAAAAMCAYAAABWdVznAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAN1wAADdcBQiibeAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAABDSURBVCiRrdBBCgAgCETRTyfr6N1s2iQYiAyRMLt5oAIsYErCCYBOLJiBBSvQwg6U0AEXHDzMt5Xso+23tsUMrGJkA3jo8B1kzNzmAAAAAElFTkSuQmCC");
				g_playbutton.hideFlags = HideFlags.HideAndDontSave;
			}
			
			if (g_loopbutton == null) {
				g_loopbutton = TextureUtility.CreateFromBase64("iVBORw0KGgoAAAANSUhEUgAAAAwAAAAKCAYAAACALL/6AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAN1wAADdcBQiibeAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAACjSURBVBiVrdA9agJxEAXw34qw5eYAghcQb5EiYCCYzhuky0EschFTCfa5g/1ewSIka/NS+BdkWdYmDx7DvJnHfFRJ9PCEb3z1CzDFHGvURVviBa844A0NOnxKcswwfpLMkrQ32rHKZacTPsqEBZ6xwQ7veCixUZxtEoWrJI83+ZVtkkwH7toPHXvFZKz4L4Yqya/LS7s7vTW6CbY4F2GMZ2z/AD4KcbR6O5QHAAAAAElFTkSuQmCC");
				g_loopbutton.hideFlags = HideFlags.HideAndDontSave;
			}
			
			if (g_stopbutton == null) {
				g_stopbutton = TextureUtility.CreateFromBase64("iVBORw0KGgoAAAANSUhEUgAAAAwAAAAMCAYAAABWdVznAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAN1wAADdcBQiibeAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAAAYSURBVCiRY2RgYPjPQAJgIkXxqIaRpAEAY7IBF8NLPuEAAAAASUVORK5CYII=");
				g_stopbutton.hideFlags = HideFlags.HideAndDontSave;
			}
			
			if (g_muteOffbutton == null) {
				g_muteOffbutton = TextureUtility.CreateFromBase64("iVBORw0KGgoAAAANSUhEUgAAAAoAAAAMCAYAAABbayygAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAN1wAADdcBQiibeAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAABoSURBVBiVndGxDYJgEAXgT8MMDmBFyR4UtoziWs7iFNJhb54FmKDhR/Ell2u+XPFOEkuDGlcckyihFgOCZhHijMeE3iE63GZX8gkrY044WMl+2rs1NIdf84L5FV7Q416Um+r5q/DSC5+uaYpB1n9dlwAAAABJRU5ErkJggg==");
				g_muteOffbutton.hideFlags = HideFlags.HideAndDontSave;
			}
			
			if (g_muteOnbutton == null) {
				g_muteOnbutton = TextureUtility.CreateFromBase64("iVBORw0KGgoAAAANSUhEUgAAAAoAAAAMCAYAAABbayygAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAN1wAADdcBQiibeAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAACuSURBVBiVddA7aoJREAbQUwUXkMIijUJAsHEBqVIJFjY2GoJkFXE9NskC4iLER+MuRNFoDGnCtRnN5TcWU8zMgRk+KKWUFAs1LFCN3giPBdTCJxIaJ3iLMR5iMMBvoD8Yyzqm6GCdoQs4xAtm6GJ7Db7jB33MA++vwYRvPAfu4VCEb9mprww/oZbDLpZZJLvAM9ydYSHD14hnE3iC8gUM3Ay4Qhsf/8LA9/FnBTdH7QSln/Hr3cMAAAAASUVORK5CYII=");
				g_muteOnbutton.hideFlags = HideFlags.HideAndDontSave;
			}
			
			if (g_metrobutton == null) {
				g_metrobutton = TextureUtility.CreateFromBase64("iVBORw0KGgoAAAANSUhEUgAAAAoAAAAMCAYAAABbayygAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAN1wAADdcBQiibeAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAADUSURBVBiVddExK4VhGMbx3zmdHKtRkTIpEynFa7Iok7LYjOooi89g9SmsZ/IJTimTlOGkTEQUO4vL8j7O2yv/eobnuu/nvq7uRxL1WU0yTnKdZKmhS6Jrwj5GGGNXm8arUZKVJFWSYWtir5ME+njELKZwjwXM4QTzxXoNN/jGJ56xiAOc4qlXN1Z1vsIVtrGJAYYlw2Wdrdz3knwkWS+aJJ0kL0n6jcbzJHft9SzjAV+17THeMI2ZkqWLLbxjB0c4xC1esdHc40X+56xpXf35hQm/tR+YpL4l3BuSTQAAAABJRU5ErkJggg==");
				g_metrobutton.hideFlags = HideFlags.HideAndDontSave;
			}
			
			
			if (g_timeline == null) {
				g_timeline = TextureUtility.CreateFromBase64("iVBORw0KGgoAAAANSUhEUgAAAAYAAAAICAYAAADaxo44AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAN1wAADdcBQiibeAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAABDSURBVAiZbY2xDcAgAMMM6hFcxUPMXNWFka/cpQMS8RgrCery5i1qI1BUk6hAElZgB7FRezjvqKjzCKfK81fHMTMAPpB5ToED6kqxAAAAAElFTkSuQmCC");
				g_timeline.hideFlags = HideFlags.HideAndDontSave;
			}
			
			//if (g_forward == null)
			//g_forward = TextureUtility.CreateFromBase64("iVBORw0KGgoAAAANSUhEUgAAAAwAAAAICAYAAADN5B7xAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAN1wAADdcBQiibeAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAABiSURBVBiVndCxCYRgEEThTxNTS7gWLMLY1MAu7MI6roqLzW3BCoyNXBN/EJEDXXjJg4GZhR+qiBAR0GBAmdwZCGz44oP2cAt6FHeBxIrx4mZ0yO4C/5hQ517cq0qPRj966w7VQm1ZNaZPmAAAAABJRU5ErkJggg==");
			
			//if (g_backward == null)
			//g_backward = TextureUtility.CreateFromBase64("iVBORw0KGgoAAAANSUhEUgAAAAwAAAAICAYAAADN5B7xAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAN1wAADdcBQiibeAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAABgSURBVBiVndChDYVAEADRBwZLCbRAEWgsgi5+F9RBFWg8LVABGsVh+IRcgjg2GTMZsbtCCGJQYkD7cDWmOCzww4aADhVGHLjDDD3WK/wzY4+cBkskX8mlTvJKn45OeesJgkZtWQcaHhAAAAAASUVORK5CYII=");
			
			if (g_toinit == null) {
				g_toinit = TextureUtility.CreateFromBase64("iVBORw0KGgoAAAANSUhEUgAAAAkAAAAICAYAAAArzdW1AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAN1wAADdcBQiibeAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAABRSURBVBiVjdCxCYBAEETRhy3YgC1YnlXYgoGJLViVqYljcqAcCrcwyf4PuwwESxJ1MGL3JWHAiqvwR0KPGWfZ5y1tmHBUMEincZrPtT/+V8ENvX9YG33inTAAAAAASUVORK5CYII=");
				g_toinit.hideFlags = HideFlags.HideAndDontSave;
			}
			
			if (g_toend == null) {
				g_toend = TextureUtility.CreateFromBase64("iVBORw0KGgoAAAANSUhEUgAAAAkAAAAICAYAAAArzdW1AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAN1wAADdcBQiibeAAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAABLSURBVBiVjdChDYBAAEPRF1ZgAVZgPKZgBQSGFZgKi6EYziDursmvatKmcGJO4g82xGcPdky1UOHGirEWKlxYcCCDTnXXNYc3L3gBIXdYG6is+n8AAAAASUVORK5CYII=");
				g_toend.hideFlags = HideFlags.HideAndDontSave;
			}
			
			
			//TextureUtility.SaveBase64TextFile("backward.png");
			//TextureUtility.SaveBase64TextFile("time.png");
			//TextureUtility.SaveBase64TextFile("metroOff.png");
			//TextureUtility.SaveBase64TextFile("muteOn.png");
			
			
		}
		
		public void SetRhythm(Rhythm r) {
			
			m_rhythm = r;
			
		}
		
		public void SetNoteList(List<NoteLayout> list) {
			//
			_noteList = list;
		}
		
		
		public void SetLoop(bool flag) {
			
			m_loop = flag;
			
		}
		
		public void SetMetronome(bool flag) {
			
			m_playMetronome = flag;
			
		}
		
		public void SetMute(bool flag) {
			
			m_mute = flag;
			
		}
		
		public void SetMetronomeAudioClip(AudioClip clip) {
			
			m_metro = clip;
			
		}
		
		public AudioClip GetMetronomeAudioClip() {
			
			return m_metro;
			
		}
		
		public double GetPlayedTime() {
			
			return m_timerPlayAmount;
			
		}
		
		public int GetSelectedNote() {
			
			return m_selected;
			
		}
		
		public void SetSelectedNote(int index) {
			
			m_selected = index;
			
		}
		
		public bool IsPlaying() {
			
			return m_playing;
			
		}
		
		public void Play() {
			
			m_indexNote = -1;
			m_timerPlay = EditorApplication.timeSinceStartup;//+((60d/m_rhythm.GetBPM())*m_rhythm.NoteList()[0].m_duration);
			m_timerPlayAmount = 0;
			m_timerMetroColor = 0;
			m_playing = true;
			
		}
		
		public void Stop() {
			
			m_indexNote = -1;
			m_timerPlayAmount = 0;
			m_playing = false;
			AudioUtility.StopAllClips();
			
			
		}
		
		public void Playing() {
			
			
			
			if (m_playing) {
				
				double delta = EditorApplication.timeSinceStartup;
				
				delta = (delta - m_timerLastDelta);
				m_timerLastDelta = EditorApplication.timeSinceStartup;
				//m_timerPlay = m_timerLastDelta;
				if (m_indexNote >= 0)
					m_timerPlayAmount += delta;
				
				m_timerMetro += delta;
				
				//Debug.Log(delta);
				
				if (m_timerNoteC > 0)
					m_timerNoteC -= (float)delta;
				
				if (EditorApplication.timeSinceStartup >= m_timerPlay) {
					
					m_indexNote++;
					
					if (m_indexNote >= m_rhythm.NoteList().Count) {
						
						//startMetro = false;
						m_indexNote = -1;
						
						if (!m_loop) {
							if (m_metro != null)
								AudioUtility.StopClip(m_metro);
							m_playing = false;
							m_timerPlay = 0; // EditorApplication.timeSinceStartup+((60d/m_rhythm.BPM)*m_rhythm.NoteList()[0].m_duration);
							m_timerPlayAmount = 0;
						} else {
							m_timerPlay =  0;//EditorApplication.timeSinceStartup+((60d/m_rhythm.BPM)*m_rhythm.NoteList()[0].m_duration);
							m_timerPlayAmount = 0;
						}
						
					} else {
						
						m_timerPlay = EditorApplication.timeSinceStartup+((60d/m_rhythm.BPM)*m_rhythm.NoteList()[m_indexNote].duration);
						
						if (!m_rhythm.NoteList()[m_indexNote].isRest) {
							
							bool invalidNote = false;
							
							if (m_rhythm.NoteList()[m_indexNote].layoutIndex > _noteList.Count-1)
								invalidNote = true;
							
							if (_noteList.Count == 0)
								invalidNote = true;
							
							AudioClip clip = null;
							
							if (!invalidNote)
								clip = _noteList[m_rhythm.NoteList()[m_indexNote].layoutIndex].Clip;
							
							if (clip != null) {
								m_timerNoteC = clip.length;
								
								if (!m_mute)
									AudioUtility.PlayClip(clip);
							} else {
								m_timerNoteC = (60f/m_rhythm.BPM)*m_rhythm.NoteList()[m_indexNote].duration;
							}
							
						} else {
							m_timerNoteC = (60f/m_rhythm.BPM)*m_rhythm.NoteList()[m_indexNote].duration;
						}
						
					}
					
					
				}
				
				if (m_timerMetro >= (double)60d/(double)m_rhythm.BPM) {
					
					//EditorApplication.timeSinceStartup + ((double)60d/(double)m_rhythm.BPM);
					
					if (m_playMetronome && m_playing) {
						
						if (m_metro != null)
							AudioUtility.PlayClip(m_metro);
						//m_timerMetroColor = EditorApplication.timeSinceStartup +  m_metro.length;
						//} else
						//m_timerMetroColor = EditorApplication.timeSinceStartup + 0.2f;
						
						m_timerMetroColor = EditorApplication.timeSinceStartup + 0.2f;
						
					}
					m_timerMetro = 0;
					
				}
			}
			
		}
		// Update is called once per frame
		public void Draw (float width, float height) {
			
			Load();
			
			if (m_button == null)
				m_button = new GUIStyle("button");
			//Rect timeLineBox2 = new Rect(size.x+1,size.y+1,size.width-2,size.height-2);
			
			
			
			//GUILayout.BeginArea( new Rect(size.x,size.y,size.width,size.height));
			GUILayout.Box("",GUILayout.Height(24),GUILayout.Width(width));
			
			//GUILayout.BeginHorizontal();
			
			float dur = 0;
			
			if (m_rhythm != null)
				dur = m_rhythm.Duration();
			
			string strTime = TimeConvertion(dur);
			string strPlayTime = TimeConvertion((float)GetPlayedTime());
			
			if (dur <= 0)
				strTime = "00:00:00";
			
			GUI.color = Color.gray;
			GUI.DrawTexture(new Rect(GUILayoutUtility.GetLastRect().x+width-136-8,GUILayoutUtility.GetLastRect().y,1,24),m_timelineSep);
			GUI.Label(new Rect(GUILayoutUtility.GetLastRect().x+width-112-16-8,GUILayoutUtility.GetLastRect().y+4,55,24),strPlayTime);
			GUI.color = Color.gray;
			GUI.DrawTexture(new Rect(GUILayoutUtility.GetLastRect().x+width-56-16,GUILayoutUtility.GetLastRect().y,1,24),m_timelineSep);
			GUI.Label(new Rect(GUILayoutUtility.GetLastRect().x+width-56-8,GUILayoutUtility.GetLastRect().y+4,55,24),strTime);
			
			GUI.color = Color.white;
			
			Texture playButton = g_playbutton;
			
			if (IsPlaying())
				playButton = g_stopbutton;
			
			if (m_rhythm.NoteCount == 0)
				GUI.enabled = false;
			
			if (GUI.Button(new Rect(GUILayoutUtility.GetLastRect().x+2,GUILayoutUtility.GetLastRect().y+2,28,20),playButton)) {
				if (!IsPlaying()) {
					Play();
				} else {
					Stop();
				}
			}
			
			
			
			if (GUI.Button(new Rect(GUILayoutUtility.GetLastRect().x+2+28+2,GUILayoutUtility.GetLastRect().y+2,24,20),g_toinit)) {
				m_selected = 0;
				hSbarValue = 0;
			}
			
			if (GUI.Button(new Rect(GUILayoutUtility.GetLastRect().x+2+28+2+26,GUILayoutUtility.GetLastRect().y+2,24,20),g_toend)) {
				m_selected = m_rhythm.NoteCount-1;
				
				//hSbarValue = lastsize;
				
				
			}
			
			/*if (GUI.Button(new Rect(GUILayoutUtility.GetLastRect().x+2+(28*2)+26,GUILayoutUtility.GetLastRect().y+2,24,20),g_forward)) {
			//m_mute = !m_mute;
		}*/
			
			GUI.enabled = true;
			
			Texture muteButton = g_muteOffbutton;
			
			
			GUI.color = Color.gray;
			GUI.DrawTexture(new Rect(GUILayoutUtility.GetLastRect().x+58+24+4,GUILayoutUtility.GetLastRect().y,1,24),m_timelineSep);
			GUI.color = Color.white;
			
			if (m_mute)
				muteButton = g_muteOnbutton;
			
			muteButton.hideFlags = HideFlags.HideAndDontSave;
			
			if (GUI.Button(new Rect(GUILayoutUtility.GetLastRect().x+86+3+2,GUILayoutUtility.GetLastRect().y+2,24,20),muteButton)) {
				m_mute = !m_mute;
			}
			
			
			if (!m_loop) {
				GUI.skin.button.normal.background = m_button.normal.background;
				GUI.contentColor = Color.black;
			} else {
				GUI.skin.button.normal.background = m_button.active.background;
				GUI.contentColor = Color.white;
			}
			
			if (GUI.Button(new Rect(GUILayoutUtility.GetLastRect().x+90+24+3,GUILayoutUtility.GetLastRect().y+2,24,20),g_loopbutton)) {
				m_loop = !m_loop;
			}
			
			GUI.skin.button.normal.background = m_button.normal.background;
			
			if (!m_playMetronome) {
				GUI.skin.button.normal.background = m_button.normal.background;
				GUI.contentColor = Color.black;
			} else {
				GUI.skin.button.normal.background = m_button.active.background;
				GUI.contentColor = Color.white;
			}
			
			if (GUI.Button(new Rect(GUILayoutUtility.GetLastRect().x+90+(24*2)+3+2,GUILayoutUtility.GetLastRect().y+2,24,20),g_metrobutton)) {
				m_playMetronome = !m_playMetronome;
			}
			
			GUI.skin.button.normal.background = m_button.normal.background;
			GUI.contentColor = Color.white;
			
			
			GUI.color = new Color(1f,1f,1f,0f);
			GUILayout.Box("",GUILayout.Height(height+16+12),GUILayout.Width(width));
			GUI.color = Color.white;
			
			float rectY = GUILayoutUtility.GetLastRect().y+1+12;
			
			GUI.Box(new Rect(GUILayoutUtility.GetLastRect().x,rectY-1+2,width,height),"");
			
			GUI.DrawTexture(new Rect(GUILayoutUtility.GetLastRect().x+1,rectY+2,width-2,height-2), m_timelineBG);
			
			Rect timeLineBox2 = new Rect(0,10,width,height);
			GUI.BeginGroup(new Rect(GUILayoutUtility.GetLastRect().x+1,rectY-12,width-2,height+16+12), "");
			
			GUI.BeginGroup(new Rect(0,0,width,height+10), "");
			
			int lmax = (int)(timeLineBox2.width/(timeLineBox2.width*m_zoomFactor));
			
			float x = 0;
			
			for (int l = 0; l < lmax; l++) {
				
				x += timeLineBox2.width*m_zoomFactor;	
				
				if (x < timeLineBox2.width) {
					
					Rect sep = new Rect(timeLineBox2.x+x,timeLineBox2.y+4,1,height-2);
					GUI.DrawTexture(sep,m_timelineSep);
				}
				
			}
			
			float lastsize = 0;
			float xx = hSbarValue;
			bool noteOpt = false;
			
			
			for (int i = 0; i < m_rhythm.NoteList().Count; i++) {
				
				bool invalidNote = false;

				if (m_rhythm.NoteList()[i].layoutIndex > _noteList.Count-1)
					invalidNote = true;
				
				if (_noteList.Count == 0)
					invalidNote = true;
				
				Texture img = m_timeline;
				img.hideFlags = HideFlags.HideAndDontSave;
				float sizew = m_rhythm.NoteList()[i].duration*(timeLineBox2.width*m_zoomFactor);
				Rect noteRect = new Rect(timeLineBox2.x+lastsize-xx,timeLineBox2.y+4,sizew,height-3);
				string word;
				Color noteColor;
				
				if (!invalidNote) {
					word = _noteList[m_rhythm.NoteList()[i].layoutIndex].Name;
					noteColor = _noteList[m_rhythm.NoteList()[i].layoutIndex].Color;
				} else {
					word = "Invalid Layout";
					noteColor = new Color(0.9f,0.8f,0.75f);
				}
				noteColor.a = 1f;
				
				
				if (m_indexNote == i && m_timerNoteC > 0) {
					noteColor = Color.grey;
				}
				
				if (m_rhythm.NoteList()[i].isRest) {
					
					if (m_indexNote == i && m_timerNoteC > 0)
						noteColor = new Color(0.5f,0.5f,0.5f,0.25f);
					else
						noteColor = new Color(1f,1f,1f,0.25f);
					
				}
				
				if (m_mouseChanged) {
					if (noteRect.Contains(m_mpos)) {
						//m_timelineSel = TextureUtility.CreateOutlineBox((int)noteRect.width+2,(int)noteRect.height+2,4,new Color(0,0,0,0),new Color(62f/255f,125f/255f,231f/255f));
						m_selected = i;
						m_mouseChanged = false;
						noteOpt = true;
					}
				}
				
				GUI.color = noteColor;
				
				GUI.DrawTexture(noteRect,img);
				
				lastsize += sizew;
				
				GUI.color = Color.black;
				GUI.DrawTexture(new Rect(timeLineBox2.x+lastsize-1-xx,timeLineBox2.y+4,1,height-3),m_timelineSep);
				
				if (m_selected == i) {
					
					GUI.color = noteColor;
					
					GUI.Box(new Rect(noteRect.x-1,noteRect.y-1,noteRect.width+1,noteRect.height+2),"");
					GUI.color = Color.white;
					
					TextureUtility.DrawBox(new Rect(noteRect.x-1,noteRect.y,noteRect.width+1,noteRect.height-1),Color.black,ref m_timelinePixel,1);
					TextureUtility.DrawBox(new Rect(noteRect.x,noteRect.y,noteRect.width-1,noteRect.height-2),Color.white/*new Color(62f/255f,125f/255f,231f/255f)*/,ref m_timelinePixel,1);
					TextureUtility.DrawBox(new Rect(noteRect.x+1,noteRect.y+1,noteRect.width-3,noteRect.height-4),Color.black,ref m_timelinePixel,1);
					//GUI.DrawTexture(new Rect(noteRect.x-1,noteRect.y-1,noteRect.width+1,noteRect.height+1),m_timelineSel);
				}
				
				
				lastsize -= sizew;
				
				if (m_indexNote == i && m_timerNoteC > 0)
					GUI.color = Color.white;
				else
					GUI.color = Color.black;
				
				GUIStyle wordStyle = EditorStyles.whiteBoldLabel;
				
				if (m_rhythm.NoteList()[i].isRest) {
					word = "Rest";
					wordStyle = EditorStyles.whiteLabel;
				}
				
				if (word == "Invalid Layout") {
					wordStyle = EditorStyles.whiteLabel;
					GUI.color = Color.red;
				}
				
				GUI.Label(new Rect(timeLineBox2.x+lastsize-xx+1,timeLineBox2.y-8+height/2f,sizew-1,16),word,wordStyle);
				
				lastsize += sizew;
				
			}
			
			if (noteOpt == false && m_mouseChanged == true) {
				m_mpos = new Vector2(-99,-99);
				m_selected = -1;
			}
			
			
			
			if (Event.current != null) {
				if (Event.current.type == EventType.MouseDown && Event.current.button == 0) {
					
					m_mpos = Event.current.mousePosition;
					m_mouseChanged = true;
					
				} 
			}
			
			GUI.EndGroup();
			
			float posx = 0;
			
			if (m_playing) {
				posx = timeLineBox2.x+(((float)m_timerPlayAmount/m_rhythm.Duration())*lastsize)-hSbarValue;
				
				if (posx >= width-1) {
					hSbarValue = posx;
				} else {
					hSbarValue = 0;
				}
			}
			
			if (m_timerPlayAmount <= m_rhythm.Duration()) {
				
				GUI.color = new Color(0.2f,0.2f,0.2f);
				
				if (m_playMetronome && EditorApplication.timeSinceStartup <= m_timerMetroColor)
					GUI.color = Color.red; 
				
				GUI.DrawTexture(new Rect(posx-2.5f,2,6,8),g_timeline);
				
				GUI.color = Color.red; 				
				
				GUI.DrawTexture(new Rect(posx,timeLineBox2.y+4,1f,height-2),m_timelineSep);
			}
			
			
			
			GUI.EndGroup();
			
			GUI.color = Color.white;
			
			
			GUI.Box(new Rect(GUILayoutUtility.GetLastRect().x,rectY+height-2,width,17),"");
			float factor =  GUI.HorizontalSlider(new Rect(GUILayoutUtility.GetLastRect().x+1+width-48-5,rectY+height-2,48,16),m_zoomFactor,0.05f,1f);
			
			float viewableRatio = ((width-1)/lastsize);
			
			//
			if (lastsize >= width-1) 
				GUI.enabled = true;
			else {
				GUI.enabled = false;
				hSbarValue = 0;
				viewableRatio = 1;
			}
			
			if (m_playing)
				GUI.enabled = false;
			
			hSbarValue = GUI.HorizontalScrollbar(new Rect(GUILayoutUtility.GetLastRect().x+1, rectY+height-1, width-48-5-2, 30),hSbarValue,viewableRatio*lastsize/*vSbarValue,entryList.Count*viewableRatio*/,0,lastsize);
			GUI.enabled = true;
			
			m_zoomFactor = factor;// Mathf.Max((float)factor/100f,0.5f);
			
		}
		
		string TimeConvertion(float timer) {
			
			if (timer <= 0f)
				return "00:00:00";
			
			float minutes = Mathf.Floor(timer / 60);
			float seconds = Mathf.FloorToInt(timer % 60);
			float mili = timer*100;
			mili = Mathf.RoundToInt(mili % 99);
			
			string m,s,mm;
			
			m = minutes.ToString("00");
			s = seconds.ToString("00");
			mm = mili.ToString();
			
			if (minutes < 10) { 
				m = "0" + minutes.ToString();
			}
			
			if(seconds < 10) { 
				s = "0" + seconds.ToString();
			} 
			
			if(mili < 10) { 
				mm = "0" + mili.ToString();
			} 
			
			return m + ":" + s + ":" + mm;
			
		}
	}
}
