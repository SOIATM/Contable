<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Diario
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Diario))
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Dtfin = New System.Windows.Forms.DateTimePicker()
        Me.DtInicio = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LstCliente = New ATMFiscal.Listas()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CmdPdf = New Telerik.WinControls.UI.RadButton()
        Me.CmdImportar = New Telerik.WinControls.UI.RadButton()
        Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
        Me.CmdExp = New Telerik.WinControls.UI.RadButton()
        Me.Tabla = New System.Windows.Forms.DataGridView()
        Me.Pol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.anio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mess = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Day = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.P = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tip = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fech = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NCta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.carg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Abon = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.MaterialBlueGreyTheme1 = New Telerik.WinControls.Themes.MaterialBlueGreyTheme()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdPdf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Controls.Add(Me.LstCliente)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Controls.Add(Me.Label5)
        Me.RadPanel1.Controls.Add(Me.CmdPdf)
        Me.RadPanel1.Controls.Add(Me.CmdImportar)
        Me.RadPanel1.Controls.Add(Me.CmdLimpiar)
        Me.RadPanel1.Controls.Add(Me.CmdExp)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1155, 100)
        Me.RadPanel1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.Dtfin)
        Me.RadGroupBox1.Controls.Add(Me.DtInicio)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.Controls.Add(Me.Label2)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Período"
        Me.RadGroupBox1.Location = New System.Drawing.Point(687, 3)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(457, 88)
        Me.RadGroupBox1.TabIndex = 4
        Me.RadGroupBox1.Text = "Período"
        '
        'Dtfin
        '
        Me.Dtfin.Location = New System.Drawing.Point(242, 52)
        Me.Dtfin.Name = "Dtfin"
        Me.Dtfin.Size = New System.Drawing.Size(200, 22)
        Me.Dtfin.TabIndex = 99
        '
        'DtInicio
        '
        Me.DtInicio.Location = New System.Drawing.Point(14, 52)
        Me.DtInicio.Name = "DtInicio"
        Me.DtInicio.Size = New System.Drawing.Size(200, 22)
        Me.DtInicio.TabIndex = 98
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(239, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 18)
        Me.Label1.TabIndex = 96
        Me.Label1.Text = "Fecha Final:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 18)
        Me.Label2.TabIndex = 97
        Me.Label2.Text = "Fecha Inicial:"
        '
        'LstCliente
        '
        Me.LstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstCliente.Location = New System.Drawing.Point(288, 23)
        Me.LstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstCliente.Name = "LstCliente"
        Me.LstCliente.SelectItem = ""
        Me.LstCliente.SelectText = ""
        Me.LstCliente.Size = New System.Drawing.Size(370, 36)
        Me.LstCliente.TabIndex = 574
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(2, 5)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 573
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(285, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 18)
        Me.Label5.TabIndex = 573
        Me.Label5.Text = "Empresa:"
        '
        'CmdPdf
        '
        Me.CmdPdf.Image = Global.ATMFiscal.My.Resources.Resources.PDF
        Me.CmdPdf.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdPdf.Location = New System.Drawing.Point(218, 5)
        Me.CmdPdf.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdPdf.Name = "CmdPdf"
        Me.CmdPdf.Size = New System.Drawing.Size(50, 54)
        Me.CmdPdf.TabIndex = 577
        Me.CmdPdf.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdPdf.ThemeName = "Aqua"
        '
        'CmdImportar
        '
        Me.CmdImportar.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdImportar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdImportar.Location = New System.Drawing.Point(110, 5)
        Me.CmdImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdImportar.Name = "CmdImportar"
        Me.CmdImportar.Size = New System.Drawing.Size(50, 54)
        Me.CmdImportar.TabIndex = 575
        Me.CmdImportar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdImportar.ThemeName = "Aqua"
        '
        'CmdLimpiar
        '
        Me.CmdLimpiar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiar.Location = New System.Drawing.Point(56, 5)
        Me.CmdLimpiar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiar.Name = "CmdLimpiar"
        Me.CmdLimpiar.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiar.TabIndex = 576
        Me.CmdLimpiar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdLimpiar.ThemeName = "Aqua"
        '
        'CmdExp
        '
        Me.CmdExp.Image = Global.ATMFiscal.My.Resources.Resources.Exportar
        Me.CmdExp.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdExp.Location = New System.Drawing.Point(164, 5)
        Me.CmdExp.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdExp.Name = "CmdExp"
        Me.CmdExp.Size = New System.Drawing.Size(50, 54)
        Me.CmdExp.TabIndex = 574
        Me.CmdExp.TabStop = False
        Me.CmdExp.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdExp.ThemeName = "Aqua"
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.AllowUserToDeleteRows = False
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Pol, Me.anio, Me.Mess, Me.Day, Me.P, Me.Tip, Me.Fech, Me.Desc, Me.Cta, Me.NCta, Me.carg, Me.Abon})
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.Location = New System.Drawing.Point(0, 100)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.ReadOnly = True
        Me.Tabla.Size = New System.Drawing.Size(1155, 172)
        Me.Tabla.TabIndex = 12
        '
        'Pol
        '
        Me.Pol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Pol.HeaderText = "Poliza_Sistema"
        Me.Pol.Name = "Pol"
        Me.Pol.ReadOnly = True
        Me.Pol.Width = 164
        '
        'anio
        '
        Me.anio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.anio.HeaderText = "Año"
        Me.anio.Name = "anio"
        Me.anio.ReadOnly = True
        Me.anio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.anio.Width = 47
        '
        'Mess
        '
        Me.Mess.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Mess.HeaderText = "Mes"
        Me.Mess.Name = "Mess"
        Me.Mess.ReadOnly = True
        Me.Mess.Width = 73
        '
        'Day
        '
        Me.Day.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Day.HeaderText = "Dia"
        Me.Day.Name = "Day"
        Me.Day.ReadOnly = True
        Me.Day.Width = 66
        '
        'P
        '
        Me.P.HeaderText = "Poliza"
        Me.P.Name = "P"
        Me.P.ReadOnly = True
        Me.P.Width = 86
        '
        'Tip
        '
        Me.Tip.HeaderText = "Tipo_Poliza"
        Me.Tip.Name = "Tip"
        Me.Tip.ReadOnly = True
        Me.Tip.Width = 85
        '
        'Fech
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.Format = "d"
        DataGridViewCellStyle1.NullValue = " "
        Me.Fech.DefaultCellStyle = DataGridViewCellStyle1
        Me.Fech.HeaderText = "Fecha"
        Me.Fech.Name = "Fech"
        Me.Fech.ReadOnly = True
        Me.Fech.Width = 86
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
        Me.Desc.Width = 112
        '
        'Cta
        '
        Me.Cta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Cta.DefaultCellStyle = DataGridViewCellStyle3
        Me.Cta.HeaderText = "Cuenta"
        Me.Cta.Name = "Cta"
        Me.Cta.ReadOnly = True
        Me.Cta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Cta.Width = 73
        '
        'NCta
        '
        Me.NCta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.NullValue = Nothing
        Me.NCta.DefaultCellStyle = DataGridViewCellStyle4
        Me.NCta.HeaderText = "Nombre_Cuenta"
        Me.NCta.Name = "NCta"
        Me.NCta.ReadOnly = True
        Me.NCta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NCta.Width = 147
        '
        'carg
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "c2"
        DataGridViewCellStyle5.NullValue = "0"
        Me.carg.DefaultCellStyle = DataGridViewCellStyle5
        Me.carg.HeaderText = "Cargo"
        Me.carg.Name = "carg"
        Me.carg.ReadOnly = True
        Me.carg.Width = 87
        '
        'Abon
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "c2"
        DataGridViewCellStyle6.NullValue = "0"
        Me.Abon.DefaultCellStyle = DataGridViewCellStyle6
        Me.Abon.HeaderText = "Abono"
        Me.Abon.Name = "Abon"
        Me.Abon.ReadOnly = True
        Me.Abon.Width = 86
        '
        'Ayuda
        '
        Me.Ayuda.IsBalloon = True
        Me.Ayuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'Diario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1155, 272)
        Me.Controls.Add(Me.Tabla)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Diario"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Diario"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdPdf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdPdf As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdImportar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdExp As Telerik.WinControls.UI.RadButton
	Friend WithEvents LstCliente As Listas
	Friend WithEvents Label5 As Label
	Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
	Friend WithEvents Dtfin As DateTimePicker
	Friend WithEvents DtInicio As DateTimePicker
	Friend WithEvents Label1 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents Tabla As DataGridView
	Friend WithEvents Pol As DataGridViewTextBoxColumn
	Friend WithEvents anio As DataGridViewTextBoxColumn
	Friend WithEvents Mess As DataGridViewTextBoxColumn
	Friend WithEvents Day As DataGridViewTextBoxColumn
	Friend WithEvents P As DataGridViewTextBoxColumn
	Friend WithEvents Tip As DataGridViewTextBoxColumn
	Friend WithEvents Fech As DataGridViewTextBoxColumn
	Friend WithEvents Desc As DataGridViewTextBoxColumn
	Friend WithEvents Cta As DataGridViewTextBoxColumn
	Friend WithEvents NCta As DataGridViewTextBoxColumn
	Friend WithEvents carg As DataGridViewTextBoxColumn
	Friend WithEvents Abon As DataGridViewTextBoxColumn
	Friend WithEvents Ayuda As ToolTip
	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents MaterialBlueGreyTheme1 As Telerik.WinControls.Themes.MaterialBlueGreyTheme
End Class

