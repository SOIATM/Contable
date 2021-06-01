<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EstadodeResultados
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EstadodeResultados))
		Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
		Me.LstCliente = New ATMFiscal.Listas()
		Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
		Me.Dtfin = New System.Windows.Forms.DateTimePicker()
		Me.DtInicio = New System.Windows.Forms.DateTimePicker()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
		Me.CmdPdf = New Telerik.WinControls.UI.RadButton()
		Me.CmdExp = New Telerik.WinControls.UI.RadButton()
		Me.CmdImportar = New Telerik.WinControls.UI.RadButton()
		Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
		Me.Tabla = New System.Windows.Forms.DataGridView()
		Me.CtaAc = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Signo = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Saldof = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Tip = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
		Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
		Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
		CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.RadPanel1.SuspendLayout()
		CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.RadGroupBox1.SuspendLayout()
		CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.CmdPdf, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.CmdExp, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
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
		Me.RadPanel1.Size = New System.Drawing.Size(1159, 100)
		Me.RadPanel1.TabIndex = 1
		'
		'LstCliente
		'
		Me.LstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LstCliente.Location = New System.Drawing.Point(276, 23)
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
		Me.RadGroupBox1.Size = New System.Drawing.Size(457, 88)
		Me.RadGroupBox1.TabIndex = 575
		Me.RadGroupBox1.Text = "Período"
		Me.RadGroupBox1.ThemeName = "Material"
		'
		'Dtfin
		'
		Me.Dtfin.Location = New System.Drawing.Point(242, 52)
		Me.Dtfin.Name = "Dtfin"
		Me.Dtfin.Size = New System.Drawing.Size(200, 20)
		Me.Dtfin.TabIndex = 99
		'
		'DtInicio
		'
		Me.DtInicio.Location = New System.Drawing.Point(14, 52)
		Me.DtInicio.Name = "DtInicio"
		Me.DtInicio.Size = New System.Drawing.Size(200, 20)
		Me.DtInicio.TabIndex = 98
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(239, 31)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(97, 17)
		Me.Label1.TabIndex = 96
		Me.Label1.Text = "Fecha Final:"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(11, 31)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(104, 17)
		Me.Label2.TabIndex = 97
		Me.Label2.Text = "Fecha Inicial:"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Location = New System.Drawing.Point(273, 4)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(76, 17)
		Me.Label5.TabIndex = 576
		Me.Label5.Text = "Empresa:"
		'
		'cmdCerrar
		'
		Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
		Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdCerrar.Location = New System.Drawing.Point(2, 2)
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
		Me.CmdPdf.Location = New System.Drawing.Point(218, 2)
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
		Me.CmdExp.Location = New System.Drawing.Point(164, 2)
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
		Me.CmdImportar.Location = New System.Drawing.Point(110, 2)
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
		Me.CmdLimpiar.Location = New System.Drawing.Point(56, 2)
		Me.CmdLimpiar.Margin = New System.Windows.Forms.Padding(2)
		Me.CmdLimpiar.Name = "CmdLimpiar"
		Me.CmdLimpiar.Size = New System.Drawing.Size(50, 54)
		Me.CmdLimpiar.TabIndex = 581
		Me.CmdLimpiar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
		Me.CmdLimpiar.ThemeName = "Aqua"
		'
		'Tabla
		'
		Me.Tabla.AllowUserToAddRows = False
		Me.Tabla.AllowUserToDeleteRows = False
		Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.Tabla.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CtaAc, Me.Desc, Me.Signo, Me.Saldof, Me.Tip})
		Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Tabla.Location = New System.Drawing.Point(0, 100)
		Me.Tabla.Name = "Tabla"
		Me.Tabla.ReadOnly = True
		Me.Tabla.Size = New System.Drawing.Size(1159, 302)
		Me.Tabla.TabIndex = 15
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
		Me.Desc.Width = 95
		'
		'Signo
		'
		Me.Signo.HeaderText = "S"
		Me.Signo.Name = "Signo"
		Me.Signo.ReadOnly = True
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
		'Tip
		'
		Me.Tip.HeaderText = "Tipo"
		Me.Tip.Name = "Tip"
		Me.Tip.ReadOnly = True
		Me.Tip.Visible = False
		'
		'Ayuda
		'
		Me.Ayuda.IsBalloon = True
		Me.Ayuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
		'
		'EstadodeResultados
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.LightSteelBlue
		Me.ClientSize = New System.Drawing.Size(1159, 402)
		Me.Controls.Add(Me.Tabla)
		Me.Controls.Add(Me.RadPanel1)
		Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "EstadodeResultados"
		'
		'
		'
		Me.RootElement.ApplyShapeToControl = True
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Estado de Resultados"
		Me.ThemeName = "Material"
		CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.RadPanel1.ResumeLayout(False)
		Me.RadPanel1.PerformLayout()
		CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.RadGroupBox1.ResumeLayout(False)
		Me.RadGroupBox1.PerformLayout()
		CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.CmdPdf, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.CmdExp, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents LstCliente As Listas
	Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
	Friend WithEvents Dtfin As DateTimePicker
	Friend WithEvents DtInicio As DateTimePicker
	Friend WithEvents Label1 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents Label5 As Label
	Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdPdf As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdExp As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdImportar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
	Friend WithEvents Tabla As DataGridView
	Friend WithEvents CtaAc As DataGridViewTextBoxColumn
	Friend WithEvents Desc As DataGridViewTextBoxColumn
	Friend WithEvents Signo As DataGridViewTextBoxColumn
	Friend WithEvents Saldof As DataGridViewTextBoxColumn
	Friend WithEvents Tip As DataGridViewTextBoxColumn
	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents Ayuda As ToolTip
End Class

