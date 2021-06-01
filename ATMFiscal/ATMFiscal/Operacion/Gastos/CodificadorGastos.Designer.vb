<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CodificadorGastos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CodificadorGastos))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.SegundoPlano = New System.ComponentModel.BackgroundWorker()
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Tabla = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TablaMigrar = New System.Windows.Forms.DataGridView()
        Me.Imagenes = New System.Windows.Forms.ImageList(Me.components)
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.Cmd_Procesar = New Telerik.WinControls.UI.RadButton()
        Me.CmdBuscar = New Telerik.WinControls.UI.RadButton()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Dtfin = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.DtInicio = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TabPage1.SuspendLayout()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.TablaMigrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.Dtfin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtInicio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SegundoPlano
        '
        Me.SegundoPlano.WorkerSupportsCancellation = True
        '
        'Ayuda
        '
        Me.Ayuda.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Ayuda.IsBalloon = True
        Me.Ayuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.Ayuda.ToolTipTitle = "Información:"
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Tabla)
        Me.TabPage1.ImageIndex = 0
        Me.TabPage1.Location = New System.Drawing.Point(4, 39)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Size = New System.Drawing.Size(1342, 414)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Facturas para Recontabilizar"
        Me.Ayuda.SetToolTip(Me.TabPage1, "Se Muestran las Facturas liberadas de Ordenes")
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.Location = New System.Drawing.Point(4, 4)
        Me.Tabla.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.RowTemplate.Height = 24
        Me.Tabla.Size = New System.Drawing.Size(1334, 406)
        Me.Tabla.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TablaMigrar)
        Me.TabPage2.ImageIndex = 0
        Me.TabPage2.Location = New System.Drawing.Point(4, 39)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage2.Size = New System.Drawing.Size(1342, 414)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Facturas para migrar de Tusa"
        Me.Ayuda.SetToolTip(Me.TabPage2, "Se muestran las Ordenes contabilziadas Previamente sin Poliza Fiscal")
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TablaMigrar
        '
        Me.TablaMigrar.AllowUserToAddRows = False
        Me.TablaMigrar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaMigrar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaMigrar.Location = New System.Drawing.Point(4, 4)
        Me.TablaMigrar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TablaMigrar.Name = "TablaMigrar"
        Me.TablaMigrar.RowTemplate.Height = 24
        Me.TablaMigrar.Size = New System.Drawing.Size(1334, 406)
        Me.TablaMigrar.TabIndex = 2
        '
        'Imagenes
        '
        Me.Imagenes.ImageStream = CType(resources.GetObject("Imagenes.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Imagenes.TransparentColor = System.Drawing.Color.Transparent
        Me.Imagenes.Images.SetKeyName(0, "XML32.ico")
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.CadetBlue
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label2)
        Me.RadPanel1.Controls.Add(Me.CmdLimpiar)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Controls.Add(Me.Cmd_Procesar)
        Me.RadPanel1.Controls.Add(Me.CmdBuscar)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1350, 111)
        Me.RadPanel1.TabIndex = 0
        Me.RadPanel1.ThemeName = "Material"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(233, 41)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(423, 36)
        Me.lstCliente.TabIndex = 108
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(229, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 18)
        Me.Label2.TabIndex = 107
        Me.Label2.Text = "Empresa:"
        '
        'CmdLimpiar
        '
        Me.CmdLimpiar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiar.Location = New System.Drawing.Point(62, 23)
        Me.CmdLimpiar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiar.Name = "CmdLimpiar"
        Me.CmdLimpiar.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiar.TabIndex = 106
        Me.CmdLimpiar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdLimpiar.ThemeName = "Aqua"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = CType(resources.GetObject("cmdCerrar.Image"), System.Drawing.Image)
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(9, 23)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 103
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'Cmd_Procesar
        '
        Me.Cmd_Procesar.Image = Global.ATMFiscal.My.Resources.Resources.Procesar
        Me.Cmd_Procesar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Cmd_Procesar.Location = New System.Drawing.Point(169, 23)
        Me.Cmd_Procesar.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmd_Procesar.Name = "Cmd_Procesar"
        Me.Cmd_Procesar.Size = New System.Drawing.Size(50, 54)
        Me.Cmd_Procesar.TabIndex = 105
        Me.Cmd_Procesar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.Cmd_Procesar.ThemeName = "Aqua"
        '
        'CmdBuscar
        '
        Me.CmdBuscar.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdBuscar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdBuscar.Location = New System.Drawing.Point(115, 23)
        Me.CmdBuscar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdBuscar.Name = "CmdBuscar"
        Me.CmdBuscar.Size = New System.Drawing.Size(50, 54)
        Me.CmdBuscar.TabIndex = 104
        Me.CmdBuscar.TabStop = False
        Me.CmdBuscar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdBuscar.ThemeName = "Aqua"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.Imagenes
        Me.TabControl1.Location = New System.Drawing.Point(0, 111)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1350, 457)
        Me.TabControl1.TabIndex = 3
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
        Me.RadGroupBox1.Location = New System.Drawing.Point(693, 5)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(643, 97)
        Me.RadGroupBox1.TabIndex = 576
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
        'CodificadorGastos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(1350, 568)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.RadPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CodificadorGastos"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Contabilizador de Gastos"
        Me.ThemeName = "MaterialBlueGrey"
        Me.TabPage1.ResumeLayout(False)
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.TablaMigrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
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
    Friend WithEvents SegundoPlano As System.ComponentModel.BackgroundWorker
    Friend WithEvents Ayuda As ToolTip
    Friend WithEvents Imagenes As ImageList
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Tabla As DataGridView
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TablaMigrar As DataGridView
    Friend WithEvents lstCliente As Listas
    Friend WithEvents Label2 As Label
    Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
    Friend WithEvents Cmd_Procesar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdBuscar As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents Dtfin As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents DtInicio As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
End Class

