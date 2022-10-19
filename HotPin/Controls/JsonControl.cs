using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HotPin.Controls
{
    /// <summary>
    /// Json control using ScintillaNET
    /// </summary>
    public partial class JsonControl : UserControl
    {
        public bool NumberMarge { get; set; } = false;
        public bool Bookmark { get; set; } = false;
        public bool BottomContextText { get; set; } = true;
        public bool HScrollBar { get; set; } = true;

        private ScintillaNET.Scintilla scintilla;
        private string lastSearch = "";
        private int lastSearchIndex;
        private int previousCursorPos = 0;
        private bool searchIsOpen = false;

        private const int BACK_COLOR = 0xFFFFFF;
        private const int MARGIN_BACK_COLOR = 0xE5EBF1;
        private const int FORE_COLOR = 0x000000;
        private const int NUMBER_MARGIN = 1;
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;
        private const int FOLDING_MARGIN = 3;
        private const bool CODEFOLDING_CIRCULAR = false;
        private List<Error> errors = new List<Error>();
        private int errorIndicator = 8;

        //private string fileName = null;
        private Error contextError = null;

        public Action ContentChanged;

        public class Error
        {
            public int Position;
            public int LineNumber;
            public int LinePosition;
            public string Message;
        }

        public JsonControl()
        {
            InitializeComponent();
            InitScintilla();
        }

        #region Controls Events

        private void JsonControl_Load(object sender, EventArgs e)
        {
            // TEMP
            //string file = @"e:\dev\JsonControlTest\JsonControl\sample.json";
            //LoadDataFromFile(file);
        }

        private void LinterTimer_Tick(object sender, EventArgs e)
        {
            UpdateErrors();
        }

        private void RequestUpdateErrors()
        {
            if (linterTimer.Enabled)
                linterTimer.Stop();
            linterTimer.Start();
        }

        private void UpdateErrors()
        {
            linterTimer.Stop();

            errors.Clear();
            scintilla.IndicatorClearRange(0, scintilla.Text.Length);

            scintilla.IndicatorCurrent = errorIndicator;

            foreach (Error e in errors)
            {
                int start = e.Position;
                if (e.Position > 0)
                    start -= 1;

                int end = e.Position;
                if (e.Position < scintilla.Lines[e.LineNumber].EndPosition)
                    end += 1;

                scintilla.IndicatorFillRange(start, end - start);
            }

            if (errors.Count != 0)
                contextError = errors[0];
            else
                contextError = null;

            if (contextError != null)
            {
                //contextTextBox.Text = contextError.Message;
                //contextTextBox.BackColor = IntToColor(0xa31515);
                //contextTextBox.ForeColor = Color.White;
            }
            else
            {
                //contextTextBox.Text = fileName != null ? fileName : "";
                //contextTextBox.BackColor = SystemColors.Control;
                //contextTextBox.ForeColor = SystemColors.WindowText;
            }
        }

        private void Scintilla_OnTextChanged(object sender, EventArgs e)
        {
            ContentChanged?.Invoke();
            RequestUpdateErrors();
        }

        private void Scintilla_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                SearchOpen();
            }

            if (e.KeyCode == Keys.F3)
            {
                SearchTxt.Text = scintilla.SelectedText;

                if (!string.IsNullOrEmpty(lastSearch))
                {
                    if (e.Shift)
                        Search(false, false);
                    else
                        Search(true, false);
                }
            }

        }

        private void Scintilla_CursorChange(int currentPos, int previousPos)
        {
            UpdateMatchingBracket(currentPos);
        }

        private void Scintilla_SelectionChanged()
        {
            //scintilla.Styles
            //System.Diagnostics.Debug.WriteLine("{0} {1}", scintilla.SelectionStart, scintilla.SelectionEnd);
        }

        private void Scintilla_ContentChanged()
        {

        }

        private void Scintilla_UpdateUI(object sender, UpdateUIEventArgs e)
        {

            switch (e.Change)
            {
                case UpdateChange.Content:
                    Scintilla_ContentChanged();
                    break;
                case UpdateChange.Selection:
                    Scintilla_SelectionChanged();
                    break;
                case UpdateChange.VScroll:
                    break;
                case UpdateChange.HScroll:
                    break;
                default:
                    break;
            }

            // Has the caret changed position?
            int currentCursorPos = scintilla.CurrentPosition;
            if (previousCursorPos != currentCursorPos)
            {
                Scintilla_CursorChange(currentCursorPos, previousCursorPos);
                previousCursorPos = currentCursorPos;
            }
        }

        private void Scintilla_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = scintilla.Lines[scintilla.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }
        }

        #endregion

        #region Init

        private void InitScintilla()
        {
            scintilla = new ScintillaNET.Scintilla();
            scintillaPanel.Controls.Add(scintilla);
            scintilla.BorderStyle = BorderStyle.None;

            scintilla.Dock = System.Windows.Forms.DockStyle.Fill;
            scintilla.TextChanged += this.Scintilla_OnTextChanged;
            scintilla.KeyDown += this.Scintilla_OnKeyDown;

            scintilla.IndentationGuides = IndentView.LookBoth;
            scintilla.UpdateUI += Scintilla_UpdateUI;

            scintilla.WrapMode = WrapMode.None;
            scintilla.IndentationGuides = IndentView.LookBoth;

            scintilla.BorderStyle = BorderStyle.None;

            InitColors();
            InitSyntaxColoring();
            InitNumberMargin();
            InitBookmarkMargin();
            InitCodeFolding();

            // Define error indicator
            scintilla.Indicators[errorIndicator].Style = IndicatorStyle.Squiggle;
            scintilla.Indicators[errorIndicator].ForeColor = Color.Red;

            // remove conflicting hot keys from scintilla
            scintilla.ClearCmdKey(Keys.Control | Keys.S);
            scintilla.ClearCmdKey(Keys.Control | Keys.F);
            scintilla.ClearCmdKey(Keys.Control | Keys.R);
            scintilla.ClearCmdKey(Keys.Control | Keys.H);
            scintilla.ClearCmdKey(Keys.Control | Keys.L);
            scintilla.ClearCmdKey(Keys.Control | Keys.U);

            scintilla.HScrollBar = HScrollBar;
            if (!BottomContextText)
            {
                scintilla.Dock = DockStyle.Fill;
                //contextTextBox.Visible = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


        }

        private void InitColors()
        {
            scintilla.SetSelectionBackColor(true, IntToColor(0xADD6FF));
        }

        private void InitSyntaxColoring()
        {
            // Configure the default style
            scintilla.StyleResetDefault();
            scintilla.Styles[Style.Default].Font = "Consolas";
            scintilla.Styles[Style.Default].Size = 10;
            scintilla.Styles[Style.Default].BackColor = IntToColor(BACK_COLOR);
            scintilla.Styles[Style.Default].ForeColor = IntToColor(FORE_COLOR);
            scintilla.Styles[Style.BraceLight].BackColor = Color.LightGray;
            scintilla.Styles[Style.BraceLight].ForeColor = Color.Blue;
            scintilla.Styles[Style.BraceLight].Bold = true;

            // Json
            scintilla.Styles[Style.Json.Default].ForeColor = IntToColor(FORE_COLOR);
            scintilla.Styles[Style.Json.BlockComment].ForeColor = IntToColor(0x0000ff);
            scintilla.Styles[Style.Json.LineComment].ForeColor = IntToColor(0x0000ff);
            scintilla.Styles[Style.Json.Number].ForeColor = IntToColor(0x09885a);
            scintilla.Styles[Style.Json.PropertyName].ForeColor = IntToColor(0x007ACC);
            scintilla.Styles[Style.Json.String].ForeColor = IntToColor(0xa31515);
            scintilla.Styles[Style.Json.Uri].ForeColor = IntToColor(0x09885a);

            //scintilla.Styles[Style.Json.StringEol].BackColor = IntToColor(0xa31515);
            scintilla.Styles[Style.Json.Operator].ForeColor = IntToColor(0x000000);
            scintilla.Styles[Style.Json.Keyword].ForeColor = IntToColor(0x0000ff);
            scintilla.Styles[Style.Json.LdKeyword].ForeColor = IntToColor(0x0000ff);
            scintilla.Styles[Style.Json.Error].ForeColor = IntToColor(0x800000);
            scintilla.Lexer = Lexer.Json;

            scintilla.SetKeywords(0, "true false");
            scintilla.SetKeywords(1, "true false");
        }

        private void InitNumberMargin()
        {
            if (NumberMarge)
            {
                scintilla.Styles[Style.LineNumber].BackColor = IntToColor(MARGIN_BACK_COLOR);
                scintilla.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);

                scintilla.Styles[Style.IndentGuide].BackColor = IntToColor(MARGIN_BACK_COLOR);
                scintilla.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);

                var nums = scintilla.Margins[NUMBER_MARGIN];
                nums.Width = 30;
                nums.Type = MarginType.Number;
                nums.Sensitive = true;
                nums.Mask = 0;


            }
            else
            {
                var nums = scintilla.Margins[NUMBER_MARGIN];
                nums.Width = 0;
            }
            scintilla.MarginClick += Scintilla_MarginClick;
        }

        private void InitBookmarkMargin()
        {
            if (Bookmark)
            {
                var margin = scintilla.Margins[BOOKMARK_MARGIN];
                margin.Width = 20;
                margin.Sensitive = true;
                margin.Type = MarginType.Symbol;
                margin.Mask = (1 << BOOKMARK_MARKER);
                //margin.Cursor = MarginCursor.Arrow;

                var marker = scintilla.Markers[BOOKMARK_MARKER];
                marker.Symbol = MarkerSymbol.Arrow;
                marker.SetBackColor(IntToColor(0xFF003B));
                marker.SetForeColor(IntToColor(0x000000));
                marker.SetAlpha(100);
            }
            else
            {
                var margin = scintilla.Margins[BOOKMARK_MARGIN];
                margin.Width = 0;
            }
        }

        private void InitCodeFolding()
        {
            scintilla.SetFoldMarginColor(true, IntToColor(MARGIN_BACK_COLOR));
            scintilla.SetFoldMarginHighlightColor(true, IntToColor(MARGIN_BACK_COLOR));

            // Enable code folding
            scintilla.SetProperty("fold", "1");
            scintilla.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            scintilla.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            scintilla.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            scintilla.Margins[FOLDING_MARGIN].Sensitive = true;
            scintilla.Margins[FOLDING_MARGIN].Width = 16;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                scintilla.Markers[i].SetForeColor(IntToColor(MARGIN_BACK_COLOR)); // styles for [+] and [-]
                scintilla.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            scintilla.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            scintilla.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            scintilla.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            scintilla.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            scintilla.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            scintilla.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            scintilla.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            scintilla.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);
        }

        #endregion

        #region Search

        private void Search(bool next, bool incremental)
        {
            bool first = lastSearch != SearchTxt.Text;

            lastSearch = SearchTxt.Text;
            if (lastSearch.Length > 0)
            {
                if (next)
                {
                    // Search the document at the last search index
                    scintilla.TargetStart = lastSearchIndex - 1;
                    scintilla.TargetEnd = lastSearchIndex + (lastSearch.Length + 1);
                    scintilla.SearchFlags = SearchFlags.None;

                    // Search, and if not found..
                    if (!incremental || scintilla.SearchInTarget(lastSearch) == -1)
                    {
                        // Search the document from the caret onwards
                        scintilla.TargetStart = scintilla.CurrentPosition;
                        scintilla.TargetEnd = scintilla.TextLength;
                        scintilla.SearchFlags = SearchFlags.None;

                        // Search, and if not found..
                        if (scintilla.SearchInTarget(lastSearch) == -1)
                        {

                            // Search again from top
                            scintilla.TargetStart = 0;
                            scintilla.TargetEnd = scintilla.TextLength;

                            // Search, and if not found..
                            if (scintilla.SearchInTarget(lastSearch) == -1)
                            {

                                // clear selection and exit
                                scintilla.ClearSelections();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    string str = scintilla.Text.Substring(0, lastSearchIndex);
                    int previousIndex = str.LastIndexOf(lastSearch, StringComparison.OrdinalIgnoreCase);
                    if (previousIndex != -1)
                    {
                        lastSearchIndex = previousIndex;
                        scintilla.SetSelection(previousIndex, previousIndex + lastSearch.Length);
                        scintilla.ScrollCaret();
                        return;
                    }
                    else
                    {
                        str = scintilla.Text;
                        previousIndex = str.LastIndexOf(lastSearch, StringComparison.OrdinalIgnoreCase);
                        if (previousIndex != -1)
                        {
                            lastSearchIndex = previousIndex;
                            scintilla.SetSelection(previousIndex, previousIndex + lastSearch.Length);
                            scintilla.ScrollCaret();
                            return;
                        }
                    }
                }

                // Select the occurance
                lastSearchIndex = scintilla.TargetStart;
                scintilla.SetSelection(scintilla.TargetEnd, scintilla.TargetStart);
                scintilla.ScrollCaret();
            }

            SearchTxt.Focus();
        }

        private void SearchOpen()
        {
            if (!searchIsOpen)
            {
                searchIsOpen = true;
                InvokeIfNeeded(delegate ()
                {
                    PanelSearch.Visible = true;
                    SearchTxt.Text = lastSearch;
                    SearchTxt.Focus();
                    SearchTxt.SelectAll();
                });
            }
            else
            {
                InvokeIfNeeded(delegate ()
                {
                    SearchTxt.Focus();
                    SearchTxt.SelectAll();
                });
            }
        }

        private void SearchClose()
        {
            if (searchIsOpen)
            {
                searchIsOpen = false;
                InvokeIfNeeded(delegate ()
                {
                    PanelSearch.Visible = false;
                    scintilla.Focus();
                });
            }
        }

        private void SearchButtonClose_Click(object sender, EventArgs e)
        {
            SearchClose();
        }

        private void SearchButtonPrevious_Click(object sender, EventArgs e)
        {
            Search(false, false);
        }

        private void SearchButtonNext_Click(object sender, EventArgs e)
        {
            Search(true, false);
        }

        private void SearchTxt_TextChanged(object sender, EventArgs e)
        {
            Search(true, true);
        }

        private void SearchTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                SearchClose();
            }

            if (e.KeyCode == Keys.Enter)
            {
                Search(true, false);
            }

            if (e.KeyCode == Keys.F3)
            {
                if (e.Shift)
                    Search(false, false);
                else
                    Search(true, false);
            }
        }

        #endregion

        private void UpdateMatchingBracket(int currentCaretPos)
        {
            var bracePos1 = -1;
            var bracePos2 = -1;

            // Is there a brace to the left or right?
            if (currentCaretPos > 0 && IsBrace(scintilla.GetCharAt(currentCaretPos - 1)))
                bracePos1 = (currentCaretPos - 1);
            else if (IsBrace(scintilla.GetCharAt(currentCaretPos)))
                bracePos1 = currentCaretPos;

            if (bracePos1 >= 0)
            {
                // Find the matching brace
                bracePos2 = scintilla.BraceMatch(bracePos1);
                if (bracePos2 == Scintilla.InvalidPosition)
                {
                    scintilla.BraceBadLight(bracePos1);
                    scintilla.HighlightGuide = 0;
                }
                else
                {
                    scintilla.BraceHighlight(bracePos1, bracePos2);
                    scintilla.HighlightGuide = scintilla.GetColumn(bracePos1);
                }
            }
            else
            {
                // Turn off brace matching
                scintilla.BraceHighlight(Scintilla.InvalidPosition, Scintilla.InvalidPosition);
                scintilla.HighlightGuide = 0;
            }
        }

        public void SetText(string text, bool readOnly)
        {
            scintilla.ReadOnly = false;
            scintilla.Text = text;
            scintilla.ReadOnly = readOnly;
        }

        public string GetText()
        {
            return scintilla.Text;
        }

        public void Lowercase()
        {
            // save the selection
            int start = scintilla.SelectionStart;
            int end = scintilla.SelectionEnd;

            // modify the selected text
            scintilla.ReplaceSelection(scintilla.GetTextRange(start, end - start).ToLower());

            // preserve the original selection
            scintilla.SetSelection(start, end);
        }

        public void Uppercase()
        {
            // save the selection
            int start = scintilla.SelectionStart;
            int end = scintilla.SelectionEnd;

            // modify the selected text
            scintilla.ReplaceSelection(scintilla.GetTextRange(start, end - start).ToUpper());

            // preserve the original selection
            scintilla.SetSelection(start, end);
        }

        public void ZoomIn()
        {
            scintilla.ZoomIn();
        }

        public void ZoomOut()
        {
            scintilla.ZoomOut();
        }

        public void ZoomDefault()
        {
            scintilla.Zoom = 0;
        }

        #region Utils

        private static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        private void InvokeIfNeeded(Action action)
        {
            if (InvokeRequired)
            {
                BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        private bool IsBrace(int c)
        {
            switch (c)
            {
                case '[':
                case ']':
                case '{':
                case '}':
                    return true;
            }

            return false;
        }

        #endregion

        private void ContextTextBox_Click(object sender, EventArgs e)
        {
            if (contextError != null)
            {
                var line = scintilla.Lines[contextError.LineNumber];
                scintilla.SetSelection(line.Position, line.EndPosition);
                scintilla.ScrollCaret();
            }
        }
    }
}
