using System.Collections.Generic;

namespace RhythmosEditor
{
    internal struct UndoNoteLayout
    {
        public int index;
        public string undoAction;
        public NoteLayout note;
    }

    internal class UndoNoteLayoutManager
    {
        int maxActions = 50;
        private static List<UndoNoteLayout> undoList;
        private static List<UndoNoteLayout> redoList;

        public int UndoCount
        {
            get { return undoList.Count; }
        }

        public int RedoCount
        {
            get { return redoList.Count; }
        }

        public UndoNoteLayoutManager()
        {
            if (undoList == null)
                undoList = new List<UndoNoteLayout>();

            if (redoList == null)
                redoList = new List<UndoNoteLayout>();
        }

        public void Clear()
        {
            if (undoList == null)
                undoList = new List<UndoNoteLayout>();
            else
                undoList.Clear();

            if (redoList == null)
                redoList = new List<UndoNoteLayout>();
            else
                redoList.Clear();
        }

        public void RecordNote(NoteLayout note, string action, int index, bool newThing)
        {
            UndoNoteLayout undo = new UndoNoteLayout();
            undo.index = index;
            undo.note = new NoteLayout(note);
            undo.undoAction = action;

            if (newThing)
                redoList.Clear();

            undoList.Add(undo);
            if (undoList.Count > maxActions)
            {
                undoList.RemoveAt(0);
            }
        }

        public void RecordNoteRedo(NoteLayout note, string action, int index)
        {
            UndoNoteLayout redo = new UndoNoteLayout();
            redo.index = index;
            redo.note = new NoteLayout(note);
            redo.undoAction = action;

            redoList.Add(redo);
            if (redoList.Count > maxActions)
            {
                redoList.RemoveAt(0);
            }
        }

        public void RemoveLastUndo()
        {
            if (undoList.Count != 0)
                undoList.RemoveAt(undoList.Count - 1);
        }

        public void RemoveLastRedo()
        {
            if (redoList.Count != 0)
                redoList.RemoveAt(redoList.Count - 1);
        }

        public UndoNoteLayout Undo()
        {
            if (undoList.Count != 0)
            {
                UndoNoteLayout undo = undoList[undoList.Count - 1];
                return undo;
            }
            else
            {
                UndoNoteLayout undo = new UndoNoteLayout();
                undo.index = -1;
                undo.undoAction = "none";
                return undo;
            }
        }

        public UndoNoteLayout Redo()
        {
            if (redoList.Count != 0)
            {
                UndoNoteLayout redo = redoList[redoList.Count - 1];
                return redo;
            }
            else
            {
                UndoNoteLayout redo = new UndoNoteLayout();
                redo.index = -1;
                redo.undoAction = "none";
                return redo;
            }
        }
    }
}
