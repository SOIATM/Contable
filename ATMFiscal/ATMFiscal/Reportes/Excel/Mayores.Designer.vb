<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Mayores
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Mayores))
		Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
		Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
		Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
		Me.Panel = New Telerik.WinControls.UI.RadPanel()
		Me.ComboAñoB = New Telerik.WinControls.UI.RadDropDownList()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.LstNiveles = New ATMFiscal.Listas()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
		Me.Lstctafinal = New ATMFiscal.Listas()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Lstctainicial = New ATMFiscal.Listas()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.LstCliente = New ATMFiscal.Listas()
		Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.CmdPdf = New Telerik.WinControls.UI.RadButton()
		Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
		Me.CmdExp = New Telerik.WinControls.UI.RadButton()
		Me.CmdImportar = New Telerik.WinControls.UI.RadButton()
		Me.Tabla = New System.Windows.Forms.DataGridView()
		Me.Cta = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Cuent = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Naturaleza = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.M = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Deb = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Hab = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Saldo = New System.Windows.Forms.DataGridViewTextBoxColumn()
		CType(Me.Panel, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Panel.SuspendLayout()
		CType(Me.ComboAñoB, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.RadGroupBox1.SuspendLayout()
		CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.CmdPdf, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.CmdExp, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'Ayuda
		'
		Me.Ayuda.IsBalloon = True
		Me.Ayuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
		'
		'Panel
		'
		Me.Panel.Controls.Add(Me.ComboAñoB)
		Me.Panel.Controls.Add(Me.Label10)
		Me.Panel.Controls.Add(Me.LstNiveles)
		Me.Panel.Controls.Add(Me.Label1)
		Me.Panel.Controls.Add(Me.RadGroupBox1)
		Me.Panel.Controls.Add(Me.LstCliente)
		Me.Panel.Controls.Add(Me.cmdCerrar)
		Me.Panel.Controls.Add(Me.Label5)
		Me.Panel.Controls.Add(Me.CmdPdf)
		Me.Panel.Controls.Add(Me.CmdLimpiar)
		Me.Panel.Controls.Add(Me.CmdExp)
		Me.Panel.Controls.Add(Me.CmdImportar)
		Me.Panel.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel.Location = New System.Drawing.Point(0, 0)
		Me.Panel.Name = "Panel"
		Me.Panel.Size = New System.Drawing.Size(1108, 134)
		Me.Panel.TabIndex = 0
		'
		'ComboAñoB
		'
		Me.ComboAñoB.Location = New System.Drawing.Point(474, 87)
		Me.ComboAñoB.Name = "ComboAñoB"
		Me.ComboAñoB.Size = New System.Drawing.Size(125, 36)
		Me.ComboAñoB.TabIndex = 577
		Me.ComboAñoB.Text = " "
		Me.ComboAñoB.ThemeName = "Material"
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label10.Location = New System.Drawing.Point(513, 64)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(41, 17)
		Me.Label10.TabIndex = 576
		Me.Label10.Text = "Año:"
		'
		'LstNiveles
		'
		Me.LstNiveles.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LstNiveles.Location = New System.Drawing.Point(284, 87)
		Me.LstNiveles.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.LstNiveles.Name = "LstNiveles"
		Me.LstNiveles.SelectItem = ""
		Me.LstNiveles.SelectText = ""
		Me.LstNiveles.Size = New System.Drawing.Size(140, 41)
		Me.LstNiveles.TabIndex = 575
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(273, 62)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(140, 17)
		Me.Label1.TabIndex = 574
		Me.Label1.Text = "Nivel de Consulta:"
		'
		'RadGroupBox1
		'
		Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
		Me.RadGroupBox1.Controls.Add(Me.Lstctafinal)
		Me.RadGroupBox1.Controls.Add(Me.Label3)
		Me.RadGroupBox1.Controls.Add(Me.Lstctainicial)
		Me.RadGroupBox1.Controls.Add(Me.Label4)
		Me.RadGroupBox1.HeaderText = "Numero de Cuenta:"
		Me.RadGroupBox1.Location = New System.Drawing.Point(697, 7)
		Me.RadGroupBox1.Name = "RadGroupBox1"
		Me.RadGroupBox1.Size = New System.Drawing.Size(399, 119)
		Me.RadGroupBox1.TabIndex = 573
		Me.RadGroupBox1.Text = "Numero de Cuenta:"
		'
		'Lstctafinal
		'
		Me.Lstctafinal.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lstctafinal.Location = New System.Drawing.Point(73, 70)
		Me.Lstctafinal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.Lstctafinal.Name = "Lstctafinal"
		Me.Lstctafinal.SelectItem = ""
		Me.Lstctafinal.SelectText = ""
		Me.Lstctafinal.Size = New System.Drawing.Size(305, 36)
		Me.Lstctafinal.TabIndex = 558
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(8, 80)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(55, 17)
		Me.Label3.TabIndex = 557
		Me.Label3.Text = "Hasta:"
		'
		'Lstctainicial
		'
		Me.Lstctainicial.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lstctainicial.Location = New System.Drawing.Point(73, 21)
		Me.Lstctainicial.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.Lstctainicial.Name = "Lstctainicial"
		Me.Lstctainicial.SelectItem = ""
		Me.Lstctainicial.SelectText = ""
		Me.Lstctainicial.Size = New System.Drawing.Size(305, 36)
		Me.Lstctainicial.TabIndex = 556
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Location = New System.Drawing.Point(8, 31)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(59, 17)
		Me.Label4.TabIndex = 555
		Me.Label4.Text = "Desde:"
		'
		'LstCliente
		'
		Me.LstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LstCliente.Location = New System.Drawing.Point(284, 22)
		Me.LstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.LstCliente.Name = "LstCliente"
		Me.LstCliente.SelectItem = ""
		Me.LstCliente.SelectText = ""
		Me.LstCliente.Size = New System.Drawing.Size(407, 36)
		Me.LstCliente.TabIndex = 572
		'
		'cmdCerrar
		'
		Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
		Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdCerrar.Location = New System.Drawing.Point(2, 7)
		Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
		Me.cmdCerrar.Name = "cmdCerrar"
		Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
		Me.cmdCerrar.TabIndex = 568
		Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
		Me.cmdCerrar.ThemeName = "Aqua"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Location = New System.Drawing.Point(281, 4)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(76, 17)
		Me.Label5.TabIndex = 571
		Me.Label5.Text = "Empresa:"
		'
		'CmdPdf
		'
		Me.CmdPdf.Image = Global.ATMFiscal.My.Resources.Resources.PDF
		Me.CmdPdf.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
		Me.CmdPdf.Location = New System.Drawing.Point(218, 7)
		Me.CmdPdf.Margin = New System.Windows.Forms.Padding(2)
		Me.CmdPdf.Name = "CmdPdf"
		Me.CmdPdf.Size = New System.Drawing.Size(50, 54)
		Me.CmdPdf.TabIndex = 572
		Me.CmdPdf.TextAlignment = System.Drawing.ContentAlignment.BottomRight
		Me.CmdPdf.ThemeName = "Aqua"
		'
		'CmdLimpiar
		'
		Me.CmdLimpiar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
		Me.CmdLimpiar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
		Me.CmdLimpiar.Location = New System.Drawing.Point(56, 7)
		Me.CmdLimpiar.Margin = New System.Windows.Forms.Padding(2)
		Me.CmdLimpiar.Name = "CmdLimpiar"
		Me.CmdLimpiar.Size = New System.Drawing.Size(50, 54)
		Me.CmdLimpiar.TabIndex = 571
		Me.CmdLimpiar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
		Me.CmdLimpiar.ThemeName = "Aqua"
		'
		'CmdExp
		'
		Me.CmdExp.Image = Global.ATMFiscal.My.Resources.Resources.Exportar
		Me.CmdExp.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
		Me.CmdExp.Location = New System.Drawing.Point(164, 7)
		Me.CmdExp.Margin = New System.Windows.Forms.Padding(2)
		Me.CmdExp.Name = "CmdExp"
		Me.CmdExp.Size = New System.Drawing.Size(50, 54)
		Me.CmdExp.TabIndex = 569
		Me.CmdExp.TabStop = False
		Me.CmdExp.TextAlignment = System.Drawing.ContentAlignment.TopRight
		Me.CmdExp.ThemeName = "Aqua"
		'
		'CmdImportar
		'
		Me.CmdImportar.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
		Me.CmdImportar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
		Me.CmdImportar.Location = New System.Drawing.Point(110, 7)
		Me.CmdImportar.Margin = New System.Windows.Forms.Padding(2)
		Me.CmdImportar.Name = "CmdImportar"
		Me.CmdImportar.Size = New System.Drawing.Size(50, 54)
		Me.CmdImportar.TabIndex = 570
		Me.CmdImportar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
		Me.CmdImportar.ThemeName = "Aqua"
		'
		'Tabla
		'
		Me.Tabla.AllowUserToAddRows = False
		Me.Tabla.AllowUserToDeleteRows = False
		Me.Tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
		Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.Tabla.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Cta, Me.Cuent, Me.Desc, Me.Naturaleza, Me.M, Me.Deb, Me.Hab, Me.Saldo})
		Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Tabla.Location = New System.Drawing.Point(0, 134)
		Me.Tabla.Name = "Tabla"
		Me.Tabla.ReadOnly = True
		Me.Tabla.Size = New System.Drawing.Size(1108, 138)
		Me.Tabla.TabIndex = 11
		'
		'Cta
		'
		Me.Cta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
		Me.Cta.HeaderText = "Cuenta"
		Me.Cta.Name = "Cta"
		Me.Cta.ReadOnly = True
		Me.Cta.Visible = False
		'
		'Cuent
		'
		Me.Cuent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
		Me.Cuent.HeaderText = "Cuenta"
		Me.Cuent.Name = "Cuent"
		Me.Cuent.ReadOnly = True
		Me.Cuent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		Me.Cuent.Width = 64
		'
		'Desc
		'
		Me.Desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
		Me.Desc.HeaderText = "Descripcion"
		Me.Desc.Name = "Desc"
		Me.Desc.ReadOnly = True
		Me.Desc.Width = 114
		'
		'Naturaleza
		'
		Me.Naturaleza.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
		Me.Naturaleza.HeaderText = "Nat"
		Me.Naturaleza.Name = "Naturaleza"
		Me.Naturaleza.ReadOnly = True
		Me.Naturaleza.Width = 59
		'
		'M
		'
		Me.M.HeaderText = "Mes"
		Me.M.Name = "M"
		Me.M.ReadOnly = True
		Me.M.Width = 63
		'
		'Deb
		'
		Me.Deb.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
		DataGridViewCellStyle1.Format = "N2"
		DataGridViewCellStyle1.NullValue = "0"
		Me.Deb.DefaultCellStyle = DataGridViewCellStyle1
		Me.Deb.HeaderText = "Debe"
		Me.Deb.Name = "Deb"
		Me.Deb.ReadOnly = True
		Me.Deb.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		Me.Deb.Width = 50
		'
		'Hab
		'
		Me.Hab.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
		DataGridViewCellStyle2.Format = "N2"
		DataGridViewCellStyle2.NullValue = "0"
		Me.Hab.DefaultCellStyle = DataGridViewCellStyle2
		Me.Hab.HeaderText = "Haber"
		Me.Hab.Name = "Hab"
		Me.Hab.ReadOnly = True
		Me.Hab.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		Me.Hab.Width = 56
		'
		'Saldo
		'
		Me.Saldo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
		DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
		DataGridViewCellStyle3.Format = "N2"
		DataGridViewCellStyle3.NullValue = "0"
		Me.Saldo.DefaultCellStyle = DataGridViewCellStyle3
		Me.Saldo.HeaderText = "Saldo_final"
		Me.Saldo.Name = "Saldo"
		Me.Saldo.ReadOnly = True
		Me.Saldo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		Me.Saldo.Width = 91
		'
		'Mayores
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.LightSteelBlue
		Me.ClientSize = New System.Drawing.Size(1108, 272)
		Me.Controls.Add(Me.Tabla)
		Me.Controls.Add(Me.Panel)
		Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "Mayores"
		'
		'
		'
		Me.RootElement.ApplyShapeToControl = True
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Mayores"
		Me.ThemeName = "Material"
		CType(Me.Panel, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Panel.ResumeLayout(False)
		Me.Panel.PerformLayout()
		CType(Me.ComboAñoB, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.RadGroupBox1.ResumeLayout(False)
		Me.RadGroupBox1.PerformLayout()
		CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.CmdPdf, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.CmdExp, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents Ayuda As ToolTip
	Friend WithEvents Panel As Telerik.WinControls.UI.RadPanel
	Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdPdf As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdExp As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdImportar As Telerik.WinControls.UI.RadButton
	Friend WithEvents LstCliente As Listas
	Friend WithEvents Label5 As Label
	Friend WithEvents LstNiveles As Listas
	Friend WithEvents Label1 As Label
	Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Lstctainicial As Listas
    Friend WithEvents Label4 As Label
    Friend WithEvents Lstctafinal As Listas
    Friend WithEvents Label3 As Label
    Friend WithEvents Tabla As DataGridView
    Friend WithEvents Cta As DataGridViewTextBoxColumn
    Friend WithEvents Cuent As DataGridViewTextBoxColumn
    Friend WithEvents Desc As DataGridViewTextBoxColumn
    Friend WithEvents Naturaleza As DataGridViewTextBoxColumn
    Friend WithEvents M As DataGridViewTextBoxColumn
    Friend WithEvents Deb As DataGridViewTextBoxColumn
    Friend WithEvents Hab As DataGridViewTextBoxColumn
    Friend WithEvents Saldo As DataGridViewTextBoxColumn
    Friend WithEvents ComboAñoB As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents Label10 As Label
End Class

