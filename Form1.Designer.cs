namespace TravelPlanningAppSusloparov
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.addpointbutton = new System.Windows.Forms.Button();
            this.nametextbox = new System.Windows.Forms.TextBox();
            this.karta = new GMap.NET.WindowsForms.GMapControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.howto2 = new System.Windows.Forms.Label();
            this.exitbuttonmap = new System.Windows.Forms.Button();
            this.usesearchcb = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.zoomminus = new System.Windows.Forms.Button();
            this.zoomplus = new System.Windows.Forms.Button();
            this.etalabel = new System.Windows.Forms.Label();
            this.distancelabel = new System.Windows.Forms.Label();
            this.pedestriancheckBox = new System.Windows.Forms.CheckBox();
            this.timecheckbox = new System.Windows.Forms.CheckBox();
            this.resetpointbutton = new System.Windows.Forms.Button();
            this.getrouteandtime = new System.Windows.Forms.Button();
            this.statuslabel = new System.Windows.Forms.Label();
            this.speedtextBox = new System.Windows.Forms.TextBox();
            this.speedlabel = new System.Windows.Forms.Label();
            this.howto = new System.Windows.Forms.Label();
            this.helplabel1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dbStatusLabel = new System.Windows.Forms.Label();
            this.cleanallbutton = new System.Windows.Forms.Button();
            this.loadbuttonth = new System.Windows.Forms.Button();
            this.savebuttonth = new System.Windows.Forms.Button();
            this.rembuttonth = new System.Windows.Forms.Button();
            this.addbuttonth = new System.Windows.Forms.Button();
            this.amountthtextBox = new System.Windows.Forms.TextBox();
            this.namethtextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.exitbuttonthings = new System.Windows.Forms.Button();
            this.neededdgv = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neededdgv)).BeginInit();
            this.SuspendLayout();
            // 
            // addpointbutton
            // 
            this.addpointbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addpointbutton.Location = new System.Drawing.Point(395, 119);
            this.addpointbutton.Margin = new System.Windows.Forms.Padding(2);
            this.addpointbutton.Name = "addpointbutton";
            this.addpointbutton.Size = new System.Drawing.Size(102, 45);
            this.addpointbutton.TabIndex = 0;
            this.addpointbutton.Text = "Выбрать пункт";
            this.addpointbutton.UseVisualStyleBackColor = true;
            this.addpointbutton.Click += new System.EventHandler(this.Addpointbutton_Click);
            // 
            // nametextbox
            // 
            this.nametextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nametextbox.Location = new System.Drawing.Point(360, 43);
            this.nametextbox.Margin = new System.Windows.Forms.Padding(2);
            this.nametextbox.Name = "nametextbox";
            this.nametextbox.Size = new System.Drawing.Size(138, 20);
            this.nametextbox.TabIndex = 1;
            // 
            // karta
            // 
            this.karta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.karta.Bearing = 0F;
            this.karta.CanDragMap = true;
            this.karta.EmptyTileColor = System.Drawing.Color.Navy;
            this.karta.GrayScaleMode = false;
            this.karta.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.karta.LevelsKeepInMemory = 5;
            this.karta.Location = new System.Drawing.Point(15, 15);
            this.karta.Margin = new System.Windows.Forms.Padding(2);
            this.karta.MarkersEnabled = true;
            this.karta.MaxZoom = 2;
            this.karta.MinZoom = 2;
            this.karta.MouseWheelZoomEnabled = true;
            this.karta.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.karta.Name = "karta";
            this.karta.NegativeMode = false;
            this.karta.PolygonsEnabled = true;
            this.karta.RetryLoadTile = 0;
            this.karta.RoutesEnabled = true;
            this.karta.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.karta.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.karta.ShowTileGridLines = false;
            this.karta.Size = new System.Drawing.Size(324, 419);
            this.karta.TabIndex = 2;
            this.karta.Zoom = 0D;
            this.karta.OnMapZoomChanged += new GMap.NET.MapZoomChanged(this.Karta_OnMapZoomChanged);
            this.karta.Load += new System.EventHandler(this.Karta_Load);
            this.karta.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Karta_MouseDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(9, 10);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(696, 470);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.howto2);
            this.tabPage1.Controls.Add(this.exitbuttonmap);
            this.tabPage1.Controls.Add(this.usesearchcb);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.zoomminus);
            this.tabPage1.Controls.Add(this.zoomplus);
            this.tabPage1.Controls.Add(this.etalabel);
            this.tabPage1.Controls.Add(this.distancelabel);
            this.tabPage1.Controls.Add(this.pedestriancheckBox);
            this.tabPage1.Controls.Add(this.timecheckbox);
            this.tabPage1.Controls.Add(this.resetpointbutton);
            this.tabPage1.Controls.Add(this.getrouteandtime);
            this.tabPage1.Controls.Add(this.statuslabel);
            this.tabPage1.Controls.Add(this.speedtextBox);
            this.tabPage1.Controls.Add(this.speedlabel);
            this.tabPage1.Controls.Add(this.howto);
            this.tabPage1.Controls.Add(this.helplabel1);
            this.tabPage1.Controls.Add(this.karta);
            this.tabPage1.Controls.Add(this.addpointbutton);
            this.tabPage1.Controls.Add(this.nametextbox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(688, 444);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Расчет расстояний и времени";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // howto2
            // 
            this.howto2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.howto2.AutoSize = true;
            this.howto2.Location = new System.Drawing.Point(348, 98);
            this.howto2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.howto2.Name = "howto2";
            this.howto2.Size = new System.Drawing.Size(340, 13);
            this.howto2.TabIndex = 20;
            this.howto2.Text = "Для подтверждения выбора нажмите на кнопку \"Выбрать пункт\"";
            // 
            // exitbuttonmap
            // 
            this.exitbuttonmap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exitbuttonmap.Location = new System.Drawing.Point(578, 367);
            this.exitbuttonmap.Margin = new System.Windows.Forms.Padding(2);
            this.exitbuttonmap.Name = "exitbuttonmap";
            this.exitbuttonmap.Size = new System.Drawing.Size(82, 39);
            this.exitbuttonmap.TabIndex = 19;
            this.exitbuttonmap.Text = "Выйти";
            this.exitbuttonmap.UseVisualStyleBackColor = true;
            this.exitbuttonmap.Click += new System.EventHandler(this.Exitbutton_Click);
            // 
            // usesearchcb
            // 
            this.usesearchcb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.usesearchcb.AutoSize = true;
            this.usesearchcb.Location = new System.Drawing.Point(351, 76);
            this.usesearchcb.Margin = new System.Windows.Forms.Padding(2);
            this.usesearchcb.Name = "usesearchcb";
            this.usesearchcb.Size = new System.Drawing.Size(327, 17);
            this.usesearchcb.TabIndex = 18;
            this.usesearchcb.Text = "Вместо выбора на карте использовать поиск по названию";
            this.usesearchcb.UseVisualStyleBackColor = true;
            this.usesearchcb.CheckedChanged += new System.EventHandler(this.Usesearchcb_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(358, 367);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Масштаб - колесом мыши или кнопками";
            // 
            // zoomminus
            // 
            this.zoomminus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomminus.Location = new System.Drawing.Point(316, 46);
            this.zoomminus.Margin = new System.Windows.Forms.Padding(2);
            this.zoomminus.Name = "zoomminus";
            this.zoomminus.Size = new System.Drawing.Size(15, 19);
            this.zoomminus.TabIndex = 16;
            this.zoomminus.Text = "-";
            this.zoomminus.UseVisualStyleBackColor = true;
            this.zoomminus.Click += new System.EventHandler(this.Zoomminus_Click);
            // 
            // zoomplus
            // 
            this.zoomplus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomplus.Location = new System.Drawing.Point(316, 23);
            this.zoomplus.Margin = new System.Windows.Forms.Padding(2);
            this.zoomplus.Name = "zoomplus";
            this.zoomplus.Size = new System.Drawing.Size(15, 19);
            this.zoomplus.TabIndex = 15;
            this.zoomplus.Text = "+";
            this.zoomplus.UseVisualStyleBackColor = true;
            this.zoomplus.Click += new System.EventHandler(this.Zoomplus_Click);
            // 
            // etalabel
            // 
            this.etalabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.etalabel.AutoSize = true;
            this.etalabel.Location = new System.Drawing.Point(358, 309);
            this.etalabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.etalabel.Name = "etalabel";
            this.etalabel.Size = new System.Drawing.Size(153, 13);
            this.etalabel.TabIndex = 14;
            this.etalabel.Text = "Время в пути: не рассчитано";
            // 
            // distancelabel
            // 
            this.distancelabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.distancelabel.AutoSize = true;
            this.distancelabel.Location = new System.Drawing.Point(358, 282);
            this.distancelabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.distancelabel.Name = "distancelabel";
            this.distancelabel.Size = new System.Drawing.Size(146, 13);
            this.distancelabel.TabIndex = 13;
            this.distancelabel.Text = "Расстояние: не рассчитано";
            // 
            // pedestriancheckBox
            // 
            this.pedestriancheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pedestriancheckBox.AutoSize = true;
            this.pedestriancheckBox.Location = new System.Drawing.Point(523, 180);
            this.pedestriancheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.pedestriancheckBox.Name = "pedestriancheckBox";
            this.pedestriancheckBox.Size = new System.Drawing.Size(160, 17);
            this.pedestriancheckBox.TabIndex = 12;
            this.pedestriancheckBox.Text = "Навигация для пешеходов";
            this.pedestriancheckBox.UseVisualStyleBackColor = true;
            this.pedestriancheckBox.CheckedChanged += new System.EventHandler(this.PedestriancheckBox_CheckedChanged);
            // 
            // timecheckbox
            // 
            this.timecheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timecheckbox.AutoSize = true;
            this.timecheckbox.Location = new System.Drawing.Point(346, 180);
            this.timecheckbox.Margin = new System.Windows.Forms.Padding(2);
            this.timecheckbox.Name = "timecheckbox";
            this.timecheckbox.Size = new System.Drawing.Size(171, 17);
            this.timecheckbox.TabIndex = 11;
            this.timecheckbox.Text = "Рассчитать время прибытия";
            this.timecheckbox.UseVisualStyleBackColor = true;
            this.timecheckbox.CheckedChanged += new System.EventHandler(this.Timecheckbox_CheckedChanged);
            // 
            // resetpointbutton
            // 
            this.resetpointbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetpointbutton.Location = new System.Drawing.Point(533, 119);
            this.resetpointbutton.Margin = new System.Windows.Forms.Padding(2);
            this.resetpointbutton.Name = "resetpointbutton";
            this.resetpointbutton.Size = new System.Drawing.Size(96, 45);
            this.resetpointbutton.TabIndex = 10;
            this.resetpointbutton.Text = "Сбросить пункт(ы)";
            this.resetpointbutton.UseVisualStyleBackColor = true;
            this.resetpointbutton.Click += new System.EventHandler(this.Resetpointbutton_Click);
            // 
            // getrouteandtime
            // 
            this.getrouteandtime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.getrouteandtime.Location = new System.Drawing.Point(478, 234);
            this.getrouteandtime.Margin = new System.Windows.Forms.Padding(2);
            this.getrouteandtime.Name = "getrouteandtime";
            this.getrouteandtime.Size = new System.Drawing.Size(182, 32);
            this.getrouteandtime.TabIndex = 9;
            this.getrouteandtime.Text = "Рассчитать расcтояние";
            this.getrouteandtime.UseVisualStyleBackColor = true;
            this.getrouteandtime.Click += new System.EventHandler(this.Getrouteandtime_Click);
            // 
            // statuslabel
            // 
            this.statuslabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statuslabel.AutoSize = true;
            this.statuslabel.Location = new System.Drawing.Point(514, 46);
            this.statuslabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.statuslabel.Name = "statuslabel";
            this.statuslabel.Size = new System.Drawing.Size(112, 13);
            this.statuslabel.TabIndex = 8;
            this.statuslabel.Text = "Пункты не выбраны!";
            // 
            // speedtextBox
            // 
            this.speedtextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.speedtextBox.Location = new System.Drawing.Point(358, 240);
            this.speedtextBox.Margin = new System.Windows.Forms.Padding(2);
            this.speedtextBox.Name = "speedtextBox";
            this.speedtextBox.Size = new System.Drawing.Size(103, 20);
            this.speedtextBox.TabIndex = 7;
            this.speedtextBox.Text = "30";
            this.speedtextBox.Visible = false;
            // 
            // speedlabel
            // 
            this.speedlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.speedlabel.AutoSize = true;
            this.speedlabel.Location = new System.Drawing.Point(356, 210);
            this.speedlabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.speedlabel.Name = "speedlabel";
            this.speedlabel.Size = new System.Drawing.Size(261, 13);
            this.speedlabel.TabIndex = 6;
            this.speedlabel.Text = "Введите среднюю скорость передвижения в км/ч";
            this.speedlabel.Visible = false;
            // 
            // howto
            // 
            this.howto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.howto.AutoSize = true;
            this.howto.Location = new System.Drawing.Point(358, 15);
            this.howto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.howto.Name = "howto";
            this.howto.Size = new System.Drawing.Size(328, 13);
            this.howto.TabIndex = 5;
            this.howto.Text = "Выберите пункт на карте и введите имя точки (необязательно)";
            // 
            // helplabel1
            // 
            this.helplabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helplabel1.AutoSize = true;
            this.helplabel1.Location = new System.Drawing.Point(358, 341);
            this.helplabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.helplabel1.Name = "helplabel1";
            this.helplabel1.Size = new System.Drawing.Size(320, 13);
            this.helplabel1.TabIndex = 4;
            this.helplabel1.Text = "Вращайте карту левой кнп. мыши, указание пунктов - правой";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dbStatusLabel);
            this.tabPage2.Controls.Add(this.cleanallbutton);
            this.tabPage2.Controls.Add(this.loadbuttonth);
            this.tabPage2.Controls.Add(this.savebuttonth);
            this.tabPage2.Controls.Add(this.rembuttonth);
            this.tabPage2.Controls.Add(this.addbuttonth);
            this.tabPage2.Controls.Add(this.amountthtextBox);
            this.tabPage2.Controls.Add(this.namethtextBox);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.exitbuttonthings);
            this.tabPage2.Controls.Add(this.neededdgv);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(688, 444);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Список необходимых вещей";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dbStatusLabel
            // 
            this.dbStatusLabel.AutoSize = true;
            this.dbStatusLabel.Location = new System.Drawing.Point(13, 421);
            this.dbStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dbStatusLabel.Name = "dbStatusLabel";
            this.dbStatusLabel.Size = new System.Drawing.Size(151, 13);
            this.dbStatusLabel.TabIndex = 30;
            this.dbStatusLabel.Text = "База данных не подключена";
            // 
            // cleanallbutton
            // 
            this.cleanallbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cleanallbutton.Location = new System.Drawing.Point(580, 327);
            this.cleanallbutton.Margin = new System.Windows.Forms.Padding(2);
            this.cleanallbutton.Name = "cleanallbutton";
            this.cleanallbutton.Size = new System.Drawing.Size(82, 39);
            this.cleanallbutton.TabIndex = 29;
            this.cleanallbutton.Text = "Очистить список";
            this.cleanallbutton.UseVisualStyleBackColor = true;
            this.cleanallbutton.Click += new System.EventHandler(this.Cleanallbutton_Click);
            // 
            // loadbuttonth
            // 
            this.loadbuttonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadbuttonth.Location = new System.Drawing.Point(471, 381);
            this.loadbuttonth.Margin = new System.Windows.Forms.Padding(2);
            this.loadbuttonth.Name = "loadbuttonth";
            this.loadbuttonth.Size = new System.Drawing.Size(82, 39);
            this.loadbuttonth.TabIndex = 28;
            this.loadbuttonth.Text = "Загрузить БД";
            this.loadbuttonth.UseVisualStyleBackColor = true;
            this.loadbuttonth.Click += new System.EventHandler(this.Loadbuttonth_Click);
            // 
            // savebuttonth
            // 
            this.savebuttonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.savebuttonth.Location = new System.Drawing.Point(471, 327);
            this.savebuttonth.Margin = new System.Windows.Forms.Padding(2);
            this.savebuttonth.Name = "savebuttonth";
            this.savebuttonth.Size = new System.Drawing.Size(82, 39);
            this.savebuttonth.TabIndex = 27;
            this.savebuttonth.Text = "Создать БД";
            this.savebuttonth.UseVisualStyleBackColor = true;
            this.savebuttonth.Click += new System.EventHandler(this.Savebuttonth_Click);
            // 
            // rembuttonth
            // 
            this.rembuttonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rembuttonth.Location = new System.Drawing.Point(363, 380);
            this.rembuttonth.Margin = new System.Windows.Forms.Padding(2);
            this.rembuttonth.Name = "rembuttonth";
            this.rembuttonth.Size = new System.Drawing.Size(82, 39);
            this.rembuttonth.TabIndex = 26;
            this.rembuttonth.Text = "Удалить";
            this.rembuttonth.UseVisualStyleBackColor = true;
            this.rembuttonth.Click += new System.EventHandler(this.Rembuttonth_Click);
            // 
            // addbuttonth
            // 
            this.addbuttonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addbuttonth.Location = new System.Drawing.Point(363, 327);
            this.addbuttonth.Margin = new System.Windows.Forms.Padding(2);
            this.addbuttonth.Name = "addbuttonth";
            this.addbuttonth.Size = new System.Drawing.Size(82, 39);
            this.addbuttonth.TabIndex = 25;
            this.addbuttonth.Text = "Добавить";
            this.addbuttonth.UseVisualStyleBackColor = true;
            this.addbuttonth.Click += new System.EventHandler(this.Addbuttonth_Click);
            // 
            // amountthtextBox
            // 
            this.amountthtextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.amountthtextBox.Location = new System.Drawing.Point(118, 384);
            this.amountthtextBox.Margin = new System.Windows.Forms.Padding(2);
            this.amountthtextBox.Name = "amountthtextBox";
            this.amountthtextBox.Size = new System.Drawing.Size(212, 20);
            this.amountthtextBox.TabIndex = 24;
            // 
            // namethtextBox
            // 
            this.namethtextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.namethtextBox.Location = new System.Drawing.Point(118, 337);
            this.namethtextBox.Margin = new System.Windows.Forms.Padding(2);
            this.namethtextBox.Name = "namethtextBox";
            this.namethtextBox.Size = new System.Drawing.Size(212, 20);
            this.namethtextBox.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 384);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Количество";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 340);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Название вещи";
            // 
            // exitbuttonthings
            // 
            this.exitbuttonthings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exitbuttonthings.Location = new System.Drawing.Point(580, 381);
            this.exitbuttonthings.Margin = new System.Windows.Forms.Padding(2);
            this.exitbuttonthings.Name = "exitbuttonthings";
            this.exitbuttonthings.Size = new System.Drawing.Size(82, 39);
            this.exitbuttonthings.TabIndex = 20;
            this.exitbuttonthings.Text = "Выйти";
            this.exitbuttonthings.UseVisualStyleBackColor = true;
            this.exitbuttonthings.Click += new System.EventHandler(this.Exitbutton_Click);
            // 
            // neededdgv
            // 
            this.neededdgv.AllowUserToAddRows = false;
            this.neededdgv.AllowUserToDeleteRows = false;
            this.neededdgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.neededdgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.neededdgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.neededdgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.neededdgv.Location = new System.Drawing.Point(31, 28);
            this.neededdgv.Margin = new System.Windows.Forms.Padding(2);
            this.neededdgv.MultiSelect = false;
            this.neededdgv.Name = "neededdgv";
            this.neededdgv.ReadOnly = true;
            this.neededdgv.RowHeadersWidth = 51;
            this.neededdgv.RowTemplate.Height = 24;
            this.neededdgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.neededdgv.ShowEditingIcon = false;
            this.neededdgv.Size = new System.Drawing.Size(632, 288);
            this.neededdgv.TabIndex = 0;
            this.neededdgv.SelectionChanged += new System.EventHandler(this.NeededDGV_SelectionChanged);
            this.neededdgv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Karta_MouseDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 490);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(603, 493);
            this.Name = "MainForm";
            this.Text = "Программа для планирования путешествий";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.neededdgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addpointbutton;
        private System.Windows.Forms.TextBox nametextbox;
        private GMap.NET.WindowsForms.GMapControl karta;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label howto;
        private System.Windows.Forms.Label helplabel1;
        private System.Windows.Forms.Label statuslabel;
        private System.Windows.Forms.TextBox speedtextBox;
        private System.Windows.Forms.Label speedlabel;
        private System.Windows.Forms.Button getrouteandtime;
        private System.Windows.Forms.Button resetpointbutton;
        private System.Windows.Forms.CheckBox pedestriancheckBox;
        private System.Windows.Forms.CheckBox timecheckbox;
        private System.Windows.Forms.Label etalabel;
        private System.Windows.Forms.Label distancelabel;
        private System.Windows.Forms.Button zoomplus;
        private System.Windows.Forms.Button zoomminus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox usesearchcb;
        private System.Windows.Forms.DataGridView neededdgv;
        private System.Windows.Forms.Button exitbuttonmap;
        private System.Windows.Forms.Button exitbuttonthings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button loadbuttonth;
        private System.Windows.Forms.Button savebuttonth;
        private System.Windows.Forms.Button rembuttonth;
        private System.Windows.Forms.Button addbuttonth;
        private System.Windows.Forms.TextBox amountthtextBox;
        private System.Windows.Forms.TextBox namethtextBox;
        private System.Windows.Forms.Button cleanallbutton;
        private System.Windows.Forms.Label howto2;
        private System.Windows.Forms.Label dbStatusLabel;
    }
}

