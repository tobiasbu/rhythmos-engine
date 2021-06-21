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
using RhythmosEngine;

namespace RhythmosEditor
{
    internal class RhythmosEditorTimeline
    {

        Rhythm m_rhythm;
        List<AudioReference> _noteList;
        private GUIStyle m_button;

        Vector2 m_mpos = new Vector2(-99, -99);
        Rect selectedRect;
        Note selectedNote;
        bool selectedIsInvalid = false;
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

        public void SetRhythm(Rhythm r)
        {
            m_rhythm = r;
        }

        public void SetNoteList(List<AudioReference> list)
        {
            _noteList = list;
        }
        public void SetLoop(bool flag)
        {
            m_loop = flag;
        }
        public void SetMetronome(bool flag)
        {
            m_playMetronome = flag;
        }

        public void SetMute(bool flag)
        {
            m_mute = flag;
        }

        public void SetMetronomeAudioClip(AudioClip clip)
        {
            m_metro = clip;
        }

        public AudioClip GetMetronomeAudioClip()
        {
            return m_metro;
        }

        public double GetPlayedTime()
        {
            return m_timerPlayAmount;
        }

        public int GetSelectedNoteIndex()
        {
            return m_selected;
        }

        public Note GetSelectedNote()
        {
            return this.m_rhythm.GetNoteAt(m_selected);
        }

        public void SetSelectedNoteIndex(int index)
        {
            m_selected = index;
            if (index > 0 && index < m_rhythm.Count)
            {
                selectedNote = m_rhythm.GetNoteAt(index);
            }
        }

        public bool IsPlaying()
        {
            return m_playing;
        }

        public void Play()
        {
            m_indexNote = -1;
            m_timerPlay = EditorApplication.timeSinceStartup;//+((60d/m_rhythm.GetBPM())*m_rhythm.NoteList()[0].m_duration);
            m_timerPlayAmount = 0;
            m_timerMetroColor = 0;
            m_playing = true;
        }

        public void Stop()
        {
            m_indexNote = -1;
            m_timerPlayAmount = 0;
            m_playing = false;
            AudioUtility.StopAllClips();
        }

        public void Playing()
        {
            if (m_playing)
            {
                double delta = EditorApplication.timeSinceStartup;
                delta = (delta - m_timerLastDelta);
                m_timerLastDelta = EditorApplication.timeSinceStartup;
                if (m_indexNote >= 0)
                    m_timerPlayAmount += delta;

                m_timerMetro += delta;

                if (m_timerNoteC > 0)
                    m_timerNoteC -= (float)delta;

                if (EditorApplication.timeSinceStartup >= m_timerPlay)
                {
                    m_indexNote++;

                    if (m_indexNote >= m_rhythm.Count)
                    {

                        //startMetro = false;
                        m_indexNote = -1;

                        if (!m_loop)
                        {
                            //if (m_metro != null)
                            //	AudioUtility.StopClip(m_metro);
                            m_playing = false;
                            m_timerPlay = 0;
                            m_timerPlayAmount = 0;
                        }
                        else
                        {
                            m_timerPlay = 0;
                            m_timerPlayAmount = 0;
                        }

                    }
                    else
                    {

                        m_timerPlay = EditorApplication.timeSinceStartup + ((60d / m_rhythm.BPM) * m_rhythm.Notes[m_indexNote].duration);
                        if (!m_rhythm.Notes[m_indexNote].isRest)
                        {

                            bool invalidNote = false;

                            if (m_rhythm.Notes[m_indexNote].layoutIndex > _noteList.Count - 1)
                                invalidNote = true;

                            if (_noteList.Count == 0)
                                invalidNote = true;

                            AudioClip clip = null;

                            if (!invalidNote)
                                clip = _noteList[m_rhythm.Notes[m_indexNote].layoutIndex].Clip;

                            if (clip != null)
                            {
                                m_timerNoteC = clip.length;

                                if (!m_mute)
                                    AudioUtility.PlayClip(clip);
                            }
                            else
                            {
                                m_timerNoteC = (60f / m_rhythm.BPM) * m_rhythm.Notes[m_indexNote].duration;
                            }
                        }
                        else
                        {
                            m_timerNoteC = (60f / m_rhythm.BPM) * m_rhythm.Notes[m_indexNote].duration;
                        }
                    }
                }

                if (m_timerMetro >= (double)60d / (double)m_rhythm.BPM)
                {
                    if (m_playMetronome && m_playing)
                    {
                        if (m_metro != null)
                        {
                            AudioUtility.PlayClip(m_metro);
                        }
                        m_timerMetroColor = EditorApplication.timeSinceStartup + 0.2f;

                    }
                    m_timerMetro = 0;

                }
            }

        }

        public void Draw(float width, float height)
        {
            if (m_button == null)
            {
                m_button = new GUIStyle("button");
            }

            GUILayout.Box("", GUILayout.Height(24), GUILayout.Width(width));

            // Time
            float dur = 0;
            if (m_rhythm != null)
            {
                dur = m_rhythm.Duration();
            }

            string strTime = TimeConvertion(dur);
            string strPlayTime = TimeConvertion((float)GetPlayedTime());

            if (dur <= 0)
            {
                strTime = "00:00:00";
            }

            Color oldGuiColor = GUI.color;
            GUI.color = Color.gray;
            GUI.DrawTexture(new Rect(GUILayoutUtility.GetLastRect().x + width - 136 - 8, GUILayoutUtility.GetLastRect().y, 1, 24), Textures.Pixel);
            GUI.DrawTexture(new Rect(GUILayoutUtility.GetLastRect().x + width - 56 - 16, GUILayoutUtility.GetLastRect().y, 1, 24), Textures.Pixel);

            float lastX = GUILayoutUtility.GetLastRect().x;
            float y = GUILayoutUtility.GetLastRect().y + 4;
            if (IsPlaying())
            {
                GUI.color = EditorStyles.label.focused.textColor;
            }
            GUI.Label(new Rect(lastX + width - 112 - 16 - 8, y, 60, 24), strPlayTime, EditorStyles.whiteLabel);

            GUI.color = EditorStyles.label.normal.textColor;
            GUI.Label(new Rect(lastX + width - 56 - 8, y, 60, 24), strTime, EditorStyles.whiteLabel);

            GUI.color = oldGuiColor;

            Texture playButton;
            if (IsPlaying())
            {
                if (EditorGUIUtility.isProSkin)
                {
                    GUI.color = EditorStyles.label.focused.textColor;
                } else
                {
                    GUI.color = Colors.LightSelection;
                }
                playButton = Textures.Stop;
            }
            else
            {
                playButton = Textures.Play;
            }

            if (m_rhythm == null || m_rhythm.Count == 0)
                GUI.enabled = false;

            Color oldContentColor = GUI.contentColor;
            if (!EditorGUIUtility.isProSkin && IsPlaying())
            {
                GUI.contentColor = EditorStyles.label.focused.textColor;
            }
            else
            {
                GUI.contentColor = EditorStyles.label.normal.textColor;
            }
            if (GUI.Button(new Rect(GUILayoutUtility.GetLastRect().x + 2, GUILayoutUtility.GetLastRect().y + 2, 28, 20), playButton))
            {
                if (!IsPlaying())
                {
                    Play();
                }
                else
                {
                    Stop();
                }
            }

            if (!EditorGUIUtility.isProSkin)
            {
                GUI.contentColor = EditorStyles.label.normal.textColor;
            }

            if (IsPlaying())
            {
                GUI.enabled = false;
               
            }

            // To init
            GUI.color = oldGuiColor;
            if (GUI.Button(new Rect(GUILayoutUtility.GetLastRect().x + 2 + 28 + 2, GUILayoutUtility.GetLastRect().y + 2, 24, 20), Textures.ToStart))
            {
                m_selected = 0;
                hSbarValue = 0;
            }

            // To end
            if (GUI.Button(new Rect(GUILayoutUtility.GetLastRect().x + 2 + 28 + 2 + 26, GUILayoutUtility.GetLastRect().y + 2, 24, 20), Textures.ToEnd))
            {
                m_selected = m_rhythm.Count - 1;
                hSbarValue = 1;
            }

            GUI.enabled = true;
            GUI.color = Color.grey;
            GUI.DrawTexture(new Rect(GUILayoutUtility.GetLastRect().x + 58 + 24 + 4, GUILayoutUtility.GetLastRect().y, 1, 24), Textures.Pixel);
            GUI.color = Color.white;

            Texture muteButton = Textures.MuteOff;
            if (m_mute)
                muteButton = Textures.MuteOn;
            muteButton.hideFlags = HideFlags.HideAndDontSave;
            if (GUI.Button(new Rect(GUILayoutUtility.GetLastRect().x + 86 + 3 + 2, GUILayoutUtility.GetLastRect().y + 2, 24, 20), muteButton))
            {
                m_mute = !m_mute;
            }

            oldContentColor = GUI.contentColor;
            Color oldBackgroundColor = GUI.backgroundColor;

            if (m_loop)
            {
                GUI.backgroundColor = EditorGUIUtility.isProSkin ? EditorStyles.label.normal.textColor : Color.gray;
                GUI.contentColor = Color.white;
            }

            if (GUI.Button(new Rect(GUILayoutUtility.GetLastRect().x + 90 + 24 + 3, GUILayoutUtility.GetLastRect().y + 2, 24, 20), Textures.Loop, m_button))
            {
                m_loop = !m_loop;
            }

            GUI.contentColor = oldContentColor;
            GUI.backgroundColor = oldBackgroundColor;

            if (m_playMetronome)
            {
                GUI.backgroundColor = EditorGUIUtility.isProSkin ? EditorStyles.label.normal.textColor : Color.gray;
                GUI.contentColor = Color.white;
            }
            if (GUI.Button(new Rect(GUILayoutUtility.GetLastRect().x + 90 + (24 * 2) + 3 + 2, GUILayoutUtility.GetLastRect().y + 2, 24, 20), Textures.Metronome))
            {
                m_playMetronome = !m_playMetronome;
            }

            GUI.contentColor = oldContentColor;
            GUI.backgroundColor = oldBackgroundColor;

            // Timeline background
            GUI.color = Color.clear;
            GUILayout.Box("", GUILayout.Height(height + 16 + 14), GUILayout.Width(width));
            GUI.color = Color.white;

            GUI.backgroundColor = (EditorGUIUtility.isProSkin) ? EditorStyles.label.hover.textColor : Color.white;
            float rectY = GUILayoutUtility.GetLastRect().y + 1 + 13;
            GUI.Box(new Rect(GUILayoutUtility.GetLastRect().x, rectY - 1 + 2, width, height), "");

            Rect timeLineBox2 = new Rect(0, 10, width, height);
            GUI.BeginGroup(new Rect(GUILayoutUtility.GetLastRect().x + 1, rectY - 12, width - 2, height + 16 + 16));
            GUI.BeginGroup(new Rect(0, 0, width, height + 32));

            float timelineRectSizeUnit = Mathf.Ceil(timeLineBox2.width * m_zoomFactor);
            float totalSize = 0;
            bool noteOpt = false;
            Rect noteRect = new Rect(timeLineBox2.x, timeLineBox2.y + 4, 0, height - 3);
            Rect noteOnPlayRect = noteRect;
            bool invalidNote = false;

            oldContentColor = GUI.contentColor;
            float scale = EditorGUIUtility.isProSkin ? 0.25f : 0.9f;
            Color restColor = new Color(scale, scale, scale, 1);
            Color noteColor;
            string word;

            int count = m_rhythm != null ? m_rhythm.Count : 0;

            // Timeline notes
            for (int i = 0; i < count; i++)
            {
                invalidNote = false;

                if (m_rhythm.Notes[i].layoutIndex > _noteList.Count - 1)
                {
                    invalidNote = true;
                }

                if (_noteList.Count == 0)
                {
                    invalidNote = true;
                }

                float sizew = Mathf.Ceil(m_rhythm.Notes[i].duration * timelineRectSizeUnit);
                noteRect.x = 2 + timeLineBox2.x + totalSize - hSbarValue;
                noteRect.width = sizew;
               

                if (!invalidNote)
                {
                    word = _noteList[m_rhythm.Notes[i].layoutIndex].Clip.name;
                    noteColor = _noteList[m_rhythm.Notes[i].layoutIndex].Color;
                }
                else
                {
                    word = "Invalid Layout";
                    noteColor = restColor;
                }
                noteColor.a = 1f;

                if (m_mouseChanged)
                {
                    if (noteRect.Contains(m_mpos))
                    {
                        m_selected = i;
                        m_mouseChanged = false;
                        noteOpt = true;
                        selectedIsInvalid = invalidNote;
                        selectedRect = noteRect;
                        selectedNote = m_rhythm.Notes[i]; 
                    }
                }

                bool isRest = m_rhythm.Notes[i].isRest;
                if (isRest)
                {
                    GUI.color = restColor;
                }
                else
                {
                    GUI.color = noteColor;    
                }
                GUI.DrawTexture(noteRect, Textures.TimelineBg);


                if (m_indexNote == i && m_timerNoteC > 0)
                {
                    noteOnPlayRect = noteRect;
                    noteOnPlayRect.height -= 3;
                    Color c = Color.black;
                    c.a = 0.4f;
                    GUI.color = c;
                    GUI.DrawTexture(noteOnPlayRect, Textures.Pixel);
                }

                if (m_selected != i)
                {
                    if (!isRest)
                    {
                        if (m_indexNote == i && m_timerNoteC > 0)
                        {
                            GUI.color = Color.white;
                        }
                        else
                        {
                            GUI.color = Color.black;
                        }

                    }
                    else
                    {
                        GUI.color = Color.white;
                    }

                    if (m_rhythm.Notes[i].isRest)
                    {
                        word = "-";
                    }

                    if (word == "Invalid Layout")
                    {
                        GUI.color = Color.red;

                    }

                    
                    GUI.contentColor = GUI.color;

                    GUI.Label(new Rect(noteRect.x + 1, timeLineBox2.y - 8 + height / 2f, sizew - 1, 18), word, EditorStyles.whiteLabel);
                }
                else
                {
                    selectedRect = noteRect;
                }

               

                totalSize += sizew + 1;
            }

            GUI.contentColor = oldContentColor;

            if (m_selected != -1)
            {

                EditorStyles.whiteBoldLabel.normal.textColor = Color.white;
                Color c = Color.white;
                c.a = IsPlaying() ? 0.0f : 0.05f;
                GUI.color = c;

                GUI.DrawTexture(selectedRect, Textures.Pixel);
                GUIDraw.Box(new Rect(selectedRect.x - 1, selectedRect.y, selectedRect.width + 1, selectedRect.height), Textures.Pixel, Color.white, 1);
                float ly = timeLineBox2.y - 8 + height / 2f;

                oldContentColor = GUI.contentColor;

                if (!selectedIsInvalid)
                {
                    if (selectedNote != null)
                    {
                        word = _noteList[selectedNote.layoutIndex].Clip.name;

                        if (!selectedNote.isRest)
                        {
                            bool shouldShow = true;
                            if (IsPlaying())
                            {
                                shouldShow = !(m_indexNote == m_selected);
                            }

                            if (shouldShow)
                            {
                                GUI.color = Color.black;
                                GUI.Label(new Rect(selectedRect.x - 1, ly, selectedRect.width - 1, 18), word, EditorStyles.whiteBoldLabel);
                                GUI.Label(new Rect(selectedRect.x + 1, ly + 1, selectedRect.width - 1, 18), word, EditorStyles.whiteBoldLabel);
                                GUI.Label(new Rect(selectedRect.x + 3, ly, selectedRect.width - 1, 16), word, EditorStyles.whiteBoldLabel);
                                GUI.Label(new Rect(selectedRect.x + 1, ly - 1, selectedRect.width - 1, 18), word, EditorStyles.whiteBoldLabel);
                                GUI.Label(new Rect(selectedRect.x + 2, ly + 1, selectedRect.width - 1, 18), word, EditorStyles.whiteBoldLabel);
                            }
                        }
                        else
                        {
                            word = "-";
                        }

                        GUI.contentColor = Color.white;
                        GUI.color = Color.white;
                        GUI.Label(new Rect(selectedRect.x + 1, ly, selectedRect.width - 1, 18), word, EditorStyles.whiteBoldLabel);
                    }
                }
                else
                {
                    GUI.color = Color.red;
                    GUI.contentColor = Color.red;
                    word = "Invalid Layout";
                    GUI.Label(new Rect(selectedRect.x + 1, ly, selectedRect.width - 1, 18), word, EditorStyles.whiteBoldLabel);
                }
                GUI.contentColor = oldContentColor;
            }

            float x = 2 + timeLineBox2.x + timelineRectSizeUnit - hSbarValue;
            int lmax = 0;
            if (totalSize > timeLineBox2.width)
            {
                lmax = Mathf.CeilToInt(totalSize / timelineRectSizeUnit);
            }
            else
            {
                lmax = Mathf.CeilToInt(timeLineBox2.width / timelineRectSizeUnit);
            }
            GUI.color = Color.grey;
            for (int l = 0; l < lmax; l++)
            {
                GUI.DrawTexture(new Rect(x, timeLineBox2.y - 4, 1, 8), Textures.Pixel);
                x += timelineRectSizeUnit;
            }

            GUI.EndGroup();
            if (noteOpt == false && m_mouseChanged == true)
            {
                m_mpos = new Vector2(-99, -99);
                m_selected = -1;
            }

            if (Event.current != null)
            {
                if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
                {
                    m_mpos = Event.current.mousePosition;
                    m_mouseChanged = true;
                }
            }

            float posx = 0;
            if (m_playing)
            {
                posx = timeLineBox2.x + (((float)m_timerPlayAmount / m_rhythm.Duration()) * totalSize) - hSbarValue;

                if (posx >= width - 1)
                {
                    hSbarValue = posx;
                }
                else
                {
                    hSbarValue = 0;
                }
            }
            else if (m_selected >= 0)
            {
                posx = selectedRect.x - 1.0f;
            }

            // Timeline tracker
            if (m_timerPlayAmount <= m_rhythm.Duration())
            {
                GUI.color = EditorStyles.label.normal.textColor;
                if (m_playMetronome && EditorApplication.timeSinceStartup <= m_timerMetroColor)
                {
                    GUI.color = Color.red;
                }
                GUI.DrawTexture(new Rect(posx - 2.5f, 2, 6, 8), Textures.TrackArrow);
                GUI.color = Color.red;
                GUI.DrawTexture(new Rect(posx, timeLineBox2.y + 4, 1f, height - 2), Textures.TrackArrow);
            }

            GUI.EndGroup();

            float sliderWidth = 48;
            if (width > 480)
            {
                sliderWidth = 48 * width / 480;
            }

            GUI.color = Color.white;
            GUI.Box(new Rect(GUILayoutUtility.GetLastRect().x, rectY + height, width, 17), "");

            float factor = GUI.HorizontalSlider(new Rect(GUILayoutUtility.GetLastRect().x + 1 + width - sliderWidth - 5, rectY + height - 2, sliderWidth, 16), m_zoomFactor, 0.05f, 1f);
            float viewableRatio = ((width - 1) / totalSize);
            if (totalSize >= width - 1)
                GUI.enabled = true;
            else
            {
                GUI.enabled = false;
                hSbarValue = 0;
                viewableRatio = 1;
            }

            if (m_playing)
                GUI.enabled = false;

            hSbarValue = GUI.HorizontalScrollbar(new Rect(GUILayoutUtility.GetLastRect().x + 1, rectY + height + 1, width - sliderWidth - 5 - 2, 30), hSbarValue, viewableRatio * totalSize/*vSbarValue,entryList.Count*viewableRatio*/, 0, totalSize);
            GUI.enabled = true;
            m_zoomFactor = factor;// Mathf.Max((float)factor/100f,0.5f);

        }

        string TimeConvertion(float timer)
        {

            if (timer <= 0f)
                return "00:00:00";

            float minutes = Mathf.Floor(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);
            float mili = timer * 100;
            mili = Mathf.RoundToInt(mili % 99);

            string m, s, mm;

            m = minutes.ToString("00");
            s = seconds.ToString("00");
            mm = mili.ToString();

            if (minutes < 10)
            {
                m = "0" + minutes.ToString();
            }

            if (seconds < 10)
            {
                s = "0" + seconds.ToString();
            }

            if (mili < 10)
            {
                mm = "0" + mili.ToString();
            }

            return m + ":" + s + ":" + mm;

        }
    }
}

