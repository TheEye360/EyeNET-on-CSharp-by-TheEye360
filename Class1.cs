using System;
using System.Security.Principal;
using System.Windows.Forms;

namespace rover
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            // Проверяем, запущено ли приложение от имени администратора
            if (!IsAdministrator())
            {
                // Показываем сообщение, если не от администратора
                MessageBox.Show("Живём живём живём живём. Опопопопоп. Ты меня запустил не от имени АДмина, поэтому я закроюсь.",
                                "Мешанина",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                // Закрываем приложение
                Application.Exit();
                return;
            }

            // Устанавливаем стиль однопоточности для работы с COM-объектами (например, для Windows Forms)
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Запуск главной формы
            Application.Run(new WebBrowserForm());
        }

        // Метод для проверки, является ли приложение администратором
        private static bool IsAdministrator()
        {
            // Получаем идентификатор текущего пользователя
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            // Проверяем, является ли пользователь администратором
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }

    public partial class WebBrowserForm : Form
    {
        private WebBrowser webBrowser1;
        private TextBox search_box;
        private Button search_btn;
        private Button forward_btn;
        private Button backward_btn;
        private Button button3;

        public WebBrowserForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.search_box = new System.Windows.Forms.TextBox();
            this.search_btn = new System.Windows.Forms.Button();
            this.forward_btn = new System.Windows.Forms.Button();
            this.backward_btn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();

            // Настроим форму для того, чтобы браузер был прямоугольным
            this.Text = "EyeNET"; // Заголовок формы
            this.ClientSize = new System.Drawing.Size(1280, 720); // Установим размер окна формы (ширина x высота)

            // Увеличиваем размер webBrowser1
            this.webBrowser1.Location = new System.Drawing.Point(20, 20); // Устанавливаем отступы от края
            this.webBrowser1.Size = new System.Drawing.Size(1240, 480); // Увеличиваем размеры браузера

            // Увеличиваем размер search_box
            this.search_box.Location = new System.Drawing.Point(20, 510);
            this.search_box.Size = new System.Drawing.Size(1000, 30); // Ширина 1000px

            // Кнопки будут размещены ниже поля ввода
            this.search_btn.Location = new System.Drawing.Point(20, 550);
            this.search_btn.Size = new System.Drawing.Size(100, 30); // Размер кнопки
            this.search_btn.Text = "Search";
            this.search_btn.Click += new System.EventHandler(this.search_btn_Click);

            this.forward_btn.Location = new System.Drawing.Point(130, 550);
            this.forward_btn.Size = new System.Drawing.Size(100, 30); // Размер кнопки
            this.forward_btn.Text = "Forward";
            this.forward_btn.Click += new System.EventHandler(this.forward_btn_Click);

            this.backward_btn.Location = new System.Drawing.Point(240, 550);
            this.backward_btn.Size = new System.Drawing.Size(100, 30); // Размер кнопки
            this.backward_btn.Text = "Back";
            this.backward_btn.Click += new System.EventHandler(this.backward_btn_Click);

            this.button3.Location = new System.Drawing.Point(350, 550);
            this.button3.Size = new System.Drawing.Size(100, 30); // Размер кнопки
            this.button3.Text = "Refresh";
            this.button3.Click += new System.EventHandler(this.button3_Click);

            // Добавляем элементы на форму
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.search_box);
            this.Controls.Add(this.search_btn);
            this.Controls.Add(this.forward_btn);
            this.Controls.Add(this.backward_btn);
            this.Controls.Add(this.button3);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh(); // Перезагружаем страницу
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            NavigateToUrl();
        }

        private void forward_btn_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
                webBrowser1.GoForward();
        }

        private void backward_btn_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();
        }

        private void search_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // Если нажата клавиша Enter
            {
                NavigateToUrl();
            }
        }

        private void NavigateToUrl()
        {
            if (string.IsNullOrEmpty(search_box.Text)) return;
            webBrowser1.Navigate(search_box.Text);
        }
    }
}
