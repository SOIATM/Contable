<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PolizasMasivas
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PolizasMasivas))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Barra = New Telerik.WinControls.UI.RadProgressBar()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.ComboMes = New Telerik.WinControls.UI.RadDropDownList()
        Me.comboAño = New Telerik.WinControls.UI.RadDropDownList()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TxtFiltro = New Telerik.WinControls.UI.RadTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ChkTodos = New System.Windows.Forms.CheckBox()
        Me.lblRegistros = New System.Windows.Forms.Label()
        Me.CmdAbrir = New Telerik.WinControls.UI.RadButton()
        Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
        Me.Cmd_Procesar = New Telerik.WinControls.UI.RadButton()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.CmdPdf = New Telerik.WinControls.UI.RadButton()
        Me.Tabla = New System.Windows.Forms.DataGridView()
        Me.Seleccion = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.An = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tpol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Np = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Des = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImpPol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Calculada = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MaterialBlueGreyTheme1 = New Telerik.WinControls.Themes.MaterialBlueGreyTheme()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.ComboMes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboAño, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtFiltro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdAbrir, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdPdf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.CadetBlue
        Me.RadPanel1.Controls.Add(Me.Label1)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label3)
        Me.RadPanel1.Controls.Add(Me.Barra)
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Controls.Add(Me.TxtFiltro)
        Me.RadPanel1.Controls.Add(Me.Label6)
        Me.RadPanel1.Controls.Add(Me.ChkTodos)
        Me.RadPanel1.Controls.Add(Me.lblRegistros)
        Me.RadPanel1.Controls.Add(Me.CmdAbrir)
        Me.RadPanel1.Controls.Add(Me.CmdLimpiar)
        Me.RadPanel1.Controls.Add(Me.Cmd_Procesar)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Controls.Add(Me.CmdPdf)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1232, 175)
        Me.RadPanel1.TabIndex = 0
        Me.RadPanel1.ThemeName = "Material"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(781, 120)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 18)
        Me.Label1.TabIndex = 727
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(298, 44)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(370, 36)
        Me.lstCliente.TabIndex = 726
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(294, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 18)
        Me.Label3.TabIndex = 725
        Me.Label3.Text = "Empresa:"
        '
        'Barra
        '
        Me.Barra.Location = New System.Drawing.Point(11, 84)
        Me.Barra.Name = "Barra"
        Me.Barra.Size = New System.Drawing.Size(266, 43)
        Me.Barra.TabIndex = 724
        Me.Barra.Text = " "
        Me.Barra.ThemeName = "Material"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox1.Controls.Add(Me.ComboMes)
        Me.RadGroupBox1.Controls.Add(Me.comboAño)
        Me.RadGroupBox1.Controls.Add(Me.Label13)
        Me.RadGroupBox1.Controls.Add(Me.Label14)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Periodo: "
        Me.RadGroupBox1.Location = New System.Drawing.Point(714, 15)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(358, 84)
        Me.RadGroupBox1.TabIndex = 661
        Me.RadGroupBox1.Text = "Periodo: "
        Me.RadGroupBox1.ThemeName = "Material"
        '
        'ComboMes
        '
        Me.ComboMes.Location = New System.Drawing.Point(185, 32)
        Me.ComboMes.Name = "ComboMes"
        Me.ComboMes.Size = New System.Drawing.Size(125, 36)
        Me.ComboMes.TabIndex = 658
        Me.ComboMes.Text = " "
        Me.ComboMes.ThemeName = "Material"
        '
        'comboAño
        '
        Me.comboAño.Location = New System.Drawing.Point(45, 32)
        Me.comboAño.Name = "comboAño"
        Me.comboAño.Size = New System.Drawing.Size(125, 36)
        Me.comboAño.TabIndex = 657
        Me.comboAño.Text = " "
        Me.comboAño.ThemeName = "Material"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(84, 8)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(34, 18)
        Me.Label13.TabIndex = 656
        Me.Label13.Text = "Año:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(194, 8)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(75, 18)
        Me.Label14.TabIndex = 655
        Me.Label14.Text = "Mes Inicial:"
        '
        'TxtFiltro
        '
        Me.TxtFiltro.AutoSize = False
        Me.TxtFiltro.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFiltro.Location = New System.Drawing.Point(785, 111)
        Me.TxtFiltro.Name = "TxtFiltro"
        Me.TxtFiltro.Size = New System.Drawing.Size(287, 36)
        Me.TxtFiltro.TabIndex = 723
        Me.TxtFiltro.ThemeName = "Material"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.CadetBlue
        Me.Label6.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(713, 119)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 18)
        Me.Label6.TabIndex = 722
        Me.Label6.Text = "Filtrar:"
        '
        'ChkTodos
        '
        Me.ChkTodos.AutoSize = True
        Me.ChkTodos.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkTodos.Location = New System.Drawing.Point(359, 123)
        Me.ChkTodos.Name = "ChkTodos"
        Me.ChkTodos.Size = New System.Drawing.Size(223, 22)
        Me.ChkTodos.TabIndex = 721
        Me.ChkTodos.Text = "Selecciona Todos / Ninguno"
        Me.ChkTodos.UseVisualStyleBackColor = True
        '
        'lblRegistros
        '
        Me.lblRegistros.AutoSize = True
        Me.lblRegistros.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegistros.Location = New System.Drawing.Point(402, 84)
        Me.lblRegistros.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRegistros.Name = "lblRegistros"
        Me.lblRegistros.Size = New System.Drawing.Size(143, 21)
        Me.lblRegistros.TabIndex = 720
        Me.lblRegistros.Text = "Total de Polizas:"
        '
        'CmdAbrir
        '
        Me.CmdAbrir.Image = Global.ATMFiscal.My.Resources.Resources.documentos
        Me.CmdAbrir.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdAbrir.Location = New System.Drawing.Point(227, 20)
        Me.CmdAbrir.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdAbrir.Name = "CmdAbrir"
        Me.CmdAbrir.Size = New System.Drawing.Size(50, 54)
        Me.CmdAbrir.TabIndex = 595
        Me.CmdAbrir.TabStop = False
        Me.CmdAbrir.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdAbrir.ThemeName = "Aqua"
        '
        'CmdLimpiar
        '
        Me.CmdLimpiar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiar.Location = New System.Drawing.Point(65, 20)
        Me.CmdLimpiar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiar.Name = "CmdLimpiar"
        Me.CmdLimpiar.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiar.TabIndex = 593
        Me.CmdLimpiar.TabStop = False
        Me.CmdLimpiar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdLimpiar.ThemeName = "Aqua"
        '
        'Cmd_Procesar
        '
        Me.Cmd_Procesar.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.Cmd_Procesar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Cmd_Procesar.Location = New System.Drawing.Point(119, 20)
        Me.Cmd_Procesar.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmd_Procesar.Name = "Cmd_Procesar"
        Me.Cmd_Procesar.Size = New System.Drawing.Size(50, 54)
        Me.Cmd_Procesar.TabIndex = 594
        Me.Cmd_Procesar.TabStop = False
        Me.Cmd_Procesar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.Cmd_Procesar.ThemeName = "Aqua"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(11, 20)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 596
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'CmdPdf
        '
        Me.CmdPdf.Image = Global.ATMFiscal.My.Resources.Resources.PDF
        Me.CmdPdf.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdPdf.Location = New System.Drawing.Point(173, 20)
        Me.CmdPdf.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdPdf.Name = "CmdPdf"
        Me.CmdPdf.Size = New System.Drawing.Size(50, 54)
        Me.CmdPdf.TabIndex = 597
        Me.CmdPdf.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdPdf.ThemeName = "Aqua"
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Seleccion, Me.An, Me.MS, Me.Tpol, Me.Np, Me.Dp, Me.Des, Me.ImpPol, Me.Calculada})
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.Location = New System.Drawing.Point(0, 175)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.RowTemplate.Height = 24
        Me.Tabla.Size = New System.Drawing.Size(1232, 294)
        Me.Tabla.TabIndex = 3
        '
        'Seleccion
        '
        Me.Seleccion.HeaderText = "Selección"
        Me.Seleccion.Name = "Seleccion"
        '
        'An
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.An.DefaultCellStyle = DataGridViewCellStyle1
        Me.An.HeaderText = "Año_Poliza"
        Me.An.Name = "An"
        Me.An.ReadOnly = True
        '
        'MS
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.MS.DefaultCellStyle = DataGridViewCellStyle2
        Me.MS.HeaderText = "Mes_Poliza"
        Me.MS.Name = "MS"
        Me.MS.ReadOnly = True
        '
        'Tpol
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Tpol.DefaultCellStyle = DataGridViewCellStyle3
        Me.Tpol.HeaderText = "Tipo_Poliza"
        Me.Tpol.Name = "Tpol"
        Me.Tpol.ReadOnly = True
        '
        'Np
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Np.DefaultCellStyle = DataGridViewCellStyle4
        Me.Np.HeaderText = "Num_Poliza"
        Me.Np.Name = "Np"
        Me.Np.ReadOnly = True
        '
        'Dp
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Dp.DefaultCellStyle = DataGridViewCellStyle5
        Me.Dp.HeaderText = "Dia_Poliza"
        Me.Dp.Name = "Dp"
        Me.Dp.ReadOnly = True
        '
        'Des
        '
        Me.Des.HeaderText = "Descripcion"
        Me.Des.Name = "Des"
        Me.Des.ReadOnly = True
        '
        'ImpPol
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "C2"
        DataGridViewCellStyle6.NullValue = "0"
        Me.ImpPol.DefaultCellStyle = DataGridViewCellStyle6
        Me.ImpPol.HeaderText = "Importe"
        Me.ImpPol.Name = "ImpPol"
        Me.ImpPol.ReadOnly = True
        '
        'Calculada
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Calculada.DefaultCellStyle = DataGridViewCellStyle7
        Me.Calculada.HeaderText = "Poliza"
        Me.Calculada.Name = "Calculada"
        Me.Calculada.ReadOnly = True
        '
        'PolizasMasivas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1232, 469)
        Me.Controls.Add(Me.Tabla)
        Me.Controls.Add(Me.RadPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PolizasMasivas"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Imprimir Pólizas"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.ComboMes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comboAño, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtFiltro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdAbrir, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdPdf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents CmdAbrir As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
    Friend WithEvents Cmd_Procesar As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdPdf As Telerik.WinControls.UI.RadButton
    Friend WithEvents ChkTodos As CheckBox
    Friend WithEvents lblRegistros As Label
    Friend WithEvents TxtFiltro As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents ComboMes As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents comboAño As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Barra As Telerik.WinControls.UI.RadProgressBar
    Friend WithEvents Label1 As Label
    Friend WithEvents lstCliente As Listas
    Friend WithEvents Label3 As Label
    Friend WithEvents Tabla As DataGridView
    Friend WithEvents Seleccion As DataGridViewCheckBoxColumn
    Friend WithEvents An As DataGridViewTextBoxColumn
    Friend WithEvents MS As DataGridViewTextBoxColumn
    Friend WithEvents Tpol As DataGridViewTextBoxColumn
    Friend WithEvents Np As DataGridViewTextBoxColumn
    Friend WithEvents Dp As DataGridViewTextBoxColumn
    Friend WithEvents Des As DataGridViewTextBoxColumn
    Friend WithEvents ImpPol As DataGridViewTextBoxColumn
    Friend WithEvents Calculada As DataGridViewTextBoxColumn
    Friend WithEvents MaterialBlueGreyTheme1 As Telerik.WinControls.Themes.MaterialBlueGreyTheme
End Class

