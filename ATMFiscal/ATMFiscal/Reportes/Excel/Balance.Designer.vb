<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Balance
    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Balance))
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.LstCliente = New ATMFiscal.Listas()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Dtfin = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.DtInicio = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.CmdPdf = New Telerik.WinControls.UI.RadButton()
        Me.CmdExp = New Telerik.WinControls.UI.RadButton()
        Me.CmdImportar = New Telerik.WinControls.UI.RadButton()
        Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel()
        Me.Tabla = New System.Windows.Forms.DataGridView()
        Me.CtaAc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Saldof = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RadPanel3 = New Telerik.WinControls.UI.RadPanel()
        Me.Tabla3 = New System.Windows.Forms.DataGridView()
        Me.CtaO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DescO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SaldoO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RadPanel4 = New Telerik.WinControls.UI.RadPanel()
        Me.Tabla2 = New System.Windows.Forms.DataGridView()
        Me.cuenta2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.desc2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sald2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SP = New System.ComponentModel.BackgroundWorker()
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.Dtfin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtInicio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdPdf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel3.SuspendLayout()
        CType(Me.Tabla3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel4.SuspendLayout()
        CType(Me.Tabla2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.AutoScroll = True
        Me.RadPanel1.Controls.Add(Me.LstCliente)
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Controls.Add(Me.Label5)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Controls.Add(Me.CmdPdf)
        Me.RadPanel1.Controls.Add(Me.CmdExp)
        Me.RadPanel1.Controls.Add(Me.CmdImportar)
        Me.RadPanel1.Controls.Add(Me.CmdLimpiar)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1313, 100)
        Me.RadPanel1.TabIndex = 0
        '
        'LstCliente
        '
        Me.LstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstCliente.Location = New System.Drawing.Point(276, 42)
        Me.LstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstCliente.Name = "LstCliente"
        Me.LstCliente.SelectItem = ""
        Me.LstCliente.SelectText = ""
        Me.LstCliente.Size = New System.Drawing.Size(370, 36)
        Me.LstCliente.TabIndex = 577
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox1.Controls.Add(Me.Dtfin)
        Me.RadGroupBox1.Controls.Add(Me.DtInicio)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.Controls.Add(Me.Label2)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Período"
        Me.RadGroupBox1.Location = New System.Drawing.Point(658, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(643, 97)
        Me.RadGroupBox1.TabIndex = 575
        Me.RadGroupBox1.Text = "Período"
        Me.RadGroupBox1.ThemeName = "MaterialBlueGrey"
        '
        'Dtfin
        '
        Me.Dtfin.AutoSize = False
        Me.Dtfin.CalendarSize = New System.Drawing.Size(290, 320)
        Me.Dtfin.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Italic)
        Me.Dtfin.Location = New System.Drawing.Point(341, 42)
        Me.Dtfin.Name = "Dtfin"
        Me.Dtfin.Size = New System.Drawing.Size(297, 41)
        Me.Dtfin.TabIndex = 636
        Me.Dtfin.TabStop = False
        Me.Dtfin.Text = "viernes, 19 de febrero de 2021"
        Me.Dtfin.ThemeName = "MaterialBlueGrey"
        Me.Dtfin.Value = New Date(2021, 2, 19, 12, 2, 23, 431)
        '
        'DtInicio
        '
        Me.DtInicio.AutoSize = False
        Me.DtInicio.CalendarSize = New System.Drawing.Size(290, 320)
        Me.DtInicio.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtInicio.Location = New System.Drawing.Point(14, 42)
        Me.DtInicio.Name = "DtInicio"
        Me.DtInicio.Size = New System.Drawing.Size(299, 41)
        Me.DtInicio.TabIndex = 635
        Me.DtInicio.TabStop = False
        Me.DtInicio.Text = "viernes, 19 de febrero de 2021"
        Me.DtInicio.ThemeName = "MaterialBlueGrey"
        Me.DtInicio.Value = New Date(2021, 2, 19, 12, 2, 23, 431)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(338, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 18)
        Me.Label1.TabIndex = 96
        Me.Label1.Text = "Fecha Final:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 18)
        Me.Label2.TabIndex = 97
        Me.Label2.Text = "Fecha Inicial:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(273, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 18)
        Me.Label5.TabIndex = 576
        Me.Label5.Text = "Empresa:"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(2, 21)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 578
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'CmdPdf
        '
        Me.CmdPdf.Image = Global.ATMFiscal.My.Resources.Resources.PDF
        Me.CmdPdf.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdPdf.Location = New System.Drawing.Point(218, 21)
        Me.CmdPdf.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdPdf.Name = "CmdPdf"
        Me.CmdPdf.Size = New System.Drawing.Size(50, 54)
        Me.CmdPdf.TabIndex = 582
        Me.CmdPdf.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdPdf.ThemeName = "Aqua"
        '
        'CmdExp
        '
        Me.CmdExp.Image = Global.ATMFiscal.My.Resources.Resources.Exportar
        Me.CmdExp.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdExp.Location = New System.Drawing.Point(164, 21)
        Me.CmdExp.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdExp.Name = "CmdExp"
        Me.CmdExp.Size = New System.Drawing.Size(50, 54)
        Me.CmdExp.TabIndex = 579
        Me.CmdExp.TabStop = False
        Me.CmdExp.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdExp.ThemeName = "Aqua"
        '
        'CmdImportar
        '
        Me.CmdImportar.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdImportar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdImportar.Location = New System.Drawing.Point(110, 21)
        Me.CmdImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdImportar.Name = "CmdImportar"
        Me.CmdImportar.Size = New System.Drawing.Size(50, 54)
        Me.CmdImportar.TabIndex = 580
        Me.CmdImportar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdImportar.ThemeName = "Aqua"
        '
        'CmdLimpiar
        '
        Me.CmdLimpiar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiar.Location = New System.Drawing.Point(56, 21)
        Me.CmdLimpiar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiar.Name = "CmdLimpiar"
        Me.CmdLimpiar.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiar.TabIndex = 581
        Me.CmdLimpiar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdLimpiar.ThemeName = "Aqua"
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.Tabla)
        Me.RadPanel2.Location = New System.Drawing.Point(0, 107)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(533, 367)
        Me.RadPanel2.TabIndex = 1
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.AllowUserToDeleteRows = False
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CtaAc, Me.Desc, Me.Saldof})
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.Location = New System.Drawing.Point(0, 0)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.ReadOnly = True
        Me.Tabla.Size = New System.Drawing.Size(533, 367)
        Me.Tabla.TabIndex = 14
        '
        'CtaAc
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.CtaAc.DefaultCellStyle = DataGridViewCellStyle1
        Me.CtaAc.HeaderText = "Cuenta"
        Me.CtaAc.Name = "CtaAc"
        Me.CtaAc.ReadOnly = True
        Me.CtaAc.Visible = False
        '
        'Desc
        '
        Me.Desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.NullValue = Nothing
        Me.Desc.DefaultCellStyle = DataGridViewCellStyle2
        Me.Desc.HeaderText = "Descripcion"
        Me.Desc.Name = "Desc"
        Me.Desc.ReadOnly = True
        Me.Desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Desc.Width = 85
        '
        'Saldof
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = "0"
        Me.Saldof.DefaultCellStyle = DataGridViewCellStyle3
        Me.Saldof.HeaderText = "Saldo_Final"
        Me.Saldof.Name = "Saldof"
        Me.Saldof.ReadOnly = True
        '
        'RadPanel3
        '
        Me.RadPanel3.Controls.Add(Me.Tabla3)
        Me.RadPanel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RadPanel3.Location = New System.Drawing.Point(0, 480)
        Me.RadPanel3.Name = "RadPanel3"
        Me.RadPanel3.Size = New System.Drawing.Size(1313, 125)
        Me.RadPanel3.TabIndex = 2
        '
        'Tabla3
        '
        Me.Tabla3.AllowUserToAddRows = False
        Me.Tabla3.AllowUserToDeleteRows = False
        Me.Tabla3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla3.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CtaO, Me.DescO, Me.SaldoO})
        Me.Tabla3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla3.Location = New System.Drawing.Point(0, 0)
        Me.Tabla3.Name = "Tabla3"
        Me.Tabla3.ReadOnly = True
        Me.Tabla3.Size = New System.Drawing.Size(1313, 125)
        Me.Tabla3.TabIndex = 16
        '
        'CtaO
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.CtaO.DefaultCellStyle = DataGridViewCellStyle4
        Me.CtaO.HeaderText = "Cuenta"
        Me.CtaO.Name = "CtaO"
        Me.CtaO.ReadOnly = True
        Me.CtaO.Visible = False
        '
        'DescO
        '
        Me.DescO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.NullValue = Nothing
        Me.DescO.DefaultCellStyle = DataGridViewCellStyle5
        Me.DescO.HeaderText = "Descripcion"
        Me.DescO.Name = "DescO"
        Me.DescO.ReadOnly = True
        Me.DescO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DescO.Width = 85
        '
        'SaldoO
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = "0"
        Me.SaldoO.DefaultCellStyle = DataGridViewCellStyle6
        Me.SaldoO.HeaderText = "Saldo_Final"
        Me.SaldoO.Name = "SaldoO"
        Me.SaldoO.ReadOnly = True
        '
        'RadPanel4
        '
        Me.RadPanel4.Controls.Add(Me.Tabla2)
        Me.RadPanel4.Location = New System.Drawing.Point(539, 107)
        Me.RadPanel4.Name = "RadPanel4"
        Me.RadPanel4.Size = New System.Drawing.Size(762, 367)
        Me.RadPanel4.TabIndex = 3
        '
        'Tabla2
        '
        Me.Tabla2.AllowUserToAddRows = False
        Me.Tabla2.AllowUserToDeleteRows = False
        Me.Tabla2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cuenta2, Me.desc2, Me.sald2})
        Me.Tabla2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla2.Location = New System.Drawing.Point(0, 0)
        Me.Tabla2.Name = "Tabla2"
        Me.Tabla2.ReadOnly = True
        Me.Tabla2.Size = New System.Drawing.Size(762, 367)
        Me.Tabla2.TabIndex = 15
        '
        'cuenta2
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.cuenta2.DefaultCellStyle = DataGridViewCellStyle7
        Me.cuenta2.HeaderText = "Cuenta"
        Me.cuenta2.Name = "cuenta2"
        Me.cuenta2.ReadOnly = True
        Me.cuenta2.Visible = False
        '
        'desc2
        '
        Me.desc2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.NullValue = Nothing
        Me.desc2.DefaultCellStyle = DataGridViewCellStyle8
        Me.desc2.HeaderText = "Descripcion"
        Me.desc2.Name = "desc2"
        Me.desc2.ReadOnly = True
        Me.desc2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.desc2.Width = 85
        '
        'sald2
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N2"
        DataGridViewCellStyle9.NullValue = "0"
        Me.sald2.DefaultCellStyle = DataGridViewCellStyle9
        Me.sald2.HeaderText = "Saldo_Final"
        Me.sald2.Name = "sald2"
        Me.sald2.ReadOnly = True
        '
        'SP
        '
        Me.SP.WorkerSupportsCancellation = True
        '
        'Ayuda
        '
        Me.Ayuda.IsBalloon = True
        Me.Ayuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'Balance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(1313, 605)
        Me.Controls.Add(Me.RadPanel4)
        Me.Controls.Add(Me.RadPanel3)
        Me.Controls.Add(Me.RadPanel2)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Balance"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Balance"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.Dtfin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtInicio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdPdf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel3.ResumeLayout(False)
        CType(Me.Tabla3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel4.ResumeLayout(False)
        CType(Me.Tabla2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdPdf As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdExp As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdImportar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
	Friend WithEvents LstCliente As Listas
	Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents Tabla As DataGridView
    Friend WithEvents CtaAc As DataGridViewTextBoxColumn
    Friend WithEvents Desc As DataGridViewTextBoxColumn
    Friend WithEvents Saldof As DataGridViewTextBoxColumn
    Friend WithEvents RadPanel3 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents Tabla3 As DataGridView
    Friend WithEvents CtaO As DataGridViewTextBoxColumn
    Friend WithEvents DescO As DataGridViewTextBoxColumn
    Friend WithEvents SaldoO As DataGridViewTextBoxColumn
    Friend WithEvents RadPanel4 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents Tabla2 As DataGridView
    Friend WithEvents cuenta2 As DataGridViewTextBoxColumn
    Friend WithEvents desc2 As DataGridViewTextBoxColumn
    Friend WithEvents sald2 As DataGridViewTextBoxColumn
    Friend WithEvents SP As System.ComponentModel.BackgroundWorker
    Friend WithEvents Ayuda As ToolTip
    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents Dtfin As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents DtInicio As Telerik.WinControls.UI.RadDateTimePicker
End Class

