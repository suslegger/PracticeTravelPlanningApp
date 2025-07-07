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
        public GMapOverlay _selmarkOverlay = new GMapOverlay("selmark"); // оверлей с маркерами
        public GMapOverlay _markerOverlay = new GMapOverlay("markers"); // оверлей с маркерами
        public GMapOverlay _routesOverlay = new GMapOverlay("_routesOverlay");
        private int countpoints = 0; // счетчик маркеров
        private bool ispedestrian = false; // пеший маршрут
        private GMapMarker _currentMarker; // маркер
        private const double DefaultLatitude = 55.742;
        private const double DefaultLongitude = 37.613;
        private const int DefaultZoom = 10;
        private const int MinZoom = 3;
        private const int MaxZoom = 18;
        private const double PedestrianSpeed = 5.5;
        private const double DefaultVehicleSpeed = 30;
        public Form1()
        {
            InitializeComponent();
            _points = new List<PointLatLng>(); // создание списка точек
            _pointnames = new List<String>(); // создание списка имен точек
            neededdgv.AllowUserToAddRows = false; // запретить добавлять поля встроенными инструментами datagridview
        }
         
        private void Karta_Load(object sender, EventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.CacheOnly;  // кеширование карты
            string CacheDirectory = Path.Combine(Directory.GetCurrentDirectory(),"mapcache");
            if (!Directory.Exists(CacheDirectory)) Directory.CreateDirectory(CacheDirectory);
            karta.CacheLocation = CacheDirectory;
            karta.DragButton = MouseButtons.Left; // лкм для перемещения карты
            karta.MapProvider = GMapProviders.OpenStreetMap; // провайдер карты
            karta.Position = new PointLatLng(DefaultLatitude, DefaultLongitude); // установка позиции карты
            karta.MinZoom = MinZoom; // минимальное увеличение
            karta.MaxZoom = MaxZoom; // максимальное увеличение
            karta.Zoom = DefaultZoom; // увеличение по умолчанию
            karta.Overlays.Add(_selmarkOverlay); // наложение маркера выбора
            karta.Overlays.Add(_markerOverlay); // наложение маркеров местоположения
            karta.Overlays.Add(_routesOverlay);
            _currentMarker = new GMarkerGoogle(karta.Position, GMarkerGoogleType.arrow); { _currentMarker.IsVisible = false; } // отключить видимость по умолчанию
            _selmarkOverlay.Markers.Add(_currentMarker); // добавление маркера
            karta.ShowCenter = false; // убрать центровой маркер
        }


        private void Addpointbutton_Click(object sender, EventArgs e)
        {
            if (countpoints >= 2)
            {
                MessageBox.Show("Невозможно добавить более чем 2 точки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_currentMarker.IsVisible == false && usesearchcb.Checked == false)
            {
                MessageBox.Show("Не выбрана точка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            bool isfailed = false; // счетчик ошибки для поиска
            GMapMarker m1 = null, m2 = null; // инициализация точек
            PointLatLng markerpos;
            if (countpoints == 0) // если нет точек
                    {
                        if (usesearchcb.Checked == true) // если используется поиск (установлена галочка)
                        {
                            karta.GetPositionByKeywords(nametextbox.Text, out markerpos); // попытка поиска
                            if (markerpos.ToString() == "{Lat=0, Lng=0}")
                            { // если место не найдено, отобразить окно и включить счётчик ошибок
                                MessageBox.Show("Не удалось найти данное место", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                isfailed = true;
                            }
                            else
                            {
                                karta.Position = markerpos; // расположить карту в этом месте
                                m1 = new GMarkerGoogle(new PointLatLng(karta.Position.Lat, karta.Position.Lng), GMarkerGoogleType.green_dot); // добавление маркера
                                if (karta.Zoom < 12) karta.Zoom = 12;
                            }
                        }
                        else
                        {
                            markerpos = new PointLatLng(_currentMarker.Position.Lat, _currentMarker.Position.Lng);
                            m1 = new GMarkerGoogle(markerpos, GMarkerGoogleType.green_dot); // добавление маркера по выбору на карте
                            karta.Position = markerpos;
                            if (karta.Zoom < 12) karta.Zoom = 12;
                        }
                        if (isfailed == false)
                        {
                            _points.Add(markerpos);
                            countpoints++;
                            _markerOverlay.Markers.Add(m1); // добавление пиктограммы на карту
                            if (nametextbox.Text != String.Empty) _pointnames.Add(nametextbox.Text); // добавление названия
                            else _pointnames.Add("пункт А");
                            m1.ToolTipText = "Начало: " + _pointnames[0]; // подсказка пиктограммы
                            m1.ToolTipMode = MarkerTooltipMode.Always;
                            statuslabel.Text = "Выбран только исходный пункт!";
                        }
                    }
            else
                    {
                        if (usesearchcb.Checked == true) // 
                        {
                            karta.GetPositionByKeywords(nametextbox.Text, out markerpos);
                            if (markerpos.ToString() == "{Lat=0, Lng=0}")
                            {
                                MessageBox.Show("Не удалось найти данное место", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                isfailed = true;
                            }
                            else
                            {
                                karta.Position = markerpos;
                                m2 = new GMarkerGoogle(new PointLatLng(karta.Position.Lat, karta.Position.Lng), GMarkerGoogleType.red_dot);
                                karta.ZoomAndCenterMarkers("markers");
                            }
                        }
                        else
                        {
                            markerpos = new PointLatLng(_currentMarker.Position.Lat, _currentMarker.Position.Lng);
                            m2 = new GMarkerGoogle(new PointLatLng(_currentMarker.Position.Lat, _currentMarker.Position.Lng), GMarkerGoogleType.red_dot);
                            karta.Position = markerpos;
                            karta.ZoomAndCenterMarkers("markers");
                        }
                        if (isfailed == false)
                        {
                            _points.Add(markerpos);
                            _markerOverlay.Markers.Add(m2);
                            if (nametextbox.Text != String.Empty) _pointnames.Add(nametextbox.Text);
                            else _pointnames.Add("пункт B");
                            m2.ToolTipText = "Конец: " + _pointnames[1];
                            m2.ToolTipMode = MarkerTooltipMode.Always;
                            _currentMarker.IsVisible = false;
                            countpoints++;
                            statuslabel.Text = "Можно расчитывать расстояние";
                            howto2.Visible = false;
                        }
                    }
                }
            

        private void Resetpointbutton_Click(object sender, EventArgs e)
        {
            _points.Clear();
            _pointnames.Clear();
            _currentMarker.IsVisible = false;
            statuslabel.Text = "Пункты не выбраны!";
            _markerOverlay.Clear();
            karta.Refresh();
            countpoints = 0;
            distancelabel.Text = "Расстояние: не рассчитано";
            etalabel.Text = "Время в пути: не рассчитано";
            _routesOverlay.Clear();
            howto2.Visible = true;
        }
        public static TimeSpan CalculateTravelTime(double distance, double speed)
        {
            if (speed <= 0)
                throw new ArgumentException("Скорость должна быть больше нуля", nameof(speed));

            double hours = distance / speed;
            return TimeSpan.FromHours(hours);
        }
        private void Getrouteandtime_Click(object sender, EventArgs e)
        {
            if (countpoints != 2)
            {
                MessageBox.Show("Не добавлены все точки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                   statuslabel.Text = "Расчет маршрута...";

                    var route = OpenStreetMapProvider.Instance.GetRoute(_points[0], _points[1], false, ispedestrian, (int)karta.Zoom);
                    var r = new GMapRoute(route.Points, "Marshrut")
                    {
                        Stroke = new Pen(Color.Red, 5)
                    };
                    _routesOverlay.Routes.Add(r);
                    karta.ZoomAndCenterRoute(r);
                    statuslabel.Text = "Маршрут расчитан";
                    double distance = route.Distance;
                    if (Convert.ToString(distance) == "0") MessageBox.Show("Невозможно вычислить маршрут!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (_pointnames[0] == "пункт А") _pointnames[0] = "пункта А";
                    if (_pointnames[1] == "пункт B") _pointnames[1] = "пункта B";
                    distancelabel.Text = "Расстояние от " + _pointnames[0] + " до " + _pointnames[1] + " равно " + distance + " км.";
                    if (timecheckbox.Checked == true)
                    {
                        try
                        {
                            double speed = Convert.ToDouble(speedtextBox.Text);
                            TimeSpan traveltime = CalculateTravelTime(distance, speed);
                            if (traveltime.TotalHours < 1)
                            {
                                etalabel.Text = $"Примерное время в пути равно: {traveltime.Minutes} мин";
                            }
                            else if (traveltime.TotalHours < 24)
                            {
                                etalabel.Text = $"Примерное время в пути равно: {traveltime.Hours} ч {traveltime.Minutes} мин";
                            }
                            else
                            {
                                etalabel.Text = $"Примерное время в пути равно: {traveltime.Days} дн {traveltime.Hours} ч";
                            }

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Было введено неправильное число!");
                        }
                    }
            }
            catch (Exception) { MessageBox.Show("Ошибка построения маршрута! Либо отсутствует интернет-соединение, либо введены неверные данные!"); }
        }


        private void Timecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (timecheckbox.Checked == true)
            {
                speedtextBox.Visible = true;
                speedlabel.Visible = true;
                getrouteandtime.Text = "Рассчитать расстояние и время";
            }
            else
            {
                speedtextBox.Visible = false;
                speedlabel.Visible = false;
                getrouteandtime.Text = "Рассчитать расстояние";
            }
        }

        private void PedestriancheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (pedestriancheckBox.Checked == true)
            {
                ispedestrian = true;
                if (timecheckbox.Checked == true) speedtextBox.Text = "5,5";
            }
            else { 
                ispedestrian = false;
                if (timecheckbox.Checked == true) speedtextBox.Text = "30";
            }
            
        }

        private void Karta_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Right && usesearchcb.Checked == false)
            {
                if (_currentMarker.IsVisible == false) { _currentMarker.IsVisible = true; }
                if (_currentMarker.IsVisible)
                {
                    _currentMarker.Position = karta.FromLocalToLatLng(e.X, e.Y);
                }
            }
        }

        private void Zoomplus_Click(object sender, EventArgs e)
        {
            karta.Zoom = karta.Zoom++;
        }

        private void Zoomminus_Click(object sender, EventArgs e)
        {
            karta.Zoom = karta.Zoom--;
        }

        private void Karta_OnMapZoomChanged()
        {
            if (karta.Zoom == karta.MinZoom)
            {
                zoomminus.Enabled = false;
                if (zoomplus.Enabled == false) zoomplus.Enabled = true;
            }
            if (karta.Zoom == karta.MaxZoom)
            {
                zoomplus.Enabled = false;
                if (zoomminus.Enabled == false) zoomminus.Enabled = true;
            }
        }

        private void Usesearchcb_CheckedChanged(object sender, EventArgs e)
        {
            if (usesearchcb.Checked == true)
            {
                howto.Text = "Введите названия исходного пункта для поиска";
                helplabel1.Text = "Вращайте карту левой кнопкой мыши";
                _selmarkOverlay.IsVisibile = false;
            }
            else
            {
                helplabel1.Text = "Вращайте карту левой кнп. мыши, указание пунктов - правой";
                howto.Text = "Выберите пункт на карте и введите имя точки (необязательно)";
                _selmarkOverlay.IsVisibile = true;
            }
            
        }

        private void Exitbuttonmap_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // список необходимых предметов
        private void Exitbuttonthings_Click(object sender, EventArgs e)
        {
            this.Close();
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
// баги: 
// 1) добавить комментарии и исправить дипсиковские комменты
// 2) пересмотреть функцию поиска местоположения (if else) (в самом конце)
// 3) мб сделать базу sqlite для вещей?