<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LiberarParcialidades
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LiberarParcialidades))
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.Cmd_Procesar = New Telerik.WinControls.UI.RadButton()
        Me.Button1 = New Telerik.WinControls.UI.RadButton()
        Me.ChkTodos = New System.Windows.Forms.CheckBox()
        Me.LstUUIDS = New ATMFiscal.Listas()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tabla = New System.Windows.Forms.DataGridView()
        Me.Seleccion = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.UUID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Poliza = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalReal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.SP1 = New System.ComponentModel.BackgroundWorker()
        Me.SP2 = New System.ComponentModel.BackgroundWorker()
        Me.CmdCerrar = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Button1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(838, 170)
        Me.RadPanel1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox1.Controls.Add(Me.CmdCerrar)
        Me.RadGroupBox1.Controls.Add(Me.Cmd_Procesar)
        Me.RadGroupBox1.Controls.Add(Me.Button1)
        Me.RadGroupBox1.Controls.Add(Me.ChkTodos)
        Me.RadGroupBox1.Controls.Add(Me.LstUUIDS)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.Controls.Add(Me.lstCliente)
        Me.RadGroupBox1.Controls.Add(Me.Label3)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Parámetros"
        Me.RadGroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(816, 144)
        Me.RadGroupBox1.TabIndex = 577
        Me.RadGroupBox1.Text = "Parámetros"
        Me.RadGroupBox1.ThemeName = "Material"
        '
        'Cmd_Procesar
        '
        Me.Cmd_Procesar.Image = Global.ATMFiscal.My.Resources.Resources.Procesar
        Me.Cmd_Procesar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Cmd_Procesar.Location = New System.Drawing.Point(758, 7)
        Me.Cmd_Procesar.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmd_Procesar.Name = "Cmd_Procesar"
        Me.Cmd_Procesar.Size = New System.Drawing.Size(50, 54)
        Me.Cmd_Procesar.TabIndex = 580
        Me.Cmd_Procesar.TabStop = False
        Me.Cmd_Procesar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.Cmd_Procesar.ThemeName = "Aqua"
        '
        'Button1
        '
        Me.Button1.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.Button1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Button1.Location = New System.Drawing.Point(704, 7)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(50, 54)
        Me.Button1.TabIndex = 581
        Me.Button1.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.Button1.ThemeName = "Aqua"
        '
        'ChkTodos
        '
        Me.ChkTodos.AutoSize = True
        Me.ChkTodos.Location = New System.Drawing.Point(524, 110)
        Me.ChkTodos.Name = "ChkTodos"
        Me.ChkTodos.Size = New System.Drawing.Size(166, 25)
        Me.ChkTodos.TabIndex = 99
        Me.ChkTodos.Text = "Todos / Ninguno"
        Me.ChkTodos.UseVisualStyleBackColor = True
        '
        'LstUUIDS
        '
        Me.LstUUIDS.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstUUIDS.Location = New System.Drawing.Point(418, 67)
        Me.LstUUIDS.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstUUIDS.Name = "LstUUIDS"
        Me.LstUUIDS.SelectItem = ""
        Me.LstUUIDS.SelectText = ""
        Me.LstUUIDS.Size = New System.Drawing.Size(370, 36)
        Me.LstUUIDS.TabIndex = 98
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(414, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 18)
        Me.Label1.TabIndex = 97
        Me.Label1.Text = "UUID Relacion:"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(5, 67)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(394, 36)
        Me.lstCliente.TabIndex = 96
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 18)
        Me.Label3.TabIndex = 95
        Me.Label3.Text = "Empresa:"
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.Tabla.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Seleccion, Me.UUID, Me.Poliza, Me.TotalReal})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        DataGridViewCellStyle3.NullValue = Nothing
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Tabla.DefaultCellStyle = DataGridViewCellStyle3
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.Location = New System.Drawing.Point(0, 170)
        Me.Tabla.Name = "Tabla"
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.Tabla.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.Tabla.Size = New System.Drawing.Size(838, 328)
        Me.Tabla.TabIndex = 54
        '
        'Seleccion
        '
        Me.Seleccion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Seleccion.HeaderText = "Seleccion"
        Me.Seleccion.Name = "Seleccion"
        Me.Seleccion.Width = 95
        '
        'UUID
        '
        Me.UUID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.UUID.HeaderText = "UUID Relacion"
        Me.UUID.Name = "UUID"
        Me.UUID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.UUID.Width = 132
        '
        'Poliza
        '
        Me.Poliza.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Poliza.HeaderText = "Poliza Interna"
        Me.Poliza.Name = "Poliza"
        Me.Poliza.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Poliza.Width = 129
        '
        'TotalReal
        '
        Me.TotalReal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "C2"
        DataGridViewCellStyle2.NullValue = "0"
        Me.TotalReal.DefaultCellStyle = DataGridViewCellStyle2
        Me.TotalReal.HeaderText = "Importe Pago"
        Me.TotalReal.Name = "TotalReal"
        Me.TotalReal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TotalReal.Width = 126
        '
        'SP1
        '
        Me.SP1.WorkerSupportsCancellation = True
        '
        'SP2
        '
        Me.SP2.WorkerSupportsCancellation = True
        '
        'CmdCerrar
        '
        Me.CmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.CmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdCerrar.Location = New System.Drawing.Point(650, 7)
        Me.CmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdCerrar.Name = "CmdCerrar"
        Me.CmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.CmdCerrar.TabIndex = 582
        Me.CmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdCerrar.ThemeName = "Aqua"
        '
        'LiberarParcialidades
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(838, 498)
        Me.ControlBox = False
        Me.Controls.Add(Me.Tabla)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "LiberarParcialidades"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "P_Master"
        Me.Text = "Liberar Parcialidades"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Button1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
	Friend WithEvents Cmd_Procesar As Telerik.WinControls.UI.RadButton
	Friend WithEvents Button1 As Telerik.WinControls.UI.RadButton
	Friend WithEvents ChkTodos As CheckBox
	Friend WithEvents LstUUIDS As Listas
	Friend WithEvents Label1 As Label
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label3 As Label
	Friend WithEvents Tabla As DataGridView
	Friend WithEvents Seleccion As DataGridViewCheckBoxColumn
	Friend WithEvents UUID As DataGridViewTextBoxColumn
	Friend WithEvents Poliza As DataGridViewTextBoxColumn
	Friend WithEvents TotalReal As DataGridViewTextBoxColumn
	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents SP1 As System.ComponentModel.BackgroundWorker
	Friend WithEvents SP2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents CmdCerrar As Telerik.WinControls.UI.RadButton
End Class

