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
            this.addpointbutton.Location = new System.Drawing.Point(527, 146);
            this.addpointbutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addpointbutton.Name = "addpointbutton";
            this.addpointbutton.Size = new System.Drawing.Size(136, 55);
            this.addpointbutton.TabIndex = 0;
            this.addpointbutton.Text = "Выбрать пункт";
            this.addpointbutton.UseVisualStyleBackColor = true;
            this.addpointbutton.Click += new System.EventHandler(this.Addpointbutton_Click);
            // 
            // nametextbox
            // 
            this.nametextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nametextbox.Location = new System.Drawing.Point(480, 53);
            this.nametextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nametextbox.Name = "nametextbox";
            this.nametextbox.Size = new System.Drawing.Size(183, 22);
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
            this.karta.Location = new System.Drawing.Point(20, 18);
            this.karta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.karta.Size = new System.Drawing.Size(432, 516);
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
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(928, 578);
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
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(920, 549);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Расчет расстояний и времени";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // howto2
            // 
            this.howto2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.howto2.AutoSize = true;
            this.howto2.Location = new System.Drawing.Point(464, 121);
            this.howto2.Name = "howto2";
            this.howto2.Size = new System.Drawing.Size(427, 16);
            this.howto2.TabIndex = 20;
            this.howto2.Text = "Для подтверждения выбора нажмите на кнопку \"Выбрать пункт\"";
            // 
            // exitbuttonmap
            // 
            this.exitbuttonmap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exitbuttonmap.Location = new System.Drawing.Point(771, 452);
            this.exitbuttonmap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.exitbuttonmap.Name = "exitbuttonmap";
            this.exitbuttonmap.Size = new System.Drawing.Size(109, 48);
            this.exitbuttonmap.TabIndex = 19;
            this.exitbuttonmap.Text = "Выйти";
            this.exitbuttonmap.UseVisualStyleBackColor = true;
            this.exitbuttonmap.Click += new System.EventHandler(this.Exitbuttonmap_Click);
            // 
            // usesearchcb
            // 
            this.usesearchcb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.usesearchcb.AutoSize = true;
            this.usesearchcb.Location = new System.Drawing.Point(491, 94);
            this.usesearchcb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.usesearchcb.Name = "usesearchcb";
            this.usesearchcb.Size = new System.Drawing.Size(413, 20);
            this.usesearchcb.TabIndex = 18;
            this.usesearchcb.Text = "Вместо выбора на карте использовать поиск по названию";
            this.usesearchcb.UseVisualStyleBackColor = true;
            this.usesearchcb.CheckedChanged += new System.EventHandler(this.Usesearchcb_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(477, 452);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(261, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Масштаб - колесом мыши или кнопками";
            // 
            // zoomminus
            // 
            this.zoomminus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomminus.Location = new System.Drawing.Point(421, 57);
            this.zoomminus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.zoomminus.Name = "zoomminus";
            this.zoomminus.Size = new System.Drawing.Size(20, 23);
            this.zoomminus.TabIndex = 16;
            this.zoomminus.Text = "-";
            this.zoomminus.UseVisualStyleBackColor = true;
            this.zoomminus.Click += new System.EventHandler(this.Zoomminus_Click);
            // 
            // zoomplus
            // 
            this.zoomplus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomplus.Location = new System.Drawing.Point(421, 28);
            this.zoomplus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.zoomplus.Name = "zoomplus";
            this.zoomplus.Size = new System.Drawing.Size(20, 23);
            this.zoomplus.TabIndex = 15;
            this.zoomplus.Text = "+";
            this.zoomplus.UseVisualStyleBackColor = true;
            this.zoomplus.Click += new System.EventHandler(this.Zoomplus_Click);
            // 
            // etalabel
            // 
            this.etalabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.etalabel.AutoSize = true;
            this.etalabel.Location = new System.Drawing.Point(477, 380);
            this.etalabel.Name = "etalabel";
            this.etalabel.Size = new System.Drawing.Size(195, 16);
            this.etalabel.TabIndex = 14;
            this.etalabel.Text = "Время в пути: не рассчитано";
            // 
            // distancelabel
            // 
            this.distancelabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.distancelabel.AutoSize = true;
            this.distancelabel.Location = new System.Drawing.Point(477, 347);
            this.distancelabel.Name = "distancelabel";
            this.distancelabel.Size = new System.Drawing.Size(186, 16);
            this.distancelabel.TabIndex = 13;
            this.distancelabel.Text = "Расстояние: не рассчитано";
            // 
            // pedestriancheckBox
            // 
            this.pedestriancheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pedestriancheckBox.AutoSize = true;
            this.pedestriancheckBox.Location = new System.Drawing.Point(711, 222);
            this.pedestriancheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pedestriancheckBox.Name = "pedestriancheckBox";
            this.pedestriancheckBox.Size = new System.Drawing.Size(200, 20);
            this.pedestriancheckBox.TabIndex = 12;
            this.pedestriancheckBox.Text = "Навигация для пешеходов";
            this.pedestriancheckBox.UseVisualStyleBackColor = true;
            this.pedestriancheckBox.CheckedChanged += new System.EventHandler(this.PedestriancheckBox_CheckedChanged);
            // 
            // timecheckbox
            // 
            this.timecheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timecheckbox.AutoSize = true;
            this.timecheckbox.Location = new System.Drawing.Point(476, 222);
            this.timecheckbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.timecheckbox.Name = "timecheckbox";
            this.timecheckbox.Size = new System.Drawing.Size(214, 20);
            this.timecheckbox.TabIndex = 11;
            this.timecheckbox.Text = "Рассчитать время прибытия";
            this.timecheckbox.UseVisualStyleBackColor = true;
            this.timecheckbox.CheckedChanged += new System.EventHandler(this.Timecheckbox_CheckedChanged);
            // 
            // resetpointbutton
            // 
            this.resetpointbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetpointbutton.Location = new System.Drawing.Point(711, 146);
            this.resetpointbutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resetpointbutton.Name = "resetpointbutton";
            this.resetpointbutton.Size = new System.Drawing.Size(128, 55);
            this.resetpointbutton.TabIndex = 10;
            this.resetpointbutton.Text = "Сбросить пункт(ы)";
            this.resetpointbutton.UseVisualStyleBackColor = true;
            this.resetpointbutton.Click += new System.EventHandler(this.Resetpointbutton_Click);
            // 
            // getrouteandtime
            // 
            this.getrouteandtime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.getrouteandtime.Location = new System.Drawing.Point(637, 288);
            this.getrouteandtime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.getrouteandtime.Name = "getrouteandtime";
            this.getrouteandtime.Size = new System.Drawing.Size(243, 39);
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
            this.statuslabel.Location = new System.Drawing.Point(685, 57);
            this.statuslabel.Name = "statuslabel";
            this.statuslabel.Size = new System.Drawing.Size(139, 16);
            this.statuslabel.TabIndex = 8;
            this.statuslabel.Text = "Пункты не выбраны!";
            // 
            // speedtextBox
            // 
            this.speedtextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.speedtextBox.Location = new System.Drawing.Point(477, 295);
            this.speedtextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.speedtextBox.Name = "speedtextBox";
            this.speedtextBox.Size = new System.Drawing.Size(136, 22);
            this.speedtextBox.TabIndex = 7;
            this.speedtextBox.Text = "30";
            this.speedtextBox.Visible = false;
            // 
            // speedlabel
            // 
            this.speedlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.speedlabel.AutoSize = true;
            this.speedlabel.Location = new System.Drawing.Point(475, 258);
            this.speedlabel.Name = "speedlabel";
            this.speedlabel.Size = new System.Drawing.Size(328, 16);
            this.speedlabel.TabIndex = 6;
            this.speedlabel.Text = "Введите среднюю скорость передвижения в км/ч";
            this.speedlabel.Visible = false;
            // 
            // howto
            // 
            this.howto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.howto.AutoSize = true;
            this.howto.Location = new System.Drawing.Point(477, 18);
            this.howto.Name = "howto";
            this.howto.Size = new System.Drawing.Size(422, 16);
            this.howto.TabIndex = 5;
            this.howto.Text = "Выберите пункт на карте и введите имя точки (необязательно)";
            // 
            // helplabel1
            // 
            this.helplabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helplabel1.AutoSize = true;
            this.helplabel1.Location = new System.Drawing.Point(477, 420);
            this.helplabel1.Name = "helplabel1";
            this.helplabel1.Size = new System.Drawing.Size(407, 16);
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
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(920, 549);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Список необходимых вещей";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dbStatusLabel
            // 
            this.dbStatusLabel.AutoSize = true;
            this.dbStatusLabel.Location = new System.Drawing.Point(17, 518);
            this.dbStatusLabel.Name = "dbStatusLabel";
            this.dbStatusLabel.Size = new System.Drawing.Size(193, 16);
            this.dbStatusLabel.TabIndex = 30;
            this.dbStatusLabel.Text = "База данных не подключена";
            // 
            // cleanallbutton
            // 
            this.cleanallbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cleanallbutton.Location = new System.Drawing.Point(773, 402);
            this.cleanallbutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cleanallbutton.Name = "cleanallbutton";
            this.cleanallbutton.Size = new System.Drawing.Size(109, 48);
            this.cleanallbutton.TabIndex = 29;
            this.cleanallbutton.Text = "Очистить список";
            this.cleanallbutton.UseVisualStyleBackColor = true;
            this.cleanallbutton.Click += new System.EventHandler(this.Cleanallbutton_Click);
            // 
            // loadbuttonth
            // 
            this.loadbuttonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadbuttonth.Location = new System.Drawing.Point(628, 469);
            this.loadbuttonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loadbuttonth.Name = "loadbuttonth";
            this.loadbuttonth.Size = new System.Drawing.Size(109, 48);
            this.loadbuttonth.TabIndex = 28;
            this.loadbuttonth.Text = "Загрузить";
            this.loadbuttonth.UseVisualStyleBackColor = true;
            this.loadbuttonth.Click += new System.EventHandler(this.Loadbuttonth_Click);
            // 
            // savebuttonth
            // 
            this.savebuttonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.savebuttonth.Location = new System.Drawing.Point(628, 402);
            this.savebuttonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.savebuttonth.Name = "savebuttonth";
            this.savebuttonth.Size = new System.Drawing.Size(109, 48);
            this.savebuttonth.TabIndex = 27;
            this.savebuttonth.Text = "Создать";
            this.savebuttonth.UseVisualStyleBackColor = true;
            this.savebuttonth.Click += new System.EventHandler(this.Savebuttonth_Click);
            // 
            // rembuttonth
            // 
            this.rembuttonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rembuttonth.Location = new System.Drawing.Point(484, 468);
            this.rembuttonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rembuttonth.Name = "rembuttonth";
            this.rembuttonth.Size = new System.Drawing.Size(109, 48);
            this.rembuttonth.TabIndex = 26;
            this.rembuttonth.Text = "Удалить";
            this.rembuttonth.UseVisualStyleBackColor = true;
            this.rembuttonth.Click += new System.EventHandler(this.Rembuttonth_Click);
            // 
            // addbuttonth
            // 
            this.addbuttonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addbuttonth.Location = new System.Drawing.Point(484, 402);
            this.addbuttonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addbuttonth.Name = "addbuttonth";
            this.addbuttonth.Size = new System.Drawing.Size(109, 48);
            this.addbuttonth.TabIndex = 25;
            this.addbuttonth.Text = "Добавить";
            this.addbuttonth.UseVisualStyleBackColor = true;
            this.addbuttonth.Click += new System.EventHandler(this.Addbuttonth_Click);
            // 
            // amountthtextBox
            // 
            this.amountthtextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.amountthtextBox.Location = new System.Drawing.Point(157, 473);
            this.amountthtextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.amountthtextBox.Name = "amountthtextBox";
            this.amountthtextBox.Size = new System.Drawing.Size(281, 22);
            this.amountthtextBox.TabIndex = 24;
            // 
            // namethtextBox
            // 
            this.namethtextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.namethtextBox.Location = new System.Drawing.Point(157, 415);
            this.namethtextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.namethtextBox.Name = "namethtextBox";
            this.namethtextBox.Size = new System.Drawing.Size(281, 22);
            this.namethtextBox.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 473);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "Количество";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 418);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 16);
            this.label1.TabIndex = 21;
            this.label1.Text = "Название вещи";
            // 
            // exitbuttonthings
            // 
            this.exitbuttonthings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exitbuttonthings.Location = new System.Drawing.Point(773, 469);
            this.exitbuttonthings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.exitbuttonthings.Name = "exitbuttonthings";
            this.exitbuttonthings.Size = new System.Drawing.Size(109, 48);
            this.exitbuttonthings.TabIndex = 20;
            this.exitbuttonthings.Text = "Выйти";
            this.exitbuttonthings.UseVisualStyleBackColor = true;
            this.exitbuttonthings.Click += new System.EventHandler(this.Exitbuttonthings_Click);
            // 
            // neededdgv
            // 
            this.neededdgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.neededdgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.neededdgv.Location = new System.Drawing.Point(41, 34);
            this.neededdgv.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.neededdgv.Name = "neededdgv";
            this.neededdgv.RowHeadersWidth = 51;
            this.neededdgv.RowTemplate.Height = 24;
            this.neededdgv.Size = new System.Drawing.Size(843, 354);
            this.neededdgv.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 603);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(799, 598);
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

