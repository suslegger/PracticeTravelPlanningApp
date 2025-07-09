using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static GMap.NET.Entity.OpenStreetMapRouteEntity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TravelPlanningAppSusloparov
{
    public partial class MainForm : Form
    {
        public List<PointLatLng> _points; // лист с маркерами
        public List<String> _pointnames; // лист с названиями маркеров
        public GMapOverlay _selmarkOverlay = new GMapOverlay("selmark"); // оверлей со стрелкой-маркером
        public GMapOverlay _markerOverlay = new GMapOverlay("markers"); // оверлей с маркерами
        public GMapOverlay _routesOverlay = new GMapOverlay("_routesOverlay"); // оверлей для маршрутов
        private int _countPoints = 0; // счетчик маркеров
        private GMapMarker _currentMarker; // маркер
        private const double DefaultLatitude = 55.742; // стандартная широта
        private const double DefaultLongitude = 37.613; // стандартная долгота
        private const int DefaultZoom = 10; // стандартное увеличение
        private const int MinZoom = 3; // минимальное увеличение
        private const int MaxZoom = 18; // максимальное увеличение
        private const double PedestrianSpeed = 5.5; // средняя скорость пешехода
        private const double DefaultVehicleSpeed = 30; // средняя скорость автомобиля
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
            GMaps.Instance.Mode = AccessMode.ServerAndCache;  // режим кеширования карты
            string CacheDirectory = Path.Combine(Directory.GetCurrentDirectory(), "cache"); // папка кеширования карты
            if (!Directory.Exists(CacheDirectory)) Directory.CreateDirectory(CacheDirectory); // если нет папки - создать
            karta.CacheLocation = CacheDirectory; // назначение папки кеша
            karta.DragButton = MouseButtons.Left; // лкм для перемещения карты
            karta.MapProvider = GMapProviders.OpenStreetMap; // провайдер карты
            karta.Position = new PointLatLng(DefaultLatitude, DefaultLongitude); // установка позиции карты
            karta.MinZoom = MinZoom; // минимальное увеличение
            karta.MaxZoom = MaxZoom; // максимальное увеличение
            karta.Zoom = DefaultZoom; // увеличение по умолчанию
            karta.Overlays.Add(_selmarkOverlay); // наложение маркера выбора
            karta.Overlays.Add(_markerOverlay); // наложение маркеров местоположения
            karta.Overlays.Add(_routesOverlay); // наложение маршрута
            _currentMarker = new GMarkerGoogle(karta.Position, GMarkerGoogleType.arrow); { _currentMarker.IsVisible = false; } // отключить видимость по умолчанию
            _selmarkOverlay.Markers.Add(_currentMarker); // добавление маркера
            karta.ShowCenter = false; // убрать центровой маркер
        }


        private void Addpointbutton_Click(object sender, EventArgs e)
        {
            if (_countPoints >= 2) // если точек 2 или более - не давать создать ещё точки
            {
                MessageBox.Show("Невозможно добавить более чем 2 точки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_currentMarker.IsVisible == false && usesearchcb.Checked == false) // если точка не выбрана в режиме точек
            {
                MessageBox.Show("Не выбрана точка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool isfailed = false; // счетчик ошибки для поиска
            GMapMarker m1 = null, m2 = null; // инициализация точек
            PointLatLng markerpos; // инициализация местоположения маркера
            if (_countPoints == 0) // если нет точек
            {
                if (usesearchcb.Checked == true) // если используется поиск (установлена галочка)
                {
                    karta.GetPositionByKeywords(nametextbox.Text, out markerpos); // попытка поиска
                    if (markerpos.ToString() == "{Lat=0, Lng=0}")
                    { // если место не найдено, отобразить окно и включить счётчик ошибок
                        MessageBox.Show("Не удалось найти данное место", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isfailed = true;
                    }
                    else // если найдено
                    {
                        karta.Position = markerpos; // расположить карту в этом месте
                        m1 = new GMarkerGoogle(markerpos, GMarkerGoogleType.green_dot); // добавление маркера
                        if (karta.Zoom < 12) karta.Zoom = 12; // увеличить карту на этой точке
                    }
                }
                else // если местоположение выбиралось по нажатию на карту
                {
                    markerpos = new PointLatLng(_currentMarker.Position.Lat, _currentMarker.Position.Lng); // задание маркера по выбранным координатам
                    m1 = new GMarkerGoogle(markerpos, GMarkerGoogleType.green_dot); // добавление маркера по выбору в переменную m1
                    karta.Position = markerpos; // расположить карту в этом месте
                    if (karta.Zoom < 12) karta.Zoom = 12; // увеличить карту на этой точке
                }
                if (isfailed == false) // если нет ошибок
                {
                    _points.Add(markerpos); // добавить точку в список
                    _countPoints++; // увеличить счётчик точек
                    _markerOverlay.Markers.Add(m1); // добавление пиктограммы на карту
                    if (nametextbox.Text != String.Empty) _pointnames.Add(nametextbox.Text); // добавление названия
                    else _pointnames.Add("пункт А"); // если нет - использовать стандартное
                    m1.ToolTipText = "Начало: " + _pointnames[0]; // подсказка маркера
                    m1.ToolTipMode = MarkerTooltipMode.Always; // режим подсказки
                    statuslabel.Text = "Выбран только исходный пункт!"; // изменение текста статуса
                }
            }
            else // если есть одна точка
            {
                if (usesearchcb.Checked == true) // если используется поиск (установлена галочка)
                {
                    karta.GetPositionByKeywords(nametextbox.Text, out markerpos); // поиск местоположения по словам
                    if (markerpos.ToString() == "{Lat=0, Lng=0}") // если не удалось найти - показать окно ошибки и изменить счётчик ошибок
                    {
                        MessageBox.Show("Не удалось найти данное место", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isfailed = true;
                    }
                    else // если удалось - занести расположение в переменную, создать маркер и центровать карту по маркеру
                    {
                        karta.Position = markerpos;
                        m2 = new GMarkerGoogle(new PointLatLng(karta.Position.Lat, karta.Position.Lng), GMarkerGoogleType.red_dot);
                        karta.ZoomAndCenterMarkers("markers");
                    }
                }
                else // если местоположение выбиралось по нажатию на карту
                {
                    markerpos = new PointLatLng(_currentMarker.Position.Lat, _currentMarker.Position.Lng); // задание маркера по выбранным координатам
                    m2 = new GMarkerGoogle(markerpos, GMarkerGoogleType.red_dot); // добавление маркера в переменную
                    karta.ZoomAndCenterMarkers("markers"); // центрирование карты по маркеру
                }
                if (isfailed == false) // если счётчик ошибок не сработал
                {
                    _points.Add(markerpos); // добавить точку в список точек
                    _markerOverlay.Markers.Add(m2); // добавление маркера на наложение
                    if (nametextbox.Text != String.Empty) _pointnames.Add(nametextbox.Text); // добавление название макера
                    else _pointnames.Add("пункт B"); // или использование стандартного
                    m2.ToolTipText = "Конец: " + _pointnames[1]; // подсказка маркера
                    m2.ToolTipMode = MarkerTooltipMode.Always; // режим подсказки маркера
                    _currentMarker.IsVisible = false; // скрытие маркера выбора
                    _countPoints++; // добавление счётчика
                    statuslabel.Text = "Можно расчитывать расстояние"; // изменение статуса
                    howto2.Visible = false; // скрытие подсказки по выбору маркера
                }
            }
        }

        private void Resetpointbutton_Click(object sender, EventArgs e)
        {
            if (_countPoints != 0)
            {
            _points.Clear(); // очистить список точек
            _pointnames.Clear(); // очистить список названий точек
            _currentMarker.IsVisible = false; // убрать видимость маркера выбора
            statuslabel.Text = "Пункты не выбраны!"; // изменить статус
            _markerOverlay.Clear(); // очистить наложение маркеров
            karta.Refresh(); // обновить карту
            _countPoints = 0; // сбросить счётчик точек и сообщения
            distancelabel.Text = "Расстояние: не рассчитано"; // текст расстояния
            etalabel.Text = "Время в пути: не рассчитано"; // текст времени в пути
            _routesOverlay.Clear(); // очистить наложение маршрутов
            howto2.Visible = true; // показать подсказку, как выбрать пункт
            }
            else
            {
                MessageBox.Show("Не добавлены точки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        public static TimeSpan CalculateTravelTime(double distance, double speed)
        {
            if (speed <= 0) // если скорость <=0 - вывести ошибку
                throw new ArgumentException("Скорость должна быть больше нуля!");

            double hours = distance / speed; // время = расстояние / скорость
            return TimeSpan.FromHours(hours); // возвращать по умолчанию часы
        }
        private void Getrouteandtime_Click(object sender, EventArgs e)
        {
            if (_countPoints != 2) // если точек меньше 2 - вывести ошибку
            {
                MessageBox.Show("Не добавлены все точки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            statuslabel.Text = "Расчет маршрута..."; // изменение статуса
            var route = OpenStreetMapProvider.Instance.GetRoute( // поиск маршрута: 2 точки, преподчитать ли трассы, пешеходные маршруты, какое увеличение использовать
                _points[0],
                _points[1],
                false,
                false,
                (int)karta.Zoom);
            if (route == null) // если маршрут не найден - вывести ошибку
            {
                MessageBox.Show("Невозможно вычислить маршрут! Проверьте интернет-соединение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                return;
            }
            var r = new GMapRoute(route.Points, "Marshrut") // создание маршрута и использование красного цвета для маршрута
            {
                Stroke = new Pen(Color.Red, 5)
            };
            _routesOverlay.Routes.Add(r); // добавление маршрута на наложение
            karta.ZoomAndCenterRoute(r); // приближение и центрирование маршрута
            statuslabel.Text = "Маршрут построен"; // изменение статуса
            double distance = route.Distance; // обозначение расстояния
            if (_pointnames[0] == "пункт А") _pointnames[0] = "пункта А"; // изменение падежа при стандартных названиях
            if (_pointnames[1] == "пункт B") _pointnames[1] = "пункта B";
            distance = Math.Truncate(distance * 1000) / 1000; // максимум 3 числа после запятой
            double meters = Math.Truncate(distance); // временно берём метры как целое число из расстояния в км
            meters = Math.Round(distance - meters, 3); // расчет метров
            if (meters == 0) // если метры = 0 - строка выглядит так
            {
                distancelabel.Text = "Расстояние от " + _pointnames[0] + " до " + _pointnames[1] + " равно " + distance + " км.";
            }
            else // если метры != 0 - строка выглядит такы
            {
                distancelabel.Text = "Расстояние от " + _pointnames[0] + " до " + _pointnames[1] + " равно " + Math.Truncate(distance) + " км. " + meters * 1000 + " м.";
            }
            if (timecheckbox.Checked == true) // если установлена галочка расчета времени
            {
                try
                {
                    double speed = Convert.ToDouble(speedtextBox.Text); // берём скорость из поля
                    TimeSpan traveltime = CalculateTravelTime(distance, speed); // вызов функции расчёта времени
                    if (traveltime.TotalHours < 1) // если время меньше 1 часа - строка выглядит так
                    {
                        etalabel.Text = $"Примерное время в пути равно: {traveltime.Minutes} мин {traveltime.Seconds} сек";
                    }
                    else if (traveltime.TotalHours < 24) // если время меньше 24 часов
                    {
                        etalabel.Text = $"Примерное время в пути равно: {traveltime.Hours} ч {traveltime.Minutes} мин {traveltime.Seconds} сек";
                    }
                    else // если больше 24 часов
                    {
                        etalabel.Text = $"Примерное время в пути равно: {traveltime.Days} дн {traveltime.Hours} ч {traveltime.Minutes} мин";
                    }

                }
                catch (Exception ex) // если ошибка в функции
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); // вывести сообщение при ошибке
                }
            }
        }



        private void Timecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (timecheckbox.Checked == true) // если поставлена галочка "рассчитать время"
            {
                speedtextBox.Visible = true; // показать поле для ввода скорости
                speedlabel.Visible = true; // показать пометку для ввода скорости
                getrouteandtime.Text = "Рассчитать расстояние и время"; // изменить текст кнопки
            }
            else // если убрана - вернуть все назад
            {
                speedtextBox.Visible = false;
                speedlabel.Visible = false;
                getrouteandtime.Text = "Рассчитать расстояние";
            }
        }

        private void Karta_MouseDown(object sender, MouseEventArgs e) // при нажатии кнопкой мыши на карту
        {

            if (e.Button == MouseButtons.Right && usesearchcb.Checked == false) // если нажата ПКМ и карта не в режиме поиска по словам
            {
                if (_currentMarker.IsVisible == false) { _currentMarker.IsVisible = true; } // если маркер не видим - сделать видимым
                if (_currentMarker.IsVisible) // если маркер видим - вернуть координаты, куда нажата мышь 
                {
                    _currentMarker.Position = karta.FromLocalToLatLng(e.X, e.Y);
                }
            }
        }

        private void Zoomplus_Click(object sender, EventArgs e) // при нажатии на кнопку увлеич. масштаба - увеличить масштаб
        {
            karta.Zoom++;
        }

        private void Zoomminus_Click(object sender, EventArgs e) // при нажатии на кнопку уменьш. масшаба - уменьшить масштаб
        {
            karta.Zoom--;
        }

        private void Karta_OnMapZoomChanged() // при изменении масштаба
        {
            if (karta.Zoom != MinZoom && karta.Zoom != MaxZoom) // если не максимальный и не мин. масшаб - если кнопки выключены - включить
            {
                if (zoomplus.Enabled == false) zoomplus.Enabled = true;
                if (zoomminus.Enabled == false) zoomminus.Enabled = true;
            }
            else if (karta.Zoom == MinZoom) // если масштаб слишком маленький - выключить кнопку уменьшения
            {
                zoomminus.Enabled = false;
                if (zoomplus.Enabled == false) zoomplus.Enabled = true;
            }
            else // если масштаб слишком большой - выкл. кнопку увеличения
            {
                zoomplus.Enabled = false;
                if (zoomminus.Enabled == false) zoomminus.Enabled = true;
            }
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

        private void Exitbutton_Click(object sender, EventArgs e)
        {
            this.Close(); // выйти из программы
        }
        // список необходимых предметов
        private void Addbuttonth_Click(object sender, EventArgs e)
        {
            if (m_dbConn.State != ConnectionState.Open) // если соединение закрыто (нет подключение к БД)
            {
                MessageBox.Show("БД не подключена! Создайте или загрузите БД!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (namethtextBox.Text == String.Empty || amountthtextBox.Text == String.Empty) // если поля пустые
            {
                MessageBox.Show("Не было указано название вещи и/или количество!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                else
                        try
                        {
                            DataTable dTable = new DataTable(); // создание таблицы данных
                            string sqlQuery = @"
                INSERT INTO TravelItems (name, amount)
                VALUES (@name, @amount)"; // запрос SQL на добавление в базу строки
                            using (var command = new SQLiteCommand(sqlQuery, m_dbConn)) // новая команда SQL (с запросом и параметрами подключения)
                            {
                                command.Parameters.AddWithValue("@name", namethtextBox.Text); // параметр в запросе @name = поле nametxhtextbox.text
                                command.Parameters.AddWithValue("@amount", amountthtextBox.Text); // @amount = amountthtextbox.text
                                command.ExecuteNonQuery(); // выполнить команды
                            }
                            sqlQuery = "SELECT * FROM TravelItems"; // запрос на выборку данных
                            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn); // новый адаптер данных
                            adapter.Fill(dTable); // заполнить адаптер данными с таблицы
                            if (dTable.Rows.Count > 0) // если в таблице есть данные
                            {
                                neededdgv.Rows.Clear(); // очистить dgv
                                for (int i = 0; i < dTable.Rows.Count; i++) // заполнение в цикле данными из таблицы
                                    neededdgv.Rows.Add(dTable.Rows[i].ItemArray);
                            }
                            namethtextBox.Text = ""; // сделать поля названия и количества пустыми
                            amountthtextBox.Text = "";
                            dbStatusLabel.Text = "Вещь добавлена в базу данных."; // изменить статус
                        }
                        catch (SQLiteException ex) // при ошибке вывести сообщение об ошибке
                        {
                            MessageBox.Show("Ошибка подключения к БД: " + ex.Message);
                        }
        }

        private void Rembuttonth_Click(object sender, EventArgs e)
        {
            if (m_dbConn.State != ConnectionState.Open) // если соединение закрыто (нет подключение к БД)
            {
                MessageBox.Show("БД не подключена! Создайте или загрузите БД!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (neededdgv.Rows.Count == 0) // если таблица пустая
                {
                    MessageBox.Show("В таблице нет вещей!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (neededdgv.SelectedRows.Count == 0) // если строка не выбрана
                {
                    MessageBox.Show("Строка для удаления не выбрана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if ((MessageBox.Show("Вы уверены, что хотите удалить выбранную вещь?", "Подтвердите операцию", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
                {
                    DataGridViewRow selectedRow = neededdgv.SelectedRows[0]; // обозначение выбранного поля
                    string sqlQuery = "DELETE FROM TravelItems WHERE id = @id"; // запрос SQL на удаление
                    using (var command = new SQLiteCommand(sqlQuery, m_dbConn)) // команда SQL
                    {
                        command.Parameters.AddWithValue("@id", dgvid); // добавить параметр id в запрос
                        command.ExecuteNonQuery(); // выполнить (удалить из БД)
                    }
                    neededdgv.Rows.Remove(selectedRow); // удалить строку из dgv
                    dbStatusLabel.Text = "Вещь удалена из базы данных.";
                }
            }
            catch (Exception ex) { MessageBox.Show("Ошибка удаления строки! " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            neededdgv.Columns.Add("id", "Номер п/п"); // добаить колонки в 
            neededdgv.Columns.Add("namec", "Название вещи");
            neededdgv.Columns.Add("amountc", "Количество");
            neededdgv.Columns["id"].Visible = false; // убрать видимость строки с ИД
            m_dbConn = new SQLiteConnection(); // инициализация соединения
            m_sqlCmd = new SQLiteCommand(); // инициализация команды
            dbStatusLabel.Text = "База данных не подключена. Для создания БД нажмите кнопку 'Создать'."; // статус
        }
        private void Loadbuttonth_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // диалог откытия файла с заголовком и фильтром
            {
                openFileDialog.Filter = "SQLite database (*.sqlite)|*.sqlite|All files (*.*)|*.*";
                openFileDialog.Title = "Загрузить базу данных";
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK) // если нажата клавиша ОК
            {
                try
                {
                    dbFileName = openFileDialog.FileName; // имя файла
                    if (m_dbConn.State != ConnectionState.Open) m_dbConn.Close(); // если есть подключение - отключиться
                    m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;"); // создание соединения
                    m_dbConn.Open(); // открыте соединения
                    savebuttonth.Text = "Сохранить и отключить"; // изменение текста кнопки сохранения
                    dbStatusLabel.Text = "БД успешно подключена.";
                    try
                    {
                        DataTable dTable = new DataTable(); // инициализация таблицы данных
                        string sqlQuery = "SELECT * FROM TravelItems"; // запрос SQL на выборку данных
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn); // инициализация адаптера sqlite
                        adapter.Fill(dTable); // заполнение адаптера данными
                        if (dTable.Rows.Count > 0) // если таблицы не пустая
                            {
                                neededdgv.Rows.Clear(); // очистить dgv
                                for (int i = 0; i < dTable.Rows.Count; i++) // цикл заполнения dgv данными из dtable
                                    neededdgv.Rows.Add(dTable.Rows[i].ItemArray);
                            }
                    }
                    catch (SQLiteException ex)
                    {
                        dbStatusLabel.Text = "Ошибка подключения к БД!";
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void Savebuttonth_Click(object sender, EventArgs e)
        {
            if (m_dbConn.State != ConnectionState.Open) // если нет соединения - кнопка работает как "создать"
            {
                SaveFileDialog sfd = new SaveFileDialog(); // диалог сохранения с заголовком, фильром и расширением
                    {
                        sfd.Title = "Выберите место для сохранения БД";
                        sfd.Filter = "SQLite database (*.sqlite)|*.sqlite|All files (*.*)|*.*";
                        sfd.DefaultExt = "sqlite";
                    }
                    if (sfd.ShowDialog() == DialogResult.OK) // если нажата кнопка ОК
                    {
                        dbFileName = sfd.FileName; // имя файла
                        try
                        {
                            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;"); // новое соединение SQLite
                            m_dbConn.Open(); // открыть соедниение
                            m_sqlCmd.Connection = m_dbConn; // объявление команды для соединения
                            m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS TravelItems (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, amount TEXT)"; // команда для создания таблицы 
                            m_sqlCmd.ExecuteNonQuery(); // выполнить команду
                            dbStatusLabel.Text = "БД успешно создана и подключена.";
                            savebuttonth.Text = "Сохранить и отключить";
                            return;
                        }
                        catch (SQLiteException ex)
                        {
                            dbStatusLabel.Text = "Ошибка подключеня к БД!";
                            MessageBox.Show("Ошибка подключения к БД: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                }
                else { return; }
            }
            else // если БД подключена
            {
                DialogResult result = MessageBox.Show("Хотите выбрать иное место сохранения базы данных? (Сохранить как)", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) // если нажата кнопка "Да"
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog(); // диалог сохранения файла
                    {
                        saveFileDialog.Filter = "SQLite database (*.sqlite)|*.sqlite|All files (*.*)|*.*";
                        saveFileDialog.Title = "Выберите место для сохранения БД";
                        saveFileDialog.DefaultExt = "sqlite";
                    }
                }
                try
                    {
                        m_dbConn.Close(); // закрытие соединения и назначение статуса
                        dbStatusLabel.Text = "БД успешно отключена и сохранена.";
                        savebuttonth.Text = "Создать БД"; // изменение текста на кнопке
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        private void Cleanallbutton_Click(object sender, EventArgs e)
        {
            if (m_dbConn.State != ConnectionState.Open) // Если БД не подключена
            {
                MessageBox.Show("БД не подключена! Создайте или загрузите БД!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (neededdgv.Rows.Count == 0) // если таблица пустая
                {
                    MessageBox.Show("В таблице нет вещей!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (MessageBox.Show("Вы уверены, что хотите очистить список вещей?", "Подтвердите операцию", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        using (var connection = new SQLiteConnection(m_dbConn)) // новое соединение SQLite
                        {
                            m_sqlCmd.Connection = m_dbConn; // обозначение соединения
                            m_sqlCmd.CommandText = "DELETE FROM TravelItems"; // команда SQL для удаления всей таблицы
                            m_sqlCmd.ExecuteNonQuery(); // выполнение командыы
                        }
                        neededdgv.Rows.Clear(); // очистка полей dgv
                        dbStatusLabel.Text = "База данных успешно очищена."; // статус
                    }
                    catch (SQLiteException ex)
                    {
                        dbStatusLabel.Text = "Ошибка подключения к БД!";
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
            }
            catch { MessageBox.Show("Ошибка очистки списка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }

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
    }      
}