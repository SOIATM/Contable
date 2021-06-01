<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ContabilizadordeGastos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ContabilizadordeGastos))
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.CmdExportaExcel = New Telerik.WinControls.UI.RadButton()
        Me.CmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.CmdBuscar = New Telerik.WinControls.UI.RadButton()
        Me.Cmdguardar = New Telerik.WinControls.UI.RadButton()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ComboMes = New Telerik.WinControls.UI.RadDropDownList()
        Me.comboAño = New Telerik.WinControls.UI.RadDropDownList()
        Me.MesFin = New Telerik.WinControls.UI.RadDropDownList()
        Me.AnioFin = New Telerik.WinControls.UI.RadDropDownList()
        Me.Tabla = New System.Windows.Forms.DataGridView()
        Me.Orden = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UUID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Serie = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Emisor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RFC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmdExportaExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmdguardar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.ComboMes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboAño, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MesFin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AnioFin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Ayuda
        '
        Me.Ayuda.IsBalloon = True
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.CadetBlue
        Me.RadPanel1.Controls.Add(Me.CmdExportaExcel)
        Me.RadPanel1.Controls.Add(Me.CmdCerrar)
        Me.RadPanel1.Controls.Add(Me.CmdBuscar)
        Me.RadPanel1.Controls.Add(Me.Cmdguardar)
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(805, 155)
        Me.RadPanel1.TabIndex = 0
        '
        'CmdExportaExcel
        '
        Me.CmdExportaExcel.Image = Global.ATMFiscal.My.Resources.Resources.Exportar
        Me.CmdExportaExcel.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdExportaExcel.Location = New System.Drawing.Point(113, 12)
        Me.CmdExportaExcel.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdExportaExcel.Name = "CmdExportaExcel"
        Me.CmdExportaExcel.Size = New System.Drawing.Size(50, 54)
        Me.CmdExportaExcel.TabIndex = 637
        Me.CmdExportaExcel.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdExportaExcel.ThemeName = "Aqua"
        Me.CmdExportaExcel.UseWaitCursor = True
        '
        'CmdCerrar
        '
        Me.CmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.CmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdCerrar.Location = New System.Drawing.Point(9, 13)
        Me.CmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdCerrar.Name = "CmdCerrar"
        Me.CmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.CmdCerrar.TabIndex = 636
        Me.CmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdCerrar.ThemeName = "Aqua"
        Me.CmdCerrar.UseWaitCursor = True
        '
        'CmdBuscar
        '
        Me.CmdBuscar.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdBuscar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdBuscar.Location = New System.Drawing.Point(165, 13)
        Me.CmdBuscar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdBuscar.Name = "CmdBuscar"
        Me.CmdBuscar.Size = New System.Drawing.Size(50, 54)
        Me.CmdBuscar.TabIndex = 635
        Me.CmdBuscar.TabStop = False
        Me.CmdBuscar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdBuscar.ThemeName = "Aqua"
        Me.CmdBuscar.UseWaitCursor = True
        '
        'Cmdguardar
        '
        Me.Cmdguardar.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.Cmdguardar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Cmdguardar.Location = New System.Drawing.Point(61, 13)
        Me.Cmdguardar.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmdguardar.Name = "Cmdguardar"
        Me.Cmdguardar.Size = New System.Drawing.Size(50, 54)
        Me.Cmdguardar.TabIndex = 634
        Me.Cmdguardar.TabStop = False
        Me.Cmdguardar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.Cmdguardar.ThemeName = "Aqua"
        Me.Cmdguardar.UseWaitCursor = True
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.Controls.Add(Me.Label6)
        Me.RadGroupBox1.Controls.Add(Me.ComboMes)
        Me.RadGroupBox1.Controls.Add(Me.comboAño)
        Me.RadGroupBox1.Controls.Add(Me.MesFin)
        Me.RadGroupBox1.Controls.Add(Me.AnioFin)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Selector      Año      -      Mes"
        Me.RadGroupBox1.Location = New System.Drawing.Point(269, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(514, 127)
        Me.RadGroupBox1.TabIndex = 0
        Me.RadGroupBox1.Text = "Selector      Año      -      Mes"
        Me.RadGroupBox1.ThemeName = "MaterialBlueGrey"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(274, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 18)
        Me.Label1.TabIndex = 598
        Me.Label1.Text = "Final:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(15, 37)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 18)
        Me.Label6.TabIndex = 597
        Me.Label6.Text = "Inicial:"
        '
        'ComboMes
        '
        Me.ComboMes.Location = New System.Drawing.Point(153, 57)
        Me.ComboMes.Name = "ComboMes"
        Me.ComboMes.Size = New System.Drawing.Size(87, 36)
        Me.ComboMes.TabIndex = 1
        Me.ComboMes.Text = " "
        Me.ComboMes.ThemeName = "Material"
        '
        'comboAño
        '
        Me.comboAño.Location = New System.Drawing.Point(18, 57)
        Me.comboAño.Name = "comboAño"
        Me.comboAño.Size = New System.Drawing.Size(125, 36)
        Me.comboAño.TabIndex = 0
        Me.comboAño.Text = " "
        Me.comboAño.ThemeName = "Material"
        '
        'MesFin
        '
        Me.MesFin.Location = New System.Drawing.Point(410, 57)
        Me.MesFin.Name = "MesFin"
        Me.MesFin.Size = New System.Drawing.Size(87, 36)
        Me.MesFin.TabIndex = 2
        Me.MesFin.Text = " "
        Me.MesFin.ThemeName = "Material"
        '
        'AnioFin
        '
        Me.AnioFin.Location = New System.Drawing.Point(277, 57)
        Me.AnioFin.Name = "AnioFin"
        Me.AnioFin.Size = New System.Drawing.Size(125, 36)
        Me.AnioFin.TabIndex = 3
        Me.AnioFin.Text = " "
        Me.AnioFin.ThemeName = "Material"
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.AllowUserToDeleteRows = False
        Me.Tabla.AllowUserToOrderColumns = True
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Orden, Me.UUID, Me.Serie, Me.Emisor, Me.Mat, Me.Nombre, Me.RFC})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        DataGridViewCellStyle1.NullValue = "<NULL>"
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Tabla.DefaultCellStyle = DataGridViewCellStyle1
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.Location = New System.Drawing.Point(0, 155)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.ReadOnly = True
        Me.Tabla.RowTemplate.Height = 18
        Me.Tabla.Size = New System.Drawing.Size(805, 251)
        Me.Tabla.TabIndex = 29
        '
        'Orden
        '
        Me.Orden.HeaderText = "Orden"
        Me.Orden.Name = "Orden"
        Me.Orden.ReadOnly = True
        '
        'UUID
        '
        Me.UUID.HeaderText = "UUID"
        Me.UUID.Name = "UUID"
        Me.UUID.ReadOnly = True
        '
        'Serie
        '
        Me.Serie.HeaderText = "Serie"
        Me.Serie.Name = "Serie"
        Me.Serie.ReadOnly = True
        '
        'Emisor
        '
        Me.Emisor.HeaderText = "Emisor"
        Me.Emisor.Name = "Emisor"
        Me.Emisor.ReadOnly = True
        '
        'Mat
        '
        Me.Mat.HeaderText = "Matricula"
        Me.Mat.Name = "Mat"
        Me.Mat.ReadOnly = True
        '
        'Nombre
        '
        Me.Nombre.HeaderText = "Nombre"
        Me.Nombre.Name = "Nombre"
        Me.Nombre.ReadOnly = True
        '
        'RFC
        '
        Me.RFC.HeaderText = "RFC"
        Me.RFC.Name = "RFC"
        Me.RFC.ReadOnly = True
        '
        'ContabilizadordeGastos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(805, 406)
        Me.Controls.Add(Me.Tabla)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ContabilizadordeGastos"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Contabilizador de Gastos"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.CmdExportaExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmdguardar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.ComboMes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comboAño, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MesFin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AnioFin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Ayuda As ToolTip
	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
	Friend WithEvents ComboMes As Telerik.WinControls.UI.RadDropDownList
	Friend WithEvents comboAño As Telerik.WinControls.UI.RadDropDownList
	Friend WithEvents MesFin As Telerik.WinControls.UI.RadDropDownList
	Friend WithEvents AnioFin As Telerik.WinControls.UI.RadDropDownList
	Friend WithEvents Label1 As Label
	Friend WithEvents Label6 As Label
	Friend WithEvents CmdExportaExcel As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdBuscar As Telerik.WinControls.UI.RadButton
	Friend WithEvents Cmdguardar As Telerik.WinControls.UI.RadButton
	Protected Friend WithEvents Tabla As DataGridView
	Friend WithEvents Orden As DataGridViewTextBoxColumn
	Friend WithEvents UUID As DataGridViewTextBoxColumn
	Friend WithEvents Serie As DataGridViewTextBoxColumn
	Friend WithEvents Emisor As DataGridViewTextBoxColumn
	Friend WithEvents Mat As DataGridViewTextBoxColumn
	Friend WithEvents Nombre As DataGridViewTextBoxColumn
	Friend WithEvents RFC As DataGridViewTextBoxColumn
End Class

