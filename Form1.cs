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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GMap.NET.Entity.OpenStreetMapRouteEntity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TravelPlanningAppSusloparov
{
    public partial class Form1 : Form
    {
        public List<PointLatLng> _points; // лист с маркерами
        public List<String> _pointnames; // лист с названиями маркеров
        public GMapOverlay _selmarkOverlay = new GMapOverlay("selmark"); // оверлей со стрелкой-маркером
        public GMapOverlay _markerOverlay = new GMapOverlay("markers"); // оверлей с маркерами
        public GMapOverlay _routesOverlay = new GMapOverlay("_routesOverlay"); // оверлей для маршрутов
        private int _countPoints = 0; // счетчик маркеров
        private bool _isPedestrian = false; // пеший маршрут
        private GMapMarker _currentMarker; // маркер
        private const double DefaultLatitude = 55.742; // стандартная широта
        private const double DefaultLongitude = 37.613; // стандартная долгота
        private const int DefaultZoom = 10; // стандартное увеличение
        private const int MinZoom = 3; // минимальное увеличение
        private const int MaxZoom = 18; // максимальное увеличение
        private const double PedestrianSpeed = 5.5; // средняя скорость пешехода
        private const double DefaultVehicleSpeed = 30; // средняя скорость автомобиля

        public Form1()
        {
            InitializeComponent();
            _points = new List<PointLatLng>(); // создание списка точек
            _pointnames = new List<String>(); // создание списка имен точек
            neededdgv.AllowUserToAddRows = false; // запретить добавлять поля встроенными инструментами datagridview
        }
         
        private void Karta_Load(object sender, EventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;  // режим кеширования карты
            string CacheDirectory = Path.Combine(Directory.GetCurrentDirectory(),"cache"); // папка кеширования карты
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
                _isPedestrian,
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
            statuslabel.Text = "Маршрут расчитан"; // изменение статуса
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
                distancelabel.Text = "Расстояние от " + _pointnames[0] + " до " + _pointnames[1] + " равно " + Math.Truncate(distance) + " км. " + meters*1000 + " м.";
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

        private void PedestriancheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (pedestriancheckBox.Checked == true) // если поставлена галочка "пеший маршрут"
            {
                _isPedestrian = true; // изменить переменную пешехода (истина) и среднюю скорость
                if (timecheckbox.Checked == true) speedtextBox.Text = "5,5"; 
            }
            else { 
                _isPedestrian = false; // изменить переменную пешехода (ложь = автомобиль) и среднюю скорость
                if (timecheckbox.Checked == true) speedtextBox.Text = "30";
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
                howto.Text = "Введите названия исходного пункта для поиска";
                helplabel1.Text = "Вращайте карту левой кнопкой мыши";
                _selmarkOverlay.IsVisibile = false; // убрать видимость маркера выбора
            }
            else
            {
                helplabel1.Text = "Вращайте карту левой кнп. мыши, указание пунктов - правой";
                howto.Text = "Выберите пункт на карте и введите имя точки (необязательно)";
                _selmarkOverlay.IsVisibile = true; // показать маркер выбора
            }
            
        }

        private void Exitbuttonmap_Click(object sender, EventArgs e)
        {
            this.Close(); // выйти из программы
        }
        // список необходимых предметов
        private void Exitbuttonthings_Click(object sender, EventArgs e)
        {
            this.Close(); // выйти из программы
        }

        private void Addbuttonth_Click(object sender, EventArgs e)
        {
            while (true)
            {
                String name = "";
                String amount = "";
                String errorstring = String.Empty;
                int num = neededdgv.Rows.Add();
                if ((namethtextBox.Text == String.Empty) || (amountthtextBox.Text == String.Empty))
                {
                    errorstring = "Не введено название и/или количество! \n";
                }
                else
                {
                    name = namethtextBox.Text;
                    amount = amountthtextBox.Text;
                }
                if (errorstring != String.Empty)
                {
                    MessageBox.Show(errorstring, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                neededdgv.Rows[num].Cells["namec"].Value = name;
                neededdgv.Rows[num].Cells["amountc"].Value = amount;
                break;
            }
        }

        private void Rembuttonth_Click(object sender, EventArgs e)
        {
            try
            {
                if (neededdgv.Rows.Count == 0)
                {
                    MessageBox.Show("В таблице нет вещей!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if ((MessageBox.Show("Вы уверены, что хотите удалить выбранную вещь?", "Подтвердите операцию", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
                {
                    int removerow = neededdgv.SelectedCells[0].RowIndex;
                    neededdgv.Rows.RemoveAt(removerow);
                }
            }
            catch { MessageBox.Show("Ошибка удаления строки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            neededdgv.Columns.Add("namec", "Название вещи");
            neededdgv.Columns.Add("amountc", "Количество");
        }     
        private void Loadbuttonth_Click(object sender, EventArgs e)
        {
            // Создаем диалог открытия файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Загрузить список вещей из текстового файла";
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Очищаем существующие данные
                    neededdgv.Rows.Clear();
                    neededdgv.Columns.Clear();

                    // Читаем файл
                    using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                    {
                        // Читаем первую строку (заголовки столбцов)
                        string headerLine = reader.ReadLine();
                        if (headerLine != null)
                        {
                            // Разделяем строку по табуляции (или другому разделителю)
                            string[] headers = headerLine.Split('\t');

                            // Создаем столбцы в DataGridView
                            foreach (string header in headers)
                            {
                                neededdgv.Columns.Add(header, header);
                            }

                            // Читаем остальные строки (данные)
                            string dataLine;
                            while ((dataLine = reader.ReadLine()) != null)
                            {
                                string[] data = dataLine.Split('\t');
                                neededdgv.Rows.Add(data);
                            }
                        }
                    }

                    MessageBox.Show("Список вещей успешно загружен!", "Успех",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }
        private void Savebuttonth_Click(object sender, EventArgs e)
        {
            // Создаем диалог сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            {
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.Title = "Сохранить список вещей в текстовый файл";
                saveFileDialog.DefaultExt = "txt";
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Создаем StreamWriter для записи в файл
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        // Записываем заголовки столбцов
                        for (int i = 0; i < neededdgv.Columns.Count; i++)
                        {
                            writer.Write(neededdgv.Columns[i].HeaderText);
                            if (i < neededdgv.Columns.Count - 1)
                            {
                                writer.Write("\t"); // Табуляция как разделитель
                            }
                        }
                        writer.WriteLine();



                        // Записываем данные из строк
                        foreach (DataGridViewRow row in neededdgv.Rows)
                        {
                            if (!row.IsNewRow) // Пропускаем новую строку, если она есть
                            {
                                for (int i = 0; i < neededdgv.Columns.Count; i++)
                                {
                                    writer.Write(row.Cells[i].Value?.ToString() ?? "");
                                    if (i < neededdgv.Columns.Count - 1)
                                    {
                                        writer.Write("\t"); // Табуляция как разделитель
                                    }
                                }
                                writer.WriteLine();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            MessageBox.Show("Список вещей успешно сохранен!", "Успех",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        private void Cleanallbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (neededdgv.Rows.Count == 0)
                {
                    MessageBox.Show("В таблице нет вещей!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (MessageBox.Show("Вы уверены, что хотите очистить список вещей?", "Подтвердите операцию", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    neededdgv.Rows.Clear();
                }
            }
            catch { MessageBox.Show("Ошибка очистки списка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
    }
}
// баги/задачи: 
// 1) добавить комментарии к реализации списка вещей и исправить дипсиковские комменты к реализации
// 2) сделать базу sqlite для вещей