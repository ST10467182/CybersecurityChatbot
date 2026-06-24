// =============================================================
// MainForm.cs
// Cybersecurity Awareness Chatbot - Part 3/POE Main Logic
// =============================================================
// Extends Part 2 GUI with:
// - Task Assistant (MySQL-backed task management)
// - Cybersecurity Mini Quiz (12 questions, MC + T/F)
// - NLP Simulation (intent detection, flexible phrasing)
// - Activity Log (timestamped action history)
// All Part 1 and Part 2 features are preserved.
//
// References:
// [1] Microsoft, 2024. Windows Forms Overview [Online].
//     Available at: https://learn.microsoft.com/en-us/dotnet/
//     desktop/winforms/overview [Accessed 20 May 2026].
//
// [3] Pieterse, H. 2021. The Cyber Threat Landscape in South
//     Africa: A 10-Year Review. The African Journal of
//     Information and Communication, 28(28).
//     doi: https://doi.org/10.23962/10539/32213
//     [Accessed 16 February 2026].
// =============================================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CybersecurityChatbotGUI
{
    public partial class MainForm : Form
    {
        // ===== PART 2 COMPONENTS (preserved) =====
        private readonly ResponseEngine _responseEngine;
        private readonly SentimentDetector _sentimentDetector;
        private readonly MemoryManager _memoryManager;
        private readonly AudioPlayer _audioPlayer;
        private readonly ConversationManager _conversationManager;

        // ===== PART 3 COMPONENTS (new) =====
        private readonly TaskManager _taskManager;
        private readonly QuizManager _quizManager;
        private readonly ActivityLogger _activityLogger;
        private readonly NLPProcessor _nlpProcessor;

        // Session state
        private bool _awaitingName = true;
        private bool _awaitingTaskReminder = false;
        private string _pendingTaskTitle = "";
        private string _pendingTaskDesc = "";
        private bool _dbConnected = false;

        // Colours
        private readonly Color _botColor = Color.FromArgb(0, 220, 160);
        private readonly Color _userColor = Color.FromArgb(255, 215, 50);
        private readonly Color _systemColor = Color.FromArgb(130, 130, 190);
        private readonly Color _sentimentColor = Color.FromArgb(255, 145, 70);
        private readonly Color _warningColor = Color.FromArgb(255, 75, 75);
        private readonly Color _successColor = Color.FromArgb(100, 220, 100);

        // Fonts
        private readonly Font _boldFont = new Font("Consolas", 10f, FontStyle.Bold);
        private readonly Font _normalFont = new Font("Consolas", 10f);

        public MainForm()
        {
            InitializeComponent();

            // Initialise Part 2 engines
            _responseEngine = new ResponseEngine();
            _sentimentDetector = new SentimentDetector();
            _memoryManager = new MemoryManager();
            _audioPlayer = new AudioPlayer();
            _conversationManager = new ConversationManager();

            // Initialise Part 3 engines
            _quizManager = new QuizManager();
            _activityLogger = new ActivityLogger();
            _nlpProcessor = new NLPProcessor();

            // Initialise TaskManager safely
            try
            {
                _taskManager = new TaskManager();
                _dbConnected = true;
            }
            catch (Exception ex)
            {
                _dbConnected = false;
                _taskManager = null!;
                lblDbStatus.Text = $"  ⚠️ Database offline: {ex.Message.Substring(0, Math.Min(60, ex.Message.Length))}";
                lblDbStatus.ForeColor = Color.FromArgb(255, 80, 80);
            }

            // Wire up events
            btnSend.Click += BtnSend_Click;
            txtInput.KeyDown += TxtInput_KeyDown;
            btnNavChat.Click += (s, e) => ShowPanel("chat");
            btnNavTasks.Click += (s, e) => ShowPanel("tasks");
            btnNavQuiz.Click += (s, e) => ShowPanel("quiz");
            btnNavLog.Click += (s, e) => ShowPanel("log");
            btnAddTask.Click += BtnAddTask_Click;
            btnCompleteTask.Click += BtnCompleteTask_Click;
            btnDeleteTask.Click += BtnDeleteTask_Click;
            btnStartQuiz.Click += BtnStartQuiz_Click;
            btnOpt0.Click += QuizOption_Click;
            btnOpt1.Click += QuizOption_Click;
            btnOpt2.Click += QuizOption_Click;
            btnOpt3.Click += QuizOption_Click;
            btnRefreshLog.Click += (s, e) => RefreshLog();
            this.Shown += MainForm_Shown;
        }

        // =====================================================
        // STARTUP
        // =====================================================

        private async void MainForm_Shown(object? sender, EventArgs e)
        {
            DisplayAsciiArt();
            _activityLogger.Log("Application started");

            AppendColoredText("\n  System >> ", _systemColor, _boldFont);
            AppendColoredText("Playing voice greeting...\n", _systemColor, _normalFont);
            await Task.Run(() => _audioPlayer.PlayGreeting());
            _activityLogger.Log("Voice greeting played");

            if (_dbConnected)
            {
                lblDbStatus.Text = "  ✅ SQLite database ready — tasks are saved automatically.";
                lblDbStatus.ForeColor = Color.FromArgb(80, 200, 80);
                _activityLogger.Log("SQLite database initialised successfully");
            }

            AppendMessage("CyberBot",
                "Hello! Welcome to the Cybersecurity Awareness Bot — Part 3.\n\n" +
                "  New features available:\n" +
                "     📋  Task Assistant — add and manage cybersecurity tasks\n" +
                "     🎯  Mini Quiz — test your cybersecurity knowledge\n" +
                "     📜  Activity Log — see everything I've done for you\n\n" +
                "  Use the sidebar to navigate, or just type naturally.\n\n" +
                "  Before we begin — what is your name?",
                _botColor);

            txtInput.Focus();
        }

        // =====================================================
        // CHAT INPUT HANDLING
        // =====================================================

        private void TxtInput_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ProcessInput();
            }
        }

        private void BtnSend_Click(object? sender, EventArgs e) => ProcessInput();

        private void ProcessInput()
        {
            string input = txtInput.Text.Trim();
            txtInput.Clear();
            txtInput.Focus();

            // Input validation
            if (string.IsNullOrWhiteSpace(input))
            {
                AppendMessage("CyberBot",
                    "Please type something. Try 'add task', 'start quiz', 'show activity log', or ask about cybersecurity.",
                    _warningColor);
                return;
            }

            string displayName = _awaitingName ? "You" : _memoryManager.GetUserName();
            AppendMessage(displayName, input, _userColor);

            // Name capture (first message)
            if (_awaitingName)
            {
                _awaitingName = false;
                _memoryManager.SetUserName(input);
                lblNavUser.Text = _memoryManager.GetUserName();
                lblStatus.Text = $"  Session active — {_memoryManager.GetUserName()}";
                _activityLogger.Log($"User identified as: {_memoryManager.GetUserName()}");

                AppendMessage("CyberBot",
                    $"Great to meet you, {_memoryManager.GetUserName()}! " +
                    "I'm here to help you stay safe online [3].\n\n" +
                    "  Use the sidebar to navigate to Tasks, Quiz, or the Activity Log.\n" +
                    "  Or just type naturally — I understand plain English!",
                    _botColor);
                return;
            }

            // Awaiting reminder after task add
            if (_awaitingTaskReminder)
            {
                HandleReminderResponse(input);
                return;
            }

            string inputLower = input.ToLower().Trim();

            // NLP intent detection — Part 3 requirement
            UserIntent intent = _nlpProcessor.DetectIntent(inputLower);

            switch (intent)
            {
                case UserIntent.AddTask:
                    HandleAddTaskFromChat(input);
                    return;

                case UserIntent.ViewTasks:
                    ShowPanel("tasks");
                    AppendMessage("CyberBot", "Opening your Task Manager now.", _botColor);
                    _activityLogger.Log("User navigated to Tasks panel via chat");
                    return;

                case UserIntent.StartQuiz:
                    ShowPanel("quiz");
                    AppendMessage("CyberBot", "Opening the Cybersecurity Quiz! Press 'Start Quiz' to begin.", _botColor);
                    _activityLogger.Log("User navigated to Quiz via chat command");
                    return;

                case UserIntent.ShowLog:
                    AppendMessage("CyberBot", _activityLogger.GetLog(), _botColor);
                    _activityLogger.Log("User requested activity log via chat");
                    return;

                case UserIntent.ViewMemory:
                    AppendMessage("CyberBot", _memoryManager.Recall(), _botColor);
                    return;

                case UserIntent.NavigateTasks:
                    ShowPanel("tasks");
                    return;

                case UserIntent.NavigateQuiz:
                    ShowPanel("quiz");
                    return;

                case UserIntent.NavigateLog:
                    ShowPanel("log");
                    RefreshLog();
                    return;

                case UserIntent.NavigateChat:
                    ShowPanel("chat");
                    return;
            }

            // Conversation follow-up
            if (_conversationManager.IsFollowUp(inputLower))
            {
                string? followUp = _conversationManager.GetFollowUp();
                if (followUp != null)
                {
                    AppendMessage("CyberBot", followUp, _botColor);
                    _activityLogger.Log("Follow-up response delivered");
                    return;
                }
            }

            // Sentiment detection
            string? sentiment = _sentimentDetector.Detect(inputLower);
            if (sentiment != null)
                AppendMessage("CyberBot", GetSentimentMessage(sentiment), _sentimentColor);

            // Keyword response (Part 2 engine)
            string? response = _responseEngine.GetResponse(inputLower);
            if (response != null)
            {
                string topic = _responseEngine.GetLastMatchedTopic();
                _memoryManager.SetFavouriteTopic(topic);
                _conversationManager.SetLastTopic(topic, _responseEngine.GetFollowUp(topic));
                AppendMessage("CyberBot", response, _botColor);
                _activityLogger.Log($"Responded to topic: {topic}");
            }
            else if (sentiment == null)
            {
                AppendMessage("CyberBot",
                    $"I'm not sure I understand that, {_memoryManager.GetUserName()}. " +
                    "Try typing 'topics', 'add task', 'start quiz', or 'show activity log'.",
                    _warningColor);
            }
            else
            {
                AppendMessage("CyberBot",
                    "Type 'topics' to see what I can help with, or try 'start quiz' or 'add task'.",
                    _botColor);
            }
        }

        // Handles adding a task from the chat panel using NLP
        private void HandleAddTaskFromChat(string input)
        {
            string title = _nlpProcessor.ExtractTaskTitle(input);
            _pendingTaskTitle = title;
            _pendingTaskDesc = $"Task created from chat: {title}";
            _awaitingTaskReminder = true;

            AppendMessage("CyberBot",
                $"Task noted: '{title}'\n\n  Would you like to set a reminder? " +
                "If so, type a timeframe like 'in 3 days' or 'tomorrow'. " +
                "Type 'no' to skip the reminder.",
                _botColor);

            _activityLogger.Log($"Task initiated from chat: {title}");
        }

        // Handles the user's reminder response after task creation
        private void HandleReminderResponse(string input)
        {
            _awaitingTaskReminder = false;
            string reminder = "";

            if (!input.ToLower().Contains("no") &&
                !input.ToLower().Contains("skip") &&
                !input.ToLower().Contains("none"))
            {
                reminder = _nlpProcessor.ExtractReminder(input);
                if (string.IsNullOrEmpty(reminder)) reminder = input;
            }

            CyberTask task = new CyberTask(_pendingTaskTitle, _pendingTaskDesc, reminder);

            if (_dbConnected)
            {
                try
                {
                    int id = _taskManager.AddTask(task);
                    string reminderMsg = string.IsNullOrEmpty(reminder)
                        ? "No reminder set."
                        : $"Reminder set for: {reminder}";

                    AppendMessage("CyberBot",
                        $"✅ Task added successfully!\n\n" +
                        $"     Title: {_pendingTaskTitle}\n" +
                        $"     {reminderMsg}\n\n" +
                        "  Navigate to the Tasks panel to view all your tasks.",
                        _successColor);

                    _activityLogger.Log($"Task added to DB: '{_pendingTaskTitle}'" +
                        (string.IsNullOrEmpty(reminder) ? "" : $" — reminder: {reminder}"));

                    RefreshTaskList();
                }
                catch (Exception ex)
                {
                    AppendMessage("CyberBot", $"Could not save task to database: {ex.Message}", _warningColor);
                }
            }
            else
            {
                AppendMessage("CyberBot",
                    "⚠️  Database is offline. Task could not be saved permanently. " +
                    "Please check your MySQL connection and restart the app.",
                    _warningColor);
            }

            _pendingTaskTitle = "";
            _pendingTaskDesc = "";
        }

        // =====================================================
        // NAVIGATION
        // =====================================================

        private void ShowPanel(string panel)
        {
            pnlChat.Visible = panel == "chat";
            pnlTasks.Visible = panel == "tasks";
            pnlQuiz.Visible = panel == "quiz";
            pnlLog.Visible = panel == "log";

            // Highlight active nav button
            btnNavChat.BackColor = panel == "chat" ? Color.FromArgb(0, 170, 120) : Color.FromArgb(40, 40, 90);
            btnNavTasks.BackColor = panel == "tasks" ? Color.FromArgb(0, 170, 120) : Color.FromArgb(40, 40, 90);
            btnNavQuiz.BackColor = panel == "quiz" ? Color.FromArgb(0, 170, 120) : Color.FromArgb(40, 40, 90);
            btnNavLog.BackColor = panel == "log" ? Color.FromArgb(0, 170, 120) : Color.FromArgb(40, 40, 90);

            if (panel == "tasks") RefreshTaskList();
            if (panel == "log") RefreshLog();
        }

        // =====================================================
        // TASK ASSISTANT
        // =====================================================

        private void BtnAddTask_Click(object? sender, EventArgs e)
        {
            string title = txtTaskTitle.Text.Trim();
            string desc = txtTaskDesc.Text.Trim();
            string reminder = txtTaskReminder.Text.Trim();

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a task title.", "Missing Title",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_dbConnected)
            {
                MessageBox.Show("Database is offline. Cannot save task.", "DB Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                CyberTask task = new CyberTask(title,
                    string.IsNullOrEmpty(desc) ? title : desc, reminder);
                _taskManager.AddTask(task);

                _activityLogger.Log($"Task added: '{title}'" +
                    (string.IsNullOrEmpty(reminder) ? "" : $" — reminder: {reminder}"));

                txtTaskTitle.Clear();
                txtTaskDesc.Clear();
                txtTaskReminder.Clear();
                RefreshTaskList();

                lblDbStatus.Text = $"  ✅ Task '{title}' added successfully.";
                lblDbStatus.ForeColor = Color.FromArgb(80, 200, 80);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving task: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCompleteTask_Click(object? sender, EventArgs e)
        {
            if (lvTasks.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a task to mark as complete.", "No Task Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!_dbConnected) return;

            int id = int.Parse(lvTasks.SelectedItems[0].Text);
            string title = lvTasks.SelectedItems[0].SubItems[1].Text;

            try
            {
                _taskManager.MarkComplete(id);
                _activityLogger.Log($"Task completed: '{title}'");
                lblDbStatus.Text = $"  ✅ Task '{title}' marked as complete.";
                lblDbStatus.ForeColor = Color.FromArgb(80, 200, 80);
                RefreshTaskList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeleteTask_Click(object? sender, EventArgs e)
        {
            if (lvTasks.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a task to delete.", "No Task Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!_dbConnected) return;

            int id = int.Parse(lvTasks.SelectedItems[0].Text);
            string title = lvTasks.SelectedItems[0].SubItems[1].Text;

            DialogResult confirm = MessageBox.Show(
                $"Delete task '{title}'?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                _taskManager.DeleteTask(id);
                _activityLogger.Log($"Task deleted: '{title}'");
                lblDbStatus.Text = $"  🗑️ Task '{title}' deleted.";
                lblDbStatus.ForeColor = Color.FromArgb(220, 120, 60);
                RefreshTaskList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Loads all tasks from MySQL into the ListView
        private void RefreshTaskList()
        {
            if (!_dbConnected) return;

            try
            {
                lvTasks.Items.Clear();
                List<CyberTask> tasks = _taskManager.GetAllTasks();

                foreach (CyberTask task in tasks)
                {
                    ListViewItem item = new ListViewItem(task.Id.ToString());
                    item.SubItems.Add(task.Title);
                    item.SubItems.Add(task.Description);
                    item.SubItems.Add(string.IsNullOrEmpty(task.Reminder) ? "None" : task.Reminder);
                    item.SubItems.Add(task.IsCompleted ? "✅ Done" : "⏳ Pending");

                    // Visually distinguish completed tasks
                    item.ForeColor = task.IsCompleted
                        ? Color.FromArgb(100, 180, 100)
                        : Color.WhiteSmoke;

                    lvTasks.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                lblDbStatus.Text = $"  ⚠️ Error loading tasks: {ex.Message}";
            }
        }

        // =====================================================
        // QUIZ GAME
        // =====================================================

        private void BtnStartQuiz_Click(object? sender, EventArgs e)
        {
            _quizManager.StartQuiz();
            _activityLogger.Log("Quiz started");
            btnStartQuiz.Visible = false;
            ShowNextQuestion();
        }

        private void QuizOption_Click(object? sender, EventArgs e)
        {
            if (!_quizManager.IsActive()) return;

            Button btn = (Button)sender!;
            int selectedIndex = (int)btn.Tag!;

            var (isCorrect, feedback) = _quizManager.SubmitAnswer(selectedIndex);

            // Show feedback
            lblFeedback.Text = feedback;
            lblFeedback.ForeColor = isCorrect
                ? Color.FromArgb(100, 220, 100)
                : Color.FromArgb(255, 100, 80);

            _activityLogger.Log($"Quiz answer submitted — {(isCorrect ? "Correct" : "Incorrect")}");
            lblScore.Text = $"Score: {_quizManager.GetScore()} / {_quizManager.GetTotalQuestions()}";

            if (!_quizManager.IsActive())
            {
                // Quiz complete
                string finalFeedback = _quizManager.GetFinalFeedback();
                lblQuestion.Text = finalFeedback;
                lblFeedback.Text = "";
                SetQuizOptionsVisible(false);
                btnStartQuiz.Text = "▶  PLAY AGAIN";
                btnStartQuiz.Visible = true;
                _activityLogger.Log($"Quiz completed — Score: {_quizManager.GetScore()}/{_quizManager.GetTotalQuestions()}");
            }
            else
            {
                // Delay briefly then show next question
                System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
                t.Interval = 1800;
                t.Tick += (s, args) => { t.Stop(); ShowNextQuestion(); };
                t.Start();
            }
        }

        private void ShowNextQuestion()
        {
            QuizQuestion? q = _quizManager.GetCurrentQuestion();
            if (q == null) return;

            int current = _quizManager.GetCurrentIndex() + 1;
            int total = _quizManager.GetTotalQuestions();
            lblQuizProgress.Text = $"Question {current} of {total}";
            lblQuestion.Text = q.Question;
            lblFeedback.Text = "";

            List<Button> optBtns = new List<Button> { btnOpt0, btnOpt1, btnOpt2, btnOpt3 };

            for (int i = 0; i < 4; i++)
            {
                if (i < q.Options.Count)
                {
                    optBtns[i].Text = q.Options[i];
                    optBtns[i].Visible = true;
                    optBtns[i].BackColor = Color.FromArgb(30, 30, 65);
                }
                else
                {
                    optBtns[i].Visible = false;
                }
            }
        }

        private void SetQuizOptionsVisible(bool visible)
        {
            btnOpt0.Visible = visible;
            btnOpt1.Visible = visible;
            btnOpt2.Visible = visible;
            btnOpt3.Visible = visible;
        }

        // =====================================================
        // ACTIVITY LOG
        // =====================================================

        private void RefreshLog()
        {
            rtbLog.Clear();
            rtbLog.SelectionColor = Color.FromArgb(0, 200, 160);
            rtbLog.SelectionFont = new Font("Consolas", 10f, FontStyle.Bold);
            rtbLog.AppendText("  Activity Log — Recent Actions\n");
            rtbLog.AppendText("  " + new string('─', 55) + "\n\n");

            rtbLog.SelectionColor = Color.WhiteSmoke;
            rtbLog.SelectionFont = new Font("Consolas", 10f);
            rtbLog.AppendText("  " + _activityLogger.GetLog().Replace("\n", "\n  "));
        }

        // =====================================================
        // CHAT DISPLAY HELPERS (from Part 2)
        // =====================================================

        private string GetSentimentMessage(string sentiment)
        {
            string name = _memoryManager.GetUserName();
            return sentiment switch
            {
                "worried" =>
                    $"I can hear that you're feeling worried, {name}. That's completely understandable — " +
                    "cybersecurity threats are real. Let me give you some clear, practical advice.",
                "curious" =>
                    $"Love the curiosity, {name}! Asking questions is the first step to staying safe online.",
                "frustrated" =>
                    $"I understand your frustration, {name}. Let me explain this as clearly as possible.",
                "positive" =>
                    $"Great to hear, {name}! A positive attitude towards security awareness makes a real difference.",
                _ => $"Thank you for sharing that, {name}. Let me help."
            };
        }

        private void AppendMessage(string sender, string message, Color color)
        {
            AppendColoredText($"\n  {sender} >> ", color, _boldFont);
            AppendColoredText(message + "\n", Color.WhiteSmoke, _normalFont);
            rtbChat.ScrollToCaret();
        }

        private void AppendColoredText(string text, Color color, Font font)
        {
            rtbChat.SelectionStart = rtbChat.TextLength;
            rtbChat.SelectionLength = 0;
            rtbChat.SelectionColor = color;
            rtbChat.SelectionFont = font;
            rtbChat.AppendText(text);
            rtbChat.SelectionColor = rtbChat.ForeColor;
            rtbChat.SelectionFont = _normalFont;
        }

        private void DisplayAsciiArt()
        {
            Color artColor = Color.FromArgb(0, 190, 190);
            Font artFont = new Font("Consolas", 8f);
            rtbChat.SelectionColor = artColor;
            rtbChat.SelectionFont = artFont;
            rtbChat.AppendText("  ╔══════════════════════════════════════════════════════════════╗\n");
            rtbChat.AppendText("  ║  ██████╗██╗   ██╗██████╗ ███████╗██████╗ ██████╗  ██████╗ ║\n");
            rtbChat.AppendText("  ║ ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔═══██╗║\n");
            rtbChat.AppendText("  ║ ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝██████╔╝██║   ██║║\n");
            rtbChat.AppendText("  ║ ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██╔══██╗██║   ██║║\n");
            rtbChat.AppendText("  ║ ╚██████╗   ██║   ██████╔╝███████╗██║  ██║██████╔╝╚██████╔╝║\n");
            rtbChat.AppendText("  ║  ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═════╝  ╚═════╝║\n");
            rtbChat.AppendText("  ║         🛡️  CYBERSECURITY AWARENESS BOT — PART 3  🛡️        ║\n");
            rtbChat.AppendText("  ╚══════════════════════════════════════════════════════════════╝\n");
            rtbChat.SelectionColor = rtbChat.ForeColor;
            rtbChat.SelectionFont = _normalFont;
        }
    }
}
