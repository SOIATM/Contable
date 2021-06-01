<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Control_de_Empresas
    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Control_de_Empresas))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.TablaEmpresas = New ATMFiscal.Tabla_Filtro()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstEmpresa = New ATMFiscal.Listas()
        Me.lstRazonSocial = New ATMFiscal.Listas()
        Me.lstRfc = New ATMFiscal.Listas()
        Me.LstGiro = New ATMFiscal.Listas()
        Me.LstCodigoPostal = New ATMFiscal.Listas()
        Me.txtTelefono = New Telerik.WinControls.UI.RadTextBox()
        Me.txtTelefono2 = New Telerik.WinControls.UI.RadTextBox()
        Me.TxtTelefono3 = New Telerik.WinControls.UI.RadTextBox()
        Me.TxtDireccion = New Telerik.WinControls.UI.RadTextBox()
        Me.txtCorreo = New Telerik.WinControls.UI.RadTextBox()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.txtTelefono, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTelefono2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTelefono3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDireccion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCorreo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadPanel1.Controls.Add(Me.TablaEmpresas)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1417, 256)
        Me.RadPanel1.TabIndex = 0
        Me.RadPanel1.ThemeName = "Material"
        '
        'TablaEmpresas
        '
        Me.TablaEmpresas.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TablaEmpresas.CmdActualizar_enabled = True
        Me.TablaEmpresas.Cmdcerrar_enabled = True
        Me.TablaEmpresas.CmdEliminar_enabled = True
        Me.TablaEmpresas.CmdExportaExcel_enabled = True
        Me.TablaEmpresas.Cmdguardar_enabled = True
        Me.TablaEmpresas.CmdNuevo_enabled = True
        Me.TablaEmpresas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaEmpresas.Location = New System.Drawing.Point(0, 0)
        Me.TablaEmpresas.Name = "TablaEmpresas"
        Me.TablaEmpresas.Size = New System.Drawing.Size(1417, 256)
        Me.TablaEmpresas.SqlSelect = "select"
        Me.TablaEmpresas.TabIndex = 0
        Me.TablaEmpresas.Tag = "P_Master"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(17, 311)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(41, 18)
        Me.Label10.TabIndex = 34
        Me.Label10.Text = "Mail:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(15, 217)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(40, 18)
        Me.Label9.TabIndex = 33
        Me.Label9.Text = "Giro:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(589, 123)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(83, 18)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "Teléfono 3:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(295, 123)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 18)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "Teléfono 2:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(883, 123)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(105, 18)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "Codigo Postal:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(15, 123)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 18)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Teléfono:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(358, 217)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 18)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Dirección:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(883, 29)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 18)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "RFC:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 18)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Empresa:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(445, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 18)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Razon Social:"
        '
        'lstEmpresa
        '
        Me.lstEmpresa.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstEmpresa.Location = New System.Drawing.Point(15, 58)
        Me.lstEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstEmpresa.Name = "lstEmpresa"
        Me.lstEmpresa.SelectItem = ""
        Me.lstEmpresa.SelectText = ""
        Me.lstEmpresa.Size = New System.Drawing.Size(397, 36)
        Me.lstEmpresa.TabIndex = 0
        '
        'lstRazonSocial
        '
        Me.lstRazonSocial.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstRazonSocial.Location = New System.Drawing.Point(448, 58)
        Me.lstRazonSocial.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstRazonSocial.Name = "lstRazonSocial"
        Me.lstRazonSocial.SelectItem = ""
        Me.lstRazonSocial.SelectText = ""
        Me.lstRazonSocial.Size = New System.Drawing.Size(397, 36)
        Me.lstRazonSocial.TabIndex = 1
        '
        'lstRfc
        '
        Me.lstRfc.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstRfc.Location = New System.Drawing.Point(886, 58)
        Me.lstRfc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstRfc.Name = "lstRfc"
        Me.lstRfc.SelectItem = ""
        Me.lstRfc.SelectText = ""
        Me.lstRfc.Size = New System.Drawing.Size(285, 36)
        Me.lstRfc.TabIndex = 2
        '
        'LstGiro
        '
        Me.LstGiro.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstGiro.Location = New System.Drawing.Point(15, 240)
        Me.LstGiro.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstGiro.Name = "LstGiro"
        Me.LstGiro.SelectItem = ""
        Me.LstGiro.SelectText = ""
        Me.LstGiro.Size = New System.Drawing.Size(319, 36)
        Me.LstGiro.TabIndex = 7
        '
        'LstCodigoPostal
        '
        Me.LstCodigoPostal.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstCodigoPostal.Location = New System.Drawing.Point(886, 149)
        Me.LstCodigoPostal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstCodigoPostal.Name = "LstCodigoPostal"
        Me.LstCodigoPostal.SelectItem = ""
        Me.LstCodigoPostal.SelectText = ""
        Me.LstCodigoPostal.Size = New System.Drawing.Size(285, 36)
        Me.LstCodigoPostal.TabIndex = 6
        '
        'txtTelefono
        '
        Me.txtTelefono.AutoSize = False
        Me.txtTelefono.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTelefono.Location = New System.Drawing.Point(15, 149)
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.Size = New System.Drawing.Size(260, 36)
        Me.txtTelefono.TabIndex = 3
        Me.txtTelefono.ThemeName = "Material"
        '
        'txtTelefono2
        '
        Me.txtTelefono2.AutoSize = False
        Me.txtTelefono2.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!)
        Me.txtTelefono2.Location = New System.Drawing.Point(298, 149)
        Me.txtTelefono2.Name = "txtTelefono2"
        Me.txtTelefono2.Size = New System.Drawing.Size(260, 36)
        Me.txtTelefono2.TabIndex = 4
        Me.txtTelefono2.ThemeName = "Material"
        '
        'TxtTelefono3
        '
        Me.TxtTelefono3.AutoSize = False
        Me.TxtTelefono3.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!)
        Me.TxtTelefono3.Location = New System.Drawing.Point(592, 149)
        Me.TxtTelefono3.Name = "TxtTelefono3"
        Me.TxtTelefono3.Size = New System.Drawing.Size(260, 36)
        Me.TxtTelefono3.TabIndex = 5
        Me.TxtTelefono3.ThemeName = "Material"
        '
        'TxtDireccion
        '
        Me.TxtDireccion.AutoSize = False
        Me.TxtDireccion.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!)
        Me.TxtDireccion.Location = New System.Drawing.Point(361, 240)
        Me.TxtDireccion.Name = "TxtDireccion"
        Me.TxtDireccion.Size = New System.Drawing.Size(810, 36)
        Me.TxtDireccion.TabIndex = 8
        Me.TxtDireccion.ThemeName = "Material"
        '
        'txtCorreo
        '
        Me.txtCorreo.AutoSize = False
        Me.txtCorreo.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!)
        Me.txtCorreo.Location = New System.Drawing.Point(15, 331)
        Me.txtCorreo.Name = "txtCorreo"
        Me.txtCorreo.Size = New System.Drawing.Size(543, 36)
        Me.txtCorreo.TabIndex = 9
        Me.txtCorreo.ThemeName = "Material"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox1.Controls.Add(Me.txtCorreo)
        Me.RadGroupBox1.Controls.Add(Me.TxtDireccion)
        Me.RadGroupBox1.Controls.Add(Me.TxtTelefono3)
        Me.RadGroupBox1.Controls.Add(Me.txtTelefono2)
        Me.RadGroupBox1.Controls.Add(Me.txtTelefono)
        Me.RadGroupBox1.Controls.Add(Me.LstCodigoPostal)
        Me.RadGroupBox1.Controls.Add(Me.LstGiro)
        Me.RadGroupBox1.Controls.Add(Me.lstRfc)
        Me.RadGroupBox1.Controls.Add(Me.lstRazonSocial)
        Me.RadGroupBox1.Controls.Add(Me.lstEmpresa)
        Me.RadGroupBox1.Controls.Add(Me.Label10)
        Me.RadGroupBox1.Controls.Add(Me.Label9)
        Me.RadGroupBox1.Controls.Add(Me.Label8)
        Me.RadGroupBox1.Controls.Add(Me.Label7)
        Me.RadGroupBox1.Controls.Add(Me.Label5)
        Me.RadGroupBox1.Controls.Add(Me.Label6)
        Me.RadGroupBox1.Controls.Add(Me.Label4)
        Me.RadGroupBox1.Controls.Add(Me.Label3)
        Me.RadGroupBox1.Controls.Add(Me.Label2)
        Me.RadGroupBox1.Controls.Add(Me.Label1)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "Datos de la Organización"
        Me.RadGroupBox1.Location = New System.Drawing.Point(10, 280)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(1192, 379)
        Me.RadGroupBox1.TabIndex = 35
        Me.RadGroupBox1.Text = "Datos de la Organización"
        Me.RadGroupBox1.ThemeName = "MaterialBlueGrey"
        '
        'Control_de_Empresas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(1417, 674)
        Me.Controls.Add(Me.RadGroupBox1)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Control_de_Empresas"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "P_Master"
        Me.Text = "Datos de la Organización"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        CType(Me.txtTelefono, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTelefono2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTelefono3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDireccion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCorreo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lstEmpresa As Listas
    Friend WithEvents lstRazonSocial As Listas
    Friend WithEvents lstRfc As Listas
    Friend WithEvents LstGiro As Listas
    Friend WithEvents LstCodigoPostal As Listas
    Friend WithEvents TablaEmpresas As Tabla_Filtro
    Friend WithEvents txtTelefono As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtTelefono2 As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents TxtTelefono3 As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents TxtDireccion As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents txtCorreo As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
End Class

