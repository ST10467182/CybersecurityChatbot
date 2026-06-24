// =============================================================
// MainForm.Designer.cs
// Cybersecurity Awareness Chatbot - Part 3/POE GUI Layout
// =============================================================
// Full redesign of the GUI with a left navigation sidebar and
// four content panels: Chat, Tasks, Quiz, and Activity Log.
// All Part 1 and Part 2 features are preserved.
//
// References:
// [1] Microsoft, 2024. Windows Forms Overview [Online].
//     Available at: https://learn.microsoft.com/en-us/dotnet/
//     desktop/winforms/overview [Accessed 20 May 2026].
// =============================================================

namespace CybersecurityChatbotGUI
{
    partial class MainForm
    {

        private System.ComponentModel.IContainer? components = null;

        // ===== SHARED CONTROLS =====
        private Panel pnlHeader = null!;
        private Label lblTitle = null!;
        private Label lblSubtitle = null!;
        private Panel pnlNav = null!;
        private Button btnNavChat = null!;
        private Button btnNavTasks = null!;
        private Button btnNavQuiz = null!;
        private Button btnNavLog = null!;
        private Label lblNavUser = null!;
        private Panel pnlContent = null!;

        // ===== CHAT PANEL =====
        private Panel pnlChat = null!;
        private RichTextBox rtbChat = null!;
        private Panel pnlChatBottom = null!;
        private TextBox txtInput = null!;
        private Button btnSend = null!;
        private Label lblStatus = null!;

        // ===== TASKS PANEL =====
        private Panel pnlTasks = null!;
        private Label lblTasksTitle = null!;
        private ListView lvTasks = null!;
        private Panel pnlTaskInput = null!;
        private Label lblTaskName = null!;
        private TextBox txtTaskTitle = null!;
        private Label lblTaskDesc = null!;
        private TextBox txtTaskDesc = null!;
        private Label lblTaskReminder = null!;
        private TextBox txtTaskReminder = null!;
        private Button btnAddTask = null!;
        private Button btnCompleteTask = null!;
        private Button btnDeleteTask = null!;
        private Label lblDbStatus = null!;

        // ===== QUIZ PANEL =====
        private Panel pnlQuiz = null!;
        private Label lblQuizTitle = null!;
        private Label lblQuizProgress = null!;
        private Label lblQuestion = null!;
        private Panel pnlOptions = null!;
        private Button btnOpt0 = null!;
        private Button btnOpt1 = null!;
        private Button btnOpt2 = null!;
        private Button btnOpt3 = null!;
        private Label lblFeedback = null!;
        private Button btnStartQuiz = null!;
        private Label lblScore = null!;

        // ===== LOG PANEL =====
        private Panel pnlLog = null!;
        private Label lblLogTitle = null!;
        private RichTextBox rtbLog = null!;
        private Button btnRefreshLog = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Instantiate all controls
            pnlHeader = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            pnlNav = new Panel();
            btnNavChat = new Button();
            btnNavTasks = new Button();
            btnNavQuiz = new Button();
            btnNavLog = new Button();
            lblNavUser = new Label();
            pnlContent = new Panel();

            // Chat
            pnlChat = new Panel();
            rtbChat = new RichTextBox();
            pnlChatBottom = new Panel();
            txtInput = new TextBox();
            btnSend = new Button();
            lblStatus = new Label();

            // Tasks
            pnlTasks = new Panel();
            lblTasksTitle = new Label();
            lvTasks = new ListView();
            pnlTaskInput = new Panel();
            lblTaskName = new Label();
            txtTaskTitle = new TextBox();
            lblTaskDesc = new Label();
            txtTaskDesc = new TextBox();
            lblTaskReminder = new Label();
            txtTaskReminder = new TextBox();
            btnAddTask = new Button();
            btnCompleteTask = new Button();
            btnDeleteTask = new Button();
            lblDbStatus = new Label();

            // Quiz
            pnlQuiz = new Panel();
            lblQuizTitle = new Label();
            lblQuizProgress = new Label();
            lblQuestion = new Label();
            pnlOptions = new Panel();
            btnOpt0 = new Button();
            btnOpt1 = new Button();
            btnOpt2 = new Button();
            btnOpt3 = new Button();
            lblFeedback = new Label();
            btnStartQuiz = new Button();
            lblScore = new Label();

            // Log
            pnlLog = new Panel();
            lblLogTitle = new Label();
            rtbLog = new RichTextBox();
            btnRefreshLog = new Button();

            SuspendLayout();

            // ===== FORM =====
            Text = "Cybersecurity Awareness Bot — Part 3/POE";
            Size = new Size(1100, 750);
            MinimumSize = new Size(900, 600);
            BackColor = Color.FromArgb(15, 15, 35);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10f);
            StartPosition = FormStartPosition.CenterScreen;

            // ===== HEADER =====
            pnlHeader.BackColor = Color.FromArgb(8, 8, 25);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 75;

            lblTitle.Text = "🛡️  CYBERSECURITY AWARENESS BOT  —  PART 3/POE";
            lblTitle.Font = new Font("Segoe UI", 18f, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 220, 180);
            lblTitle.Dock = DockStyle.Top;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Height = 50;

            lblSubtitle.Text = "Task Assistant  •  Mini Quiz  •  NLP Simulation  •  Activity Log  •  Protecting South Africa Online";
            lblSubtitle.Font = new Font("Segoe UI", 8.5f, FontStyle.Italic);
            lblSubtitle.ForeColor = Color.FromArgb(100, 100, 160);
            lblSubtitle.Dock = DockStyle.Top;
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            lblSubtitle.Height = 24;

            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);

            // ===== LEFT NAV SIDEBAR =====
            pnlNav.BackColor = Color.FromArgb(10, 10, 28);
            pnlNav.Dock = DockStyle.Left;
            pnlNav.Width = 160;
            pnlNav.Padding = new Padding(8, 10, 8, 10);

            lblNavUser.Text = "Welcome!";
            lblNavUser.ForeColor = Color.FromArgb(0, 180, 140);
            lblNavUser.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblNavUser.Dock = DockStyle.Top;
            lblNavUser.TextAlign = ContentAlignment.MiddleCenter;
            lblNavUser.Height = 40;
            lblNavUser.Padding = new Padding(0, 8, 0, 0);

            // Nav buttons
            SetupNavButton(btnNavChat, "💬  Chat", Color.FromArgb(0, 170, 120));
            SetupNavButton(btnNavTasks, "📋  Tasks", Color.FromArgb(40, 40, 90));
            SetupNavButton(btnNavQuiz, "🎯  Quiz", Color.FromArgb(40, 40, 90));
            SetupNavButton(btnNavLog, "📜  Activity Log", Color.FromArgb(40, 40, 90));

            pnlNav.Controls.Add(btnNavLog);
            pnlNav.Controls.Add(btnNavQuiz);
            pnlNav.Controls.Add(btnNavTasks);
            pnlNav.Controls.Add(btnNavChat);
            pnlNav.Controls.Add(lblNavUser);

            // ===== CONTENT AREA =====
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.BackColor = Color.FromArgb(15, 15, 35);

            // ===== CHAT PANEL =====
            pnlChat.Dock = DockStyle.Fill;
            pnlChat.BackColor = Color.FromArgb(15, 15, 35);

            rtbChat.BackColor = Color.FromArgb(18, 18, 42);
            rtbChat.ForeColor = Color.WhiteSmoke;
            rtbChat.Font = new Font("Consolas", 10f);
            rtbChat.Dock = DockStyle.Fill;
            rtbChat.ReadOnly = true;
            rtbChat.BorderStyle = BorderStyle.None;
            rtbChat.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbChat.WordWrap = true;
            rtbChat.DetectUrls = false;

            pnlChatBottom.BackColor = Color.FromArgb(8, 8, 25);
            pnlChatBottom.Dock = DockStyle.Bottom;
            pnlChatBottom.Height = 80;
            pnlChatBottom.Padding = new Padding(10, 8, 10, 8);

            txtInput.BackColor = Color.FromArgb(28, 28, 55);
            txtInput.ForeColor = Color.White;
            txtInput.Font = new Font("Segoe UI", 11f);
            txtInput.BorderStyle = BorderStyle.FixedSingle;
            txtInput.Dock = DockStyle.Fill;
            txtInput.PlaceholderText = "Type a message, 'add task', 'start quiz', or 'show activity log'...";

            btnSend.Text = "SEND ➤";
            btnSend.BackColor = Color.FromArgb(0, 170, 120);
            btnSend.ForeColor = Color.White;
            btnSend.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.Dock = DockStyle.Right;
            btnSend.Width = 120;
            btnSend.Cursor = Cursors.Hand;

            Panel pnlInputRow = new Panel();
            pnlInputRow.Dock = DockStyle.Top;
            pnlInputRow.Height = 44;
            pnlInputRow.BackColor = Color.FromArgb(8, 8, 25);
            pnlInputRow.Controls.Add(txtInput);
            pnlInputRow.Controls.Add(btnSend);

            lblStatus.Text = "  Ready — type your name to begin.";
            lblStatus.ForeColor = Color.FromArgb(80, 80, 130);
            lblStatus.Font = new Font("Segoe UI", 8f);
            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Height = 20;

            pnlChatBottom.Controls.Add(pnlInputRow);
            pnlChatBottom.Controls.Add(lblStatus);

            pnlChat.Controls.Add(rtbChat);
            pnlChat.Controls.Add(pnlChatBottom);

            // ===== TASKS PANEL =====
            pnlTasks.Dock = DockStyle.Fill;
            pnlTasks.BackColor = Color.FromArgb(15, 15, 35);
            pnlTasks.Visible = false;
            pnlTasks.Padding = new Padding(15);

            lblTasksTitle.Text = "📋  Cybersecurity Task Assistant";
            lblTasksTitle.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            lblTasksTitle.ForeColor = Color.FromArgb(0, 220, 180);
            lblTasksTitle.Dock = DockStyle.Top;
            lblTasksTitle.Height = 45;

            lblDbStatus.Text = "  Database: Initialising SQLite...";
            lblDbStatus.ForeColor = Color.FromArgb(180, 180, 60);
            lblDbStatus.Font = new Font("Segoe UI", 8.5f);
            lblDbStatus.Dock = DockStyle.Top;
            lblDbStatus.Height = 24;

            // Task input form
            pnlTaskInput.BackColor = Color.FromArgb(20, 20, 45);
            pnlTaskInput.Dock = DockStyle.Top;
            pnlTaskInput.Height = 200;
            pnlTaskInput.Padding = new Padding(10);

            lblTaskName.Text = "Task Title:";
            lblTaskName.ForeColor = Color.FromArgb(0, 200, 160);
            lblTaskName.Location = new Point(10, 12);
            lblTaskName.Size = new Size(100, 22);

            txtTaskTitle.BackColor = Color.FromArgb(30, 30, 60);
            txtTaskTitle.ForeColor = Color.White;
            txtTaskTitle.BorderStyle = BorderStyle.FixedSingle;
            txtTaskTitle.Location = new Point(115, 10);
            txtTaskTitle.Size = new Size(400, 26);
            txtTaskTitle.PlaceholderText = "e.g. Enable two-factor authentication";

            lblTaskDesc.Text = "Description:";
            lblTaskDesc.ForeColor = Color.FromArgb(0, 200, 160);
            lblTaskDesc.Location = new Point(10, 52);
            lblTaskDesc.Size = new Size(100, 22);

            txtTaskDesc.BackColor = Color.FromArgb(30, 30, 60);
            txtTaskDesc.ForeColor = Color.White;
            txtTaskDesc.BorderStyle = BorderStyle.FixedSingle;
            txtTaskDesc.Location = new Point(115, 50);
            txtTaskDesc.Size = new Size(400, 26);
            txtTaskDesc.PlaceholderText = "e.g. Set up 2FA on all important accounts";

            lblTaskReminder.Text = "Reminder:";
            lblTaskReminder.ForeColor = Color.FromArgb(0, 200, 160);
            lblTaskReminder.Location = new Point(10, 92);
            lblTaskReminder.Size = new Size(100, 22);

            txtTaskReminder.BackColor = Color.FromArgb(30, 30, 60);
            txtTaskReminder.ForeColor = Color.White;
            txtTaskReminder.BorderStyle = BorderStyle.FixedSingle;
            txtTaskReminder.Location = new Point(115, 90);
            txtTaskReminder.Size = new Size(400, 26);
            txtTaskReminder.PlaceholderText = "e.g. In 3 days, Tomorrow, Next week (optional)";

            SetupTaskButton(btnAddTask, "➕  Add Task", Color.FromArgb(0, 150, 100), new Point(10, 135));
            SetupTaskButton(btnCompleteTask, "✅  Mark Complete", Color.FromArgb(50, 100, 180), new Point(175, 135));
            SetupTaskButton(btnDeleteTask, "🗑️  Delete Task", Color.FromArgb(160, 40, 40), new Point(370, 135));

            pnlTaskInput.Controls.AddRange(new Control[] {
                lblTaskName, txtTaskTitle, lblTaskDesc, txtTaskDesc,
                lblTaskReminder, txtTaskReminder,
                btnAddTask, btnCompleteTask, btnDeleteTask
            });

            // Task list view
            lvTasks.BackColor = Color.FromArgb(18, 18, 42);
            lvTasks.ForeColor = Color.WhiteSmoke;
            lvTasks.Font = new Font("Segoe UI", 9.5f);
            lvTasks.Dock = DockStyle.Fill;
            lvTasks.View = View.Details;
            lvTasks.FullRowSelect = true;
            lvTasks.GridLines = false;
            lvTasks.BorderStyle = BorderStyle.None;
            lvTasks.Columns.Add("ID", 40);
            lvTasks.Columns.Add("Title", 220);
            lvTasks.Columns.Add("Description", 280);
            lvTasks.Columns.Add("Reminder", 150);
            lvTasks.Columns.Add("Status", 90);

            pnlTasks.Controls.Add(lvTasks);
            pnlTasks.Controls.Add(pnlTaskInput);
            pnlTasks.Controls.Add(lblDbStatus);
            pnlTasks.Controls.Add(lblTasksTitle);

            // ===== QUIZ PANEL =====
            pnlQuiz.Dock = DockStyle.Fill;
            pnlQuiz.BackColor = Color.FromArgb(15, 15, 35);
            pnlQuiz.Visible = false;
            pnlQuiz.Padding = new Padding(30, 20, 30, 20);

            lblQuizTitle.Text = "🎯  Cybersecurity Mini Quiz";
            lblQuizTitle.Font = new Font("Segoe UI", 16f, FontStyle.Bold);
            lblQuizTitle.ForeColor = Color.FromArgb(0, 220, 180);
            lblQuizTitle.Dock = DockStyle.Top;
            lblQuizTitle.Height = 50;
            lblQuizTitle.TextAlign = ContentAlignment.MiddleLeft;

            lblQuizProgress.Text = "Press 'Start Quiz' to begin!";
            lblQuizProgress.Font = new Font("Segoe UI", 10f);
            lblQuizProgress.ForeColor = Color.FromArgb(130, 130, 190);
            lblQuizProgress.Dock = DockStyle.Top;
            lblQuizProgress.Height = 28;

            lblScore.Text = "Score: 0 / 12";
            lblScore.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            lblScore.ForeColor = Color.FromArgb(255, 215, 50);
            lblScore.Dock = DockStyle.Top;
            lblScore.Height = 30;

            lblQuestion.Text = "Welcome to the Cybersecurity Quiz!\n\nTest your knowledge across phishing, passwords, malware, safe browsing, and more.\n\nPress 'Start Quiz' below when you are ready.";
            lblQuestion.Font = new Font("Segoe UI", 12f);
            lblQuestion.ForeColor = Color.WhiteSmoke;
            lblQuestion.Dock = DockStyle.Top;
            lblQuestion.Height = 110;
            lblQuestion.Padding = new Padding(0, 10, 0, 10);

            // Option buttons
            pnlOptions.Dock = DockStyle.Top;
            pnlOptions.Height = 200;
            pnlOptions.BackColor = Color.FromArgb(15, 15, 35);

            SetupQuizButton(btnOpt0, 0);
            SetupQuizButton(btnOpt1, 1);
            SetupQuizButton(btnOpt2, 2);
            SetupQuizButton(btnOpt3, 3);

            pnlOptions.Controls.AddRange(new Control[] { btnOpt3, btnOpt2, btnOpt1, btnOpt0 });

            lblFeedback.Text = "";
            lblFeedback.Font = new Font("Segoe UI", 10f, FontStyle.Italic);
            lblFeedback.ForeColor = Color.FromArgb(255, 200, 60);
            lblFeedback.Dock = DockStyle.Top;
            lblFeedback.Height = 65;
            lblFeedback.Padding = new Padding(0, 8, 0, 0);

            btnStartQuiz.Text = "▶  START QUIZ";
            btnStartQuiz.BackColor = Color.FromArgb(0, 160, 110);
            btnStartQuiz.ForeColor = Color.White;
            btnStartQuiz.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
            btnStartQuiz.FlatStyle = FlatStyle.Flat;
            btnStartQuiz.FlatAppearance.BorderSize = 0;
            btnStartQuiz.Dock = DockStyle.Top;
            btnStartQuiz.Height = 48;
            btnStartQuiz.Cursor = Cursors.Hand;

            pnlQuiz.Controls.Add(btnStartQuiz);
            pnlQuiz.Controls.Add(lblFeedback);
            pnlQuiz.Controls.Add(pnlOptions);
            pnlQuiz.Controls.Add(lblQuestion);
            pnlQuiz.Controls.Add(lblScore);
            pnlQuiz.Controls.Add(lblQuizProgress);
            pnlQuiz.Controls.Add(lblQuizTitle);

            // ===== LOG PANEL =====
            pnlLog.Dock = DockStyle.Fill;
            pnlLog.BackColor = Color.FromArgb(15, 15, 35);
            pnlLog.Visible = false;
            pnlLog.Padding = new Padding(15);

            lblLogTitle.Text = "📜  Activity Log";
            lblLogTitle.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            lblLogTitle.ForeColor = Color.FromArgb(0, 220, 180);
            lblLogTitle.Dock = DockStyle.Top;
            lblLogTitle.Height = 45;

            btnRefreshLog.Text = "🔄  Refresh Log";
            btnRefreshLog.BackColor = Color.FromArgb(40, 40, 90);
            btnRefreshLog.ForeColor = Color.White;
            btnRefreshLog.Font = new Font("Segoe UI", 10f);
            btnRefreshLog.FlatStyle = FlatStyle.Flat;
            btnRefreshLog.FlatAppearance.BorderSize = 0;
            btnRefreshLog.Dock = DockStyle.Top;
            btnRefreshLog.Height = 36;
            btnRefreshLog.Cursor = Cursors.Hand;

            rtbLog.BackColor = Color.FromArgb(18, 18, 42);
            rtbLog.ForeColor = Color.WhiteSmoke;
            rtbLog.Font = new Font("Consolas", 10f);
            rtbLog.Dock = DockStyle.Fill;
            rtbLog.ReadOnly = true;
            rtbLog.BorderStyle = BorderStyle.None;

            pnlLog.Controls.Add(rtbLog);
            pnlLog.Controls.Add(btnRefreshLog);
            pnlLog.Controls.Add(lblLogTitle);

            // ===== ASSEMBLE CONTENT AREA =====
            pnlContent.Controls.Add(pnlChat);
            pnlContent.Controls.Add(pnlTasks);
            pnlContent.Controls.Add(pnlQuiz);
            pnlContent.Controls.Add(pnlLog);

            // ===== ASSEMBLE FORM =====
            Controls.Add(pnlContent);
            Controls.Add(pnlNav);
            Controls.Add(pnlHeader);

            ResumeLayout(false);
        }

        // Helper — configures a left nav sidebar button
        private void SetupNavButton(Button btn, string text, Color backColor)
        {
            btn.Text = text;
            btn.BackColor = backColor;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10f);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Dock = DockStyle.Top;
            btn.Height = 44;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(8, 0, 0, 0);
            btn.Margin = new Padding(0, 3, 0, 3);
        }

        // Helper — configures a task action button
        private void SetupTaskButton(Button btn, string text, Color color, Point location)
        {
            btn.Text = text;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 9.5f);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Location = location;
            btn.Size = new Size(155, 36);
            btn.Cursor = Cursors.Hand;
        }

        // Helper — configures a quiz answer button
        private void SetupQuizButton(Button btn, int index)
        {
            btn.Text = "";
            btn.BackColor = Color.FromArgb(30, 30, 65);
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10.5f);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 100);
            btn.Dock = DockStyle.Top;
            btn.Height = 44;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(10, 0, 0, 0);
            btn.Tag = index;
            btn.Margin = new Padding(0, 3, 0, 3);
        }
    }
}
