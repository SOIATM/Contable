<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Auxiliares
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Auxiliares))
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Lstctafinal = New ATMFiscal.Listas()
        Me.Lstctainicial = New ATMFiscal.Listas()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Dtfin = New System.Windows.Forms.DateTimePicker()
        Me.DtInicio = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmdSalirF = New Telerik.WinControls.UI.RadButton()
        Me.CmdGenerar = New Telerik.WinControls.UI.RadButton()
        Me.CmdExportar = New Telerik.WinControls.UI.RadButton()
        Me.CmdPdf = New Telerik.WinControls.UI.RadButton()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.auxiliar = New System.Windows.Forms.DataGridView()
        Me.Ctas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nop = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Concepto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Si = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Car = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Saldo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Suma = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nom = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.MaterialBlueGreyTheme1 = New Telerik.WinControls.Themes.MaterialBlueGreyTheme()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGenerar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdExportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdPdf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.auxiliar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label5)
        Me.RadPanel1.Controls.Add(Me.RadGroupBox2)
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Controls.Add(Me.CmdSalirF)
        Me.RadPanel1.Controls.Add(Me.CmdGenerar)
        Me.RadPanel1.Controls.Add(Me.CmdExportar)
        Me.RadPanel1.Controls.Add(Me.CmdPdf)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1297, 157)
        Me.RadPanel1.TabIndex = 0
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(240, 20)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(482, 36)
        Me.lstCliente.TabIndex = 570
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(237, 2)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 18)
        Me.Label5.TabIndex = 569
        Me.Label5.Text = "Empresa:"
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox2.Controls.Add(Me.Lstctafinal)
        Me.RadGroupBox2.Controls.Add(Me.Lstctainicial)
        Me.RadGroupBox2.Controls.Add(Me.Label3)
        Me.RadGroupBox2.Controls.Add(Me.Label4)
        Me.RadGroupBox2.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold)
        Me.RadGroupBox2.HeaderText = "Número de Cuenta"
        Me.RadGroupBox2.Location = New System.Drawing.Point(737, 12)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(543, 139)
        Me.RadGroupBox2.TabIndex = 568
        Me.RadGroupBox2.Text = "Número de Cuenta"
        Me.RadGroupBox2.ThemeName = "Material"
        '
        'Lstctafinal
        '
        Me.Lstctafinal.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lstctafinal.Location = New System.Drawing.Point(83, 86)
        Me.Lstctafinal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Lstctafinal.Name = "Lstctafinal"
        Me.Lstctafinal.SelectItem = ""
        Me.Lstctafinal.SelectText = ""
        Me.Lstctafinal.Size = New System.Drawing.Size(440, 36)
        Me.Lstctafinal.TabIndex = 555
        '
        'Lstctainicial
        '
        Me.Lstctainicial.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lstctainicial.Location = New System.Drawing.Point(83, 27)
        Me.Lstctainicial.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Lstctainicial.Name = "Lstctainicial"
        Me.Lstctainicial.SelectItem = ""
        Me.Lstctainicial.SelectText = ""
        Me.Lstctainicial.Size = New System.Drawing.Size(440, 36)
        Me.Lstctainicial.TabIndex = 554
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 18)
        Me.Label3.TabIndex = 98
        Me.Label3.Text = "Hasta:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 37)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 18)
        Me.Label4.TabIndex = 99
        Me.Label4.Text = "Desde:"
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
        Me.RadGroupBox1.Location = New System.Drawing.Point(240, 63)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(482, 85)
        Me.RadGroupBox1.TabIndex = 4
        Me.RadGroupBox1.Text = "Período"
        Me.RadGroupBox1.ThemeName = "Material"
        '
        'Dtfin
        '
        Me.Dtfin.Location = New System.Drawing.Point(248, 45)
        Me.Dtfin.Name = "Dtfin"
        Me.Dtfin.Size = New System.Drawing.Size(216, 22)
        Me.Dtfin.TabIndex = 99
        '
        'DtInicio
        '
        Me.DtInicio.Location = New System.Drawing.Point(14, 45)
        Me.DtInicio.Name = "DtInicio"
        Me.DtInicio.Size = New System.Drawing.Size(216, 22)
        Me.DtInicio.TabIndex = 98
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(300, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 18)
        Me.Label1.TabIndex = 96
        Me.Label1.Text = "Fecha Final:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(61, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 18)
        Me.Label2.TabIndex = 97
        Me.Label2.Text = "Fecha Inicial:"
        '
        'CmdSalirF
        '
        Me.CmdSalirF.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.CmdSalirF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdSalirF.Location = New System.Drawing.Point(8, 18)
        Me.CmdSalirF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdSalirF.Name = "CmdSalirF"
        Me.CmdSalirF.Size = New System.Drawing.Size(50, 54)
        Me.CmdSalirF.TabIndex = 564
        Me.CmdSalirF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdSalirF.ThemeName = "Aqua"
        '
        'CmdGenerar
        '
        Me.CmdGenerar.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdGenerar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGenerar.Location = New System.Drawing.Point(62, 18)
        Me.CmdGenerar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdGenerar.Name = "CmdGenerar"
        Me.CmdGenerar.Size = New System.Drawing.Size(50, 54)
        Me.CmdGenerar.TabIndex = 567
        Me.CmdGenerar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdGenerar.ThemeName = "Aqua"
        '
        'CmdExportar
        '
        Me.CmdExportar.Image = Global.ATMFiscal.My.Resources.Resources.Exportar
        Me.CmdExportar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdExportar.Location = New System.Drawing.Point(116, 18)
        Me.CmdExportar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdExportar.Name = "CmdExportar"
        Me.CmdExportar.Size = New System.Drawing.Size(50, 54)
        Me.CmdExportar.TabIndex = 566
        Me.CmdExportar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdExportar.ThemeName = "Aqua"
        '
        'CmdPdf
        '
        Me.CmdPdf.Image = Global.ATMFiscal.My.Resources.Resources.PDF
        Me.CmdPdf.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdPdf.Location = New System.Drawing.Point(170, 18)
        Me.CmdPdf.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdPdf.Name = "CmdPdf"
        Me.CmdPdf.Size = New System.Drawing.Size(50, 54)
        Me.CmdPdf.TabIndex = 565
        Me.CmdPdf.TabStop = False
        Me.CmdPdf.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdPdf.ThemeName = "Aqua"
        '
        'auxiliar
        '
        Me.auxiliar.AllowUserToAddRows = False
        Me.auxiliar.AllowUserToDeleteRows = False
        Me.auxiliar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.auxiliar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.auxiliar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Ctas, Me.Tipo, Me.Nop, Me.Fecha, Me.Concepto, Me.Si, Me.Car, Me.AB, Me.Saldo, Me.Suma, Me.Nom})
        Me.auxiliar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.auxiliar.Location = New System.Drawing.Point(0, 157)
        Me.auxiliar.Name = "auxiliar"
        Me.auxiliar.ReadOnly = True
        Me.auxiliar.Size = New System.Drawing.Size(1297, 465)
        Me.auxiliar.TabIndex = 501
        '
        'Ctas
        '
        Me.Ctas.HeaderText = "Cuenta"
        Me.Ctas.Name = "Ctas"
        Me.Ctas.ReadOnly = True
        Me.Ctas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Tipo
        '
        Me.Tipo.HeaderText = "Tipo"
        Me.Tipo.Name = "Tipo"
        Me.Tipo.ReadOnly = True
        Me.Tipo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Nop
        '
        Me.Nop.HeaderText = "No Póliza"
        Me.Nop.Name = "Nop"
        Me.Nop.ReadOnly = True
        Me.Nop.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Fecha
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.Format = "d"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.Fecha.DefaultCellStyle = DataGridViewCellStyle1
        Me.Fecha.HeaderText = "Fecha"
        Me.Fecha.Name = "Fecha"
        Me.Fecha.ReadOnly = True
        Me.Fecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Concepto
        '
        Me.Concepto.HeaderText = "Concepto"
        Me.Concepto.Name = "Concepto"
        Me.Concepto.ReadOnly = True
        Me.Concepto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Si
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = "0"
        Me.Si.DefaultCellStyle = DataGridViewCellStyle2
        Me.Si.HeaderText = "Saldo Inicial"
        Me.Si.Name = "Si"
        Me.Si.ReadOnly = True
        '
        'Car
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "C2"
        DataGridViewCellStyle3.NullValue = "0"
        Me.Car.DefaultCellStyle = DataGridViewCellStyle3
        Me.Car.HeaderText = "Cargos"
        Me.Car.Name = "Car"
        Me.Car.ReadOnly = True
        Me.Car.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'AB
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "C2"
        DataGridViewCellStyle4.NullValue = "0"
        Me.AB.DefaultCellStyle = DataGridViewCellStyle4
        Me.AB.HeaderText = "Abonos"
        Me.AB.Name = "AB"
        Me.AB.ReadOnly = True
        Me.AB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Saldo
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "C2"
        DataGridViewCellStyle5.NullValue = "0"
        Me.Saldo.DefaultCellStyle = DataGridViewCellStyle5
        Me.Saldo.HeaderText = "Saldo Final"
        Me.Saldo.Name = "Saldo"
        Me.Saldo.ReadOnly = True
        Me.Saldo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Suma
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Format = "C2"
        DataGridViewCellStyle6.NullValue = "0"
        Me.Suma.DefaultCellStyle = DataGridViewCellStyle6
        Me.Suma.HeaderText = "Suma"
        Me.Suma.MinimumWidth = 2
        Me.Suma.Name = "Suma"
        Me.Suma.ReadOnly = True
        Me.Suma.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Suma.Visible = False
        '
        'Nom
        '
        Me.Nom.HeaderText = "Nombre"
        Me.Nom.Name = "Nom"
        Me.Nom.ReadOnly = True
        Me.Nom.Visible = False
        '
        'Ayuda
        '
        Me.Ayuda.IsBalloon = True
        '
        'Auxiliares
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1297, 622)
        Me.Controls.Add(Me.auxiliar)
        Me.Controls.Add(Me.RadPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Auxiliares"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Auxiliares"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGenerar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdExportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdPdf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.auxiliar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents CmdSalirF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdGenerar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdExportar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdPdf As Telerik.WinControls.UI.RadButton
	Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
	Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
	Friend WithEvents Dtfin As DateTimePicker
	Friend WithEvents DtInicio As DateTimePicker
	Friend WithEvents Label1 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents Label3 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents Lstctafinal As Listas
	Friend WithEvents Lstctainicial As Listas
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label5 As Label
	Friend WithEvents auxiliar As DataGridView
	Friend WithEvents Ctas As DataGridViewTextBoxColumn
	Friend WithEvents Tipo As DataGridViewTextBoxColumn
	Friend WithEvents Nop As DataGridViewTextBoxColumn
	Friend WithEvents Fecha As DataGridViewTextBoxColumn
	Friend WithEvents Concepto As DataGridViewTextBoxColumn
	Friend WithEvents Si As DataGridViewTextBoxColumn
	Friend WithEvents Car As DataGridViewTextBoxColumn
	Friend WithEvents AB As DataGridViewTextBoxColumn
	Friend WithEvents Saldo As DataGridViewTextBoxColumn
	Friend WithEvents Suma As DataGridViewTextBoxColumn
	Friend WithEvents Nom As DataGridViewTextBoxColumn
	Friend WithEvents Ayuda As ToolTip
    Friend WithEvents MaterialBlueGreyTheme1 As Telerik.WinControls.Themes.MaterialBlueGreyTheme
End Class

