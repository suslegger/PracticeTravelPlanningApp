using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PracticeTravelPlanningApp
{
    public partial class MainForm : Form
    {
        public List<PointLatLng> _points; // лист с маркерами и названиями маркеров
        public List<String> _pointnames;
        public GMapOverlay _selmarkOverlay = new GMapOverlay("selmark"); // оверлей со стрелкой-маркером, с маркерами и для маршрутов
        public GMapOverlay _markerOverlay = new GMapOverlay("markers");
        public GMapOverlay _routesOverlay = new GMapOverlay("_routesOverlay");
        private int _countPoints = 0; // счетчик маркеров
        private GMapMarker _currentMarker; // маркер
        private const double DefaultLatitude = 55.742; // стандартная широта
        private const double DefaultLongitude = 37.613; // стандартная долгота
        private const int DefaultZoom = 10; // стандартное, минимальное и максимальное увеличение
        private const int MinZoom = 3;
        private const int MaxZoom = 18;
        private string dbFileName;
        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;
        private int dgvid;

        public MainForm()
        {
            InitializeComponent();
            _points = new List<PointLatLng>(); // создание списка точек
            _pointnames = new List<String>(); // создание списка имен точек
            neededdgv.AllowUserToAddRows = false; // запретить добавлять поля встроенными инструментами datagridview
        }
        private void Karta_Load(object sender, EventArgs e)
        {
            try
            {
                GMaps.Instance.Mode = AccessMode.ServerAndCache;  // режим и папка кеширования карты
                string CacheDirectory = Path.Combine(Directory.GetCurrentDirectory(), "cache");
                if (!Directory.Exists(CacheDirectory)) Directory.CreateDirectory(CacheDirectory); // если нет папки - создать
                karta.CacheLocation = CacheDirectory;
            }
            catch (System.IO.FileNotFoundException) { GMaps.Instance.Mode = AccessMode.ServerOnly; ShowErrorMessage("Невозможно создать/использовать папку кеша карты! Для загрузки карты будет использоваться только доступ в Интернет"); }
            karta.DragButton = MouseButtons.Left; // лкм для перемещения карты
            karta.MapProvider = GMapProviders.OpenStreetMap; // провайдер карты
            karta.Position = new PointLatLng(DefaultLatitude, DefaultLongitude); // установка позиции карты
            karta.MinZoom = MinZoom; // минимальное, максимальное и стандартное увеличение
            karta.MaxZoom = MaxZoom;
            karta.Zoom = DefaultZoom;
            karta.Overlays.Add(_selmarkOverlay); // наложение маркера выбора, местоположения и маршурта
            karta.Overlays.Add(_markerOverlay);
            karta.Overlays.Add(_routesOverlay);
            _currentMarker = new GMarkerGoogle(karta.Position, GMarkerGoogleType.arrow);
            { _currentMarker.IsVisible = false; } // отключить видимость по умолчанию
            _selmarkOverlay.Markers.Add(_currentMarker); // добавление маркера
            karta.ShowCenter = false; // убрать центровой маркер
        }
        private void ShowErrorMessage(string message) { MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); } // сообщение об ошибке

        private void Addpointbutton_Click(object sender, EventArgs e)
        {
            if (_countPoints >= 2) { ShowErrorMessage("Невозможно добавить более чем 2 точки!"); return; }
            if (_currentMarker.IsVisible == false && usesearchcb.Checked == false) { ShowErrorMessage("Не выбрана точка!"); return; }
            bool isfailed = false; // счетчик ошибки для поиска
            GMapMarker m1 = null, m2 = null; // инициализация точек и местоположения маркера
            PointLatLng markerpos;
            if (_countPoints == 0)
            {
                if (usesearchcb.Checked == true) /* если используется поиск (установлена галочка) */
                {
                    karta.GetPositionByKeywords(nametextbox.Text, out markerpos); // попытка поиска
                    if (markerpos.ToString() == "{Lat=0, Lng=0}") { ShowErrorMessage("Не удалось найти данное место"); isfailed = true; } // если место не найдено, отобразить окно и включить счётчик ошибок
                    else { karta.Position = markerpos; m1 = new GMarkerGoogle(markerpos, GMarkerGoogleType.green_dot); if (karta.Zoom < 12) karta.Zoom = 12; } // если найдено - расположить карту в этом месте, добавить маркер и увеличить карту на этой точке
                }
                else // если местоположение выбиралось по нажатию на карту
                {
                    markerpos = new PointLatLng(_currentMarker.Position.Lat, _currentMarker.Position.Lng); // задание маркера по выбранным координатам
                    m1 = new GMarkerGoogle(markerpos, GMarkerGoogleType.green_dot); // добавление маркера по выбору в переменную m1
                    karta.Position = markerpos; // расположить карту в этом месте
                    if (karta.Zoom < 12) karta.Zoom = 12; // увеличить карту на этой точке
                }
            }
            else // если есть одна точка – использовать такой же алгоритм, но для второй точки
            {
                if (usesearchcb.Checked == true)
                {
                    karta.GetPositionByKeywords(nametextbox.Text, out markerpos); // поиск местоположения по словам
                    if (markerpos.ToString() == "{Lat=0, Lng=0}") { ShowErrorMessage("Не удалось найти данное место"); isfailed = true; } // если не удалось найти - показать окно ошибки и изменить счётчик ошибок
                    else { karta.Position = markerpos; m2 = new GMarkerGoogle(markerpos, GMarkerGoogleType.red_dot); karta.ZoomAndCenterMarkers("markers"); } // если удалось - занести расположение в переменную, создать маркер и центровать карту по маркеру
                }
                else // если местоположение выбиралось по нажатию на карту
                {
                    markerpos = new PointLatLng(_currentMarker.Position.Lat, _currentMarker.Position.Lng); // задание маркера по выбранным координатам
                    m2 = new GMarkerGoogle(markerpos, GMarkerGoogleType.red_dot); // добавление маркера в переменную
                    karta.ZoomAndCenterMarkers("markers"); // центрирование карты по маркерам
                }
            }
            if (isfailed == false) // если счётчик ошибок не сработал
            {
                _countPoints++;
                _points.Add(markerpos);
                if (nametextbox.Text != String.Empty) _pointnames.Add(nametextbox.Text); // добавление название маркера
                else if (_countPoints == 1) _pointnames.Add("пункт А");
                else _pointnames.Add("пункт B");
                if (_countPoints == 1)
                {
                    _markerOverlay.Markers.Add(m1);
                    m1.ToolTipText = "Начало: " + _pointnames[0];
                    m1.ToolTipMode = MarkerTooltipMode.Always;
                    statuslabel.Text = "Выбран только исходный пункт!";
                }
                else
                {
                    _markerOverlay.Markers.Add(m2);
                    m2.ToolTipText = "Конец: " + _pointnames[1];
                    m2.ToolTipMode = MarkerTooltipMode.Always;
                    _currentMarker.IsVisible = false;
                    statuslabel.Text = "Можно расчитывать расстояние";
                    howto2.Visible = false;
                }
            }
        }
        private void Resetpointbutton_Click(object sender, EventArgs e)
        {
            if (_countPoints != 0)
            {
                _points.Clear(); // очистить список точек, их названий
                _pointnames.Clear();
                _currentMarker.IsVisible = false; // убрать видимость маркера выбора
                statuslabel.Text = "Пункты не выбраны!"; // изменить статус
                _markerOverlay.Clear(); // очистить наложение маркеров
                karta.Refresh(); // обновить карту
                _countPoints = 0; // сбросить счётчик точек и сообщения
                distancelabel.Text = "Расстояние: не рассчитано"; // текст расстояния и времени в пути
                etalabel.Text = "Время в пути: не рассчитано";
                _routesOverlay.Clear(); // очистить наложение маршрутов
                howto2.Visible = true; // показать подсказку, как выбрать пункт
            }
            else { ShowErrorMessage("Не добавлены точки!"); return; }
        }
        public static TimeSpan CalculateTravelTime(double distance, double speed)
        {
            if (speed <= 0) throw new ArgumentException("Скорость должна быть больше нуля!");
            double hours = distance / speed; // время = расстояние / скорость
            return TimeSpan.FromHours(hours); // возвращать по умолчанию часы
        }
        private void Getrouteandtime_Click(object sender, EventArgs e)
        {
            if (_countPoints != 2) { ShowErrorMessage("Не добавлены все точки!"); return; }
            statuslabel.Text = "Расчет маршрута..."; // изменение статуса
            var route = OpenStreetMapProvider.Instance.GetRoute(_points[0], _points[1], false, false, (int)karta.Zoom); // поиск маршрута: 2 точки, предпочитать ли трассы, пешеходные маршруты, какое увеличение использовать
            if (route == null) { ShowErrorMessage("Невозможно вычислить маршрут! Проверьте интернет-соединение"); return; } // если маршрут не найден - вывести ошибку
            var r = new GMapRoute(route.Points, "Marshrut") { Stroke = new Pen(Color.Red, 5) }; // создание маршрута и использование красного цвета для маршрута
            _routesOverlay.Routes.Add(r); // добавление маршрута на наложение
            karta.ZoomAndCenterRoute(r); // приближение и центрирование маршрута
            statuslabel.Text = "Маршрут построен"; // изменение статуса
            double distance = route.Distance; // обозначение расстояния
            if (_pointnames[0] == "пункт А") _pointnames[0] = "пункта А"; // изменение падежа при стандартных названиях
            if (_pointnames[1] == "пункт B") _pointnames[1] = "пункта B";
            distance = Math.Truncate(distance * 1000) / 1000; // максимум 3 числа после запятой
            double meters = Math.Truncate(distance); // временно берём метры как целое число из расстояния в км
            meters = Math.Round(distance - meters, 3); // расчет метров
            if (meters == 0) distancelabel.Text = "Расстояние от " + _pointnames[0] + " до " + _pointnames[1] + " равно " + distance + " км."; // если метры = 0 - строка выглядит так
            else distancelabel.Text = "Расстояние от " + _pointnames[0] + " до " + _pointnames[1] + " равно " + Math.Truncate(distance) + " км. " + meters * 1000 + " м."; // если метры != 0 - строка выглядит такы
            if (timecheckbox.Checked == true) // если установлена галочка расчета времени
            {
                try
                {
                    double speed = Convert.ToDouble(speedtextBox.Text); // берём скорость из поля
                    TimeSpan traveltime = CalculateTravelTime(distance, speed); // вызов функции расчёта времени
                    if (traveltime.TotalHours < 1) etalabel.Text = $"Примерное время в пути равно: {traveltime.Minutes} мин {traveltime.Seconds} сек";
                    else if (traveltime.TotalHours < 24) etalabel.Text = $"Примерное время в пути равно: {traveltime.Hours} ч {traveltime.Minutes} мин {traveltime.Seconds} сек";
                    else etalabel.Text = $"Примерное время в пути равно: {traveltime.Days} дн {traveltime.Hours} ч {traveltime.Minutes} мин"; // если больше 24 часов
                }
                catch (Exception ex) { ShowErrorMessage(ex.Message); } // если ошибка в функции - вывести сообщение при ошибке
            }
        }
        private void Timecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            // если поставлена галочка "рассчитать время" показать поле для ввода скорости, показать пометку для ввода скорости и изменить текст кнопки
            if (timecheckbox.Checked == true) { speedtextBox.Visible = true; speedlabel.Visible = true; getrouteandtime.Text = "Рассчитать расстояние и время"; }
            else { speedtextBox.Visible = false; speedlabel.Visible = false; getrouteandtime.Text = "Рассчитать расстояние"; } // если убрана - вернуть все назад
        }
        private void Karta_MouseDown(object sender, MouseEventArgs e) // при нажатии кнопкой мыши на карту
        {
            if (e.Button == MouseButtons.Right && usesearchcb.Checked == false) // если нажата ПКМ и карта не в режиме поиска по словам
            {
                if (_currentMarker.IsVisible == false) { _currentMarker.IsVisible = true; } // если маркер не видим - сделать видимым
                if (_currentMarker.IsVisible) { _currentMarker.Position = karta.FromLocalToLatLng(e.X, e.Y); } // если маркер видим - вернуть координаты, куда нажата мышь 
            }
        }
        private void Zoomplus_Click(object sender, EventArgs e) { karta.Zoom++; } // при нажатии на кнопку увлеич. масштаба - увеличить масштаб

        private void Zoomminus_Click(object sender, EventArgs e) { karta.Zoom--; } // при нажатии на кнопку уменьш. масштаба - уменьшить масштаб

        private void Karta_OnMapZoomChanged() // при изменении масштаба
        {
            if (karta.Zoom != MinZoom && karta.Zoom != MaxZoom) { if (zoomplus.Enabled == false) zoomplus.Enabled = true; if (zoomminus.Enabled == false) zoomminus.Enabled = true; } // если не максимальный и не мин. масшаб - если кнопки выключены - включить
            else if (karta.Zoom == MinZoom) { zoomminus.Enabled = false; if (zoomplus.Enabled == false) zoomplus.Enabled = true; } // если масштаб слишком маленький - выключить кнопку уменьшения
            else { zoomplus.Enabled = false; if (zoomminus.Enabled == false) zoomminus.Enabled = true; } // если масштаб слишком большой - выкл. кнопку увеличения
        }
        private void Usesearchcb_CheckedChanged(object sender, EventArgs e) // при изменении галочки поиска
        {
            if (usesearchcb.Checked == true) // если поставлена галочка поиска, показывать один текст подсказки, если нет - другой
            {
                howto.Text = "Введите названия места для поиска";
                helplabel1.Text = "Вращайте карту левой кнопкой мыши";
                _selmarkOverlay.IsVisibile = false; // убрать видимость маркера выбора
                addpointbutton.Text = "Найти место";
            }
            else
            {
                helplabel1.Text = "Вращайте карту левой кнп. мыши, указание пунктов - правой";
                howto.Text = "Выберите пункт на карте и введите имя точки (необязательно)";
                _selmarkOverlay.IsVisibile = true; // показать маркер выбора
                addpointbutton.Text = "Выбрать пункт";
            }
        }
        private void Exitbutton_Click(object sender, EventArgs e) { this.Close(); } // выйти из программы
        // список необходимых предметов
        private void Addbuttonth_Click(object sender, EventArgs e)
        {
            if (m_dbConn.State != ConnectionState.Open) /* если соединение закрыто (нет подключение к БД) */ { ShowErrorMessage("БД не подключена! Создайте или загрузите БД!"); return; }
            else if (namethtextBox.Text == String.Empty || amountthtextBox.Text == String.Empty) /* если поля пустые */ { ShowErrorMessage("Не было указано название вещи и/или количество!"); return; }
            else try
                {
                    DataTable dTable = new DataTable(); // создание таблицы данных
                    using (var command = new SQLiteCommand("INSERT INTO TravelItems (name, amount) VALUES (@name, @amount)", m_dbConn)) // новая команда SQL (с запросом и параметрами подключения)
                    {
                        command.Parameters.AddWithValue("@name", namethtextBox.Text); // параметр в запросе @name = поле nametxhtextbox.text
                        command.Parameters.AddWithValue("@amount", amountthtextBox.Text); // @amount = amountthtextbox.text
                        command.ExecuteNonQuery(); // выполнить команды
                    }
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM TravelItems", m_dbConn); // новый адаптер данных
                    adapter.Fill(dTable); // заполнить адаптер данными с таблицы
                    if (dTable.Rows.Count > 0) { neededdgv.Rows.Clear(); for (int i = 0; i < dTable.Rows.Count; i++) neededdgv.Rows.Add(dTable.Rows[i].ItemArray); } // если в таблице есть данные - очистить dgv и заполнить в цикле данными из таблицы    
                    namethtextBox.Text = ""; // сделать поля названия и количества пустыми
                    amountthtextBox.Text = "";
                    dbStatusLabel.Text = "Вещь добавлена в базу данных."; // изменить статус
                }
                catch (SQLiteException ex) { ShowErrorMessage("Ошибка подключения к БД: " + ex.Message); } // при ошибке вывести сообщение об ошибке
        }
        private void Rembuttonth_Click(object sender, EventArgs e)
        {
            if (m_dbConn.State != ConnectionState.Open) /* если соединение закрыто (нет подключение к БД) */ { ShowErrorMessage("БД не подключена! Создайте или загрузите БД!"); return; }
            try
            {
                if (neededdgv.Rows.Count == 0) /* если таблица пустая */ { ShowErrorMessage("В таблице нет вещей!"); return; }
                else if (neededdgv.SelectedRows.Count == 0) /* если строка не выбрана */ { ShowErrorMessage("Строка для удаления не выбрана!"); return; }
                else if ((MessageBox.Show("Вы уверены, что хотите удалить выбранную вещь?", "Подтвердите операцию", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
                {
                    DataGridViewRow selectedRow = neededdgv.SelectedRows[0]; // обозначение выбранного поля
                    using (var command = new SQLiteCommand("DELETE FROM TravelItems WHERE id = @id", m_dbConn)) // команда SQL
                    {
                        command.Parameters.AddWithValue("@id", dgvid); // добавить параметр id в запрос
                        command.ExecuteNonQuery(); // выполнить (удалить из БД)
                    }
                    neededdgv.Rows.Remove(selectedRow); // удалить строку из dgv
                    dbStatusLabel.Text = "Вещь удалена из базы данных.";
                }
            }
            catch (Exception ex) { ShowErrorMessage("Ошибка удаления строки! " + ex.Message); }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            neededdgv.Columns.Add("id", "Номер п/п"); // добавить колонки в dgv
            neededdgv.Columns.Add("namec", "Название вещи");
            neededdgv.Columns.Add("amountc", "Количество");
            neededdgv.Columns["id"].Visible = false; // убрать видимость строки с ИД
            m_dbConn = new SQLiteConnection(); // инициализация соединения
            m_sqlCmd = new SQLiteCommand(); // инициализация команды
        }
        private void Loadbuttonth_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            { openFileDialog.Title = "Загрузить базу данных"; openFileDialog.Filter = "SQLite database (*.sqlite)|*.sqlite|All files (*.*)|*.*"; } // диалог открытия файла с заголовком и фильтром
            if (openFileDialog.ShowDialog() == DialogResult.OK) // если нажата клавиша ОК
            {
                try
                {
                    dbFileName = openFileDialog.FileName; // имя файла
                    if (m_dbConn.State != ConnectionState.Open) m_dbConn.Close(); // если есть подключение - отключиться
                    m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;"); // создание соединения
                    m_dbConn.Open(); // открытие соединения
                    savebuttonth.Text = "Сохранить и отключить"; // изменение текста кнопки сохранения
                    dbStatusLabel.Text = "БД успешно подключена.";
                    try
                    {
                        DataTable dTable = new DataTable(); // инициализация таблицы данных
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM TravelItems", m_dbConn); // инициализация адаптера sqlite
                        adapter.Fill(dTable); // заполнение адаптера данными
                        if (dTable.Rows.Count > 0) /* если таблица не пустая */ { neededdgv.Rows.Clear(); /* очистить dgv */ for (int i = 0; i < dTable.Rows.Count; i++) neededdgv.Rows.Add(dTable.Rows[i].ItemArray); /* цикл заполнения dgv данными из dtable */  }
                    }
                    catch (SQLiteException ex) { dbStatusLabel.Text = "Ошибка подключения к БД!"; ShowErrorMessage("Ошибка: " + ex.Message); }
                }
                catch (Exception ex) { ShowErrorMessage("Ошибка: " + ex.Message); }
            }
        }
        private void Savebuttonth_Click(object sender, EventArgs e)
        {
            if (m_dbConn.State != ConnectionState.Open) // если нет соединения - кнопка работает как "создать"
            {
                SaveFileDialog sfd = new SaveFileDialog();
                { sfd.Title = "Выберите место для сохранения БД"; sfd.Filter = "SQLite database (*.sqlite)|*.sqlite|All files (*.*)|*.*"; sfd.DefaultExt = "sqlite"; } // диалог сохранения с заголовком, фильтром и расширением
                if (sfd.ShowDialog() == DialogResult.OK) // если нажата кнопка ОК
                {
                    dbFileName = sfd.FileName; // имя файла
                    try
                    {
                        m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;"); // новое соединение SQLite
                        m_dbConn.Open(); // открыть соединение
                        m_sqlCmd.Connection = m_dbConn; // объявление команды для соединения
                        m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS TravelItems (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, amount TEXT)"; // команда для создания таблицы 
                        m_sqlCmd.ExecuteNonQuery(); // выполнить команду
                        dbStatusLabel.Text = "БД успешно создана и подключена.";
                        savebuttonth.Text = "Сохранить и отключить";
                        return;
                    }
                    catch (SQLiteException ex) { dbStatusLabel.Text = "Ошибка подключеня к БД!"; ShowErrorMessage("Ошибка подключения к БД: " + ex.Message); }
                }
                else { return; }
            }
            else // если БД подключена
            {
                if (MessageBox.Show("Хотите выбрать иное место сохранения базы данных? (Сохранить как)", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) // если нажата кнопка "Да"
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    { saveFileDialog.Title = "Выберите место для сохранения БД"; saveFileDialog.Filter = "SQLite database (*.sqlite)|*.sqlite|All files (*.*)|*.*"; saveFileDialog.DefaultExt = "sqlite"; }  // диалог сохранения файла
                    saveFileDialog.ShowDialog();
                    using (var destination = new SQLiteConnection($"Data Source={saveFileDialog.FileName}" + ";Version=3;")) { destination.Open(); m_dbConn.BackupDatabase(destination, "main", "main", -1, null, 0); destination.Close(); } // сохранить бд
                }
                try
                {
                    m_dbConn.Close(); // закрытие соединения и назначение статуса
                    dbStatusLabel.Text = "БД успешно отключена и сохранена.";
                    savebuttonth.Text = "Создать БД"; /* изменение текста на кнопке */
                    neededdgv.Rows.Clear(); // очистка полей dgv
                }
                catch (Exception ex) { ShowErrorMessage($"Ошибка при сохранении файла: {ex.Message}"); }
            }
        }
        private void Cleanallbutton_Click(object sender, EventArgs e)
        {
            if (m_dbConn.State != ConnectionState.Open) /* Если БД не подключена */ { ShowErrorMessage("БД не подключена! Создайте или загрузите БД!"); return; }
            try
            {
                if (neededdgv.Rows.Count == 0) /* если таблица пустая */ { ShowErrorMessage("В таблице нет вещей!"); return; }
                else if (MessageBox.Show("Вы уверены, что хотите очистить список вещей?", "Подтвердите операцию", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        using (var connection = new SQLiteConnection(m_dbConn)) { m_sqlCmd.Connection = m_dbConn; m_sqlCmd.CommandText = "DELETE FROM TravelItems"; m_sqlCmd.ExecuteNonQuery(); } // новое соединение SQLite: обозначение соединения, команда SQL для удаления всей таблицы и выполнение команды
                        neededdgv.Rows.Clear(); // очистка полей dgv
                        dbStatusLabel.Text = "База данных успешно очищена."; // статус
                    }
                    catch (SQLiteException ex) { dbStatusLabel.Text = "Ошибка подключения к БД!"; ShowErrorMessage("Ошибка: " + ex.Message); }
                }
            }
            catch { ShowErrorMessage("Ошибка очистки списка!"); }
        }
        private void NeededDGV_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection t = neededdgv.SelectedRows; // коллекция выбранных строк
            if (t.Count > 0) // если есть строки
            {
                DataGridViewRow row = t[0]; // массив строк
                dgvid = Convert.ToInt32(row.Cells[0].Value); //  назначение значения id выбранной строки переменной dgvid
                namethtextBox.Text = Convert.ToString(row.Cells[1].Value).Trim(); // вставка в поле названия текста названия
                amountthtextBox.Text = Convert.ToString(row.Cells[2].Value).Trim(); // вставка в поле количества текста количества
            }
        }
        private void Editbutton_Click(object sender, EventArgs e)
        {
            if (m_dbConn.State != ConnectionState.Open) /* если соединение закрыто (нет подключение к БД) */ { ShowErrorMessage("БД не подключена! Создайте или загрузите БД!"); return; }
            else if (namethtextBox.Text == String.Empty || amountthtextBox.Text == String.Empty) /* если поля пустые */ { ShowErrorMessage("Не было указано название вещи и/или количество!"); return; }
            else if (neededdgv.SelectedRows.Count == 0) /* если строка не выбрана */ { ShowErrorMessage("Строка для изменения не выбрана!"); return; }
            else try
                {
                    if (MessageBox.Show("Вы уверены, что хотите изменить данную запись?", "Подтвердите операцию", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DataTable dTable = new DataTable(); // создание таблицы данных
                        using (var command = new SQLiteCommand("UPDATE TravelItems SET name = @name, amount = @amount WHERE id = @id", m_dbConn)) // новая команда SQL (с запросом и параметрами подключения)
                        {
                            command.Parameters.AddWithValue("@name", namethtextBox.Text); // параметр в запросе @name = поле nametxhtextbox.text
                            command.Parameters.AddWithValue("@amount", amountthtextBox.Text); // @amount = amountthtextbox.text
                            command.Parameters.AddWithValue("@id", dgvid);
                            command.ExecuteNonQuery(); // выполнить команды
                        }
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM TravelItems", m_dbConn); // новый адаптер данных
                        adapter.Fill(dTable); // заполнить адаптер данными с таблицы
                        if (dTable.Rows.Count > 0) { neededdgv.Rows.Clear(); for (int i = 0; i < dTable.Rows.Count; i++) neededdgv.Rows.Add(dTable.Rows[i].ItemArray); } // если в таблице есть данные - очистить dgv и заполнить в цикле данными из таблицы    
                        dbStatusLabel.Text = "Строка была успешно изменена";
                        neededdgv.ClearSelection(); // убрать выбор строки
                        namethtextBox.Text = ""; // сделать поля названия и количества пустыми
                        amountthtextBox.Text = "";
                    }
                    else return;
                }
                catch (SQLiteException ex) { dbStatusLabel.Text = "Ошибка изменения строки!"; ShowErrorMessage("Ошибка: " + ex.Message); }
        }
    }
}