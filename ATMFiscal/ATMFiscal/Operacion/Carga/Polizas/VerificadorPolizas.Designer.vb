<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VerificadorPolizas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VerificadorPolizas))
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.CmdExp = New Telerik.WinControls.UI.RadButton()
        Me.Barra = New Telerik.WinControls.UI.RadProgressBar()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.CmdImportar = New Telerik.WinControls.UI.RadButton()
        Me.Tabla = New System.Windows.Forms.DataGridView()
        Me.Anio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tip = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nump = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Conce = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cargo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Abono = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dif = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Dtfin = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.DtInicio = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LbLtOTAL = New System.Windows.Forms.Label()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmdExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.Dtfin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtInicio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.CadetBlue
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Controls.Add(Me.LbLtOTAL)
        Me.RadPanel1.Controls.Add(Me.CmdExp)
        Me.RadPanel1.Controls.Add(Me.Barra)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label2)
        Me.RadPanel1.Controls.Add(Me.CmdLimpiar)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Controls.Add(Me.CmdImportar)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1387, 128)
        Me.RadPanel1.TabIndex = 0
        Me.RadPanel1.ThemeName = "Material"
        '
        'CmdExp
        '
        Me.CmdExp.Image = Global.ATMFiscal.My.Resources.Resources.Exportar
        Me.CmdExp.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdExp.Location = New System.Drawing.Point(171, 11)
        Me.CmdExp.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdExp.Name = "CmdExp"
        Me.CmdExp.Size = New System.Drawing.Size(50, 54)
        Me.CmdExp.TabIndex = 584
        Me.CmdExp.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdExp.ThemeName = "Aqua"
        '
        'Barra
        '
        Me.Barra.Location = New System.Drawing.Point(12, 70)
        Me.Barra.Name = "Barra"
        Me.Barra.Size = New System.Drawing.Size(209, 43)
        Me.Barra.TabIndex = 517
        Me.Barra.ThemeName = "Material"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(243, 29)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(456, 36)
        Me.lstCliente.TabIndex = 104
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(239, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 18)
        Me.Label2.TabIndex = 103
        Me.Label2.Text = "Empresa:"
        '
        'CmdLimpiar
        '
        Me.CmdLimpiar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiar.Location = New System.Drawing.Point(64, 11)
        Me.CmdLimpiar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiar.Name = "CmdLimpiar"
        Me.CmdLimpiar.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiar.TabIndex = 11
        Me.CmdLimpiar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdLimpiar.ThemeName = "Aqua"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = CType(resources.GetObject("cmdCerrar.Image"), System.Drawing.Image)
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(11, 11)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 9
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'CmdImportar
        '
        Me.CmdImportar.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdImportar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdImportar.Location = New System.Drawing.Point(117, 11)
        Me.CmdImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdImportar.Name = "CmdImportar"
        Me.CmdImportar.Size = New System.Drawing.Size(50, 54)
        Me.CmdImportar.TabIndex = 10
        Me.CmdImportar.TabStop = False
        Me.CmdImportar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdImportar.ThemeName = "Aqua"
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.AllowUserToDeleteRows = False
        Me.Tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.Tabla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Anio, Me.Mes, Me.Tip, Me.Nump, Me.dia, Me.Conce, Me.Cargo, Me.Abono, Me.Dif, Me.Fecha})
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.Location = New System.Drawing.Point(0, 128)
        Me.Tabla.Margin = New System.Windows.Forms.Padding(4)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.ReadOnly = True
        Me.Tabla.Size = New System.Drawing.Size(1387, 531)
        Me.Tabla.TabIndex = 15
        '
        'Anio
        '
        Me.Anio.HeaderText = "Año"
        Me.Anio.Name = "Anio"
        Me.Anio.ReadOnly = True
        Me.Anio.Width = 71
        '
        'Mes
        '
        Me.Mes.HeaderText = "Mes"
        Me.Mes.Name = "Mes"
        Me.Mes.ReadOnly = True
        Me.Mes.Width = 74
        '
        'Tip
        '
        Me.Tip.HeaderText = "Tipo de Poliza"
        Me.Tip.Name = "Tip"
        Me.Tip.ReadOnly = True
        Me.Tip.Width = 137
        '
        'Nump
        '
        Me.Nump.HeaderText = "Numero de Poliza"
        Me.Nump.Name = "Nump"
        Me.Nump.ReadOnly = True
        Me.Nump.Width = 121
        '
        'dia
        '
        Me.dia.HeaderText = "Dia"
        Me.dia.Name = "dia"
        Me.dia.ReadOnly = True
        Me.dia.Width = 65
        '
        'Conce
        '
        Me.Conce.HeaderText = "Conceptos"
        Me.Conce.Name = "Conce"
        Me.Conce.ReadOnly = True
        Me.Conce.Width = 125
        '
        'Cargo
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = "0"
        Me.Cargo.DefaultCellStyle = DataGridViewCellStyle4
        Me.Cargo.HeaderText = "Cargos"
        Me.Cargo.Name = "Cargo"
        Me.Cargo.ReadOnly = True
        Me.Cargo.Width = 96
        '
        'Abono
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = "0"
        Me.Abono.DefaultCellStyle = DataGridViewCellStyle5
        Me.Abono.HeaderText = "Abonos"
        Me.Abono.Name = "Abono"
        Me.Abono.ReadOnly = True
        '
        'Dif
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = "0"
        Me.Dif.DefaultCellStyle = DataGridViewCellStyle6
        Me.Dif.HeaderText = "Diferencia"
        Me.Dif.Name = "Dif"
        Me.Dif.ReadOnly = True
        Me.Dif.Width = 120
        '
        'Fecha
        '
        Me.Fecha.HeaderText = "Fecha"
        Me.Fecha.Name = "Fecha"
        Me.Fecha.ReadOnly = True
        Me.Fecha.Width = 88
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox1.Controls.Add(Me.Dtfin)
        Me.RadGroupBox1.Controls.Add(Me.DtInicio)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.Controls.Add(Me.Label5)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Período"
        Me.RadGroupBox1.Location = New System.Drawing.Point(720, 16)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(654, 97)
        Me.RadGroupBox1.TabIndex = 586
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
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(11, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 18)
        Me.Label5.TabIndex = 97
        Me.Label5.Text = "Fecha Inicial:"
        '
        'LbLtOTAL
        '
        Me.LbLtOTAL.AutoSize = True
        Me.LbLtOTAL.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbLtOTAL.Location = New System.Drawing.Point(240, 83)
        Me.LbLtOTAL.Name = "LbLtOTAL"
        Me.LbLtOTAL.Size = New System.Drawing.Size(0, 18)
        Me.LbLtOTAL.TabIndex = 585
        '
        'VerificadorPolizas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1387, 659)
        Me.Controls.Add(Me.Tabla)
        Me.Controls.Add(Me.RadPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "VerificadorPolizas"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Verificador de cuadre de Pólizas"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.CmdExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.Dtfin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtInicio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdImportar As Telerik.WinControls.UI.RadButton
    Friend WithEvents lstCliente As Listas
    Friend WithEvents Label2 As Label
    Friend WithEvents Barra As Telerik.WinControls.UI.RadProgressBar
    Friend WithEvents Tabla As DataGridView
    Friend WithEvents Anio As DataGridViewTextBoxColumn
    Friend WithEvents Mes As DataGridViewTextBoxColumn
    Friend WithEvents Tip As DataGridViewTextBoxColumn
    Friend WithEvents Nump As DataGridViewTextBoxColumn
    Friend WithEvents dia As DataGridViewTextBoxColumn
    Friend WithEvents Conce As DataGridViewTextBoxColumn
    Friend WithEvents Cargo As DataGridViewTextBoxColumn
    Friend WithEvents Abono As DataGridViewTextBoxColumn
    Friend WithEvents Dif As DataGridViewTextBoxColumn
    Friend WithEvents Fecha As DataGridViewTextBoxColumn
    Friend WithEvents CmdExp As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Dtfin As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents DtInicio As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents LbLtOTAL As Label
End Class

